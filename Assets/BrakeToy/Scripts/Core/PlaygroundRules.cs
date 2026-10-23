using System;
namespace BrakeToy.Core
{
    public enum ToyPower { Slow, Rocket, Jetpack }
    public static class PlaygroundRules
    {
        public const string Version="playground-r1";
        public static readonly int[] RefillCosts={350,500,650};
        public static float Duration(ToyPower power)=>power==ToyPower.Jetpack?5:3;
        public static bool EnsureStarter(CareerData data)
        {if(data.powerStarterGranted)return false;data.slowCharges=data.rocketCharges=data.jetCharges=3;data.powerStarterGranted=true;return true;}
        public static int Charges(CareerData data,ToyPower p)=>p==ToyPower.Slow?data.slowCharges:p==ToyPower.Rocket?data.rocketCharges:data.jetCharges;
        public static bool Spend(CareerData data,ToyPower p)
        {if((int)p<0||(int)p>2||Charges(data,p)<=0)return false;Set(data,p,Charges(data,p)-1);return true;}
        public static bool Refill(CareerData data,ToyPower p)
        {if((int)p<0||(int)p>2||Charges(data,p)>=99||data.points<RefillCosts[(int)p])return false;data.points-=RefillCosts[(int)p];Set(data,p,Charges(data,p)+1);return true;}
        private static void Set(CareerData d,ToyPower p,int n){if(p==ToyPower.Slow)d.slowCharges=n;else if(p==ToyPower.Rocket)d.rocketCharges=n;else d.jetCharges=n;}
        // Cover geometry: x ±.95, low end z -2.50, .65-high tip +.80, supported book ends +.975.
        // Footprint contact, not just car centre: grazing wheels may ride the edge without passing through it.
        public static float RampSurface(float relativeX,float relativeZ,float carHalfWidth,float carHalfLength)
        {if(Math.Abs(relativeX)>.95f+carHalfWidth-.015f||relativeZ< -2.5f-carHalfLength||relativeZ>.975f+carHalfLength)return 0;float front=relativeZ+carHalfLength;return .665f*Math.Max(0,Math.Min(1,(front+2.5f)/3.3f));}
    }
    public readonly struct PlaygroundBeat
    {
        public readonly int Index,SafeLane,RiskLane,Family,Variant;public readonly float Distance;public float SafeX=>ArcadeRules.LaneX(SafeLane);public float RiskX=>ArcadeRules.LaneX(RiskLane);
        public PlaygroundBeat(int i,int safe,int risk,int family,int variant,float distance){Index=i;SafeLane=safe;RiskLane=risk;Family=family;Variant=variant;Distance=distance;}
    }
    // Authored approach/decision/exit phrases, seeded choice with no adjacent repeated family.
    public sealed class PlaygroundSequence
    {
        private uint rng;private int index,safe=1,last=-1;private float distance=24;private readonly bool relax;
        public PlaygroundSequence(int seed,bool relax=false){rng=(uint)seed;if(rng==0)rng=1;this.relax=relax;}
        private int Next(int n){rng^=rng<<13;rng^=rng>>17;rng^=rng<<5;return (int)(rng%(uint)n);}
        public PlaygroundBeat Next()
        {int i=index++;safe=safe==1?Next(2)*2:1;int risk=safe==1?Next(2)*2:1;int family=i==0?0:Next(6);if(family==last)family=(family+1+Next(5))%6;last=family;var beat=new PlaygroundBeat(i,safe,risk,family,Next(1000),distance);float t=JoyfulRules.TimeAtDistance(distance,relax);float seconds=relax?3.0f:2.65f;distance+=Math.Max(48,JoyfulRules.Distance(t+seconds,relax)-JoyfulRules.Distance(t,relax));return beat;}
    }
}
