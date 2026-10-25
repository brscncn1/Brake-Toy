using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PlayMode = BrakeToy.Core.PlayMode;

namespace BrakeToy.Tests
{
    public sealed class AdventureEndingSceneTests
    {
        private GameManager game;
        private AdventureEncounters toys;
        private JoyfulRun jump;
        private int slot;

        private IEnumerator Load()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            game = UnityEngine.Object.FindFirstObjectByType<GameManager>();
            toys = game.GetComponent<AdventureEncounters>();
            jump = game.GetComponent<JoyfulRun>();
            Assert.That(toys.VariedEndings, Is.True, "Existing scene must enable the new default.");
        }

        private void Clear()
        {
            foreach (var hazard in game.Arcade.Pool) hazard.Release();
            foreach (var ramp in jump.Ramps) ramp.Release();
            game.Arcade.Challenges.ResetRun();
            game.GetComponent<PlaygroundEncounters>().ResetRun();
            toys.ResetRun();
            slot = 0;
        }

        private void Place(PlaygroundBeat beat, float z)
        {
            toys.Spawn(beat, z, (kind, x, depth, crossing, slope) =>
            {
                slot++;
                int offset = 0;
                for (int i = 0; i < kind; i++) offset += game.Arcade.Definitions[i].PoolSize;
                var definition = game.Arcade.Definitions[kind];
                for (int i = 0; i < definition.PoolSize; i++)
                {
                    var hazard = game.Arcade.Pool[offset + i];
                    if (hazard.InUse) continue;
                    hazard.HalfWidth = definition.HalfWidth;
                    hazard.HalfLength = definition.HalfLength;
                    hazard.MotionKind = 0;
                    hazard.ForwardFactor = 1;
                    hazard.Place(x, depth, crossing, slope);
                    return hazard;
                }
                throw new InvalidOperationException("Fixture hazard pool exhausted");
            });
        }

        [UnityTest]
        public IEnumerator EndingGeometryKeepsThreeRewardsAndCanRestoreClassicLayout()
        {
            yield return Load();
            foreach (TrackToy family in new[] { TrackToy.Windup, TrackToy.Train })
            foreach (bool varied in new[] { false, true })
            foreach (int variant in new[] { 0, 1, 2, 3 })
            {
                toys.VariedEndings = varied;
                game.StartRun();
                Clear();
                var beat = new PlaygroundBeat(0, 0, 1, (int)family, variant, 24);
                Place(beat, 20);
                bool ground = AdventureRules.Ending(beat, varied) == AdventureEnding.GroundGate;
                int ramps = 0, rewards = 0, airborneRewards = 0;
                foreach (var ramp in jump.Ramps) if (ramp.InUse) ramps++;
                foreach (var star in game.Arcade.Challenges.RiskStars) if (star.gameObject.activeSelf)
                {
                    rewards++;
                    if (star.position.y > 1) airborneRewards++;
                    Assert.That(star.position.x, Is.EqualTo(beat.RiskX));
                    if (ground) Assert.That(star.position.z, Is.InRange(33f, 45f));
                }
                Assert.That(rewards, Is.EqualTo(3));
                Assert.That(ramps, Is.EqualTo(ground ? 0 : 1));
                Assert.That(airborneRewards, Is.EqualTo(ground ? 0 : 1));
                Assert.That(slot, Is.EqualTo(1));
                if (ground)
                {
                    bool raisedArrow = false;
                    foreach (var marker in game.GetComponent<PlaygroundEncounters>().RouteMarkers)
                        if (marker.gameObject.activeSelf && marker.position.y > .07f) raisedArrow = true;
                    Assert.That(raisedArrow, Is.True, "New reward arrow must sit above the playmat.");
                }
            }
        }

