using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace BrakeToy.Editor
{
    public static class JoyfulReleaseChecks
    {
        [Serializable]private sealed class Report
        {public string scene="ToyRoomJoyful",version="0.7.0";public int sceneRenderersIncludingInactiveCars,sceneTrianglesIncludingInactiveCars,uniqueMeshes,missingMeshes,missingMaterials,shaderErrors,decorativeColliders;public bool physicalDeviceProfiled;public string note="Scene counts before runtime catalog pool replacement. All seven prefab models separately checked.";}
        public static void CheckAndBuild(){Check();AndroidPrototypeBuild.Build();}
        public static void Check()
        {
            EditorSceneManager.OpenScene(JoyfulSceneBuilder.ScenePath);var report=new Report();var meshes=new HashSet<Mesh>();
            void Inspect(MeshFilter f,bool scene)
            {if(!f.sharedMesh){report.missingMeshes++;return;}var renderer=f.GetComponent<Renderer>();if(!renderer){report.missingMaterials++;return;}foreach(var m in renderer.sharedMaterials)if(!m||!m.shader)report.missingMaterials++;else if(ShaderUtil.ShaderHasError(m.shader))report.shaderErrors++;
             foreach(var v in f.sharedMesh.vertices)if(!float.IsFinite(v.x)||!float.IsFinite(v.y)||!float.IsFinite(v.z))throw new InvalidOperationException("Nonfinite mesh vertex");foreach(int index in f.sharedMesh.triangles)if(index<0||index>=f.sharedMesh.vertexCount)throw new InvalidOperationException("Invalid triangle index");meshes.Add(f.sharedMesh);if(scene){report.sceneRenderersIncludingInactiveCars++;report.sceneTrianglesIncludingInactiveCars+=f.sharedMesh.triangles.Length/3;}}
            foreach(var f in UnityEngine.Object.FindObjectsByType<MeshFilter>(FindObjectsInactive.Include,FindObjectsSortMode.None))Inspect(f,true);
            var game=UnityEngine.Object.FindFirstObjectByType<GameManager>();foreach(var d in game.Arcade.Definitions)foreach(var f in d.Prefab.GetComponentsInChildren<MeshFilter>(true))Inspect(f,false);
            foreach(string name in new[]{"Bedroom Set","Joyful room additions — decoration only","Joyful final room dressing","Window outdoor diorama"})report.decorativeColliders+=GameObject.Find(name).GetComponentsInChildren<Collider>(true).Length;
            report.uniqueMeshes=meshes.Count;File.WriteAllText(ReportPaths.Report("MODEL_VALIDATION-v07.json"),JsonUtility.ToJson(report,true));if(report.missingMeshes+report.missingMaterials+report.shaderErrors+report.decorativeColliders>0)throw new InvalidOperationException("Joyful model audit failed");
            if(game.GetComponent<ReviewOnline>().Configured)throw new InvalidOperationException("New joyful-r2 cannot use review-r1 backend");Debug.Log("Joyful asset audit passed; not physical device FPS evidence.");
        }
    }
}
