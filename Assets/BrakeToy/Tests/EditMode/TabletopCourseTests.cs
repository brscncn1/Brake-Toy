using System;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class TabletopCourseTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void DenseStreamReplaysAndDoesNotReserveOneLaneForAnEntirePhrase(bool relax)
        {
            for (int seed = 1; seed <= 100; seed++)
            {
                var a = new AdventureSequence(seed, relax, true);
                var b = new AdventureSequence(seed, relax, true);
                var patterns = new HashSet<int>();
                var families = new HashSet<int>();
                PlaygroundBeat previous = default;
                for (int i = 0; i < 120; i++)
                {
                    var beat = a.Next();
                    var replay = b.Next();
                    Assert.That(beat.Distance, Is.EqualTo(replay.Distance));
                    Assert.That(beat.Variant, Is.EqualTo(replay.Variant));
                    patterns.Add(TabletopCourseRules.Pattern(beat));
                    families.Add(beat.Family);
                    Assert.That(TabletopCourseRules.GapLane(beat, 0), Is.Not.EqualTo(TabletopCourseRules.GapLane(beat, 1)));
                    for (int gate = 1; gate < 3; gate++)
                    {
                        Assert.That(Math.Abs(TabletopCourseRules.GapLane(beat, gate) - TabletopCourseRules.GapLane(beat, gate - 1)), Is.LessThanOrEqualTo(1));
                        Assert.That(TabletopCourseRules.GateOffset(beat, gate) - TabletopCourseRules.GateOffset(beat, gate - 1), Is.InRange(14f, 22f));
                    }
                    if (i > 0) Assert.That(beat.Distance - previous.Distance,
                        Is.EqualTo(TabletopCourseRules.Spacing(previous.Index, relax)).Within(.01));
                    previous = beat;
                }
                Assert.That(patterns.Count, Is.EqualTo(6));
                Assert.That(families.Count, Is.EqualTo(8));
            }
        }
        [Test]
        public void ChangedTopologyUsesItsOwnGhostNamespace()
        {
            Assert.That(TabletopCourseRules.Version, Is.Not.EqualTo(AdventureRules.GhostNamespace(true)));
            Assert.That(TabletopCourseRules.Version, Is.Not.EqualTo(AdventureRules.GhostNamespace(false)));
        }
    }
}
