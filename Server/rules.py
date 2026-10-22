"""Server-owned rules for review-r1. Scores are derived from inputs, never client totals."""
import math,struct
def f(x):return struct.unpack('<f',struct.pack('<f',x))[0]
def distance(t):return f(f(18*f(t))-f(144*f(1-f(math.exp(f(-f(t)/18))))))
def clamp(x,a=0,b=1):return max(a,min(b,x))
def box(x,z,x1,z1,hx,hz):
 lo,hi=0.,1.
 for origin,delta,size in [(x,x1-x,hx),(z,z1-z,hz)]:
  if abs(delta)<1e-7:
   if abs(origin)>size:return None
  else:
   a,b=(-size-origin)/delta,(size-origin)/delta
   if a>b:a,b=b,a
   lo,hi=max(lo,a),min(hi,b)
   if lo>hi:return None
 return lo
def circle(x,z,x1,z1,hx,hz,r):
 hits=[v for v in [box(x,z,x1,z1,hx+r,hz),box(x,z,x1,z1,hx,hz+r)] if v is not None]
 for ix in [-1,1]:
  for iz in [-1,1]:
   a,b=x-ix*hx,z-iz*hz;dx,dz=x1-x,z1-z;c=a*a+b*b-r*r
   if c<=0:hits.append(0);continue
   aa=dx*dx+dz*dz;bb=2*(a*dx+b*dz);disc=bb*bb-4*aa*c
   if aa<1e-16 or disc<0:continue
   root=(-bb-math.sqrt(disc))/(2*aa)
   if 0<=root<=1:hits.append(root)
 return min(hits) if hits else None
class Sequence:
 def __init__(self,seed):self.state=seed&0xffffffff or 1;self.i=0;self.safe=1;self.variant=-1;self.repeats=0;self.ball=2;self.distance=24;self.mirrored=seed%2==0
 def rand(self,n):
  s=self.state;s^=(s<<13)&0xffffffff;s^=s>>17;s^=(s<<5)&0xffffffff;self.state=s&0xffffffff;return self.state%n
 def next(self):
  i=self.i;self.i+=1;candidate=(0 if self.rand(2)==0 else 2) if i==0 else clamp(self.safe+self.rand(3)-1,0,2)
  beat,family=i%8,(i//8)%3
  desired=(beat if beat<=2 else 4-beat if beat<=4 else 0 if beat==5 else beat-5) if family==0 else (0 if beat//2%2==0 else 1) if family==1 else (beat%4 if beat%4<=2 else 1)
  if self.mirrored:desired=2-desired
  candidate=int(clamp(desired,self.safe-1,self.safe+1))
  if i==0:candidate=2 if self.mirrored else 0
  if candidate==self.safe and self.repeats>=2:candidate=self.rand(2)*2 if self.safe==1 else 1
  self.repeats=self.repeats+1 if candidate==self.safe else 1;self.safe=candidate;self.variant=(self.variant+1+self.rand(2))%3
  ball=i==self.ball
  if ball:self.ball+=5+self.rand(4)
  mask=7^(1<<self.safe);breathing=i>0 and self.rand(5)==0
  if breathing or i==0:mask=1<<(self.rand(2)*2 if self.safe==1 else 1)
  lane=(self.safe+1+self.rand(2))%3;slope=f((-1 if self.rand(2)==0 else 1)*f(.035+f(self.rand(31)*.001)))
  row=dict(i=i,safe=self.safe,kind=self.variant,ball=ball,mask=0 if ball else mask,distance=self.distance,cross=(lane-1)*2.25,slope=slope)
  self.distance+=34 if breathing else 25+self.rand(7);return row
def validate(seed,steps):
 if not isinstance(steps,list) or not 1<=len(steps)<=72000:raise ValueError('invalid trace length')
 sequence=Sequence(seed);pending=sequence.next();hazards=[];stars=[];elapsed=dist=x=0.;score=chain=misses=0;remaining=0.
 def fill():
  nonlocal pending
  while pending['distance']-dist<95:
   row=pending;z=row['distance']-dist
   if row['ball']:hazards.append(dict(x=row['cross']+row['slope']*z,z=z,hw=.51,hl=.51,ball=True,cross=row['cross'],slope=row['slope'],gap=math.inf,scored=False))
   else:
    if row['i']>0:
     direction=1 if row['safe']==0 else -1 if row['safe']==2 else -1 if row['mask']&1 else 1
     stars.append([(row['safe']-1)*2.25+direction*.72,z])
    for lane in range(3):
     if row['mask']&(1<<lane):hazards.append(dict(x=(lane-1)*2.25,z=z,hw=[.82,.66,.74][row['kind']],hl=[.61,.62,.48][row['kind']],ball=False,gap=math.inf,scored=False))
   pending=sequence.next()
 fill()
 crashed=False
 for step in steps:
  dt,newx=step['dt'],step['x']
  if not isinstance(dt,(int,float)) or not isinstance(newx,(int,float)) or not math.isfinite(dt) or not math.isfinite(newx) or dt<=0 or dt>.10001 or abs(newx)>3.111 or abs(newx-x)>9.5*dt+.003:raise ValueError('invalid steering or time')
  n=max(1,math.ceil(f(dt*60)));sub=f(dt/n)
  for j in range(n):
   x0=x+(newx-x)*j/n;x1=x+(newx-x)*(j+1)/n;d=f(distance(f(elapsed+sub))-distance(elapsed));fraction=1.;hit=False
   for h in hazards:
    bz=f(h['z']-d);bx=f(h['cross']+h['slope']*bz) if h['ball'] else h['x']
    t=circle(x0-h['x'],-h['z'],x1-bx,-bz,.39,.7,.51) if h['ball'] else box(x0-h['x'],-h['z'],x1-bx,-bz,.39+h['hw'],.7+h['hl'])
    if t is not None and t<=fraction:hit=True;fraction=t
   moved=f(d*fraction)
   for h in hazards:
    az=h['z'];bz=f(az-moved);ax=h['x'];bx=f(h['cross']+h['slope']*bz) if h['ball'] else ax;half=.7+h['hl']
    if az>=-half and bz<=half:
     enter=clamp((az-half)/max(1e-6,moved));exit=clamp((az+half)/max(1e-6,moved));r0=x0+(x1-x0)*fraction*enter-(ax+(bx-ax)*enter);r1=x0+(x1-x0)*fraction*exit-(ax+(bx-ax)*exit);separation=0 if r0*r1<=0 else min(abs(r0),abs(r1));h['gap']=min(h['gap'],separation-.39-h['hw'])
    h['x'],h['z']=bx,bz
    if not hit and not h['scored'] and bz < -half:
     h['scored']=True
     if .005<h['gap']<=.48:chain+=1;misses+=1;remaining=6;score+=100*(5 if chain>=8 else 3 if chain>=5 else 2 if chain>=3 else 1)
   hazards[:]=[h for h in hazards if h['z']>=-9];dist=f(dist+moved);elapsed=f(elapsed+f(sub*fraction));remaining=max(0,remaining-sub*fraction)
   if remaining<=0:chain=0
   for star in list(stars):
    az=star[1];star[1]=f(az-moved)
    if not hit and circle(x0-star[0],-az,x0+(x1-x0)*fraction-star[0],-star[1],.39,.7,.11) is not None:score+=75;stars.remove(star)
    elif star[1]<-5:stars.remove(star)
   if hit:crashed=True;break
   fill()
  x=newx
  if crashed:break
 return dict(distance=dist,style=score,nearMisses=misses,elapsed=elapsed,crashed=crashed)
