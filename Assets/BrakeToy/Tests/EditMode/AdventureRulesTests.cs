using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;
namespace BrakeToy.Tests
{
    public sealed class AdventureRulesTests
    {
        [Test]public void BagsCoverAllEightWithoutRepeatingLastThreeAndSeedsReplay()
        {for(int seed=1;seed<=100;seed++){var a=new AdventureSequence(seed);var b=new AdventureSequence(seed);var recent=new Queue<int>();var bag=new HashSet<int>();float last=-24;int previous=1;for(int i=0;i<256;i++){var p=a.Next();var q=b.Next();Assert.That(p.Family,Is.EqualTo(q.Family));Assert.That(p.Variant,Is.EqualTo(q.Variant));Assert.That(p.Distance,Is.EqualTo(q.Distance));Assert.That(recent.Contains(p.Family),Is.False);recent.Enqueue(p.Family);if(recent.Count>3)recent.Dequeue();bag.Add(p.Family);if(i%8==7){Assert.That(bag.Count,Is.EqualTo(8));bag.Clear();}Assert.That(p.Distance-last,Is.GreaterThanOrEqualTo(48-.001f));Assert.That(System.Math.Abs(p.SafeLane-previous),Is.EqualTo(1));Assert.That(p.SafeLane,Is.Not.EqualTo(p.RiskLane));last=p.Distance;previous=p.SafeLane;}}}
        [Test]public void IceSteeringHasBoundedInertiaAndResetClearsIt()
        {var a=new SteeringModel(3,6,8);a.BeginDrag();a.Drag(1000,1000);for(int i=0;i<12;i++)a.Step(1f/60,.23f);float before=a.Position;a.EndDrag();a.Step(1f/60,.23f);Assert.That(a.Position,Is.GreaterThan(before));for(int i=0;i<2000;i++)a.Step(1f/60,.23f);Assert.That(System.Math.Abs(a.Position),Is.LessThanOrEqualTo(3));a.Reset();Assert.That(a.Step(.1f,.23f),Is.Zero);Assert.That(AdventureRules.TapeSpeed(true),Is.EqualTo(1));}
    }
}
