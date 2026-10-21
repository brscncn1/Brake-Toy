using System;
namespace BrakeToy.Core
{
    /// <summary>Three cumulative goals for this run; no currency, daily penalty or persistent economy.</summary>
    public sealed class RunGoals
    {
        public int Completed { get; private set; }
        public int Pickups { get; private set; }
        public void Reset() { Completed=Pickups=0; }
        public void Collect() { Pickups++; }
        public bool Advance(float distance,int nearMisses)
        {
            int before=Completed;
            if(Completed==0 && distance>=60)Completed=1;
            if(Completed==1 && Pickups>=3)Completed=2;
            if(Completed==2 && nearMisses>=6)Completed=3;
            return Completed>before;
        }
        public int Target=>Completed==0?60:Completed==1?3:6;
        public int Progress(float distance,int nearMisses)=>Math.Min(Target,Completed==0?(int)Math.Max(0,distance):Completed==1?Pickups:nearMisses);
    }
}
