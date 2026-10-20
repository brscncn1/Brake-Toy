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
            const string scene = "Assets/BrakeToy/Scenes/ToyRoom.unity";
            if (!File.Exists(scene)) PrototypeBuilder.Create();
            Directory.CreateDirectory("Builds/Android");
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.SetScriptingBackend(UnityEditor.Build.NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            EditorUserBuildSettings.buildAppBundle = false;
            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { scene },
                locationPathName = "Builds/Android/BrakeToy-Phase1.apk",
                target = BuildTarget.Android,
                options = BuildOptions.Development
            });
            if (report.summary.result != BuildResult.Succeeded)
                throw new System.InvalidOperationException("Android prototip derlemesi başarısız: " + report.summary.result);
            Debug.Log("Brake Toy Android prototype: " + Path.GetFullPath("Builds/Android/BrakeToy-Phase1.apk"));
        }
    }
}
