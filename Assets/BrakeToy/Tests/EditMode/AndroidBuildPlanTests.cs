using System;
using System.Collections.Generic;
using System.IO;
using BrakeToy.Editor;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build;

namespace BrakeToy.Tests
{
    public sealed class AndroidBuildPlanTests
    {
        [TestCase(AndroidBuildProfile.Adventure, "ToyRoomAdventure", false)]
        [TestCase(AndroidBuildProfile.Tidy, "ToyRoomTidy", false)]
        [TestCase(AndroidBuildProfile.Playground, "ToyRoomPlayground", false)]
        [TestCase(AndroidBuildProfile.Joyful, "ToyRoomJoyful", false)]
        [TestCase(AndroidBuildProfile.Review, "ToyRoomReview", false)]
        [TestCase(AndroidBuildProfile.Living, "ToyRoomLiving", true)]
        [TestCase(AndroidBuildProfile.Refined, "ToyRoomRefined", true)]
        [TestCase(AndroidBuildProfile.Arcade, "ToyRoomArcade", true)]
        [TestCase(AndroidBuildProfile.Tabletop, "ToyRoomTabletop", true)]
        [TestCase(AndroidBuildProfile.Prototype, "ToyRoom", true)]
        public void ExplicitProfileSelectsItsOwnExistingSceneAndSeparateCandidate(AndroidBuildProfile profile, string scene, bool development)
        {
            var plan = AndroidBuildPlan.Create(profile);
            Assert.That(plan.ScenePath, Is.EqualTo("Assets/BrakeToy/Scenes/" + scene + ".unity"));
            Assert.That(plan.Profile, Is.EqualTo(profile));
            Assert.That(plan.Options.HasFlag(BuildOptions.Development), Is.EqualTo(development));
            Assert.That(plan.OutputPath, Is.EqualTo("Builds/Android/Candidates/BrakeToy-" + profile + ".apk"));
        }

        [Test]
        public void MissingSelectedSceneDoesNotProbeOlderScenesOrGenerateAnything()
        {
            var probes = new List<string>();
            Assert.Throws<FileNotFoundException>(() => AndroidBuildPlan.Create(AndroidBuildProfile.Adventure, path =>
            {
                probes.Add(path);
                return path.EndsWith("ToyRoom.unity"); // An older scene would exist if probed.
            }));
            Assert.That(probes, Is.EqualTo(new[] { AdventureSceneBuilder.ScenePath }));
        }

        [Test]
        public void InvalidEnumFailsBeforeCheckingFilesystem()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => AndroidBuildPlan.Create((AndroidBuildProfile)999,
                _ => throw new Exception("Must not touch filesystem")));
        }

        [Test]
        public void CliDefaultIsAdventureAndLegacySelectionIsCaseInsensitive()
        {
            Assert.That(AndroidBuildPlan.ParseProfile(new[] { "Unity.exe", "-batchmode" }), Is.EqualTo(AndroidBuildProfile.Adventure));
            Assert.That(AndroidBuildPlan.ParseProfile(new[] { "-brakeToyProfile", "tIdY" }), Is.EqualTo(AndroidBuildProfile.Tidy));
        }

        [TestCase("-brakeToyProfile")]
        [TestCase("-brakeToyProfile", "Unknown")]
        [TestCase("-brakeToyProfile", "0")]
        [TestCase("-brakeToyProfile", "-batchmode")]
        [TestCase("-brakeToyProfile=Adventure")]
        [TestCase("-brakeToyProfile", "Tidy", "-brakeToyProfile", "Adventure")]
        public void MalformedOrAmbiguousCliIsRejected(params string[] args)
        {
            Assert.Throws<ArgumentException>(() => AndroidBuildPlan.ParseProfile(args));
        }

        [Test]
        public void PlanningLeavesAndroidSettingsAndEnabledScenesUnchanged()
        {
            var architecture = PlayerSettings.Android.targetArchitectures;
            var backend = PlayerSettings.GetScriptingBackend(NamedBuildTarget.Android);
            bool bundle = EditorUserBuildSettings.buildAppBundle;
            var scenes = EditorBuildSettings.scenes;
            foreach (AndroidBuildProfile profile in Enum.GetValues(typeof(AndroidBuildProfile)))
                AndroidBuildPlan.Create(profile);
            Assert.That(PlayerSettings.Android.targetArchitectures, Is.EqualTo(architecture));
            Assert.That(PlayerSettings.GetScriptingBackend(NamedBuildTarget.Android), Is.EqualTo(backend));
            Assert.That(EditorUserBuildSettings.buildAppBundle, Is.EqualTo(bundle));
            var after = EditorBuildSettings.scenes;
            Assert.That(after.Length, Is.EqualTo(scenes.Length));
            for (int i = 0; i < scenes.Length; i++)
            {
                Assert.That(after[i].path, Is.EqualTo(scenes[i].path));
                Assert.That(after[i].enabled, Is.EqualTo(scenes[i].enabled));
            }
        }

        [Test]
        public void BatchEntryPointsAreUnambiguousParameterlessMethods()
        {
            Assert.That(typeof(AndroidPrototypeBuild).GetMethod("Build").GetParameters(), Is.Empty);
            Assert.That(typeof(AndroidPrototypeBuild).GetMethod("Validate").GetParameters(), Is.Empty);
        }
    }
}
