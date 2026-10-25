using System;
using System.Collections;
using System.IO;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using PlayMode = BrakeToy.Core.PlayMode;
using Object = UnityEngine.Object;

namespace BrakeToy.Tests
{
    public sealed class AdventureRecoveryTests
    {
        private GameManager game;
        private AdventureEncounters toys;

        private IEnumerator Load()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            game = Object.FindFirstObjectByType<GameManager>();
            toys = game.GetComponent<AdventureEncounters>();
        }

        private void Clear()
        {
            foreach (var hazard in game.Arcade.Pool) hazard.Release();
            foreach (var ramp in game.GetComponent<JoyfulRun>().Ramps) ramp.Release();
            game.GetComponent<PlaygroundEncounters>().ResetRun();
            toys.ResetRun();
        }

        [UnityTest]
        public IEnumerator RelaxContactClearsNearbyAdventureToysButKeepsFutureEncounters()
        {
            yield return Load();
            game.Review.Mode = PlayMode.Relax;
            game.StartRun();
            Clear();
            var beat = new PlaygroundBeat(0, 0, 1, 1, 1, 0);
            var contact = toys.Place(TrackToy.Windup, beat, .9f);
            var future = toys.Place(TrackToy.Crane, beat, 65);

            game.Arcade.Step(.1f, 0, 0);
            Assert.That(game.Review.Lives, Is.EqualTo(2));
            Assert.That(contact.InUse, Is.False, "The same toy must not consume another life.");
            Assert.That(future.InUse, Is.True, "Recovery must not erase the whole future course.");
            for (int i = 0; i < 10; i++) game.Arcade.Step(1f / 60, 0, 0);
            Assert.That(game.Review.Lives, Is.EqualTo(2));
            Assert.That(game.State, Is.EqualTo(RunState.Running));
        }

        [UnityTest]
        public IEnumerator RecoveryResetsSlipperySurfaceAndNearbyRampWithoutErasingFutureRamp()
        {
            yield return Load();
            game.Review.Mode = PlayMode.Relax;
            game.StartRun();
            Clear();
            var beat = new PlaygroundBeat(0, 0, 1, 5, 1, 0);
            toys.Place(TrackToy.Surfaces, beat, 0);
            toys.Advance(.01f, 0, 0, 0);
            Assert.That(toys.OnIce, Is.True);
            var jump = game.GetComponent<JoyfulRun>();
            jump.SpawnRamp(0, 5);
            jump.SpawnRamp(0, 60);
            Assert.That(game.Review.SurviveHit(), Is.True);
            Assert.That(toys.OnIce, Is.False);
            Assert.That(toys.SurfaceFactor, Is.EqualTo(1));
            int ramps = 0;
            foreach (var ramp in jump.Ramps) if (ramp.InUse)
            {
                ramps++;
                Assert.That(ramp.transform.position.z, Is.EqualTo(60));
            }
            Assert.That(ramps, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator SeparateContactsStillConsumeThreeRelaxLivesAndOneArcadeLife()
        {
            yield return Load();
            foreach (var mode in new[] { PlayMode.Relax, PlayMode.Arcade })
            {
                game.Review.Mode = mode;
                game.StartRun();
                int lives = mode == PlayMode.Relax ? 3 : 1;
                for (int hit = 1; hit <= lives; hit++)
                {
                    Clear();
                    toys.Place(TrackToy.Windup, new PlaygroundBeat(0, 0, 1, 1, 1, 0), .9f);
                    game.Arcade.Step(.1f, 0, 0);
                    Assert.That(game.Review.Lives, Is.EqualTo(lives - hit));
                    Assert.That(game.State, Is.EqualTo(hit < lives ? RunState.Running : RunState.Crashed));
                }
            }
        }

        [UnityTest]
        public IEnumerator AdventureMigratesProgressWithoutImportingLegacyRecordsOrWritingLegacyFile()
        {
            yield return Load();
            string directory = Path.Combine(Application.temporaryCachePath, "adventure-profile-" + Guid.NewGuid().ToString("N"));
            string key = "BrakeToy.Tests." + Guid.NewGuid().ToString("N");
            string originalDistanceKey = game.Arcade.DistanceRecordKey;
            string originalStyleKey = game.Arcade.StyleRecordKey;
            try
            {
                string legacyPath = Path.Combine(directory, "career-playground-v1.json");
                var legacy = new CareerData { points = 2345, unlocked = 3, selectedCar = 1,
                    arcadeDistance = 9000, arcadeStyle = 8000, relaxDistance = 7000,
                    relaxStyle = 6000, dailyDistance = 5000, dailyStyle = 4000,
                    powerStarterGranted = true, slowCharges = 2, rocketCharges = 0, jetCharges = 0 };
                Assert.That(new CareerSave(legacyPath).Save(legacy), Is.True);
                byte[] original = File.ReadAllBytes(legacyPath);
                game.Arcade.DistanceRecordKey = key + ".distance";
                game.Arcade.StyleRecordKey = key + ".style";
                PlayerPrefs.SetFloat(game.Arcade.DistanceRecordKey, 123);
                PlayerPrefs.SetInt(game.Arcade.StyleRecordKey, 321);
                game.Review.Persist = true;
                game.Review.Initialize(game, directory);
                var data = game.Review.Data;
                Assert.That(data.arcadeDistance, Is.EqualTo(123));
                Assert.That(data.arcadeStyle, Is.EqualTo(321));
                Assert.That(data.relaxDistance + data.dailyDistance + data.relaxStyle + data.dailyStyle, Is.Zero);
                Assert.That(data.points, Is.EqualTo(2345));
                Assert.That(data.selectedCar, Is.EqualTo(1));
                Assert.That(data.slowCharges, Is.EqualTo(2));
                game.Review.InitializePowers();
                Assert.That(data.rocketCharges + data.jetCharges, Is.Zero, "Migration must not grant starter powers again.");
                Assert.That(game.Review.BuyOrSelect(1), Is.True); // Save the already-owned selection.
                string currentPath = Path.Combine(directory, "career-adventure-v1.json");
                Assert.That(File.Exists(currentPath), Is.True);
                Assert.That(File.ReadAllBytes(legacyPath), Is.EqualTo(original));
                game.Review.Initialize(game, directory);
                Assert.That(game.Review.Data.points, Is.EqualTo(2345));
                Assert.That(game.Review.Data.arcadeStyle, Is.EqualTo(321));
            }
            finally
            {
                game.Review.Persist = false;
                PlayerPrefs.DeleteKey(key + ".distance");
                PlayerPrefs.DeleteKey(key + ".style");
                game.Arcade.DistanceRecordKey = originalDistanceKey;
                game.Arcade.StyleRecordKey = originalStyleKey;
                if (Directory.Exists(directory))
                {
                    foreach (string file in Directory.GetFiles(directory)) File.Delete(file);
                    Directory.Delete(directory);
                }
            }
        }
    }
}
