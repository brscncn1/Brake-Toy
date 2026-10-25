using BrakeToy.Core;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class AdventureEndingTests
    {
        [TestCase(TrackToy.Windup)]
        [TestCase(TrackToy.Train)]
        public void BothEndingsExistForEachModelParityAndClassicCanBeRestored(TrackToy family)
        {
            for (int parity = 0; parity < 2; parity++)
            {
                var ground = new PlaygroundBeat(0, 0, 1, (int)family, parity, 24);
                var ramp = new PlaygroundBeat(0, 0, 1, (int)family, parity + 2, 24);
                Assert.That(AdventureRules.Ending(ground), Is.EqualTo(AdventureEnding.GroundGate));
                Assert.That(AdventureRules.Ending(ramp), Is.EqualTo(AdventureEnding.BookRamp));
                Assert.That(AdventureRules.Ending(ground, false), Is.EqualTo(AdventureEnding.BookRamp));
            }
        }

        [Test]
        public void OtherSixFamiliesKeepTheirOriginalEnding()
        {
            for (int family = 0; family < 8; family++)
            {
                if (family == (int)TrackToy.Windup || family == (int)TrackToy.Train) continue;
                for (int variant = 0; variant < 20; variant++)
                {
                    var beat = new PlaygroundBeat(0, 0, 1, family, variant, 24);
                    Assert.That(AdventureRules.Ending(beat), Is.EqualTo(family == (int)TrackToy.Seesaw
                        ? AdventureEnding.Ruler : AdventureEnding.BookRamp));
                }
            }
        }

        [Test]
        public void BothEndingsAppearAcrossSeededStream()
        {
            int ground = 0, ramp = 0;
            for (int seed = 1; seed <= 100; seed++)
            {
                var sequence = new AdventureSequence(seed);
                for (int i = 0; i < 64; i++)
                {
                    var beat = sequence.Next();
                    var ending = AdventureRules.Ending(beat);
                    if (beat.Family != (int)TrackToy.Windup && beat.Family != (int)TrackToy.Train) continue;
                    if (ending == AdventureEnding.GroundGate) ground++; else ramp++;
                }
            }
            Assert.That(ground, Is.InRange(600, 1000));
            Assert.That(ramp, Is.InRange(600, 1000));
        }

        [Test]
        public void OldAndComparisonGhostsCannotLoadAsCurrentGhosts()
        {
            Assert.That(AdventureRules.GhostNamespace(true), Is.Not.EqualTo("adventure"));
            Assert.That(AdventureRules.GhostNamespace(true), Is.Not.EqualTo(AdventureRules.GhostNamespace(false)));
        }
    }
}
