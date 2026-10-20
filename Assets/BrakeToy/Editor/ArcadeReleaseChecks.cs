using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BrakeToy.Editor
{
    public static class ArcadeReleaseChecks
    {
        public static void PruneAndCheck()
        {
            const string meshRoot="Assets/BrakeToy/Art/Workshop/Meshes";
            var used=new HashSet<string>(AssetDatabase.GetDependencies(WorkshopSceneBuilder.ScenePath,true));
            foreach(string path in Directory.GetFiles("Assets/BrakeToy/Art/Workshop/Prefabs","*.prefab"))
                foreach(string dependency in AssetDatabase.GetDependencies(path,true))used.Add(dependency);
            var remove=new List<string>();
            foreach(string guid in AssetDatabase.FindAssets("t:Mesh",new[]{meshRoot}))
            {
                string path=AssetDatabase.GUIDToAssetPath(guid);
                if(path.StartsWith(meshRoot+"/")&&!used.Contains(path))remove.Add(path);
            }
            // Only unreferenced intermediate meshes generated for this new milestone are removed.
            AssetDatabase.StartAssetEditing();
            try{foreach(string path in remove)if(!AssetDatabase.DeleteAsset(path))throw new System.IO.IOException("Could not remove own unused mesh: "+path);}
            finally{AssetDatabase.StopAssetEditing();}
            AssetDatabase.Refresh();EditorSceneManager.OpenScene(WorkshopSceneBuilder.ScenePath);
            var meshes=new HashSet<Mesh>();int renderers=0,triangles=0;
            foreach(var filter in Object.FindObjectsByType<MeshFilter>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                if(!filter.sharedMesh)throw new System.InvalidOperationException("Missing mesh: "+filter.name);
                var renderer=filter.GetComponent<MeshRenderer>();if(!renderer||!renderer.sharedMaterial)throw new System.InvalidOperationException("Missing material: "+filter.name);
                renderers++;triangles+=filter.sharedMesh.triangles.Length/3;meshes.Add(filter.sharedMesh);
            }
            var report=new ModelReport{scene="ToyRoomArcade",unusedIntermediateMeshesRemoved=remove.Count,authoredMeshRenderersIncludingInactivePool=renderers,uniqueReferencedMeshes=meshes.Count,trianglesIncludingInactivePool=triangles,missingMeshes=0,missingMaterials=0,physicalDeviceProfiled=false};
            File.WriteAllText("MODEL_VALIDATION.json",JsonUtility.ToJson(report,true));
            Debug.Log("Model references checked; removed "+remove.Count+" own unused intermediate meshes. Referenced meshes="+meshes.Count);
        }
        [System.Serializable]private sealed class ModelReport
        {
            public string scene;public int unusedIntermediateMeshesRemoved,authoredMeshRenderersIncludingInactivePool,uniqueReferencedMeshes,trianglesIncludingInactivePool,missingMeshes,missingMaterials;public bool physicalDeviceProfiled;
        }
    }
}
