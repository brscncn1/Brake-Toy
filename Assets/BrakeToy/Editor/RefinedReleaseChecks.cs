using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BrakeToy.Editor
{
    public static class RefinedReleaseChecks
    {
        [Serializable] private sealed class Report
        {
            public string scene="ToyRoomRefined",version="0.4.0";
            public int authoredMeshRenderersIncludingInactivePool,uniqueReferencedMeshes,trianglesIncludingInactivePool,missingMeshes,missingMaterials,shaderErrors,decorativeColliders;
            public bool physicalDeviceProfiled;
        }
        public static void Check()
        {
            EditorSceneManager.OpenScene(RefinedSceneBuilder.ScenePath);var report=new Report();var meshes=new HashSet<Mesh>();
            foreach(var filter in UnityEngine.Object.FindObjectsByType<MeshFilter>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                var renderer=filter.GetComponent<MeshRenderer>();
                if(!filter.sharedMesh){report.missingMeshes++;continue;}if(!renderer||!renderer.sharedMaterial){report.missingMaterials++;continue;}
                report.authoredMeshRenderersIncludingInactivePool++;report.trianglesIncludingInactivePool+=filter.sharedMesh.triangles.Length/3;meshes.Add(filter.sharedMesh);
                foreach(var material in renderer.sharedMaterials)if(!material||!material.shader||ShaderUtil.ShaderHasError(material.shader))report.shaderErrors++;
                foreach(var v in filter.sharedMesh.vertices)if(!float.IsFinite(v.x)||!float.IsFinite(v.y)||!float.IsFinite(v.z))throw new InvalidOperationException("Nonfinite model vertex.");
                foreach(int i in filter.sharedMesh.triangles)if(i<0||i>=filter.sharedMesh.vertexCount)throw new InvalidOperationException("Invalid model index.");
            }
            report.uniqueReferencedMeshes=meshes.Count;report.decorativeColliders=GameObject.Find("Bedroom Set").GetComponentsInChildren<Collider>(true).Length;
            File.WriteAllText("MODEL_VALIDATION.json",JsonUtility.ToJson(report,true));
            if(report.missingMeshes+report.missingMaterials+report.shaderErrors+report.decorativeColliders>0)throw new InvalidOperationException("Refined model audit failed.");
            Debug.Log("Refined model audit passed; unique meshes="+meshes.Count+". This is not a physical GPU profile.");
        }
    }
}
