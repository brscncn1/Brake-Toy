using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;
using PlayMode = BrakeToy.Core.PlayMode;

namespace BrakeToy.Tests
{
    public sealed class TabletopCourseSceneTests
    {
        private GameManager game;
        private AdventureEncounters toys;
        private IEnumerator Load()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            game = Object.FindFirstObjectByType<GameManager>();
            toys = game.GetComponent<AdventureEncounters>();
            Assert.That(toys.DenseCourse, Is.True);
            game.Arcade.VaryRuns = false;
        }

        [UnityTest]
        public IEnumerator RestoredDeskObjectsBusAndSixGatePatternsActuallySpawn()
        {
            yield return Load();
            Assert.That(game.Arcade.Definitions.Length, Is.EqualTo(10));
            var activate = typeof(ArcadeDirector).GetMethod("Activate", BindingFlags.Instance | BindingFlags.NonPublic);
            var allKinds = new int[10];
            for (int pattern = 0; pattern < 6; pattern++)
            {
                game.StartRun();
                foreach (var h in game.Arcade.Pool) h.Release();
                foreach (var r in game.GetComponent<JoyfulRun>().Ramps) r.Release();
                game.Arcade.Challenges.ResetRun();
                game.GetComponent<PlaygroundEncounters>().ResetRun();
                toys.ResetRun();
                var counts = new int[10];
                var beat = new PlaygroundBeat(pattern, 0, 1, pattern, 7, 24);
                toys.Spawn(beat, 24, (kind, x, z, crossing, slope) =>
                {
                    counts[kind]++;
                    allKinds[kind]++;
                    return (ArcadeHazard)activate.Invoke(game.Arcade, new object[] { kind, x, z, crossing, slope });
                });
                foreach (int kind in new[] { 7, 8, 9 }) Assert.That(counts[kind], Is.GreaterThanOrEqualTo(1));
                Assert.That(counts[6], Is.EqualTo(pattern == 3 || pattern == 5 ? 1 : 0));
                foreach (var marker in game.GetComponent<PlaygroundEncounters>().RouteMarkers)
                    Assert.That(marker.gameObject.activeSelf, Is.False, "No permanent green bypass arrows.");
                foreach (var hazard in game.Arcade.Pool)
                {
                    if (!hazard.InUse) continue;
                    if (hazard.MotionKind == 2)
                    {
                        float gapX = ArcadeRules.LaneX(TabletopCourseRules.GapLane(beat, 1));
                        float margin = Mathf.Abs(hazard.CrossingX - gapX) - hazard.CurveAmplitude
                            - hazard.HalfWidth - game.Config.PlayerHalfWidth;
                        Assert.That(margin, Is.GreaterThanOrEqualTo(.15f), "Bus must leave clearance throughout its entire sweep.");
                        float before = hazard.PositionAfter(0).x;
                        float after = hazard.PositionAfter(8).x;
                        Assert.That(Mathf.Abs(after - before), Is.GreaterThan(.05f));
                    }
                    if (hazard.Model.name.EndsWith("visual"))
                    {
                        Bounds bounds = hazard.Model.GetComponentInChildren<Renderer>().bounds;
                        foreach (var r in hazard.Model.GetComponentsInChildren<Renderer>()) bounds.Encapsulate(r.bounds);
                        Assert.That(bounds.size.x * .5f, Is.EqualTo(hazard.HalfWidth).Within(.025f));
                        Assert.That(bounds.size.z * .5f, Is.EqualTo(hazard.HalfLength).Within(.025f));
                    }
                }
                game.Player.StopAt(beat.SafeX);
                yield return null;
                if (pattern == 1 || pattern == 3 || pattern == 5)
                    OpeningRuntimeAuditTests.Capture(game, 0, "Tabletop-pattern-" + pattern);
            }
            for (int kind = 0; kind < 10; kind++)
                Assert.That(allKinds[kind], Is.GreaterThan(0), "Catalog kind must actually spawn: " + kind);
        }

        [UnityTest]
        public IEnumerator ChangingGapsRemainTraversableWithoutPowersAcrossModesSeedsAndFrameRates()
        {
            yield return Load();
            foreach (bool relax in new[] { false, true })
            foreach (int seed in new[] { 7331, 109, 917 })
            foreach (int fps in new[] { 30, 60, 120 })
            {
                game.Review.Mode = relax ? PlayMode.Relax : PlayMode.Arcade;
                game.Arcade.Seed = seed;
                game.StartRun();
                var sequence = new AdventureSequence(seed, relax, true);
                var beats = new List<PlaygroundBeat>();
                for (int i = 0; i < 100; i++) beats.Add(sequence.Next());
                int row = 0;
                float x = 0;
                for (int frame = 0; frame < 60 * fps; frame++)
                {
                    while (row < beats.Count - 1 && TabletopCourseRules.ExitDistance(beats[row]) < game.Arcade.Distance) row++;
                    float target = TabletopCourseRules.WitnessX(beats[row], game.Arcade.Distance);
                    float next = Mathf.MoveTowards(x, target, game.Config.MaxLateralSpeed / fps);
                    game.Arcade.Step(1f / fps, x, next);
                    game.Player.StopAt(next);
                    x = next;
                    Assert.That(game.State, Is.EqualTo(RunState.Running),
                        "seed=" + seed + " fps=" + fps + " relax=" + relax + " row=" + row + " distance=" + game.Arcade.Distance);
                    if (!relax && seed == 7331 && fps == 60 && (frame + 1) % 600 == 0 && frame < 1800)
                    {
                        yield return null;
                        OpeningRuntimeAuditTests.Capture(game, (frame + 1) / 60, "Tabletop-run-" + (frame + 1) / 60);
                    }
                }
                Assert.That(game.Review.Lives, Is.EqualTo(relax ? 3 : 1));
                Assert.That(game.GetComponent<PlaygroundRun>().Assisted, Is.False);
                Assert.That(toys.PoolExhaustions + game.Arcade.PoolExhaustions + game.Arcade.Challenges.PoolExhaustions, Is.Zero);
            }
        }

        [UnityTest]
        public IEnumerator ParkingInOneLaneNoLongerBypassesTheCourse()
        {
            yield return Load();
            game.Review.Mode = PlayMode.Arcade;
            foreach (float x in new[] { -2.25f, 0, 2.25f })
            {
                game.Arcade.Seed = 7331;
                game.StartRun();
                game.Player.StopAt(x);
                for (int frame = 0; frame < 1800 && game.State == RunState.Running; frame++)
                    game.Arcade.Step(1f / 60, x, x);
                Assert.That(game.State, Is.Not.EqualTo(RunState.Running), "Stationary lane " + x);
            }
        }
    }
}
