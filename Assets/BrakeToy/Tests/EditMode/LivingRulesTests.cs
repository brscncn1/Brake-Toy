using System;
using BrakeToy.Core;
using NUnit.Framework;
namespace BrakeToy.Tests
{
    public sealed class LivingRulesTests
    {
        [Test] public void GoalsAreCumulativeOnceAndResetCompletely()
        {var g=new RunGoals();g.Collect();g.Collect();g.Collect();Assert.That(g.Advance(59,6),Is.False);Assert.That(g.Advance(60,6),Is.True);Assert.That(g.Completed,Is.EqualTo(3));Assert.That(g.Advance(100,20),Is.False);g.Reset();Assert.That(g.Completed,Is.Zero);Assert.That(g.Pickups,Is.Zero);Assert.That(g.Advance(60,0),Is.True);Assert.That(g.Completed,Is.EqualTo(1));}
        [Test] public void RiskScoreDoesNotInventNearMissesOrExtendChain()
        {var s=new StyleScore();s.Award();int chain=s.Chain,misses=s.NearMisses,total=s.Total;s.AwardRiskStar();Assert.That(s.Total,Is.EqualTo(total+75));Assert.That(s.Chain,Is.EqualTo(chain));Assert.That(s.NearMisses,Is.EqualTo(misses));}
        [Test] public void AuthoredBeatsKeepReachableDeterministicSafeCorridors()
        {
            for(int seed=1;seed<=1000;seed++)
            {var a=new ArcadeSequence(seed,true,true);var b=new ArcadeSequence(seed,true,true);int previous=1,repeats=0;float distance=0;
                for(int i=0;i<128;i++)
                {var row=a.Next();var replay=b.Next();Assert.That(row.SafeLane,Is.EqualTo(replay.SafeLane));Assert.That(row.Distance,Is.EqualTo(replay.Distance));Assert.That(row.IsBlocked(row.SafeLane),Is.False);Assert.That(Math.Abs(row.SafeLane-previous),Is.LessThanOrEqualTo(1));repeats=row.SafeLane==previous?repeats+1:1;Assert.That(repeats,Is.LessThanOrEqualTo(2));if(i>0)Assert.That(row.Distance-distance,Is.InRange(25,34));if(row.Ball)for(float z=-2;z<=2;z+=.2f)Assert.That(Math.Abs(ArcadeRules.LaneX(row.SafeLane)-row.BallX(z)),Is.GreaterThan(1));previous=row.SafeLane;distance=row.Distance;}
            }
        }
    }
}
