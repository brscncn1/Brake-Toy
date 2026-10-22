"""Dependency-free reference backend. Bind localhost by default; deploy behind HTTPS."""
import argparse,datetime,hashlib,json,secrets,sqlite3,time,threading
from contextlib import closing
from http.server import BaseHTTPRequestHandler,ThreadingHTTPServer
from rules import validate
RULES='review-r1'
def daily(now=None):
 date=(now or datetime.datetime.now(datetime.timezone.utc)).strftime('%Y-%m-%d');seed=2166136261
 for c in date:seed=((seed^ord(c))*16777619)&0xffffffff
 return dict(date=date,seed=seed&0x7fffffff,rules=RULES)
def database(path):
 con=sqlite3.connect(path);con.executescript('CREATE TABLE IF NOT EXISTS runs(id TEXT PRIMARY KEY, player TEXT, seed INTEGER, mode TEXT, issued REAL, expires REAL, used INTEGER DEFAULT 0);CREATE TABLE IF NOT EXISTS scores(player TEXT, mode TEXT, day TEXT, distance REAL, style INTEGER, PRIMARY KEY(player,mode,day));');con.commit();return con
def make_handler(db):
 sessions={};rate={};lock=threading.Lock()
 class Handler(BaseHTTPRequestHandler):
  server_version='BrakeToy/0.6';sys_version=''
  def setup(self):
   super().setup();self.connection.settimeout(15)
  def log_message(self,*args):pass # Do not log tokens, trajectories or IP addresses.
  def respond(self,status,data):
   raw=json.dumps(data,separators=(',',':')).encode();self.send_response(status);self.send_header('Content-Type','application/json');self.send_header('Content-Length',str(len(raw)));self.send_header('Cache-Control','no-store');self.end_headers();self.wfile.write(raw)
  def handle_request(self):
   now=time.time()
   with lock:
    for key in [k for k,v in sessions.items() if v[1]<now]:del sessions[key]
    for key in [k for k,v in rate.items() if not v or v[-1]<now-60]:del rate[key]
    bucket=rate.get(self.client_address[0],[]);bucket=[t for t in bucket if t>now-60]
    if len(bucket)>=120:return self.respond(429,{'error':'rate limit'})
    bucket.append(now);rate[self.client_address[0]]=bucket
   route=self.path.split('?')[0]
   if self.command=='GET' and route=='/health':return self.respond(200,{'rules':RULES,'status':'ok'})
   if self.command=='GET' and route=='/daily':return self.respond(200,daily())
   length=int(self.headers.get('Content-Length','0'))
   if length<0 or length>4_000_000:return self.respond(413,{'error':'body too large'})
   body=json.loads(self.rfile.read(length) or '{}') if self.command=='POST' else {}
   if not isinstance(body,dict):return self.respond(400,{'error':'object body required'})
   if self.command=='POST' and route=='/session':
    token=secrets.token_urlsafe(32);player='Toy-'+secrets.token_hex(4)
    with lock:sessions[hashlib.sha256(token.encode()).hexdigest()]=(player,now+86400)
    return self.respond(200,{'token':token,'player':player})
   auth=self.headers.get('Authorization','');key=hashlib.sha256(auth.removeprefix('Bearer ').encode()).hexdigest()
   with lock:session=sessions.get(key)
   if not session or session[1]<now:return self.respond(401,{'error':'session expired'})
   player=session[0]
   with closing(database(db)) as con:
    con.execute('DELETE FROM runs WHERE expires<?',(now-86400,));con.commit()
    if self.command=='POST' and route=='/delete-player':
     con.execute('DELETE FROM runs WHERE player=?',(player,));con.execute('DELETE FROM scores WHERE player=?',(player,));con.commit()
     with lock:
      for session_key in [k for k,v in sessions.items() if v[0]==player]:del sessions[session_key]
     return self.respond(200,{'deleted':True})
    if self.command=='POST' and route=='/runs':
     mode=body.get('mode');day=daily()
     if mode not in ('arcade','daily'):return self.respond(400,{'error':'competitive modes only'})
     seed=day['seed'] if mode=='daily' else secrets.randbelow(2147483646)+1;ticket=secrets.token_urlsafe(24)
     con.execute('INSERT INTO runs(id,player,seed,mode,issued,expires) VALUES(?,?,?,?,?,?)',(ticket,player,seed,mode,now,now+7200))
     con.commit()
     return self.respond(200,dict(ticket=ticket,seed=seed,date=day['date'],rules=RULES))
    if self.command=='POST' and route=='/submit':
     con.execute('BEGIN IMMEDIATE');row=con.execute('SELECT player,seed,mode,issued,expires,used FROM runs WHERE id=?',(body.get('ticket'),)).fetchone()
     if not row or row[0]!=player or row[4]<now or row[5]:return self.respond(409,{'error':'invalid, expired or used run ticket'})
     if body.get('rules')!=RULES:return self.respond(400,{'error':'rules mismatch'})
     result=validate(row[1],body.get('steps'))
     if result['elapsed']>now-row[3]+2:return self.respond(400,{'error':'run faster than wall time'})
     day=datetime.datetime.fromtimestamp(row[3],datetime.timezone.utc).strftime('%Y-%m-%d') if row[2]=='daily' else ''
     con.execute('UPDATE runs SET used=1 WHERE id=?',(body['ticket'],));con.execute('INSERT INTO scores VALUES(?,?,?,?,?) ON CONFLICT(player,mode,day) DO UPDATE SET distance=max(distance,excluded.distance),style=max(style,excluded.style)',(player,row[2],day,result['distance'],result['style']));con.commit();return self.respond(200,result)
    if self.command=='GET' and route=='/leaderboard':
     from urllib.parse import parse_qs,urlsplit
     q=parse_qs(urlsplit(self.path).query);mode=q.get('mode',['arcade'])[0];metric=q.get('metric',['distance'])[0]
     if mode not in ('arcade','daily') or metric not in ('distance','style'):return self.respond(400,{'error':'invalid board'})
     day=daily()['date'] if mode=='daily' else '';rows=con.execute('SELECT player,distance,style FROM scores WHERE mode=? AND day=? ORDER BY '+metric+' DESC,player LIMIT 100',(mode,day)).fetchall();own=con.execute('SELECT player,distance,style FROM scores WHERE player=? AND mode=? AND day=?',(player,mode,day)).fetchone();neighbors=[];rank=0
     if own:
      score=own[1 if metric=='distance' else 2];rank=1+con.execute('SELECT count(*) FROM scores WHERE mode=? AND day=? AND ('+metric+'>? OR ('+metric+'=? AND player<?))',(mode,day,score,score,player)).fetchone()[0];neighbors=con.execute('SELECT player,distance,style FROM scores WHERE mode=? AND day=? ORDER BY '+metric+' DESC,player LIMIT 5 OFFSET ?',(mode,day,max(0,rank-3))).fetchall()
     return self.respond(200,{'rows':[dict(player=r[0],distance=r[1],style=r[2]) for r in rows],'ownRank':rank,'nearby':[dict(player=r[0],distance=r[1],style=r[2]) for r in neighbors],'verified':True})
   self.respond(404,{'error':'not found'})
  def do_GET(self):
   try:self.handle_request()
   except (ValueError,TypeError,KeyError,OverflowError):self.respond(400,{'error':'invalid request'})
  do_POST=do_GET
 return Handler
if __name__=='__main__':
 p=argparse.ArgumentParser();p.add_argument('--host',default='127.0.0.1');p.add_argument('--port',type=int,default=8765);p.add_argument('--db',default='braketoy.sqlite3');a=p.parse_args();database(a.db).close();ThreadingHTTPServer((a.host,a.port),make_handler(a.db)).serve_forever()
