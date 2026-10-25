using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace BrakeToy.Editor
{
    public static class AdventureValidation
    {
        [Serializable]private sealed class Report
        {public string scene="ToyRoomAdventure",version="0.9.0";public int toyFamilies,pooledSlotsPerFamily,templateRenderers,templateTriangles,uniqueMeshes,missingMeshes,missingMaterials,shaderErrors,colliders;public bool physicalDeviceProfiled;public string note="Template counts include dynamic pivots; scene/pooled inactive geometry is not runtime draw-call or FPS evidence.";}
        public static void CheckAndBuild(){Check();AndroidPrototypeBuild.BuildProfile(AndroidBuildProfile.Adventure);}
        public static void Check()
        {
            EditorSceneManager.OpenScene(AdventureSceneBuilder.ScenePath);var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();var a=g.GetComponent<AdventureEncounters>();var report=new Report{toyFamilies=a.Templates.Length,pooledSlotsPerFamily=a.SlotsPerKind};var meshes=new HashSet<Mesh>();
            void Inspect(MeshFilter f,bool template)
            {
                if(!f.sharedMesh){report.missingMeshes++;return;}var r=f.GetComponent<Renderer>();if(!r){report.missingMaterials++;return;}foreach(var m in r.sharedMaterials)if(!m||!m.shader)report.missingMaterials++;else if(ShaderUtil.ShaderHasError(m.shader))report.shaderErrors++;
                foreach(var v in f.sharedMesh.vertices)if(!float.IsFinite(v.x)||!float.IsFinite(v.y)||!float.IsFinite(v.z))throw new InvalidOperationException("Nonfinite vertex");foreach(int index in f.sharedMesh.triangles)if(index<0||index>=f.sharedMesh.vertexCount)throw new InvalidOperationException("Bad index");meshes.Add(f.sharedMesh);if(template){report.templateRenderers++;report.templateTriangles+=f.sharedMesh.triangles.Length/3;}
            }
            for(int i=0;i<a.Templates.Length;i++){var t=a.Templates[i];if((int)t.Kind!=i||GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)>0)throw new InvalidOperationException("Bad toy catalog");foreach(var f in t.GetComponentsInChildren<MeshFilter>(true))Inspect(f,true);report.colliders+=t.GetComponentsInChildren<Collider>(true).Length;foreach(var s in t.Solids)if(s.Half.x<=0||s.Half.y<=0||s.Half.z<=0)throw new InvalidOperationException("Bad collision extent");}
            foreach(var f in UnityEngine.Object.FindObjectsByType<MeshFilter>(FindObjectsInactive.Include,FindObjectsSortMode.None))Inspect(f,false);foreach(var d in g.Arcade.Definitions)foreach(var f in d.Prefab.GetComponentsInChildren<MeshFilter>(true))Inspect(f,false);
            report.uniqueMeshes=meshes.Count;File.WriteAllText(ReportPaths.Report("MODEL_VALIDATION-v09.json"),JsonUtility.ToJson(report,true));if(report.toyFamilies!=8||report.missingMeshes+report.missingMaterials+report.shaderErrors+report.colliders>0)throw new InvalidOperationException("Adventure audit failed");if(g.GetComponent<ReviewOnline>().Configured)throw new InvalidOperationException("Adventure online leaderboard must remain gated");Debug.Log("Adventure model audit passed.");
        }
    }
}
