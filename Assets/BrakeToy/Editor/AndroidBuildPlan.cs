using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BrakeToy.Editor
{
    public enum AndroidBuildProfile
    {
        Adventure, Tidy, Playground, Joyful, Review, Living, Refined, Arcade, Tabletop, Prototype
    }

    public sealed class AndroidBuildPlan
    {
        public AndroidBuildProfile Profile { get; }
        public string ScenePath { get; }
        public string OutputPath { get; }
        public BuildOptions Options { get; }

        private AndroidBuildPlan(AndroidBuildProfile profile, string scene, bool development)
        {
            Profile = profile;
            ScenePath = scene;
            OutputPath = "Builds/Android/Candidates/BrakeToy-" + profile + ".apk";
            Options = development ? BuildOptions.Development : BuildOptions.None;
        }

        // No scene generation, settings mutation or BuildPipeline call while making a plan.
        public static AndroidBuildPlan Create(AndroidBuildProfile profile, Func<string, bool> sceneExists = null)
        {
            AndroidBuildPlan plan;
            switch (profile)
            {
                case AndroidBuildProfile.Adventure: plan = new AndroidBuildPlan(profile, AdventureSceneBuilder.ScenePath, false); break;
                case AndroidBuildProfile.Tidy: plan = new AndroidBuildPlan(profile, TidySceneBuilder.ScenePath, false); break;
                case AndroidBuildProfile.Playground: plan = new AndroidBuildPlan(profile, PlaygroundSceneBuilder.ScenePath, false); break;
                case AndroidBuildProfile.Joyful: plan = new AndroidBuildPlan(profile, JoyfulSceneBuilder.ScenePath, false); break;
                case AndroidBuildProfile.Review: plan = new AndroidBuildPlan(profile, ReviewSceneBuilder.ScenePath, false); break;
                case AndroidBuildProfile.Living: plan = new AndroidBuildPlan(profile, LivingSceneBuilder.ScenePath, true); break;
                case AndroidBuildProfile.Refined: plan = new AndroidBuildPlan(profile, RefinedSceneBuilder.ScenePath, true); break;
                case AndroidBuildProfile.Arcade: plan = new AndroidBuildPlan(profile, WorkshopSceneBuilder.ScenePath, true); break;
                case AndroidBuildProfile.Tabletop: plan = new AndroidBuildPlan(profile, TabletopSceneBuilder.ScenePath, true); break;
                case AndroidBuildProfile.Prototype: plan = new AndroidBuildPlan(profile, "Assets/BrakeToy/Scenes/ToyRoom.unity", true); break;
                default: throw new ArgumentOutOfRangeException(nameof(profile), profile, "Unknown Android profile.");
            }
            if (sceneExists == null)
                sceneExists = scene => File.Exists(Path.Combine(Application.dataPath, "..", scene));
            if (!sceneExists(plan.ScenePath))
                throw new FileNotFoundException("Selected " + profile + " scene is missing; no fallback or generator will run.", plan.ScenePath);
            return plan;
        }

        public static AndroidBuildProfile ParseProfile(string[] args)
        {
            const string flag = "-brakeToyProfile";
            AndroidBuildProfile profile = AndroidBuildProfile.Adventure;
            bool found = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (!args[i].StartsWith(flag, StringComparison.OrdinalIgnoreCase)) continue;
                if (found || !args[i].Equals(flag, StringComparison.OrdinalIgnoreCase) || ++i >= args.Length)
                    throw new ArgumentException("Use one -brakeToyProfile followed by a named profile.");
                found = true;
                string value = args[i];
                if (Array.FindIndex(Enum.GetNames(typeof(AndroidBuildProfile)), name => name.Equals(value, StringComparison.OrdinalIgnoreCase)) < 0
                    || !Enum.TryParse(value, true, out profile))
                    throw new ArgumentException("Unknown Android profile: " + value);
            }
            return profile;
        }
    }
}
