using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BrakeToy.Editor
{
    public static class AndroidPrototypeBuild
    {
        // Batch default is explicitly Adventure, never whichever scene happens to exist.
        public static void Build() => BuildProfile(AndroidBuildPlan.ParseProfile(Environment.GetCommandLineArgs()));

        [MenuItem("Tools/Brake Toy/Build Android Adventure")]
        public static void BuildAdventure() => BuildProfile(AndroidBuildProfile.Adventure);

        [MenuItem("Tools/Brake Toy/Validate Android Adventure (no APK)")]
        public static void ValidateAdventure() => ValidateProfile(AndroidBuildProfile.Adventure);

        public static void Validate() => ValidateProfile(AndroidBuildPlan.ParseProfile(Environment.GetCommandLineArgs()));

        public static void ValidateProfile(AndroidBuildProfile profile)
        {
            var plan = AndroidBuildPlan.Create(profile);
            string directory = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Reports", "Local"));
            Directory.CreateDirectory(directory);
            WriteEvidence(plan, "ValidatedOnly", false, Path.Combine(directory, "AndroidBuildPlan.json"));
        }

        public static void BuildProfile(AndroidBuildProfile profile)
        {
            var plan = AndroidBuildPlan.Create(profile); // Fail before changing settings or creating output dirs.
            var architecture = PlayerSettings.Android.targetArchitectures;
            var backend = PlayerSettings.GetScriptingBackend(NamedBuildTarget.Android);
            bool bundle = EditorUserBuildSettings.buildAppBundle;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(plan.OutputPath));
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
                PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
                EditorUserBuildSettings.buildAppBundle = false;
                var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
                {
                    scenes = new[] { plan.ScenePath }, locationPathName = plan.OutputPath,
                    target = BuildTarget.Android, options = plan.Options
                });
                WriteEvidence(plan, report.summary.result.ToString(), true,
                    ReportPaths.Report("ANDROID_BUILD_REPORT-" + profile + "-candidate.json"));
                if (report.summary.result != BuildResult.Succeeded)
                    throw new InvalidOperationException("Android candidate build failed: " + report.summary.result);
            }
            finally
            {
                PlayerSettings.Android.targetArchitectures = architecture;
                PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, backend);
                EditorUserBuildSettings.buildAppBundle = bundle;
            }
        }

        [Serializable]
        private sealed class Evidence
        {
            public string profile, scene, outputPath, options, unityVersion, applicationVersion, result;
            public int versionCode;
            public bool buildExecuted;
        }

        private static void WriteEvidence(AndroidBuildPlan plan, string result, bool executed, string path)
        {
            var evidence = new Evidence
            {
                profile = plan.Profile.ToString(), scene = plan.ScenePath,
                outputPath = plan.OutputPath, options = plan.Options.ToString(),
                unityVersion = Application.unityVersion, applicationVersion = PlayerSettings.bundleVersion,
                versionCode = PlayerSettings.Android.bundleVersionCode, result = result, buildExecuted = executed
            };
            File.WriteAllText(path, JsonUtility.ToJson(evidence, true));
            Debug.Log("Brake Toy Android " + plan.Profile + ": " + result + " -> " + plan.OutputPath);
        }
    }
}
