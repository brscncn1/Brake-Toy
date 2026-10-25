using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace BrakeToy.Editor
{
    public static class ReviewReleaseChecks
    {
        [Serializable]private sealed class Report
        {
            public string scene="ToyRoomReview",version="0.6.0";
            public int authoredRenderersIncludingInactiveCarsAndPool,uniqueReferencedMeshes,trianglesIncludingInactiveCarsAndPool,missingMeshes,missingMaterials,shaderErrors,decorativeColliders;
            public bool physicalDeviceProfiled;
        }
        public static void CheckAndBuild(){Check();AndroidPrototypeBuild.Build();}
        public static void Check()
        {
            EditorSceneManager.OpenScene(ReviewSceneBuilder.ScenePath);var report=new Report();var meshes=new HashSet<Mesh>();
            foreach(var filter in UnityEngine.Object.FindObjectsByType<MeshFilter>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                var renderer=filter.GetComponent<MeshRenderer>();if(!filter.sharedMesh){report.missingMeshes++;continue;}if(!renderer||!renderer.sharedMaterial){report.missingMaterials++;continue;}
                report.authoredRenderersIncludingInactiveCarsAndPool++;report.trianglesIncludingInactiveCarsAndPool+=filter.sharedMesh.triangles.Length/3;meshes.Add(filter.sharedMesh);
                foreach(var material in renderer.sharedMaterials)if(!material||!material.shader||ShaderUtil.ShaderHasError(material.shader))report.shaderErrors++;
                foreach(var v in filter.sharedMesh.vertices)if(!float.IsFinite(v.x)||!float.IsFinite(v.y)||!float.IsFinite(v.z))throw new InvalidOperationException("Nonfinite model vertex.");
                foreach(int index in filter.sharedMesh.triangles)if(index<0||index>=filter.sharedMesh.vertexCount)throw new InvalidOperationException("Invalid model index.");
            }
            report.uniqueReferencedMeshes=meshes.Count;report.decorativeColliders=GameObject.Find("Bedroom Set").GetComponentsInChildren<Collider>(true).Length;
            File.WriteAllText(ReportPaths.Report("MODEL_VALIDATION-review.json"),JsonUtility.ToJson(report,true));
            if(report.missingMeshes+report.missingMaterials+report.shaderErrors+report.decorativeColliders>0)throw new InvalidOperationException("Review model audit failed.");
            var online=UnityEngine.Object.FindFirstObjectByType<ReviewOnline>();if(online.Configured)throw new InvalidOperationException("Review artifact must ship offline until eligibility/production acceptance.");
            Debug.Log("Review asset audit passed. Counts include all ten inactive vehicle variants, not per-frame draws or device FPS.");
        }
    }
}
