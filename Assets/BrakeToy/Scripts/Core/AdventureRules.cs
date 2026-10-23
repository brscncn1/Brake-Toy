using System;
namespace BrakeToy.Core
{
    public enum TrackToy { Tunnel, Windup, Crane, Train, Marbles, Surfaces, Seesaw, Garage }
    // A shuffled bag guarantees coverage; the last three families cannot repeat across a bag boundary.
    public sealed class AdventureSequence
    {
        private uint rng;private readonly bool relax;private readonly int[] bag=new int[8],recent={-1,-1,-1};private int cursor=8,index,safe=1;private float distance=24;
        public AdventureSequence(int seed,bool relax=false){rng=(uint)seed;if(rng==0)rng=1;this.relax=relax;}
        private int Next(int n){rng^=rng<<13;rng^=rng>>17;rng^=rng<<5;return (int)(rng%(uint)n);}
        public PlaygroundBeat Next()
        {
            if(cursor==8){for(int i=0;i<8;i++)bag[i]=i;for(int i=7;i>0;i--){int j=Next(i+1);int tmp=bag[i];bag[i]=bag[j];bag[j]=tmp;}cursor=0;}
            int chosen=cursor;while(chosen<8&&(bag[chosen]==recent[0]||bag[chosen]==recent[1]||bag[chosen]==recent[2]))chosen++;
            if(chosen==8)throw new InvalidOperationException("Invalid adventure bag");int swap=bag[cursor];bag[cursor]=bag[chosen];bag[chosen]=swap;int family=bag[cursor++];recent[2]=recent[1];recent[1]=recent[0];recent[0]=family;
            safe=safe==1?Next(2)*2:1;int risk=safe==1?Next(2)*2:1;
            var beat=new PlaygroundBeat(index++,safe,risk,family,Next(10000),distance);float t=JoyfulRules.TimeAtDistance(distance,relax);distance+=Math.Max(48,JoyfulRules.Distance(t+(relax?3:2.65f),relax)-JoyfulRules.Distance(t,relax));return beat;
        }
    }
    public static class AdventureRules
    {
        public const string Version="adventure-r1";
        public static float Phase(float z,float offset)=>z*.16f+offset;
        public static float Hop(float phase)=>Math.Max(0,(float)Math.Sin(phase))* .9f;
        public static float TapeSpeed(bool airborne)=>airborne?1:.62f;
    }
}
