using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BrakeToy.Editor
{
    public static class AndroidPrototypeBuild
    {
        [MenuItem("Tools/Brake Toy/Build Android Prototype")]
        public static void Build()
        {
            string scene = File.Exists(ReviewSceneBuilder.ScenePath) ? ReviewSceneBuilder.ScenePath : File.Exists(LivingSceneBuilder.ScenePath) ? LivingSceneBuilder.ScenePath : File.Exists(RefinedSceneBuilder.ScenePath) ? RefinedSceneBuilder.ScenePath : File.Exists(WorkshopSceneBuilder.ScenePath) ? WorkshopSceneBuilder.ScenePath : File.Exists(TabletopSceneBuilder.ScenePath) ? TabletopSceneBuilder.ScenePath : "Assets/BrakeToy/Scenes/ToyRoom.unity";
            if (!File.Exists(scene)) PrototypeBuilder.Create();
            Directory.CreateDirectory("Builds/Android");
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.SetScriptingBackend(UnityEditor.Build.NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            EditorUserBuildSettings.buildAppBundle = false;
            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { scene },
                locationPathName = scene == ReviewSceneBuilder.ScenePath ? "Builds/Android/BrakeToy-v0.6-Review.apk" : scene == LivingSceneBuilder.ScenePath ? "Builds/Android/BrakeToy-v0.5-Living.apk" : scene == RefinedSceneBuilder.ScenePath ? "Builds/Android/BrakeToy-v0.4-Refined.apk" : scene == WorkshopSceneBuilder.ScenePath ? "Builds/Android/BrakeToy-v0.3-Arcade.apk" : scene == TabletopSceneBuilder.ScenePath ? "Builds/Android/BrakeToy-v0.2-Tabletop.apk" : "Builds/Android/BrakeToy-Phase1.apk",
                target = BuildTarget.Android,
                options = scene == ReviewSceneBuilder.ScenePath ? BuildOptions.None : BuildOptions.Development
            });
            if (report.summary.result != BuildResult.Succeeded)
                throw new System.InvalidOperationException("Android prototip derlemesi başarısız: " + report.summary.result);
            Debug.Log("Brake Toy Android prototype: " + Path.GetFullPath(report.summary.outputPath));
        }
    }
}
