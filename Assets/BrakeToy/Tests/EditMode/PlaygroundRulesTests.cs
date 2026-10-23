using System;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;
namespace BrakeToy.Tests
{
    public sealed class PlaygroundRulesTests
    {
        [Test]public void StarterChargesAreGrantedOnceAndRetryCannotReplenish()
        {var d=new CareerData();Assert.That(PlaygroundRules.EnsureStarter(d),Is.True);foreach(ToyPower p in Enum.GetValues(typeof(ToyPower))){for(int i=0;i<3;i++)Assert.That(PlaygroundRules.Spend(d,p),Is.True);Assert.That(PlaygroundRules.Spend(d,p),Is.False);}Assert.That(PlaygroundRules.EnsureStarter(d),Is.False);Assert.That(d.jetCharges+d.slowCharges+d.rocketCharges,Is.Zero);}
        [Test]public void RefillCannotOverspendAndNeverChangesRecords()
        {var d=new CareerData{points=349,arcadeDistance=500};Assert.That(PlaygroundRules.Refill(d,ToyPower.Slow),Is.False);Assert.That(d.points,Is.EqualTo(349));d.points=1000;Assert.That(PlaygroundRules.Refill(d,ToyPower.Jetpack),Is.True);Assert.That(d.points,Is.EqualTo(350));Assert.That(d.jetCharges,Is.EqualTo(1));Assert.That(d.arcadeDistance,Is.EqualTo(500));Assert.That(PlaygroundRules.Spend(d,(ToyPower)99),Is.False);d.jetCharges=99;Assert.That(PlaygroundRules.Refill(d,ToyPower.Jetpack),Is.False);}
        [Test]public void RampSurfaceIncludesWheelEdgesAndMatchesLowAndHighEnds()
        {for(float x=-1.1f;x<=1.1f;x+=.1f){Assert.That(PlaygroundRules.RampSurface(x,-1,.3f,.4f),Is.GreaterThan(.3f));Assert.That(PlaygroundRules.RampSurface(x,.6f,.3f,.4f),Is.EqualTo(.665f).Within(.001f));}Assert.That(PlaygroundRules.RampSurface(1.3f,0,.3f,.4f),Is.Zero);Assert.That(PlaygroundRules.RampSurface(0,-3,.3f,.4f),Is.Zero);}
        [Test]public void AuthoredPhrasesAreSeededAdjacentReadableAndOfferEveryRampLane()
        {var lanes=new HashSet<int>();var families=new HashSet<int>();for(int seed=1;seed<=128;seed++){var a=new PlaygroundSequence(seed);var b=new PlaygroundSequence(seed);int last=-1,prior=1;float distance=0;for(int i=0;i<1000;i++){var r=a.Next();var same=b.Next();Assert.That(r.Distance,Is.EqualTo(same.Distance));Assert.That(r.Family,Is.EqualTo(same.Family));Assert.That(Math.Abs(r.SafeLane-prior),Is.EqualTo(1));Assert.That(r.SafeLane,Is.Not.EqualTo(r.RiskLane));Assert.That(r.Distance-distance,Is.GreaterThan(i==0?20:35));if(i>0)Assert.That(r.Family,Is.Not.EqualTo(last));lanes.Add(r.RiskLane);families.Add(r.Family);last=r.Family;prior=r.SafeLane;distance=r.Distance;}}Assert.That(lanes.Count,Is.EqualTo(3));Assert.That(families.Count,Is.EqualTo(6));}
    }
}
