using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object=UnityEngine.Object;

namespace BrakeToy.Editor
{
    public static class RefinedSurfaceFinish
    {
        private const string Root="Assets/BrakeToy/Art/Refined/Meshes/";
        public static void Apply()
        {
            var scene=EditorSceneManager.OpenScene(RefinedSceneBuilder.ScenePath);
            var game=Object.FindFirstObjectByType<GameManager>();
            string path=Root+"ContinuousTabletop.asset";
            var mesh=AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if(!mesh)
            {
                mesh=new Mesh{name="Continuous tabletop extrusion"};var v=new List<Vector3>();var n=new List<Vector3>();var uv=new List<Vector2>();var tri=new List<int>();
                var ring=new[]{new Vector2(-5.985f,-.16f),new Vector2(5.985f,-.16f),new Vector2(6,-.145f),new Vector2(6,.145f),new Vector2(5.985f,.16f),new Vector2(-5.985f,.16f),new Vector2(-6,.145f),new Vector2(-6,-.145f)};
                for(int edge=0;edge<8;edge++)
                {
                    var a=ring[edge];var b=ring[(edge+1)%8];var normal=new Vector3(b.y-a.y,a.x-b.x,0).normalized;int first=v.Count;
                    foreach(float z in new[]{-9f,9f})foreach(var p in new[]{a,b}){v.Add(new Vector3(p.x,p.y,z));n.Add(normal);uv.Add(new Vector2(Mathf.Abs(normal.y)>.5f?p.x/12:p.y/.32f,z/18));}
                    tri.AddRange(new[]{first,first+1,first+2,first+1,first+3,first+2});
                }
                mesh.SetVertices(v);mesh.SetNormals(n);mesh.SetUVs(0,uv);mesh.SetTriangles(tri,0);mesh.RecalculateBounds();mesh.RecalculateTangents();AssetDatabase.CreateAsset(mesh,path);
            }
            foreach(var chunk in game.World.FloorChunks)chunk.Find("Table surface").GetComponent<MeshFilter>().sharedMesh=mesh;
            var wall=GameObject.Find("room • Wall");path=Root+"SeamlessWall.asset";
            if(wall && !AssetDatabase.LoadAssetAtPath<Mesh>(path))
            {
                var original=wall.GetComponent<MeshFilter>().sharedMesh;var oldV=original.vertices;var oldN=original.normals;var oldUv=original.uv;var oldT=original.triangles;
                var v=new List<Vector3>();var n=new List<Vector3>();var uv=new List<Vector2>();var tri=new List<int>();
                for(int i=0;i<oldT.Length;i+=3)
                {
                    if(oldV[oldT[i]].z>42.6f&&oldV[oldT[i+1]].z>42.6f&&oldV[oldT[i+2]].z>42.6f)continue;
                    for(int j=0;j<3;j++){int k=oldT[i+j];tri.Add(v.Count);v.Add(oldV[k]);n.Add(oldN[k]);uv.Add(oldUv[k]);}
                }
                foreach(var rect in new[]{new Rect(-20,-7,9.3f,27),new Rect(-2.9f,-7,22.9f,27),new Rect(-10.7f,-7,7.8f,10),new Rect(-10.7f,11.4f,7.8f,8.6f)})
                {
                    int first=v.Count;v.AddRange(new[]{new Vector3(rect.xMin,rect.yMin,42.7f),new Vector3(rect.xMax,rect.yMin,42.7f),new Vector3(rect.xMin,rect.yMax,42.7f),new Vector3(rect.xMax,rect.yMax,42.7f)});
                    for(int i=0;i<4;i++){n.Add(Vector3.back);uv.Add(new Vector2(v[first+i].x/10,v[first+i].y/10));}
                    tri.AddRange(new[]{first,first+2,first+1,first+1,first+2,first+3});
                }
                var replacement=new Mesh{indexFormat=UnityEngine.Rendering.IndexFormat.UInt32,name="Continuous wall around window aperture"};replacement.SetVertices(v);replacement.SetNormals(n);replacement.SetUVs(0,uv);replacement.SetTriangles(tri,0);replacement.RecalculateBounds();replacement.RecalculateTangents();AssetDatabase.CreateAsset(replacement,path);wall.GetComponent<MeshFilter>().sharedMesh=replacement;
            }
            else if(wall)wall.GetComponent<MeshFilter>().sharedMesh=AssetDatabase.LoadAssetAtPath<Mesh>(path);
            EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();
        }
    }
}
