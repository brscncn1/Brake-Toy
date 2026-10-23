using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    public static class AdventurePolish
    {
        private static int part;
        public static void Apply()
        {
            for(int kind=0;kind<8;kind++)
            {
                string path=AdventureSceneBuilder.Root+"Prefabs/"+(Core.TrackToy)kind+".prefab";var root=PrefabUtility.LoadPrefabContents(path);
                try
                {
                    var a=root.GetComponent<AdventureToy>();if(kind==0&&(a.ExitArrows==null||a.ExitArrows.Length==0))a.ExitArrows=root.GetComponentsInChildren<MeshRenderer>().Where(r=>r.name=="Printed route arrow").Select(r=>r.transform).OrderBy(t=>t.localPosition.x).ToArray();
                    if(a.Signal&&!a.ReadySignal){a.ReadySignal=Object.Instantiate(a.Signal,a.Signal.parent);a.ReadySignal.name="Green ready signal";foreach(var r in a.ReadySignal.GetComponentsInChildren<Renderer>())r.sharedMaterial=AssetDatabase.LoadAssetAtPath<Material>(AdventureSceneBuilder.Root+"Materials/Mint plastic.mat");}
                    var preserve=new List<Transform>(a.Moving);if(a.Options!=null)preserve.AddRange(a.Options);if(a.ExitArrows!=null)preserve.AddRange(a.ExitArrows);foreach(var t in new[]{a.Signal,a.ReadySignal,a.Link,a.Shadow})if(t)preserve.Add(t);
                    foreach(var t in preserve)Bake(t,preserve.Where(p=>p!=t&&p.IsChildOf(t)).ToArray());Bake(root.transform,preserve.ToArray());PrefabUtility.SaveAsPrefabAsset(root,path);
                }
                finally{PrefabUtility.UnloadPrefabContents(root);}
            }
            AssetDatabase.SaveAssets();Debug.Log("Adventure static meshes batched per material; dynamic pivots and exit arrows preserved.");
        }
        public static void FinishFrogLegs()
        {
            string path=AdventureSceneBuilder.Root+"Prefabs/Windup.prefab";var root=PrefabUtility.LoadPrefabContents(path);try{var frog=root.GetComponent<AdventureToy>().Options[1];var mat=AssetDatabase.LoadAssetAtPath<Material>(AdventureSceneBuilder.Root+"Materials/Mint plastic.mat");foreach(float side in new[]{-1f,1f})LivingMeshFactory.Box("Frog foreleg",frog,new Vector3(side*.28f,.18f,-.22f),new Vector3(.14f,.30f,.20f),mat,.035f);Bake(frog,System.Array.Empty<Transform>());PrefabUtility.SaveAsPrefabAsset(root,path);}finally{PrefabUtility.UnloadPrefabContents(root);}AssetDatabase.SaveAssets();AdventureValidation.Check();
        }
        private static void Bake(Transform root,Transform[] preserve)
        {
            var filters=root.GetComponentsInChildren<MeshFilter>(true).Where(f=>!preserve.Any(p=>f.transform.IsChildOf(p))).ToArray();
            foreach(var group in filters.GroupBy(f=>f.GetComponent<Renderer>().sharedMaterial))
            {
                var mesh=new Mesh{indexFormat=IndexFormat.UInt32,name="Adventure batched "+group.Key.name};mesh.CombineMeshes(group.Select(f=>new CombineInstance{mesh=f.sharedMesh,transform=root.worldToLocalMatrix*f.transform.localToWorldMatrix}).ToArray(),true,true);mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,AssetDatabase.GenerateUniqueAssetPath(AdventureSceneBuilder.Root+"Meshes/Batched_"+part+++".asset"));LivingMeshFactory.MeshObject("Batched "+group.Key.name,root,mesh,group.Key,Vector3.zero);
            }
            // MeshFilter may live on a functional pivot (e.g. exit arrow): keep that transform.
            foreach(var f in filters)if(f.transform==root){Object.DestroyImmediate(f.GetComponent<Renderer>());Object.DestroyImmediate(f);}else Object.DestroyImmediate(f.gameObject);
        }
    }
}
