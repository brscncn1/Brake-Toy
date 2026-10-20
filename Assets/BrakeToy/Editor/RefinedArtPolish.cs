using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object=UnityEngine.Object;

namespace BrakeToy.Editor
{
    public static class RefinedArtPolish
    {
        private const string Root="Assets/BrakeToy/Art/Refined/";
        public static void Apply()
        {
            EditorSceneManager.OpenScene(RefinedSceneBuilder.ScenePath);
            var wood=Mat("Wood");wood.SetColor("_BaseColor",new Color(1.48f,1.06f,.60f));wood.SetFloat("_Smoothness",.32f);EditorUtility.SetDirty(wood);
            var curtain=Mat("Curtain");curtain.SetColor("_BaseColor",new Color(.95f,.73f,.32f));EditorUtility.SetDirty(curtain);
            var game=Object.FindFirstObjectByType<GameManager>();
            game.Arcade.BuildCatalogPool = true;
            if(!GameObject.Find("Refined surface finish"))
            {
                foreach(var filter in game.Player.Visual.GetComponentsInChildren<MeshFilter>())
                {
                    if(!filter.name.StartsWith("car_body"))continue;
                    var mesh=filter.sharedMesh;var vertices=mesh.vertices;
                    for(int i=0;i<vertices.Length;i++)if(vertices[i].y>.46f)
                    {
                        float blend=Mathf.Clamp01((vertices[i].y-.46f)/.11f);
                        float x=vertices[i].x;vertices[i].x*=1-.055f*blend;
                        vertices[i].y-=.018f*blend*(x*x/(.27f*.27f));
                    }
                    mesh.vertices=vertices;mesh.RecalculateNormals();mesh.RecalculateTangents();mesh.RecalculateBounds();EditorUtility.SetDirty(mesh);
                    if(filter.GetComponent<Renderer>().sharedMaterial.name=="Enamel")OpenWheelArches(mesh);
                }
                PrefabUtility.SaveAsPrefabAsset(game.Player.gameObject,Root+"Prefabs/SparkyRefined.prefab");
                var details=new GameObject("Refined surface finish").transform;details.SetParent(GameObject.Find("Bedroom Set").transform,false);
                Plant(details);WindowHardware(details);RefinedSceneBuilder.CombineDetails(details);
            }
            var scene=UnityEngine.SceneManagement.SceneManager.GetActiveScene();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();
        }
        private static Material Mat(string name)=>AssetDatabase.LoadAssetAtPath<Material>(Root+"Materials/"+name+".mat");
        private static void WindowHardware(Transform parent)
        {
            foreach(float x in new[]{-7.02f,-6.58f})
                RefinedSceneBuilder.Box("Satin window latch",parent,new Vector3(x,6.9f,41.62f),new Vector3(.10f,.57f,.10f),Mat("Chrome"),.025f);
            // A fine recessed bead around the aperture adds depth without covering the view.
            foreach(float x in new[]{-10.65f,-2.95f})
                RefinedSceneBuilder.Box("Window frame bead",parent,new Vector3(x,7.25f,41.96f),new Vector3(.12f,8.3f,.18f),Mat("Ivory"),.03f);
        }
        private static void Plant(Transform parent)
        {
            var green=new Material(Shader.Find("Universal Render Pipeline/Lit")){name="Leaf green"};green.SetColor("_BaseColor",new Color(.19f,.37f,.12f));green.SetFloat("_Smoothness",.28f);AssetDatabase.CreateAsset(green,Root+"Materials/Leaf green.mat");
            var soil=new Material(green){name="Pot soil"};soil.SetColor("_BaseColor",new Color(.09f,.045f,.02f));AssetDatabase.CreateAsset(soil,Root+"Materials/Pot soil.mat");
            var pot=GameObject.CreatePrimitive(PrimitiveType.Cylinder);Object.DestroyImmediate(pot.GetComponent<Collider>());pot.name="Ceramic window planter";pot.transform.SetParent(parent,false);pot.transform.localPosition=new Vector3(-8.65f,3.88f,40.7f);pot.transform.localScale=new Vector3(.95f,.55f,.95f);pot.GetComponent<Renderer>().sharedMaterial=Mat("Ivory");
            var dirt=GameObject.CreatePrimitive(PrimitiveType.Cylinder);Object.DestroyImmediate(dirt.GetComponent<Collider>());dirt.name="Inset soil";dirt.transform.SetParent(parent,false);dirt.transform.localPosition=new Vector3(-8.65f,4.43f,40.7f);dirt.transform.localScale=new Vector3(.78f,.02f,.78f);dirt.GetComponent<Renderer>().sharedMaterial=soil;
            for(int i=0;i<9;i++)
            {
                float angle=i*2.39996f;var leaf=GameObject.CreatePrimitive(PrimitiveType.Sphere);Object.DestroyImmediate(leaf.GetComponent<Collider>());leaf.name="Curved plant leaf";leaf.transform.SetParent(parent,false);
                leaf.transform.localPosition=new Vector3(-8.65f+Mathf.Cos(angle)*.46f,4.76f+i%3*.25f,40.7f+Mathf.Sin(angle)*.36f);leaf.transform.localScale=new Vector3(.26f,1.12f,.10f);leaf.transform.localRotation=Quaternion.Euler(24*Mathf.Sin(angle),angle*Mathf.Rad2Deg,30*Mathf.Cos(angle));leaf.GetComponent<Renderer>().sharedMaterial=green;
            }
        }
        private static void OpenWheelArches(Mesh combined)
        {
            // The first 66 vertices / 288 indices are the original six-station pressed body loft.
            // Replace that closed slab with a loft whose lower edge follows both wheel circles.
            var old=combined.vertices;var normals=combined.normals;var uv=combined.uv;var indices=combined.triangles;
            var rest=new Mesh();rest.vertices=old.Skip(66).ToArray();rest.normals=normals.Skip(66).ToArray();rest.uv=uv.Skip(66).ToArray();rest.triangles=indices.Skip(288).Select(i=>i-66).ToArray();
            var v=new List<Vector3>();var n=new List<Vector3>();var tex=new List<Vector2>();var tri=new List<int>();
            float[] zStops={-.65f,-.56f,-.25f,.25f,.52f,.65f},width={.25f,.32f,.34f,.34f,.30f,.25f},height={.31f,.39f,.38f,.39f,.35f,.32f};
            const int sections=100;
            for(int i=0;i<=sections;i++)
            {
                float z=Mathf.Lerp(-.65f,.65f,i/(float)sections);int segment=0;while(segment<4&&z>zStops[segment+1])segment++;
                float t=Mathf.InverseLerp(zStops[segment],zStops[segment+1],z),w=Mathf.Lerp(width[segment],width[segment+1],t),h=Mathf.Lerp(height[segment],height[segment+1],t);if(h>.34f)h=.34f+(h-.34f)*.78f;
                float bottom=.14f;
                foreach(float center in new[]{-.43f,.42f})
                {float dz=Mathf.Abs(z-center);if(dz<.183f)bottom=Mathf.Max(bottom,.16f+Mathf.Sqrt(.183f*.183f-dz*dz));}
                float r=Mathf.Min(.022f,(h-bottom)*.25f);
                var ring=new[]{new Vector2(-w+r,bottom),new Vector2(w-r,bottom),new Vector2(w,bottom+r),new Vector2(w,h-r),new Vector2(w-r,h),new Vector2(-w+r,h),new Vector2(-w,h-r),new Vector2(-w,bottom+r)};
                for(int j=0;j<8;j++){v.Add(new Vector3(ring[j].x,ring[j].y,z));n.Add(j<=1?Vector3.down:j<=3?Vector3.right:j<=5?Vector3.up:Vector3.left);tex.Add(new Vector2(j/7f,i/(float)sections));}
                if(i<sections)for(int j=0;j<8;j++){int a=i*8+j,b=i*8+(j+1)%8,c=a+8,d=b+8;tri.AddRange(new[]{a,d,c,a,b,d});}
            }
            for(int end=0;end<2;end++)
            {
                int source=end==0?0:sections*8,first=v.Count;for(int j=0;j<8;j++){v.Add(v[source+j]);n.Add(end==0?Vector3.back:Vector3.forward);tex.Add(Vector2.one*.5f);}
                int center=v.Count;v.Add(new Vector3(0,(v[source].y+v[source+4].y)*.5f,v[source].z));n.Add(end==0?Vector3.back:Vector3.forward);tex.Add(Vector2.one*.5f);
                for(int j=0;j<8;j++)if(end==0)tri.AddRange(new[]{center,first+(j+1)%8,first+j});else tri.AddRange(new[]{center,first+j,first+(j+1)%8});
            }
            var body=new Mesh();body.SetVertices(v);body.SetNormals(n);body.SetUVs(0,tex);body.SetTriangles(tri,0);
            combined.Clear();combined.CombineMeshes(new[]{new CombineInstance{mesh=body,transform=Matrix4x4.identity},new CombineInstance{mesh=rest,transform=Matrix4x4.identity}},true,true);combined.RecalculateBounds();combined.RecalculateTangents();EditorUtility.SetDirty(combined);Object.DestroyImmediate(body);Object.DestroyImmediate(rest);
        }
    }
}
