using BrakeToy.Core;
using BrakeToy.Editor;
using NUnit.Framework;
using UnityEngine;

namespace BrakeToy.Tests
{
    public sealed class OpeningPaceAuditTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void WindowsMatchIndependentInverseTimeCrossingsAndPreserveMode(bool relax)
        {
            var measured = OpeningPaceAudit.Measure(7331, relax);
            Assert.That(measured.relax, Is.EqualTo(relax));
            foreach (var window in measured.windows)
            {
                int expected = 0;
                var sequence = new AdventureSequence(7331, relax);
                for (var beat = sequence.Next(); JoyfulRules.TimeAtDistance(beat.Distance, relax) <= window.seconds; beat = sequence.Next()) expected++;
                Assert.That(window.phrases, Is.EqualTo(expected));
                Assert.That(window.uniqueFamilies, Is.InRange(1, 8));
                Assert.That(window.meanIntervalSeconds, Is.GreaterThan(0));
                Assert.That(window.meanAuthoredGapSeconds, Is.InRange(0, window.meanIntervalSeconds));
                Assert.That(window.centerEdgeTransitions, Is.EqualTo(window.phrases - 1));
            }
        }

        [Test]
        public void AuditIsDeterministicAndDoesNotMixModeTiming()
        {
            Assert.That(JsonUtility.ToJson(OpeningPaceAudit.Measure(42, false)),
                Is.EqualTo(JsonUtility.ToJson(OpeningPaceAudit.Measure(42, false))));
            var arcade = OpeningPaceAudit.Measure(42, false).windows[2];
            var relax = OpeningPaceAudit.Measure(42, true).windows[2];
            Assert.That(arcade.phrases, Is.GreaterThan(relax.phrases));
            Assert.That(arcade.distance, Is.GreaterThan(relax.distance));
        }
    }
}
