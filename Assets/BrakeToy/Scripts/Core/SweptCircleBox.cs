using System;

namespace BrakeToy.Core
{
    /// <summary>Exact continuous circle against an axis-aligned rectangle in relative XZ space.</summary>
    public static class SweptCircleBox
    {
        public static bool TryHit(float x0, float z0, float x1, float z1, float halfX, float halfZ, float radius, out float time)
        {
            time = 1; bool hit = false;
            if (SweptCollision.TryHit(x0,z0,x1,z1,halfX+radius,halfZ,out float t)) {time=t;hit=true;}
            if (SweptCollision.TryHit(x0,z0,x1,z1,halfX,halfZ+radius,out t) && t<=time) {time=t;hit=true;}
            for(int ix=-1;ix<=1;ix+=2) for(int iz=-1;iz<=1;iz+=2)
            {
                double x=x0-ix*halfX,z=z0-iz*halfZ,dx=x1-x0,dz=z1-z0;
                double c=x*x+z*z-radius*radius;
                if(c<=0){time=0;hit=true;continue;}
                double a=dx*dx+dz*dz,b=2*(x*dx+z*dz),disc=b*b-4*a*c;
                if(a<1e-16||disc<0)continue;
                double root=(-b-Math.Sqrt(disc))/(2*a);
                if(root>=0&&root<=time){time=(float)root;hit=true;}
            }
            return hit;
        }
    }
}
