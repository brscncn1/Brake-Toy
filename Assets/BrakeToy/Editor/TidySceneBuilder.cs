using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    public static class TidySceneBuilder
    {
        public const string ScenePath="Assets/BrakeToy/Scenes/ToyRoomTidy.unity",Root="Assets/BrakeToy/Art/Tidy/";
        [MenuItem("Tools/Brake Toy/Open Tidy Room v0.8.1")]public static void Open(){if(!File.Exists(ScenePath))Create();else EditorSceneManager.OpenScene(ScenePath);}
        public static void Create()
        {
            if(File.Exists(ScenePath)){EditorSceneManager.OpenScene(ScenePath);if(Object.FindFirstObjectByType<TidyRoom>())throw new InvalidOperationException("Preserve existing tidy scene");}Directory.CreateDirectory(Root);AssetDatabase.Refresh();var scene=EditorSceneManager.OpenScene(PlaygroundSceneBuilder.ScenePath);EditorSceneManager.SaveScene(scene,ScenePath);var game=Object.FindFirstObjectByType<GameManager>();var tidy=game.gameObject.AddComponent<TidyRoom>();game.name="Brake Toy — Tidy Room v0.8.1";
            foreach(var p in game.GetComponent<PlaygroundEncounters>().Props){foreach(var d in p.Dominoes)if(d)Object.DestroyImmediate(d.gameObject);p.Dominoes=Array.Empty<Transform>();}game.GetComponent<PlaygroundRun>().DominoEnabled=false;
            var near=Object.FindFirstObjectByType<TabletopDecorScroller>();near.enabled=false;tidy.NearProps=near.Targets;tidy.PropStarts=new Vector3[near.Targets.Length];for(int i=0;i<near.Targets.Length;i++){var t=near.Targets[i];var b=TidyRoom.BoundsOf(t);float factor=Mathf.Min(1,1.25f/b.size.x);t.localScale*=factor;b=TidyRoom.BoundsOf(t);var target=new Vector3(t.name.Contains("notebook",StringComparison.OrdinalIgnoreCase)?-4.45f:4.45f,0,t.name.Contains("notebook",StringComparison.OrdinalIgnoreCase)?14:18);t.position+=new Vector3(target.x-b.center.x,.055f-b.min.y,target.z-b.center.z);tidy.PropStarts[i]=t.position;}
            var cells=new List<Transform>();var starts=new List<Vector3>();int[] slots={0,8,24,32,40,48,56};foreach(var zone in game.GetComponent<PlaygroundAtmosphere>().Zones)
            {var children=zone.transform.Cast<Transform>().ToArray();foreach(int side in new[]{-1,1}){var groups=children.Where(t=>t.parent==zone.transform&&Mathf.Sign(t.localPosition.x)==side).GroupBy(t=>Mathf.RoundToInt(t.localPosition.z)).OrderBy(g=>g.Key).ToArray();if(groups.Length!=7)throw new InvalidOperationException("Expected seven complete dressing cells per side");for(int i=0;i<groups.Length;i++){var cell=new GameObject("Reserved edge cell "+side+" "+i).transform;cell.SetParent(zone.transform,false);cell.localPosition=new Vector3(side*4.75f,0,groups[i].Key);foreach(var child in groups[i])child.SetParent(cell,true);cell.localPosition=new Vector3(side*4.6f,.04f,slots[i]);cells.Add(cell);starts.Add(cell.position);}}}
            tidy.Cells=cells.ToArray();tidy.CellStarts=starts.ToArray();MoveRadio(tidy);tidy.Apply(0);
            PlayerSettings.bundleVersion="0.8.1";PlayerSettings.Android.bundleVersionCode=9;EditorBuildSettings.scenes=new[]{new EditorBuildSettingsScene(ScenePath,true)}.Concat(EditorBuildSettings.scenes.Where(s=>s.path!=ScenePath)).ToArray();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();Debug.Log("Tidy scene generated: domino removed, shared edge layout and radio shelf; difficulty unchanged.");
        }
        private static bool RadioVertex(Vector3 p)=>p.x> -6.2f&&p.x< -2.8f&&p.y>0&&p.y<3&&p.z>22.5f&&p.z<25.5f;
        private static void MoveRadio(TidyRoom tidy)
        {
            var root=new GameObject("Happy FM radio — on shelf").transform;root.position=new Vector3(-10.2f,0,27);tidy.Radio=root;int count=0;
            foreach(var f in GameObject.Find("Joyful room additions — decoration only").GetComponentsInChildren<MeshFilter>())
            {
                var mesh=f.sharedMesh;var v=mesh.vertices;var tri=mesh.triangles;var take=new List<int>();var keep=new List<int>();for(int i=0;i<tri.Length;i+=3){bool chosen=RadioVertex(f.transform.TransformPoint(v[tri[i]]))&&RadioVertex(f.transform.TransformPoint(v[tri[i+1]]))&&RadioVertex(f.transform.TransformPoint(v[tri[i+2]]));var target=chosen?take:keep;target.Add(tri[i]);target.Add(tri[i+1]);target.Add(tri[i+2]);}if(take.Count==0)continue;
                var moved=Compact(mesh,take,index=>(f.transform.TransformPoint(v[index])-new Vector3(-4.5f,.03f,24))*1.35f);AssetDatabase.CreateAsset(moved,AssetDatabase.GenerateUniqueAssetPath(Root+"ShelfRadio.asset"));LivingMeshFactory.MeshObject("Shelf radio part "+count++,root,moved,f.GetComponent<Renderer>().sharedMaterial,Vector3.zero);
                var remainder=Compact(mesh,keep,index=>v[index]);AssetDatabase.CreateAsset(remainder,AssetDatabase.GenerateUniqueAssetPath(Root+"RoomWithoutTableRadio.asset"));f.sharedMesh=remainder;
            }
            if(count<3)throw new InvalidOperationException("Radio extraction incomplete");float top=float.NegativeInfinity;foreach(var f in GameObject.Find("Bedroom Set").GetComponentsInChildren<MeshFilter>())foreach(var v in f.sharedMesh.vertices){var p=f.transform.TransformPoint(v);if(p.x> -13.1f&&p.x< -7.2f&&p.y>2.85f&&p.y<3.2f&&p.z>25.4f&&p.z<28.6f)top=Mathf.Max(top,p.y);}if(!float.IsFinite(top))throw new InvalidOperationException("Shelf support surface missing");tidy.ShelfTop=top;var bounds=TidyRoom.BoundsOf(root);root.position+=Vector3.up*(top+.025f-bounds.min.y);var support=new GameObject("Radio shelf support reference").transform;support.position=new Vector3(-10.2f,top,27);tidy.Shelf=support;
        }
        private static Mesh Compact(Mesh source,List<int> indices,Func<int,Vector3> point)
        {var oldUV=source.uv;var oldNormals=source.normals;var map=new Dictionary<int,int>();var v=new List<Vector3>();var uv=new List<Vector2>();var normals=new List<Vector3>();var triangles=new int[indices.Count];for(int i=0;i<indices.Count;i++){int old=indices[i];if(!map.TryGetValue(old,out int n)){n=v.Count;map[old]=n;v.Add(point(old));uv.Add(oldUV.Length>old?oldUV[old]:Vector2.zero);normals.Add(oldNormals.Length>old?oldNormals[old]:Vector3.up);}triangles[i]=n;}var mesh=new Mesh{indexFormat=IndexFormat.UInt32};mesh.SetVertices(v);mesh.SetUVs(0,uv);mesh.SetNormals(normals);mesh.triangles=triangles;mesh.RecalculateBounds();mesh.RecalculateTangents();return mesh;}
    }
}
