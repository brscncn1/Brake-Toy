using System;
namespace BrakeToy.Core
{
    public static class JoyfulRules
    {
        public const string Version="joyful-r2";
        public static float Speed(float t,bool relax=false)=>relax?9+5*(1-(float)Math.Exp(-Math.Max(0,t)/30)):14+16*(1-(float)Math.Exp(-Math.Max(0,t)/14));
        public static float Distance(float t,bool relax=false){t=Math.Max(0,t);return relax?14*t-150*(1-(float)Math.Exp(-t/30)):30*t-224*(1-(float)Math.Exp(-t/14));}
        public static float TimeAtDistance(float distance,bool relax=false){float low=0,high=distance/(relax?9:14)+1;for(int i=0;i<24;i++){float mid=(low+high)*.5f;if(Distance(mid,relax)<distance)low=mid;else high=mid;}return (low+high)*.5f;}
        public static float FlightHeight(float age,float duration=1.65f)=>age<0||age>=duration?0:.65f*(1-age/duration)+1.85f*(float)Math.Sin(Math.PI*age/duration);
        public static float MotionX(float crossing,float z,float amplitude,float frequency,float phase)=>Math.Max(-4.8f,Math.Min(4.8f,crossing+amplitude*((float)Math.Sin(phase+z*frequency)-(float)Math.Sin(phase))));
    }
    public readonly struct JoyfulRow
    {
        public readonly int Index,SafeLane,Pattern,Kind,Balls,Stage;
        public readonly float Distance,Phase;public readonly bool Ramp,Bus;
        public float SafeX=>ArcadeRules.LaneX(SafeLane);
        public JoyfulRow(int index,int safe,int pattern,int kind,int balls,int stage,float distance,float phase,bool ramp,bool bus)
        {Index=index;SafeLane=safe;Pattern=pattern;Kind=kind;Balls=balls;Stage=stage;Distance=distance;Phase=phase;Ramp=ramp;Bus=bus;}
    }
    // Bounded random stream with recent-pattern rejection. Continuous steering is unchanged.
    public sealed class JoyfulSequence
    {
        private uint random;private int index,safe=1,lastKind=-1;private float distance;
        private readonly bool relax;private readonly int[] history={-1,-1,-1,-1,-1,-1,-1,-1};
        public JoyfulSequence(int seed,bool relax=false){random=(uint)seed;if(random==0)random=1;this.relax=relax;distance=relax?23:21;}
        private int Next(int n){random^=random<<13;random^=random>>17;random^=random<<5;return (int)(random%(uint)n);}
        public JoyfulRow Next()
        {
            int i=index++,stage=Math.Min(5,(int)(JoyfulRules.TimeAtDistance(distance,relax)/12));int pattern=Next(32);
            for(int tries=0;Array.IndexOf(history,pattern)>=0&&tries<40;tries++)pattern=(pattern+1+Next(31))%32;
            if(Array.IndexOf(history,pattern)>=0)for(int p=0;p<32;p++)if(Array.IndexOf(history,p)<0){pattern=p;break;}
            history[i%history.Length]=pattern;
            // Every row demands an adjacent readable steering decision; no endless safe-lane hold.
            safe=safe==1?Next(2)*2:1;
            bool bus=!relax&&i>=3&&pattern%7==0;if(bus&&safe==1)safe=Next(2)*2;
            int balls=i>=2&&(pattern%4==0||i==2)?(relax?1:stage>=3?3:stage>=1?2:1):0;
            if(bus)balls=0;
            bool ramp=i>=1&&i%4==1; // predictable opportunity, varying lane/content around it
            int kind=Next(6);if(kind==3)kind=4;if(kind==lastKind)kind=(kind+1)%6;if(kind==3)kind=5;lastKind=kind;
            var row=new JoyfulRow(i,safe,pattern,kind,balls,stage,distance,Next(628)/100f,ramp,bus);
            float t=JoyfulRules.TimeAtDistance(distance,relax);distance+=JoyfulRules.Distance(t+(relax?2.2f:1.55f+(pattern%3)*.08f),relax)-JoyfulRules.Distance(t,relax);
            return row;
        }
    }
}
