using BrakeToy.Core;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class RampChoiceRulesTests
    {
        [Test]
        public void RampExitIsNotAlwaysBlockedAndDoesNotDependOnOneToyFamily()
        {
            int open = 0, blocked = 0;
            for (int seed = 1; seed <= 100; seed++)
            {
                var sequence = new AdventureSequence(seed, false, true);
                for (int i = 0; i < 120; i++)
                {
                    var beat = sequence.Next();
                    if (beat.Family == (int)TrackToy.Seesaw) continue;
                    if (TabletopCourseRules.OpenRampExit(beat)) open++; else blocked++;
                }
            }
            Assert.That(open, Is.InRange(5000, 5500));
            Assert.That(blocked, Is.InRange(5000, 5500));
            for (int family = 0; family < 8; family++)
            {
                Assert.That(TabletopCourseRules.OpenRampExit(new PlaygroundBeat(0, 0, 1, family, 0, 24)), Is.True);
                Assert.That(TabletopCourseRules.OpenRampExit(new PlaygroundBeat(0, 0, 1, family, 11, 24)), Is.False);
            }
        }
    }
}
