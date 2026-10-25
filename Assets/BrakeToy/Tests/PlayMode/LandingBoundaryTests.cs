using System.Collections;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BrakeToy.Tests
{
    public sealed class LandingBoundaryTests
    {
        private GameManager game;
        private JoyfulRun jump;
        private PlaygroundRun powers;
        private AdventureEncounters toys;

        private IEnumerator Load()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            game = Object.FindFirstObjectByType<GameManager>();
            jump = game.GetComponent<JoyfulRun>();
            powers = game.GetComponent<PlaygroundRun>();
            toys = game.GetComponent<AdventureEncounters>();
        }

        private void Clear()
        {
            foreach (var hazard in game.Arcade.Pool) hazard.Release();
            foreach (var ramp in jump.Ramps) ramp.Release();
            game.GetComponent<PlaygroundEncounters>().ResetRun();
            toys.ResetRun();
        }

        [UnityTest]
        public IEnumerator JetDescentDoesNotCrashBeforeLandingGraceStarts()
        {
            yield return Load();
            foreach (int fps in new[] { 30, 60, 120 })
            {
                game.StartRun();
                Clear();
                game.Review.Data.jetCharges = 1;
                Assert.That(powers.Activate(ToyPower.Jetpack), Is.True);
                while (powers.Remaining > .1f)
                {
                    Clear();
                    game.Arcade.Step(1f / fps, 0, 0);
                }
                Clear();
                var toy = toys.Place(TrackToy.Windup, new PlaygroundBeat(0, 0, 1, 1, 1, 0), .9f);
                game.Arcade.Step(1f / fps, 0, 0);
                Assert.That(game.State, Is.EqualTo(RunState.Running), "Landing descent at " + fps + " FPS");
                Assert.That(toy.InUse, Is.False);
                Assert.That(game.Review.Data.jetCharges, Is.Zero);
            }
        }

        [UnityTest]
        public IEnumerator RulerSupportMatchesItsVisiblePositionInTheSameStep()
        {
            yield return Load();
            foreach (int fps in new[] { 30, 60, 120 })
            {
                game.StartRun();
                Clear();
                toys.Place(TrackToy.Seesaw, new PlaygroundBeat(0, 0, 1, 6, 0, 0), 2.6f + game.Config.PlayerHalfLength + .02f);
                for (int i = 0; i < 10; i++)
                {
                    game.Arcade.Step(1f / fps, 0, 0);
                    float surface = toys.SurfaceHeight(0, 0);
                    Assert.That(jump.Height, Is.EqualTo(surface).Within(.001f), "Support lags visible ruler at " + fps + " FPS");
                }
            }
        }

        [UnityTest]
        public IEnumerator BookAndRulerFootprintEdgesLiftInsideAndStayGroundedOutside()
        {
            yield return Load();
            foreach (int fps in new[] { 30, 60, 120 })
            foreach (bool ruler in new[] { false, true })
            foreach (float side in new[] { -1f, 1f })
            foreach (bool inside in new[] { false, true })
            {
                game.StartRun();
                Clear();
                float edge = (ruler ? .85f : .95f) + game.Config.PlayerHalfWidth - .015f;
                float x = side * (edge + (inside ? -.02f : .02f));
                if (ruler) toys.Place(TrackToy.Seesaw, new PlaygroundBeat(0, 0, 1, 6, 0, 0), 4);
                else jump.SpawnRamp(0, 4);
                bool climbed = false;
                for (int i = 0; i < fps * 3; i++)
                {
                    game.Arcade.Step(1f / fps, x, x);
                    climbed |= jump.Height > .2f;
                    Assert.That(game.State, Is.EqualTo(RunState.Running));
                }
                Assert.That(climbed, Is.EqualTo(inside), "Footprint boundary ruler=" + ruler + " fps=" + fps);
                Assert.That(jump.Jumps, Is.EqualTo(inside ? 1 : 0));
                Assert.That(jump.Height, Is.Zero.Within(.001f));
            }
        }

        [UnityTest]
        public IEnumerator RulerSideExitFallsSmoothlyAndCannotLaunchAfterLosingSupport()
        {
            yield return Load();
            game.StartRun();
            Clear();
            toys.Place(TrackToy.Seesaw, new PlaygroundBeat(0, 0, 1, 6, 0, 0), 1);
            game.Arcade.Step(1f / 60, 0, 0);
            float before = jump.Height;
            game.Arcade.Step(1f / 60, 0, 1.6f);
            Assert.That(jump.Height, Is.GreaterThan(before * .8f));
            Assert.That(jump.Airborne, Is.True);
            for (int i = 0; i < 60; i++) game.Arcade.Step(1f / 60, 1.6f, 1.6f);
            Assert.That(jump.Height, Is.Zero.Within(.001f));
            Assert.That(jump.Jumps, Is.Zero);
        }

        [UnityTest]
        public IEnumerator LandingGraceExpiresAndRetryDoesNotRefundAnyPower()
        {
            yield return Load();
            foreach (ToyPower power in new[] { ToyPower.Slow, ToyPower.Rocket, ToyPower.Jetpack })
            {
                game.StartRun();
                Clear();
                game.Review.Data.slowCharges = game.Review.Data.rocketCharges = game.Review.Data.jetCharges = 1;
                Assert.That(powers.Activate(power), Is.True);
                while (powers.ActivePower.HasValue)
                {
                    Clear();
                    game.Arcade.Step(1f / 60, 0, 0);
                }
                Assert.That(powers.Protected, Is.EqualTo(power != ToyPower.Slow));
                for (int i = 0; i < 60; i++)
                {
                    Clear();
                    game.Arcade.Step(1f / 60, 0, 0);
                }
                Assert.That(powers.Protected, Is.False);
                Clear();
                toys.Place(TrackToy.Windup, new PlaygroundBeat(0, 0, 1, 1, 1, 0), .9f);
                game.Arcade.Step(1f / 60, 0, 0);
                Assert.That(game.State, Is.EqualTo(RunState.Crashed));
                game.StartSameRoute();
                Assert.That(PlaygroundRules.Charges(game.Review.Data, power), Is.Zero);
                Assert.That(powers.Protected || powers.ActivePower.HasValue, Is.False);
            }
        }
    }
}