        [UnityTest]
        public IEnumerator ProfileSwitchAppliesOnlyWhenStartingTheNextRun()
        {
            yield return Load();
            game.StartRun();
            Clear();
            toys.VariedEndings = false;
            Assert.That(toys.ActiveVariedEndings, Is.True);
            Place(new PlaygroundBeat(0, 0, 1, (int)TrackToy.Windup, 1, 24), 20);
            foreach (var ramp in jump.Ramps) Assert.That(ramp.InUse, Is.False);
            game.StartRun();
            Clear();
            Assert.That(toys.ActiveVariedEndings, Is.False);
            Place(new PlaygroundBeat(0, 0, 1, (int)TrackToy.Windup, 1, 24), 20);
            int ramps = 0;
            foreach (var ramp in jump.Ramps) if (ramp.InUse) ramps++;
            Assert.That(ramps, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator RiskRewardRoutesWorkWithoutPowersAtThreeStepRates()
        {
            yield return Load();
            foreach (TrackToy family in new[] { TrackToy.Windup, TrackToy.Train })
            foreach (int fps in new[] { 30, 60, 120 })
            foreach (bool ground in new[] { false, true })
            {
                toys.VariedEndings = true;
                game.StartRun();
                Clear();
                var beat = new PlaygroundBeat(0, 0, 1, (int)family, ground ? 1 : 3, 20);
                Place(beat, 20);
                float x = beat.SafeX;
                game.Player.StopAt(x);
                while (game.Arcade.Distance < 52)
                {
                    float target = game.Arcade.Distance >= 25 ? beat.RiskX : beat.SafeX;
                    float next = Mathf.MoveTowards(x, target, game.Config.MaxLateralSpeed / fps);
                    game.Arcade.Step(1f / fps, x, next);
                    game.Player.StopAt(next);
                    x = next;
                    Assert.That(game.State, Is.EqualTo(RunState.Running), family + " fps=" + fps + " ground=" + ground);
                }
                Assert.That(game.GetComponent<PlaygroundRun>().Assisted, Is.False);
                Assert.That(game.Arcade.Challenges.Goals.Pickups, ground ? Is.EqualTo(3) : Is.GreaterThanOrEqualTo(1));
                Assert.That(jump.Jumps, Is.EqualTo(ground ? 0 : 1));
            }
        }

        [Serializable] private sealed class Witness
        { public int seed; public bool relax, varied; public float distance; public int lives, spawnedRows; }
        [Serializable] private sealed class WitnessReport { public List<Witness> runs = new List<Witness>(); }

        [UnityTest]
        public IEnumerator SameSeedProfilesKeepSafeRouteSpeedAndSpawnSchedule()
        {
            yield return Load();
            var report = new WitnessReport();
            foreach (bool relax in new[] { false, true })
            foreach (int seed in new[] { 7331, 109, 917 })
            {
                float baselineDistance = 0;
                int baselineRows = 0;
                foreach (bool varied in new[] { false, true })
                {
                    toys.VariedEndings = varied;
                    game.Review.Mode = relax ? PlayMode.Relax : PlayMode.Arcade;
                    game.Arcade.VaryRuns = false;
                    game.Arcade.Seed = seed;
                    game.StartRun();
                    var sequence = new AdventureSequence(seed, relax);
                    var beats = new List<PlaygroundBeat>();
                    for (int n = 0; n < 100; n++) beats.Add(sequence.Next());
                    int row = 0;
                    float x = 0;
                    for (int frame = 0; frame < 3600; frame++)
                    {
                        while (row < beats.Count - 1 && beats[row].Distance + 30 < game.Arcade.Distance) row++;
                        float next = Mathf.MoveTowards(x, beats[row].SafeX, game.Config.MaxLateralSpeed / 60);
                        game.Arcade.Step(1f / 60, x, next);
                        x = next;
                        Assert.That(game.State, Is.EqualTo(RunState.Running));
                    }
                    Assert.That(game.Review.Lives, Is.EqualTo(relax ? 3 : 1));
                    Assert.That(toys.PoolExhaustions + game.Arcade.PoolExhaustions + game.Arcade.Challenges.PoolExhaustions, Is.Zero);
                    if (!varied) { baselineDistance = game.Arcade.Distance; baselineRows = game.Arcade.SpawnedRows; }
                    else
                    {
                        Assert.That(game.Arcade.Distance, Is.EqualTo(baselineDistance).Within(.01f));
                        Assert.That(game.Arcade.SpawnedRows, Is.EqualTo(baselineRows));
                    }
                    report.runs.Add(new Witness { seed = seed, relax = relax, varied = varied,
                        distance = game.Arcade.Distance, lives = game.Review.Lives, spawnedRows = game.Arcade.SpawnedRows });
                }
            }
            Directory.CreateDirectory("Reports/Local");
            File.WriteAllText("Reports/Local/EndingWitness.json", JsonUtility.ToJson(report, true));
        }

        [UnityTest]
        public IEnumerator CaptureSameVariantClassicAndVariedEndings()
        {
            yield return Load();
            foreach (TrackToy family in new[] { TrackToy.Windup, TrackToy.Train })
            foreach (bool varied in new[] { false, true })
            {
                toys.VariedEndings = varied;
                game.StartRun();
                Clear();
                Place(new PlaygroundBeat(0, 0, 1, (int)family, 1, 20), 10);
                game.Player.StopAt(-2.25f);
                yield return null;
                OpeningRuntimeAuditTests.Capture(game, 0, "Ending-" + family + (varied ? "-varied" : "-classic"));
            }
        }
    }
}
