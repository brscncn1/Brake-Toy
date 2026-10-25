using System;
using System.IO;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;

namespace BrakeToy.Tests
{
    public sealed class CareerMigrationTests
    {
        private string directory;
        private CareerSave legacy, current;

        [SetUp]
        public void SetUp()
        {
            directory = Path.Combine(Application.temporaryCachePath, "career-migration-" + Guid.NewGuid().ToString("N"));
            legacy = new CareerSave(Path.Combine(directory, "legacy.json"));
            current = new CareerSave(Path.Combine(directory, "current.json"));
        }

        [TearDown]
        public void TearDown()
        {
            if (!Directory.Exists(directory)) return;
            foreach (string file in Directory.GetFiles(directory)) File.Delete(file);
            Directory.Delete(directory);
        }

        [Test]
        public void MigrationPreservesEconomyAchievementsAndSpentPowersWithoutTouchingSource()
        {
            var old = new CareerData { points = 2345, unlocked = 7, selectedCar = 2,
                runs = 20, arcadeRuns = 12, relaxRuns = 8, totalDistance = 87654,
                achievements = 15, completedMissions = 8, horn = 1, ownedHorns = 3,
                trail = 2, ownedTrails = 5, powerStarterGranted = true,
                slowCharges = 2, rocketCharges = 0, jetCharges = 0,
                arcadeDistance = 1000, relaxDistance = 2000, dailyDistance = 3000,
                arcadeStyle = 4000, relaxStyle = 5000, dailyStyle = 6000, dailyDate = "2026-10-24" };
            Assert.That(legacy.Save(old), Is.True);
            byte[] original = File.ReadAllBytes(legacy.PathName);
            var data = current.LoadProgressFromLegacy(legacy.PathName);
            old.arcadeDistance = old.relaxDistance = old.dailyDistance = 0;
            old.arcadeStyle = old.relaxStyle = old.dailyStyle = 0;
            old.dailyDate = "";
            Assert.That(JsonUtility.ToJson(data), Is.EqualTo(JsonUtility.ToJson(old)));
            Assert.That(File.ReadAllBytes(legacy.PathName), Is.EqualTo(original));
            Assert.That(current.HasStoredData, Is.False, "Reading a migration must not silently write a save.");
            Assert.That(PlaygroundRules.EnsureStarter(data), Is.False);
            Assert.That(current.Save(data), Is.True);
            data.arcadeDistance = 125;
            data.arcadeStyle = 250;
            Assert.That(current.Save(data), Is.True);
            Assert.That(current.LoadProgressFromLegacy(legacy.PathName).arcadeStyle, Is.EqualTo(250));
        }

        [Test]
        public void MissingCurrentPrimaryUsesItsOwnBackupBeforeLegacyProgress()
        {
            Assert.That(legacy.Save(new CareerData { points = 9999 }), Is.True);
            Assert.That(current.Save(new CareerData { points = 100, arcadeStyle = 77 }), Is.True);
            Assert.That(current.Save(new CareerData { points = 200, arcadeStyle = 88 }), Is.True);
            File.Delete(current.PathName);
            var restored = current.LoadProgressFromLegacy(legacy.PathName);
            Assert.That(current.Recovered, Is.True);
            Assert.That(restored.points, Is.EqualTo(100));
            Assert.That(restored.arcadeStyle, Is.EqualTo(77));
        }

        [Test]
        public void CorruptLegacyPrimaryCanMigrateItsBackupAndMissingLegacyStartsFresh()
        {
            Assert.That(legacy.Save(new CareerData { points = 123, powerStarterGranted = true }), Is.True);
            Assert.That(legacy.Save(new CareerData { points = 456 }), Is.True);
            File.WriteAllText(legacy.PathName, "broken");
            var recovered = current.LoadProgressFromLegacy(legacy.PathName);
            Assert.That(recovered.points, Is.EqualTo(123));
            Assert.That(recovered.powerStarterGranted, Is.True);
            Assert.That(current.Recovered, Is.True);
            var fresh = current.LoadProgressFromLegacy(Path.Combine(directory, "missing.json"));
            Assert.That(CareerRules.IsValid(fresh), Is.True);
            Assert.That(fresh.points, Is.Zero);
            Assert.That(current.Recovered, Is.False);
        }
    }
}
