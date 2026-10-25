using System;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class RollingBallRulesTests
    {
        [TestCase(BallPath.Straight)]
        [TestCase(BallPath.Diagonal)]
        [TestCase(BallPath.Curve)]
        [TestCase(BallPath.DoubleCurve)]
        public void PathsRemainContinuousAndLeaveAnAdjacentCarWidth(BallPath path)
        {
            for (int variant = 0; variant < 120; variant++)
            {
                float phase = variant * .13f;
                float previous = RollingBallRules.Offset(path, -10, phase);
                float min = previous, max = previous;
                for (float depth = -9.9f; depth <= 120; depth += .1f)
                {
                    float x = RollingBallRules.Offset(path, depth, phase);
                    Assert.That(Math.Abs(x), Is.LessThanOrEqualTo(.55001));
                    Assert.That(Math.Abs(x - previous), Is.LessThan(.012f));
                    Assert.That(2.25f - Math.Abs(x) - .48f - .39f, Is.GreaterThan(.8f));
                    min = Math.Min(min, x); max = Math.Max(max, x); previous = x;
                }
                Assert.That(max - min, path == BallPath.Straight ? Is.EqualTo(0) : Is.GreaterThan(.2f));
            }
        }
        [Test]
        public void VariantProfilesAndProgressiveSpeedsAreBoundedAndReplayable()
        {
            var profiles = new HashSet<BallPath>();
            for (int variant = 0; variant < 120; variant++)
            {
                profiles.Add(RollingBallRules.Profile(variant));
                Assert.That(RollingBallRules.SpeedFactor(variant, 0), Is.GreaterThan(1));
                Assert.That(RollingBallRules.SpeedFactor(variant, 3), Is.GreaterThan(RollingBallRules.SpeedFactor(variant, 0)));
                Assert.That(RollingBallRules.SpeedFactor(variant, 99), Is.InRange(1.2f, 1.46f));
            }
            Assert.That(profiles.Count, Is.EqualTo(4));
            Assert.That(RollingBallRules.Version, Is.Not.EqualTo(TabletopCourseRules.Version));
        }
    }
}
