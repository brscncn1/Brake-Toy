using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BrakeToy.Editor
{
    /// <summary>Original die-cast toy meshes and a separate arcade milestone; v0.1/v0.2 assets are preserved.</summary>
    public static class WorkshopSceneBuilder
    {
        public const string ScenePath = "Assets/BrakeToy/Scenes/ToyRoomArcade.unity";
        private const string Root = "Assets/BrakeToy/Art/Workshop/";
        private static readonly Dictionary<string, Material> mats = new Dictionary<string, Material>();
        private static int meshId;
        [MenuItem("Tools/Brake Toy/Open Arcade Workshop v0.3")]
        public static void Open() { if (!File.Exists(ScenePath)) Create(); else EditorSceneManager.OpenScene(ScenePath); }
        public static void Refine()
        {
            EditorSceneManager.OpenScene(ScenePath);
            // Correct the independently rolling model pivot to the ball centre, preserving root XZ collision.
            var director=Object.FindFirstObjectByType<ArcadeDirector>();
            foreach(var h in director.Pool) if(h.RollingBall && h.Model.localPosition.y < .1f)
            {
                foreach(Transform child in h.Model) child.localPosition -= Vector3.up*.51f;
                h.Model.localPosition=Vector3.up*.51f;
            }
            // Rebuild the car in this generated scene using corrected source geometry.
            mats.Clear();foreach(string guid in AssetDatabase.FindAssets("t:Material",new[]{Root+"Materials"})){var m=AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(guid));mats[m.name]=m;}
            meshId=1000;ReplaceCar(Object.FindFirstObjectByType<GameManager>());
            EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());EditorSceneManager.SaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());AssetDatabase.SaveAssets();
        }
        public static void Polish()
        {
            EditorSceneManager.OpenScene(ScenePath);
            mats.Clear();foreach(string guid in AssetDatabase.FindAssets("t:Material",new[]{Root+"Materials"})){var m=AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(guid));mats[m.name]=m;}
            meshId=2000;
            var window=GameObject.Find("Window and curtains").transform;window.localPosition=new Vector3(-6.8f,7.2f,42.5f);window.localScale=new Vector3(.72f,.72f,1);
            var poster=GameObject.Find("Space poster").transform;poster.localPosition=new Vector3(4,8,42.6f);poster.localScale=Vector3.one*.68f;
            mats["Wood"].SetColor("_BaseColor",new Color(1.65f,1.48f,1.22f));mats["Wood"].SetFloat("_Smoothness",.25f);EditorUtility.SetDirty(mats["Wood"]);
            // A real open-frustum shade replaces the solid placeholder cylinder.
            var shade=GameObject.Find("Lamp shade");if(shade)Object.DestroyImmediate(shade);
            var room=GameObject.Find("Bedroom Set").transform;
            var v=new List<Vector3>();var uv=new List<Vector2>();var tr=new List<int>();
            for(int ring=0;ring<4;ring++)for(int i=0;i<=48;i++)
            {
                float a=i*Mathf.PI*2/48,r=ring==0?1.55f:ring==1?.94f:ring==2?.87f:1.48f,y=ring==0||ring==3?0:1.9f;
                v.Add(new Vector3(Mathf.Cos(a)*r,y,Mathf.Sin(a)*r));uv.Add(new Vector2(i/48f,ring/3f));
                if(ring<3&&i<48){int c=ring*49+i,d=c+49;tr.AddRange(new[]{c,d,d+1,c,d+1,c+1});}
            }
            var mesh=new Mesh();mesh.SetVertices(v);mesh.SetUVs(0,uv);mesh.SetTriangles(tr,0);mesh.RecalculateNormals();MeshObject("Fabric lampshade",room,SaveMesh(mesh,"Lampshade"),"Curtain",new Vector3(6.9f,.2f,37));
            mats["Curtain"].SetFloat("_Cull",0);EditorUtility.SetDirty(mats["Curtain"]);
            // Fix front-facing cloth normals; duplicate reversed triangles cancel vertex normals.
            foreach(string guid in AssetDatabase.FindAssets("t:Mesh",new[]{Root+"Meshes"}))
            {
                var m=AssetDatabase.LoadAssetAtPath<Mesh>(AssetDatabase.GUIDToAssetPath(guid));if(m.name!="Curtain")continue;
                var original=m.triangles;if(original.Length!=384)continue;var single=new List<int>();for(int i=0;i<original.Length;i+=12)for(int j=0;j<6;j++)single.Add(original[i+j]);m.triangles=single.ToArray();m.RecalculateNormals();m.RecalculateTangents();EditorUtility.SetDirty(m);
            }
            var game=Object.FindFirstObjectByType<GameManager>();game.Config.CameraPitch=20;EditorUtility.SetDirty(game.Config);Camera.main.GetComponent<PortraitCamera>().Reframe();
            var ballPrefab=PrefabUtility.LoadPrefabContents(Root+"Prefabs/Hazard_3.prefab");var ball=ballPrefab.GetComponent<ArcadeHazard>();
            if(ball.Model.localPosition.y<.1f){foreach(Transform c in ball.Model)c.localPosition-=Vector3.up*.51f;ball.Model.localPosition=Vector3.up*.51f;}
            PrefabUtility.SaveAsPrefabAsset(ballPrefab,Root+"Prefabs/Hazard_3.prefab");PrefabUtility.UnloadPrefabContents(ballPrefab);
            EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());EditorSceneManager.SaveScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene());AssetDatabase.SaveAssets();
        }
        public static void FinishLighting()
        {
            EditorSceneManager.OpenScene(ScenePath);
            var wood=AssetDatabase.LoadAssetAtPath<Material>(Root+"Materials/Wood.mat");wood.SetColor("_BaseColor",new Color(1.15f,1.14f,1.06f));wood.SetFloat("_BumpScale",.065f);EditorUtility.SetDirty(wood);
            var pipe=AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(Root+"WorkshopPipeline.asset");pipe.shadowDistance=75;pipe.shadowCascadeCount=2;pipe.msaaSampleCount=4;pipe.mainLightShadowmapResolution=2048;EditorUtility.SetDirty(pipe);
            // A softer cool fill and warm directional light distinguish rubber, enamel and cloth.
            RenderSettings.sun.intensity=1.3f;RenderSettings.ambientSkyColor=new Color(.50f,.60f,.68f);RenderSettings.ambientEquatorColor=new Color(.45f,.48f,.48f);
            mats.Clear();foreach(string guid in AssetDatabase.FindAssets("t:Material",new[]{Root+"Materials"})){var m=AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(guid));mats[m.name]=m;}
            var wall=GameObject.Find("Back wall");
            if(wall)
            {
                var parent=wall.transform.parent;Object.DestroyImmediate(wall);meshId=3000;
                Box("Back wall left of window",parent,new Vector3(-15.35f,6.5f,43),new Vector3(9.3f,27,.5f),"Wall",.02f);
                Box("Back wall right of window",parent,new Vector3(8.55f,6.5f,43),new Vector3(22.9f,27,.5f),"Wall",.02f);
                Box("Back wall below window",parent,new Vector3(-6.8f,-2,43),new Vector3(7.8f,10,.5f),"Wall",.02f);
                Box("Back wall above window",parent,new Vector3(-6.8f,15.7f,43),new Vector3(7.8f,8.6f,.5f),"Wall",.02f);
            }
            var daylight=GameObject.Find("Daylight sky");if(daylight)daylight.GetComponent<Renderer>().shadowCastingMode=ShadowCastingMode.Off;
            RenderSettings.sun.transform.rotation=Quaternion.Euler(12,172,0);
            if(!GameObject.Find("Soft room bounce fill"))
            {
                var fill=new GameObject("Soft room bounce fill").AddComponent<Light>();fill.type=LightType.Directional;
                fill.transform.rotation=Quaternion.Euler(46,-30,0);fill.color=new Color(.82f,.91f,1);fill.intensity=.95f;fill.shadows=LightShadows.None;
            }
            var ambient=new SphericalHarmonicsL2();ambient.AddAmbientLight(new Color(.50f,.55f,.59f));RenderSettings.ambientProbe=ambient;
            pipe.shadowNormalBias=.15f;pipe.shadowDepthBias=.25f;EditorUtility.SetDirty(pipe);
            var scene=UnityEngine.SceneManagement.SceneManager.GetActiveScene();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();
        }
        public static void OptimizeRoom()
        {
            EditorSceneManager.OpenScene(ScenePath);meshId=4000;
            var room=GameObject.Find("Bedroom Set").transform;
            if(room.Find("room • Wood"))return;
            var decor=GameObject.Find("Table decorations — no collision").transform;
            Combine(room,"room",new[]{decor});
            foreach(var renderer in room.GetComponentsInChildren<MeshRenderer>())if(renderer.sharedMaterial.name=="Window daylight")renderer.shadowCastingMode=ShadowCastingMode.Off;
            var scene=UnityEngine.SceneManagement.SceneManager.GetActiveScene();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();
        }
        [MenuItem("Tools/Brake Toy/Create Arcade Workshop v0.3")]
        public static void Create()
        {
            if (File.Exists(ScenePath)) { Debug.Log("v0.3 scene preserved; already exists."); return; }
            if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            foreach (string folder in new[] { "Meshes", "Materials", "Textures", "Prefabs" }) Directory.CreateDirectory(Root + folder);
            AssetDatabase.Refresh(); meshId = 0; mats.Clear(); Palette();
            var scene = EditorSceneManager.OpenScene(TabletopSceneBuilder.ScenePath);
            EditorSceneManager.SaveScene(scene, ScenePath);
            var game = Object.FindFirstObjectByType<GameManager>();
            var config = Object.Instantiate(game.Config); config.name = "ArcadeConfig"; config.CameraPitch = 23; config.WorldSpeed = 10;
            AssetDatabase.CreateAsset(config, "Assets/BrakeToy/ScriptableObjects/ArcadeConfig.asset");
            game.name = "Brake Toy — Arcade Workshop v0.3"; game.Config = config;
            Camera.main.GetComponent<PortraitCamera>().Config = config;
            Camera.main.GetComponent<PortraitCamera>().Reframe();
            foreach (var old in game.World.Obstacles) Object.DestroyImmediate(old.gameObject);
            game.World.Obstacles = Array.Empty<ObstacleView>();
            ReplaceRoomMaterials();
            ReplaceCar(game);
            var director = game.gameObject.AddComponent<ArcadeDirector>(); game.Arcade = director;
            director.Sound = game.gameObject.AddComponent<ArcadeAudio>();
            var pool = new List<ArcadeHazard>();
            var poolRoot = Group("Encounter pool — 24 reusable hazards");
            for (int kind = 0; kind < 4; kind++)
            {
                var prototype = Hazard(kind);
                Combine(prototype.Model, "hazard_" + kind);
                string path = Root + "Prefabs/Hazard_" + kind + ".prefab";
                var prefab = PrefabUtility.SaveAsPrefabAsset(prototype.gameObject, path); Object.DestroyImmediate(prototype.gameObject);
                for (int i = 0; i < 6; i++)
                {
                    var item = (GameObject)PrefabUtility.InstantiatePrefab(prefab, poolRoot);
                    item.name = prefab.name + " [" + i + "]"; item.SetActive(false); pool.Add(item.GetComponent<ArcadeHazard>());
                }
            }
            director.Pool = pool.ToArray(); director.HUD = BuildHUD(game);
            LightAndRenderer();
            PlayerSettings.bundleVersion = "0.3.0"; PlayerSettings.Android.bundleVersionCode = 3;
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true), new EditorBuildSettingsScene(TabletopSceneBuilder.ScenePath, true), new EditorBuildSettingsScene("Assets/BrakeToy/Scenes/ToyRoom.unity", true) };
            EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene); AssetDatabase.SaveAssets();
            Debug.Log("Arcade Workshop generated: " + ScenePath);
            Polish(); FinishLighting(); OptimizeRoom();
        }
        private static Material Mat(string name, Color color, float smooth = .35f, float metal = 0)
        {
            var material = new Material(Shader.Find("Universal Render Pipeline/Lit")) { name = name };
            material.SetColor("_BaseColor", color); material.SetFloat("_Smoothness", smooth); material.SetFloat("_Metallic", metal);
            AssetDatabase.CreateAsset(material, Root + "Materials/" + name + ".mat"); mats[name] = material; return material;
        }
        private static void Palette()
        {
            Mat("Enamel", new Color(.91f,.13f,.045f), .73f, .42f);
            Mat("Ivory", new Color(.94f,.91f,.77f), .52f);
            Mat("Rubber", new Color(.022f,.026f,.030f), .20f);
            Mat("Glass", new Color(.045f,.105f,.15f), .86f, .32f);
            Mat("Chrome", new Color(.68f,.72f,.73f), .68f, .82f);
            Mat("Ink", new Color(.035f,.065f,.085f), .32f);
            Mat("Teal", new Color(.025f,.43f,.44f), .48f);
            Mat("Amber", new Color(.98f,.52f,.075f), .46f);
            Mat("Pink", new Color(.80f,.32f,.31f), .09f);
            Mat("Paper", new Color(.87f,.84f,.72f), .12f);
            Mat("RedLight", new Color(.65f,.015f,.01f), .64f);
            Mat("Blue", new Color(.13f,.28f,.46f), .28f);
            Mat("Wall", new Color(.26f,.39f,.43f), .02f);
            Mat("Curtain", new Color(.77f,.60f,.34f), .04f);
            Mat("Wood", Color.white, .34f);
            string diffuse = Root + "Textures/wood_table_001_diff_1k.jpg", normal = Root + "Textures/wood_table_001_nor_gl_1k.jpg";
            foreach (string path in new[] { diffuse, normal })
            {
                var importer = (TextureImporter)AssetImporter.GetAtPath(path);
                importer.maxTextureSize = 1024; importer.mipmapEnabled = true; importer.wrapMode = TextureWrapMode.Repeat;
                if (path == normal) importer.textureType = TextureImporterType.NormalMap;
                importer.SaveAndReimport();
            }
            mats["Wood"].SetTexture("_BaseMap", AssetDatabase.LoadAssetAtPath<Texture2D>(diffuse));
            mats["Wood"].SetTexture("_BumpMap", AssetDatabase.LoadAssetAtPath<Texture2D>(normal)); mats["Wood"].EnableKeyword("_NORMALMAP"); mats["Wood"].SetFloat("_BumpScale", .32f);
            mats["Wood"].SetTextureScale("_BaseMap", new Vector2(1, 1.5f)); mats["Wood"].SetTextureScale("_BumpMap", new Vector2(1, 1.5f));
        }
        private static Transform Group(string name, Transform parent = null)
        { var t = new GameObject(name).transform; t.SetParent(parent, false); return t; }
        private static GameObject MeshObject(string name, Transform parent, Mesh mesh, string mat, Vector3 position)
        {
            var obj = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer)); obj.transform.SetParent(parent, false); obj.transform.localPosition = position;
            obj.GetComponent<MeshFilter>().sharedMesh = mesh; obj.GetComponent<MeshRenderer>().sharedMaterial = mats[mat]; return obj;
        }
        private static Mesh SaveMesh(Mesh mesh, string name)
        {
            mesh.name = name; mesh.RecalculateBounds(); mesh.RecalculateTangents();
            AssetDatabase.CreateAsset(mesh, AssetDatabase.GenerateUniqueAssetPath(Root + "Meshes/" + name + "_" + meshId++ + ".asset")); return mesh;
        }
        private static GameObject Box(string name, Transform parent, Vector3 p, Vector3 size, string mat, float radius = .025f)
        {
            radius = Mathf.Min(radius, Mathf.Min(size.x, Mathf.Min(size.y,size.z)) * .35f);
            var vertices = new List<Vector3>(); var normals = new List<Vector3>(); var uv = new List<Vector2>(); var tris = new List<int>();
            Vector3 half = size * .5f, inner = half - Vector3.one * radius;
            foreach (var n in new[] {Vector3.right,Vector3.left,Vector3.up,Vector3.down,Vector3.forward,Vector3.back})
            {
                Vector3 u = Mathf.Abs(n.y) > .5f ? Vector3.right : Vector3.up, v = Vector3.Cross(n,u);
                float hu=Vector3.Scale(u,half).magnitude, hv=Vector3.Scale(v,half).magnitude;
                float[] cu={-hu,-hu+radius,0,hu-radius,hu}, cv={-hv,-hv+radius,0,hv-radius,hv}; int first=vertices.Count;
                for(int y=0;y<5;y++) for(int x=0;x<5;x++)
                {
                    Vector3 q=Vector3.Scale(n,half)+u*cu[x]+v*cv[y];
                    Vector3 c=new Vector3(Mathf.Clamp(q.x,-inner.x,inner.x),Mathf.Clamp(q.y,-inner.y,inner.y),Mathf.Clamp(q.z,-inner.z,inner.z));
                    Vector3 normal=(q-c).normalized; vertices.Add(c+normal*radius); normals.Add(normal);
                    // Physical face-space UVs avoid stretching grain across bevels.
                    uv.Add(new Vector2((cu[x]+hu)/(2*hu),(cv[y]+hv)/(2*hv)));
                }
                for(int y=0;y<4;y++) for(int x=0;x<4;x++) {int a=first+y*5+x,b=a+1,c=a+5,d=c+1; tris.AddRange(new[]{a,b,d,a,d,c});}
            }
            var mesh=new Mesh(); mesh.SetVertices(vertices);mesh.SetNormals(normals);mesh.SetUVs(0,uv);mesh.SetTriangles(tris,0);
            return MeshObject(name,parent,SaveMesh(mesh,name),mat,p);
        }
        // Circular cross sections for a tire, rim, ball seam or metal ring; Y is the axle.
        private static GameObject Torus(string name, Transform parent, Vector3 p, float major, float tube, string mat, int segments=40, int sides=10)
        {
            var verts=new List<Vector3>(); var norms=new List<Vector3>(); var uv=new List<Vector2>(); var tris=new List<int>();
            for(int i=0;i<=segments;i++) for(int j=0;j<=sides;j++)
            {
                float a=i*Mathf.PI*2/segments,b=j*Mathf.PI*2/sides;
                Vector3 n=new Vector3(Mathf.Cos(a)*Mathf.Cos(b),Mathf.Sin(b),Mathf.Sin(a)*Mathf.Cos(b));
                verts.Add(new Vector3(Mathf.Cos(a)*major,0,Mathf.Sin(a)*major)+n*tube); norms.Add(n); uv.Add(new Vector2(i/(float)segments,j/(float)sides));
                if(i<segments&&j<sides){int c=i*(sides+1)+j,d=c+sides+1;tris.AddRange(new[]{c,c+1,d+1,c,d+1,d});}
            }
            var m=new Mesh();m.SetVertices(verts);m.SetNormals(norms);m.SetUVs(0,uv);m.SetTriangles(tris,0);
            return MeshObject(name,parent,SaveMesh(m,name),mat,p);
        }
        private static GameObject Sphere(string name, Transform parent, Vector3 p, Vector3 scale, string mat)
        {
            var obj=GameObject.CreatePrimitive(PrimitiveType.Sphere);obj.name=name; Object.DestroyImmediate(obj.GetComponent<Collider>());
            obj.transform.SetParent(parent,false);obj.transform.localPosition=p;obj.transform.localScale=scale;obj.GetComponent<Renderer>().sharedMaterial=mats[mat];return obj;
        }
        private static GameObject Loft(string name, Transform parent, float[] z, float[] width, float[] height, float bottom, string mat)
        {
            var vertices=new List<Vector3>();var normals=new List<Vector3>();var uv=new List<Vector2>();var tris=new List<int>();
            for(int i=0;i<z.Length;i++)
            {
                float w=width[i],h=height[i],r=.035f;
                var ring=new[]{new Vector2(-w+r,bottom),new Vector2(w-r,bottom),new Vector2(w,bottom+r),new Vector2(w,h-r),new Vector2(w-r,h),new Vector2(-w+r,h),new Vector2(-w,h-r),new Vector2(-w,bottom+r)};
                int before=Mathf.Max(0,i-1),after=Mathf.Min(z.Length-1,i+1);
                float dw=(width[after]-width[before])/(z[after]-z[before]),dh=(height[after]-height[before])/(z[after]-z[before]);
                for(int j=0;j<8;j++)
                {
                    vertices.Add(new Vector3(ring[j].x,ring[j].y,z[i]));uv.Add(new Vector2(j/7f,i/(float)(z.Length-1)));
                    Vector3 n=j<=1?Vector3.down:j<=3?new Vector3(1,0,-dw):j<=5?new Vector3(0,1,-dh):new Vector3(-1,0,-dw);
                    normals.Add(n.normalized);
                }
            }
            for(int i=0;i<z.Length-1;i++)for(int j=0;j<8;j++){int a=i*8+j,b=i*8+(j+1)%8,c=a+8,d=b+8;tris.AddRange(new[]{a,c,d,a,d,b});}
            // Cap centres avoid degenerate triangles and leave no open end faces.
            for(int end=0;end<2;end++)
            {
                int source=end==0?0:(z.Length-1)*8;int ring=vertices.Count;
                for(int j=0;j<8;j++){vertices.Add(vertices[source+j]);uv.Add(new Vector2(.5f,.5f));normals.Add(end==0?Vector3.back:Vector3.forward);}
                int centre=vertices.Count;vertices.Add(new Vector3(0,(bottom+height[end==0?0:z.Length-1])*.5f,z[end==0?0:z.Length-1]));uv.Add(new Vector2(.5f,.5f));normals.Add(end==0?Vector3.back:Vector3.forward);
                for(int j=0;j<8;j++) if(end==0)tris.AddRange(new[]{centre,ring+j,ring+(j+1)%8});else tris.AddRange(new[]{centre,ring+(j+1)%8,ring+j});
            }
            for(int i=0;i<tris.Count;i+=3){int swap=tris[i+1];tris[i+1]=tris[i+2];tris[i+2]=swap;}
            var mesh=new Mesh();mesh.SetVertices(vertices);mesh.SetNormals(normals);mesh.SetUVs(0,uv);mesh.SetTriangles(tris,0);
            return MeshObject(name,parent,SaveMesh(mesh,name),mat,Vector3.zero);
        }
        private static void ReplaceCar(GameManager game)
        {
            var root=Group("Sparky — diecast rally coupe");var player=root.gameObject.AddComponent<PlayerController>();var visual=Group("Sparky detailed body",root);player.Visual=visual;
            Loft("Pressed metal body",visual,new[]{-.65f,-.56f,-.25f,.25f,.52f,.65f},new[]{.25f,.32f,.34f,.34f,.30f,.25f},new[]{.31f,.39f,.38f,.39f,.35f,.32f},.14f,"Enamel");
            Loft("Window cabin",visual,new[]{-.36f,-.22f,.12f,.34f},new[]{.27f,.245f,.235f,.25f},new[]{.40f,.64f,.64f,.35f},.34f,"Glass");
            Box("Enamel roof",visual,new Vector3(0,.644f,-.055f),new Vector3(.48f,.045f,.38f),"Enamel",.03f);
            foreach(float x in new[]{-.061f,.061f})
            {
                Box("Roof ivory racing stripe",visual,new Vector3(x,.669f,-.055f),new Vector3(.065f,.008f,.32f),"Ivory",.002f);
                var stripe=Box("Hood ivory racing stripe",visual,new Vector3(x,.365f,.445f),new Vector3(.065f,.01f,.27f),"Ivory",.002f);stripe.transform.localRotation=Quaternion.Euler(9,0,0);
                Box("Rear stripe",visual,new Vector3(x,.396f,-.50f),new Vector3(.065f,.008f,.18f),"Ivory",.002f);
            }
            foreach(float side in new[]{-1f,1f})
            {
                var a=Box("A pillar",visual,new Vector3(side*.239f,.49f,.236f),new Vector3(.026f,.35f,.03f),"Enamel",.005f);a.transform.localRotation=Quaternion.Euler(-34,0,0);
                var b=Box("Rear pillar",visual,new Vector3(side*.251f,.51f,-.285f),new Vector3(.03f,.27f,.04f),"Enamel",.005f);b.transform.localRotation=Quaternion.Euler(29,0,0);
                Box("B pillar",visual,new Vector3(side*.246f,.50f,-.085f),new Vector3(.025f,.21f,.032f),"Enamel",.005f);
                Box("Lower sill",visual,new Vector3(side*.331f,.205f,0),new Vector3(.023f,.06f,.74f),"Chrome",.008f);
                Box("Door handle",visual,new Vector3(side*.340f,.375f,-.12f),new Vector3(.016f,.021f,.08f),"Chrome",.005f);
                Box("Mirror stalk",visual,new Vector3(side*.337f,.42f,.20f),new Vector3(.055f,.025f,.035f),"Ink",.004f);
                Box("Mirror shell",visual,new Vector3(side*.365f,.44f,.20f),new Vector3(.045f,.06f,.09f),"Enamel",.015f);
                Box("Tail lamp",visual,new Vector3(side*.18f,.30f,-.657f),new Vector3(.13f,.07f,.018f),"RedLight",.008f);
                Sphere("Headlamp lens",visual,new Vector3(side*.19f,.257f,.665f),new Vector3(.13f,.105f,.031f),"Ivory");
            }
            Box("Front bumper",visual,new Vector3(0,.17f,.652f),new Vector3(.54f,.055f,.055f),"Chrome",.015f);
            Box("Rear bumper",visual,new Vector3(0,.18f,-.668f),new Vector3(.56f,.045f,.04f),"Chrome",.01f);
            Box("Rear black grille",visual,new Vector3(0,.274f,-.663f),new Vector3(.20f,.065f,.012f),"Ink",.003f);
            Box("Number badge",visual,new Vector3(0,.275f,-.672f),new Vector3(.12f,.039f,.012f),"Ivory",.002f);
            var wheels=new List<Transform>();
            foreach(float side in new[]{-1f,1f})foreach(float z in new[]{-.43f,.42f})
            {
                var wheel=Group("Wheel assembly",visual);wheel.localPosition=new Vector3(side*.315f,.16f,z);wheel.localRotation=Quaternion.Euler(0,0,90);wheels.Add(wheel);
                Torus("Moulded rubber tire",wheel,Vector3.zero,.113f,.044f,"Rubber");
                foreach(float y in new[]{-.033f,.033f})Torus("Sidewall ridge",wheel,new Vector3(0,y,0),.119f,.008f,"Rubber");
                Torus("Alloy rim lip",wheel,new Vector3(0,side*-.045f,0),.077f,.010f,"Chrome");
                Sphere("Hub cap",wheel,new Vector3(0,side*-.047f,0),new Vector3(.06f,.018f,.06f),"Chrome");
                for(int i=0;i<6;i++)
                {
                    float angle=i*Mathf.PI/3;
                    var spoke=Box("Alloy spoke",wheel,new Vector3(Mathf.Cos(angle)*.043f,side*-.045f,Mathf.Sin(angle)*.043f),new Vector3(.066f,.012f,.014f),"Chrome",.003f);
                    spoke.transform.localRotation=Quaternion.Euler(0,-angle*Mathf.Rad2Deg,0);
                }
                for(int i=0;i<24;i++)
                {
                    float angle=i*Mathf.PI/12;
                    var tread=Box("Tire tread",wheel,new Vector3(Mathf.Cos(angle)*.154f,0,Mathf.Sin(angle)*.154f),new Vector3(.009f,.051f,.013f),"Rubber",.002f);
                    tread.transform.localRotation=Quaternion.Euler(0,-angle*Mathf.Rad2Deg,0);
                }
                Combine(wheel,"wheel");
            }
            // Merge body by material, preserving independently animated wheel transforms.
            Combine(visual,"car_body",wheels.ToArray());
            var hit=root.gameObject.AddComponent<BoxCollider>();hit.center=new Vector3(0,.35f,0);hit.size=new Vector3(.78f,.7f,1.4f);hit.isTrigger=true;
            var anim=root.gameObject.AddComponent<ToyWheelAnimation>();anim.Wheels=wheels.ToArray();anim.LocalAxis=Vector3.up;
            string prefabPath=Root+"Prefabs/SparkyDiecast.prefab";var prefab=PrefabUtility.SaveAsPrefabAsset(root.gameObject,prefabPath);Object.DestroyImmediate(root.gameObject);
            Object.DestroyImmediate(game.Player.gameObject);var instance=(GameObject)PrefabUtility.InstantiatePrefab(prefab);game.Player=instance.GetComponent<PlayerController>();instance.GetComponent<ToyWheelAnimation>().Game=game;
            PrefabUtility.RecordPrefabInstancePropertyModifications(instance.GetComponent<ToyWheelAnimation>());
        }
        private static ArcadeHazard Hazard(int kind)
        {
            var root=Group(new[]{"Pocket sketchbook","Stacked wooden blocks","Rubber eraser","Rolling play ball"}[kind]);var h=root.gameObject.AddComponent<ArcadeHazard>();var model=Group("Model",root);h.Model=model;
            if(kind==0)
            {
                h.HalfWidth=.82f;h.HalfLength=.61f;
                Box("Individual page block",model,new Vector3(0,.27f,0),new Vector3(1.57f,.40f,1.12f),"Paper",.035f);
                foreach(float y in new[]{.045f,.505f})Box("Bound teal cover",model,new Vector3(0,y,0),new Vector3(1.64f,.07f,1.22f),"Teal",.035f);
                Box("Rounded spine",model,new Vector3(-.78f,.27f,0),new Vector3(.09f,.51f,1.22f),"Teal",.035f);
                for(int i=0;i<9;i++)Box("Page edge",model,new Vector3(.025f,.11f+i*.038f,-.565f),new Vector3(1.43f,.008f,.012f),"Ivory",.001f);
                Box("Cover deboss frame",model,new Vector3(.04f,.543f,0),new Vector3(1.05f,.014f,.75f),"Amber",.006f);
                Box("Cover inset",model,new Vector3(.04f,.553f,0),new Vector3(.99f,.01f,.69f),"Ink",.004f);
                Box("Bookmark ribbon",model,new Vector3(.45f,.052f,-.63f),new Vector3(.10f,.016f,.20f),"Enamel",.005f);
                for(int i=0;i<3;i++)Box("Cover foil lines",model,new Vector3(.04f,.563f,-.16f+i*.15f),new Vector3(i==1?.52f:.71f,.007f,.025f),"Ivory",.002f);
            }
            else if(kind==1)
            {
                h.HalfWidth=.66f;h.HalfLength=.62f;
                for(int i=0;i<2;i++)
                {
                    Box("Satin wooden block",model,new Vector3(0,.31f+i*.6f,0),new Vector3(1.32f,.58f,1.24f),i==0?"Amber":"Teal",.07f);
                    Box("Inset front panel",model,new Vector3(0,.31f+i*.6f,-.626f),new Vector3(.92f,.37f,.015f),"Ivory",.012f);
                    Torus("Round embossed icon",model,new Vector3(0,.31f+i*.6f,-.643f),.12f,.022f,i==0?"Enamel":"Teal",24,8).transform.localRotation=Quaternion.Euler(90,0,0);
                }
            }
            else if(kind==2)
            {
                h.HalfWidth=.74f;h.HalfLength=.48f;
                Box("Soft bevelled rubber",model,new Vector3(0,.23f,0),new Vector3(1.48f,.46f,.96f),"Pink",.10f);
                Box("Paper sleeve",model,new Vector3(.10f,.23f,0),new Vector3(.69f,.48f,.975f),"Paper",.025f);
                Box("Sleeve blue band",model,new Vector3(.10f,.478f,0),new Vector3(.39f,.012f,.85f),"Blue",.003f);
                for(int i=0;i<4;i++)Box("Sleeve typography",model,new Vector3(.1f,.487f,-.22f+i*.13f),new Vector3(.22f,.006f,.033f),"Ivory",.001f);
            }
            else
            {
                h.RollingBall=true;h.HalfWidth=h.HalfLength=.51f;
                model.localPosition=Vector3.up*.51f;
                Sphere("Soft orange play ball",model,Vector3.zero,Vector3.one*1.02f,"Amber");
                var seam=Torus("Equatorial seam",model,Vector3.zero,.505f,.010f,"Ivory",64,8);
                var seam2=Torus("Meridian seam",model,Vector3.zero,.505f,.010f,"Ivory",64,8);seam2.transform.localRotation=Quaternion.Euler(90,0,0);
            }
            return h;
        }
        private static void Combine(Transform root,string name,Transform[] preserve=null)
        {
            var groups=new Dictionary<Material,List<CombineInstance>>();var remove=new List<GameObject>();
            foreach(var filter in root.GetComponentsInChildren<MeshFilter>())
            {
                if(preserve!=null&&preserve.Any(t=>filter.transform.IsChildOf(t)))continue;
                var renderer=filter.GetComponent<MeshRenderer>();if(!renderer)continue;
                var mat=renderer.sharedMaterial;if(!groups.ContainsKey(mat))groups[mat]=new List<CombineInstance>();
                groups[mat].Add(new CombineInstance{mesh=filter.sharedMesh,transform=root.worldToLocalMatrix*filter.transform.localToWorldMatrix});remove.Add(filter.gameObject);
            }
            foreach(var entry in groups)
            {
                var mesh=new Mesh{indexFormat=IndexFormat.UInt32};mesh.CombineMeshes(entry.Value.ToArray(),true,true);mesh.RecalculateBounds();
                var obj=new GameObject(name+" • "+entry.Key.name,typeof(MeshFilter),typeof(MeshRenderer));obj.transform.SetParent(root,false);obj.GetComponent<MeshFilter>().sharedMesh=SaveMesh(mesh,name);obj.GetComponent<MeshRenderer>().sharedMaterial=entry.Key;
            }
            foreach(var obj in remove)Object.DestroyImmediate(obj);
        }
        private static void ReplaceRoomMaterials()
        {
            var map=new Dictionary<string,string>{{"HoneyWood","Wood"},{"DarkWood","Wood"},{"Wall","Wall"},{"WallSide","Wall"},{"Cream","Ivory"},{"Paper","Paper"},{"Glass","Glass"},{"Rubber","Rubber"},{"Yellow","Amber"},{"Teal","Teal"},{"Blanket","Blue"}};
            foreach(var renderer in Object.FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None))
                if(renderer.sharedMaterial&&map.TryGetValue(renderer.sharedMaterial.name,out var replacement))renderer.sharedMaterial=mats[replacement];
            foreach(var floor in Object.FindFirstObjectByType<WorldScroller>().FloorChunks)
            {
                foreach(Transform child in floor) if(child.name=="Table surface")
                {
                    var filter=child.GetComponent<MeshFilter>();var mesh=Object.Instantiate(filter.sharedMesh);var uv=mesh.uv;var verts=mesh.vertices;var norms=mesh.normals;
                    for(int i=0;i<uv.Length;i++)if(norms[i].y>.5f)uv[i]=new Vector2(verts[i].x/12,verts[i].z/18);
                    mesh.uv=uv;filter.sharedMesh=SaveMesh(mesh,"Wood_table_chunk");
                }
            }
            var room=GameObject.Find("Bedroom Set").transform;
            var sunDisk=GameObject.Find("Sun disk");if(sunDisk)Object.DestroyImmediate(sunDisk);
            var sky=GameObject.Find("Daylight sky");if(sky){var m=Mat("Window daylight",new Color(.72f,.84f,.87f),.1f);m.SetColor("_EmissionColor",new Color(.26f,.34f,.35f));m.EnableKeyword("_EMISSION");sky.GetComponent<Renderer>().sharedMaterial=m;}
            var window=GameObject.Find("Window and curtains").transform;
            foreach(Transform child in window.Cast<Transform>().ToArray())if(child.name=="Curtain fold")Object.DestroyImmediate(child.gameObject);
            foreach(float x in new[]{-6.25f,6.25f})
            {
                var verts=new List<Vector3>();var uv=new List<Vector2>();var tris=new List<int>();
                for(int y=0;y<2;y++)for(int i=0;i<=32;i++){float t=i/32f;verts.Add(new Vector3(x+(t-.5f)*2.6f,y==0?-5.5f:6.15f,-.60f+Mathf.Cos(t*Mathf.PI*8)*.20f));uv.Add(new Vector2(t,y));}
                for(int i=0;i<32;i++){int a=i,b=i+1,c=i+33,d=c+1;tris.AddRange(new[]{a,c,d,a,d,b,d,c,a,b,d,a});}
                var m=new Mesh();m.SetVertices(verts);m.SetUVs(0,uv);m.SetTriangles(tris,0);m.RecalculateNormals();MeshObject("Woven curtain",window,SaveMesh(m,"Curtain"),"Curtain",Vector3.zero);
            }
            // Lower wall trim and cabinet detail strengthen room scale without new gameplay colliders.
            Box("Picture rail",room,new Vector3(0,3.8f,42.65f),new Vector3(40,.12f,.12f),"Ivory");
            for(int i=0;i<3;i++){Box("Cabinet drawer",room,new Vector3(6.9f,-5.9f+i*1.45f,35.37f),new Vector3(3.28f,1.30f,.07f),"Wood",.05f);Box("Drawer brass pull",room,new Vector3(6.9f,-5.9f+i*1.45f,35.29f),new Vector3(.64f,.12f,.10f),"Chrome",.03f);}
            GameObject.Find("Spiral notebook").transform.localPosition=new Vector3(-5.2f,.12f,14);
            GameObject.Find("Pencil cup").transform.localPosition=new Vector3(5.1f,0,18);
        }
        private static void LightAndRenderer()
        {
            RenderSettings.ambientMode=AmbientMode.Trilight;RenderSettings.ambientSkyColor=new Color(.48f,.57f,.63f);RenderSettings.ambientEquatorColor=new Color(.40f,.43f,.43f);RenderSettings.ambientGroundColor=new Color(.17f,.14f,.12f);
            RenderSettings.sun.intensity=1.55f;RenderSettings.sun.color=new Color(1,.94f,.84f);RenderSettings.sun.transform.rotation=Quaternion.Euler(48,-30,0);RenderSettings.sun.shadowBias=.025f;RenderSettings.sun.shadowNormalBias=.12f;
            var cube=new Cubemap(32,TextureFormat.RGB24,true);cube.name="Original softbox reflections";
            for(int face=0;face<6;face++)
            {
                var colors=new Color[1024];for(int y=0;y<32;y++)for(int x=0;x<32;x++)
                {float t=y/31f;Color c=Color.Lerp(new Color(.15f,.19f,.21f),new Color(.62f,.72f,.78f),t);if((face==2||face==4)&&x>8&&x<20&&y>12&&y<26)c=new Color(.95f,.94f,.84f);colors[y*32+x]=c;}
                cube.SetPixels(colors,(CubemapFace)face);
            }
            cube.Apply();AssetDatabase.CreateAsset(cube,Root+"Textures/SoftboxReflections.cubemap");RenderSettings.defaultReflectionMode=DefaultReflectionMode.Custom;RenderSettings.customReflectionTexture=cube;RenderSettings.reflectionIntensity=.65f;
            string settings=Root+"WorkshopRenderer.asset";
            AssetDatabase.CopyAsset("Assets/BrakeToy/Settings/MobileRenderer.asset",settings);
            var data=AssetDatabase.LoadAssetAtPath<UniversalRendererData>(settings);
            var ao=ScriptableObject.CreateInstance<ScreenSpaceAmbientOcclusion>();ao.name="Workshop contact shading";
            AssetDatabase.AddObjectToAsset(ao,data);var serialized=new SerializedObject(ao);
            serialized.FindProperty("m_Settings.Downsample").boolValue=true;serialized.FindProperty("m_Settings.Intensity").floatValue=1.15f;
            serialized.FindProperty("m_Settings.Radius").floatValue=.25f;serialized.FindProperty("m_Settings.Samples").enumValueIndex=2;
            serialized.FindProperty("m_Settings.AOMethod").enumValueIndex=1;serialized.ApplyModifiedPropertiesWithoutUndo();ao.Create();data.rendererFeatures.Add(ao);EditorUtility.SetDirty(data);
            var original=(UniversalRenderPipelineAsset)GraphicsSettings.defaultRenderPipeline;var pipeline=Object.Instantiate(original);pipeline.name="Workshop mobile pipeline";
            AssetDatabase.CreateAsset(pipeline,Root+"WorkshopPipeline.asset");var so=new SerializedObject(pipeline);so.FindProperty("m_RendererDataList").GetArrayElementAtIndex(0).objectReferenceValue=data;so.ApplyModifiedPropertiesWithoutUndo();
            GraphicsSettings.defaultRenderPipeline=pipeline;QualitySettings.renderPipeline=pipeline;
        }
        private static Text Label(string name,Transform parent,string text,int size,Color color,Vector2 min,Vector2 max)
        {
            var rect=Rect(name,parent,min,max);var label=rect.gameObject.AddComponent<Text>();label.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");label.text=text;label.fontSize=size;label.color=color;label.alignment=TextAnchor.MiddleCenter;label.raycastTarget=false;return label;
        }
        private static RectTransform Rect(string name,Transform parent,Vector2 min,Vector2 max)
        {var r=new GameObject(name,typeof(RectTransform)).GetComponent<RectTransform>();r.SetParent(parent,false);r.anchorMin=min;r.anchorMax=max;r.offsetMin=r.offsetMax=Vector2.zero;return r;}
        private static ArcadeHUD BuildHUD(GameManager game)
        {
            var ui=game.UI;var hud=ui.gameObject.AddComponent<ArcadeHUD>();
            var hint=ui.SafeArea.Find("Steering hint");if(hint)Object.DestroyImmediate(hint.gameObject);
            var driving=Rect("Arcade instruments",ui.SafeArea,Vector2.zero,Vector2.one);hud.Driving=driving.gameObject;
            var panel=Rect("Top instrument strip",driving,new Vector2(.035f,.885f),new Vector2(.965f,.985f));var bg=panel.gameObject.AddComponent<Image>();bg.color=new Color(.025f,.06f,.08f,.88f);bg.raycastTarget=false;
            hud.DistanceLabel=Label("Distance",panel,"0 m",58,Color.white,new Vector2(.02f,.34f),new Vector2(.39f,.98f));hud.DistanceLabel.fontStyle=FontStyle.Bold;
            hud.StyleLabel=Label("Style",panel,"STİL 0",30,new Color(.65f,.94f,.9f),new Vector2(.40f,.42f),new Vector2(.80f,.95f));
            hud.SpeedLabel=Label("Speed",panel,"",23,new Color(.83f,.86f,.84f),new Vector2(.03f,0),new Vector2(.77f,.35f));
            ui.PauseButton.transform.SetParent(panel,false);var pr=ui.PauseButton.GetComponent<RectTransform>();pr.anchorMin=new Vector2(.83f,.15f);pr.anchorMax=new Vector2(.98f,.85f);pr.offsetMin=pr.offsetMax=Vector2.zero;
            hud.ComboLabel=Label("Combo",driving,"",31,Color.white,new Vector2(.08f,.825f),new Vector2(.92f,.874f));
            var bar=Rect("Combo countdown",driving,new Vector2(.19f,.814f),new Vector2(.81f,.82f));var barBg=bar.gameObject.AddComponent<Image>();barBg.color=new Color(.03f,.07f,.09f,.65f);barBg.raycastTarget=false;
            var fill=Rect("Fill",bar,Vector2.zero,Vector2.one);hud.ComboFill=fill.gameObject.AddComponent<Image>();hud.ComboFill.color=new Color(.25f,.92f,.79f);hud.ComboFill.type=Image.Type.Filled;hud.ComboFill.fillMethod=Image.FillMethod.Horizontal;hud.ComboFill.raycastTarget=false;
            hud.NoticeLabel=Label("Near miss notification",driving,"",39,Color.white,new Vector2(.04f,.38f),new Vector2(.96f,.47f));hud.NoticeLabel.fontStyle=FontStyle.Bold;
            hud.WarningLabel=Label("Ball warning",driving,"",31,new Color(1,.75f,.2f),new Vector2(.03f,.73f),new Vector2(.97f,.79f));
            var flash=Rect("Impact flash",ui.SafeArea,Vector2.zero,Vector2.one);hud.ImpactFlash=flash.gameObject.AddComponent<Image>();hud.ImpactFlash.color=Color.clear;hud.ImpactFlash.raycastTarget=false;
            ui.Overlay.transform.SetAsLastSibling();var overlay=ui.Overlay.GetComponent<RectTransform>();overlay.anchorMin=new Vector2(.055f,.23f);overlay.anchorMax=new Vector2(.945f,.79f);overlay.offsetMin=overlay.offsetMax=Vector2.zero;
            ui.Overlay.GetComponent<Image>().color=new Color(.025f,.055f,.07f,.94f);ui.Heading.color=new Color(1,.82f,.38f);ui.Heading.fontSize=78;
            var hr=ui.Heading.rectTransform;hr.anchorMin=new Vector2(.05f,.76f);hr.anchorMax=new Vector2(.95f,.94f);hr.offsetMin=hr.offsetMax=Vector2.zero;
            ui.Description.fontSize=34;var dr=ui.Description.rectTransform;dr.anchorMin=new Vector2(.06f,.30f);dr.anchorMax=new Vector2(.94f,.75f);dr.offsetMin=dr.offsetMax=Vector2.zero;
            var ar=ui.ActionButton.GetComponent<RectTransform>();ar.anchorMin=new Vector2(.10f,.13f);ar.anchorMax=new Vector2(.90f,.26f);ar.offsetMin=ar.offsetMax=Vector2.zero;ui.ActionText.fontSize=46;
            var version=ui.Overlay.transform.Find("Prototype version").GetComponent<Text>();version.text="v0.3  /  ARCADE WORKSHOP";version.fontSize=23;version.rectTransform.anchorMin=new Vector2(.02f,.025f);version.rectTransform.anchorMax=new Vector2(.98f,.09f);
            var sound=Rect("Sound toggle",ui.SafeArea,new Vector2(.33f,.025f),new Vector2(.67f,.065f));var image=sound.gameObject.AddComponent<Image>();image.color=new Color(.03f,.07f,.09f,.85f);hud.SoundButton=sound.gameObject.AddComponent<Button>();hud.SoundButton.targetGraphic=image;hud.SoundLabel=Label("Label",sound,"SES AÇIK",25,Color.white,Vector2.zero,Vector2.one);
            return hud;
        }
    }
}
