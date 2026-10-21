using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;

namespace BrakeToy.Editor
{
    internal static class LivingMeshFactory
    {
        private const string Root="Assets/BrakeToy/Art/Living/Meshes/";
        public static GameObject MeshObject(string name,Transform parent,Mesh mesh,Material mat,Vector3 position)
        {
            var obj=new GameObject(name,typeof(MeshFilter),typeof(MeshRenderer));obj.transform.SetParent(parent,false);obj.transform.localPosition=position;
            obj.GetComponent<MeshFilter>().sharedMesh=mesh;obj.GetComponent<MeshRenderer>().sharedMaterial=mat;return obj;
        }
        public static void Box(string name,Transform parent,Vector3 p,Vector3 size,Material mat,float radius=.04f)=>RefinedSceneBuilder.Box(name,parent,p,size,mat,radius);
        public static GameObject Sphere(string name,Transform parent,Vector3 p,Vector3 size,Material mat)
        {var o=GameObject.CreatePrimitive(PrimitiveType.Sphere);Object.DestroyImmediate(o.GetComponent<Collider>());o.name=name;o.transform.SetParent(parent,false);o.transform.localPosition=p;o.transform.localScale=size;o.GetComponent<Renderer>().sharedMaterial=mat;return o;}
        public static Mesh Grid(int columns,int rows,Func<float,float,Vector3> point,Vector2 tiling)
        {
            var verts=new Vector3[(columns+1)*(rows+1)];var uv=new Vector2[verts.Length];var tri=new int[columns*rows*6];int k=0;
            for(int y=0;y<=rows;y++)for(int x=0;x<=columns;x++){int i=y*(columns+1)+x;float u=x/(float)columns,v=y/(float)rows;verts[i]=point(u,v);uv[i]=new Vector2(u*tiling.x,v*tiling.y);}
            for(int y=0;y<rows;y++)for(int x=0;x<columns;x++){int a=y*(columns+1)+x,b=a+1,c=a+columns+1,d=c+1;tri[k++]=a;tri[k++]=c;tri[k++]=b;tri[k++]=b;tri[k++]=c;tri[k++]=d;}
            var mesh=new Mesh();mesh.vertices=verts;mesh.uv=uv;mesh.triangles=tri;mesh.RecalculateNormals();mesh.RecalculateBounds();mesh.RecalculateTangents();return mesh;
        }
        public static void Tube(string name,Transform parent,IList<Vector3> path,float radius,Material mat,int sides=10)
        {
            var v=new List<Vector3>();var n=new List<Vector3>();var uv=new List<Vector2>();var t=new List<int>();
            for(int i=0;i<path.Count;i++)
            {
                var tangent=(path[Mathf.Min(i+1,path.Count-1)]-path[Mathf.Max(0,i-1)]).normalized;
                var right=Vector3.Cross(tangent,Mathf.Abs(tangent.y)<.9f?Vector3.up:Vector3.forward).normalized;var up=Vector3.Cross(right,tangent).normalized;
                for(int j=0;j<=sides;j++){float a=j*Mathf.PI*2/sides;var normal=right*Mathf.Cos(a)+up*Mathf.Sin(a);v.Add(path[i]+normal*radius);n.Add(normal);uv.Add(new Vector2(j/(float)sides,i/(float)(path.Count-1)));if(i<path.Count-1&&j<sides){int c=i*(sides+1)+j,d=c+sides+1;t.AddRange(new[]{c,d,c+1,c+1,d,d+1});}}
            }
            var mesh=new Mesh();mesh.SetVertices(v);mesh.SetNormals(n);mesh.SetUVs(0,uv);mesh.SetTriangles(t,0);mesh.RecalculateBounds();mesh.RecalculateTangents();MeshObject(name,parent,mesh,mat,Vector3.zero);
        }
        public static void Lathe(string name,Transform parent,Vector3 position,Vector2[] profile,Material mat,int segments=48)
        {
            var v=new List<Vector3>();var uv=new List<Vector2>();var tri=new List<int>();
            for(int ring=0;ring<profile.Length;ring++)for(int s=0;s<=segments;s++)
            {
                float a=s*Mathf.PI*2/segments;v.Add(new Vector3(Mathf.Cos(a)*profile[ring].x,profile[ring].y,Mathf.Sin(a)*profile[ring].x));uv.Add(new Vector2(s/(float)segments,profile[ring].y));
                if(ring<profile.Length-1&&s<segments){int c=ring*(segments+1)+s,d=c+segments+1;tri.AddRange(new[]{c,d,c+1,c+1,d,d+1});}
            }
            var mesh=new Mesh();mesh.SetVertices(v);mesh.SetUVs(0,uv);mesh.SetTriangles(tri,0);mesh.RecalculateNormals();mesh.RecalculateBounds();mesh.RecalculateTangents();MeshObject(name,parent,mesh,mat,position);
        }
        public static void Combine(Transform root,string prefix,Transform[] preserve=null)
        {
            var filters=root.GetComponentsInChildren<MeshFilter>(true).Where(f=>preserve==null||!preserve.Any(p=>f.transform.IsChildOf(p))).ToArray();int part=0;
            foreach(var group in filters.GroupBy(f=>f.GetComponent<Renderer>().sharedMaterial))
            {
                var mesh=new Mesh{indexFormat=IndexFormat.UInt32,name=prefix+" "+group.Key.name};mesh.CombineMeshes(group.Select(f=>new CombineInstance{mesh=f.sharedMesh,transform=root.worldToLocalMatrix*f.transform.localToWorldMatrix}).ToArray(),true,true);mesh.RecalculateBounds();
                string path=AssetDatabase.GenerateUniqueAssetPath(Root+prefix+"_"+part+++".asset");AssetDatabase.CreateAsset(mesh,path);MeshObject(prefix+" • "+group.Key.name,root,mesh,group.Key,Vector3.zero);
            }
            foreach(var f in filters){if(!EditorUtility.IsPersistent(f.sharedMesh))Object.DestroyImmediate(f.sharedMesh);Object.DestroyImmediate(f.gameObject);}
        }
    }
}
