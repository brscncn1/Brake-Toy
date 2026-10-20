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
    public static class RefinedSceneBuilder
    {
        public const string ScenePath = "Assets/BrakeToy/Scenes/ToyRoomRefined.unity";
        private const string Root = "Assets/BrakeToy/Art/Refined/";
        private static readonly Dictionary<Material, Material> materials = new Dictionary<Material, Material>();
        private static int meshId;
        [MenuItem("Tools/Brake Toy/Open Refined Room v0.4")]
        public static void Open() { if (!File.Exists(ScenePath)) Create(); else EditorSceneManager.OpenScene(ScenePath); }
        public static void Create()
        {
            if (File.Exists(ScenePath)) { Debug.Log("Refined scene exists; preserved."); return; }
            foreach (string dir in new[] { "Materials", "Meshes", "Prefabs", "Definitions" }) Directory.CreateDirectory(Root + dir);
            AssetDatabase.Refresh(); materials.Clear(); meshId = 0;
            var scene = EditorSceneManager.OpenScene(WorkshopSceneBuilder.ScenePath);
            EditorSceneManager.SaveScene(scene, ScenePath);
            var game = Object.FindFirstObjectByType<GameManager>();
            game.name = "Brake Toy — Refined Room v0.4";
            var config = Object.Instantiate(game.Config); config.name = "RefinedConfig"; config.CameraPitch = 16; config.CameraSidePadding = .45f;
            AssetDatabase.CreateAsset(config, "Assets/BrakeToy/ScriptableObjects/RefinedConfig.asset"); game.Config = config;
            Camera.main.GetComponent<PortraitCamera>().Config = config; Camera.main.GetComponent<PortraitCamera>().Reframe();
            // Unpack only the new scene's instances; all older prefab files remain immutable.
            foreach (var root in scene.GetRootGameObjects())
                foreach (var t in root.GetComponentsInChildren<Transform>(true))
                    if (PrefabUtility.IsAnyPrefabInstanceRoot(t.gameObject)) PrefabUtility.UnpackPrefabInstance(t.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            foreach (var renderer in Object.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                renderer.sharedMaterials = renderer.sharedMaterials.Select(CloneMaterial).ToArray();
            RefineCar(game); RoomDetails(); ConfigureLighting(); BuildUI(game); ConfigureProfiles(game);
            game.gameObject.AddComponent<ToyImpactFeedback>(); game.Arcade.Sound.EnhancedLayers = true;
            game.Arcade.UseVariedPatterns = game.Arcade.VaryRuns = true;
            game.Arcade.BuildCatalogPool = true;
            var definitions = new ObstacleDefinition[4];
            string[] names = { "Pocket sketchbook", "Wooden blocks", "Rubber eraser", "Rolling play ball" };
            for (int kind = 0; kind < 4; kind++)
            {
                var prototype = game.Arcade.Pool[kind * 6];
                var prefab = PrefabUtility.SaveAsPrefabAsset(prototype.gameObject, Root + "Prefabs/Hazard_" + kind + ".prefab");
                var definition = ScriptableObject.CreateInstance<ObstacleDefinition>(); definition.name = names[kind]; definition.Kind = kind;
                definition.Prefab = prefab.GetComponent<ArcadeHazard>(); definition.PoolSize = 6;
                definition.HalfWidth = prototype.HalfWidth; definition.HalfLength = prototype.HalfLength;
                definition.ReadabilityNotes = kind == 3 ? "Visible side warning; swept sphere; safe lane remains clear." : "Rounded silhouette; collision dimensions match the existing tested gameplay footprint.";
                AssetDatabase.CreateAsset(definition, Root + "Definitions/" + kind + ".asset"); definitions[kind] = definition;
            }
            game.Arcade.Definitions = definitions;
            PlayerSettings.bundleVersion = "0.4.0"; PlayerSettings.Android.bundleVersionCode = 4;
            EditorBuildSettings.scenes = new[] { ScenePath, WorkshopSceneBuilder.ScenePath, TabletopSceneBuilder.ScenePath, "Assets/BrakeToy/Scenes/ToyRoom.unity" }.Select(p => new EditorBuildSettingsScene(p, true)).ToArray();
            EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene); AssetDatabase.SaveAssets();
            AddDiagnostics(); RefinedArtPolish.Apply(); RefinedSurfaceFinish.Apply();
            Debug.Log("Refined Room created; original scenes and records preserved.");
        }
        public static void AddDiagnostics()
        {
            EditorSceneManager.OpenScene(ScenePath); var game=Object.FindFirstObjectByType<GameManager>();
            if(!game.GetComponent<PerformanceProbe>())
            {
                var probe=game.gameObject.AddComponent<PerformanceProbe>();
                probe.CaptureButton=Button("Performance capture",game.UI.SafeArea,"60 sn PERFORMANS TESTİ",new Vector2(.23f,.09f),new Vector2(.77f,.135f),out var label);probe.Label=label;label.fontSize=21;
            }
            var wood=AssetDatabase.LoadAssetAtPath<Material>(Root+"Materials/Wood.mat");wood.SetColor("_BaseColor",new Color(1.15f,1.05f,.90f));EditorUtility.SetDirty(wood);
            RenderSettings.sun.shadows=LightShadows.Soft;
            var scene=UnityEngine.SceneManagement.SceneManager.GetActiveScene();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();
        }
        private static Material CloneMaterial(Material original)
        {
            if (!original) return original;
            if (materials.TryGetValue(original, out var cached)) return cached;
            var m = Object.Instantiate(original); m.name = original.name;
            if (m.name == "Wood") { m.shader = Shader.Find("Brake Toy/Quiet Wood"); m.SetColor("_BaseColor", new Color(1.32f, 1.14f, .91f)); m.SetFloat("_Smoothness", .27f); m.SetFloat("_BumpScale", .045f); }
            if (m.name == "Wall") m.SetColor("_BaseColor", new Color(.49f, .62f, .66f));
            if (m.name == "Glass") { m.SetColor("_BaseColor", new Color(.18f, .32f, .39f)); m.SetFloat("_Metallic", .24f); m.SetFloat("_Smoothness", .88f); }
            AssetDatabase.CreateAsset(m, AssetDatabase.GenerateUniqueAssetPath(Root + "Materials/" + m.name + ".mat")); materials[original] = m; return m;
        }
        private static Material Mat(string name) => materials.Values.First(m => m.name == name);
        private static void RefineCar(GameManager game)
        {
            foreach (var filter in game.Player.Visual.GetComponentsInChildren<MeshFilter>())
            {
                if (!filter.name.StartsWith("car_body")) continue;
                var mesh = Object.Instantiate(filter.sharedMesh); var vertices = mesh.vertices; var normals = mesh.normals;
                for (int i = 0; i < vertices.Length; i++) if (vertices[i].y > .34f)
                { vertices[i].y = .34f + (vertices[i].y - .34f) * .78f; normals[i] = new Vector3(normals[i].x, normals[i].y / .78f, normals[i].z).normalized; }
                mesh.vertices = vertices; mesh.normals = normals; mesh.RecalculateTangents(); mesh.RecalculateBounds();
                AssetDatabase.CreateAsset(mesh, Root + "Meshes/Car_" + meshId++ + ".asset"); filter.sharedMesh = mesh;
            }
            PrefabUtility.SaveAsPrefabAsset(game.Player.gameObject, Root + "Prefabs/SparkyRefined.prefab");
        }
        private static void ConfigureLighting()
        {
            RenderSettings.sun.intensity = 1.15f; RenderSettings.sun.shadowStrength = .43f;
            RenderSettings.sun.transform.rotation = Quaternion.Euler(27, 160, 0);
            RenderSettings.sun.color = new Color(1, .95f, .87f);
            RenderSettings.ambientSkyColor = new Color(.59f, .65f, .69f);
            RenderSettings.ambientEquatorColor = new Color(.53f, .55f, .53f);
            var ambient = new SphericalHarmonicsL2(); ambient.AddAmbientLight(new Color(.54f, .58f, .59f)); RenderSettings.ambientProbe = ambient;
            GameObject.Find("Soft room bounce fill").GetComponent<Light>().intensity = .85f;
        }
        private static void ConfigureProfiles(GameManager game)
        {
            AssetDatabase.CopyAsset("Assets/BrakeToy/Art/Workshop/WorkshopRenderer.asset", Root + "HighRenderer.asset");
            AssetDatabase.CopyAsset("Assets/BrakeToy/Settings/MobileRenderer.asset", Root + "LowRenderer.asset");
            var high = AssetDatabase.LoadAssetAtPath<UniversalRendererData>(Root + "HighRenderer.asset");
            foreach (var feature in high.rendererFeatures)
            {
                var so = new SerializedObject(feature); var intensity = so.FindProperty("m_Settings.Intensity");
                if (intensity != null) { intensity.floatValue = .65f; so.ApplyModifiedPropertiesWithoutUndo(); }
            }
            var source = Object.Instantiate(AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>("Assets/BrakeToy/Art/Workshop/WorkshopPipeline.asset")); source.name = "Refined source pipeline";
            AssetDatabase.CreateAsset(source, Root + "RefinedPipeline.asset");
            var serialized = new SerializedObject(source); var list = serialized.FindProperty("m_RendererDataList"); list.arraySize = 2;
            list.GetArrayElementAtIndex(0).objectReferenceValue = high; list.GetArrayElementAtIndex(1).objectReferenceValue = AssetDatabase.LoadAssetAtPath<UniversalRendererData>(Root + "LowRenderer.asset"); serialized.ApplyModifiedPropertiesWithoutUndo();
            var controller = game.gameObject.AddComponent<GraphicsProfileController>(); controller.Source = source;
            controller.ToggleButton = Button("Graphics profile", game.UI.SafeArea, "GRAFİK: YÜKSEK", new Vector2(.04f, .025f), new Vector2(.49f, .075f), out var label); controller.Label = label;
            // Project pipeline remains v0.3. Runtime controller owns and restores its private copy.
        }
        private static void BuildUI(GameManager game)
        {
            var ui = game.UI; var hud = game.Arcade.HUD; hud.CompactLayout = true;
            ui.PauseButton.transform.SetParent(ui.SafeArea, false); Object.DestroyImmediate(hud.Driving);
            var oldCard = ui.Overlay.transform.Find("Menu card"); if (oldCard) Object.DestroyImmediate(oldCard.gameObject);
            var driving = Rect("Compact driving HUD", ui.SafeArea, Vector2.zero, Vector2.one); hud.Driving = driving.gameObject;
            hud.DistanceLabel = Label("Distance", driving, "0 m", 42, Color.white, new Vector2(.035f,.927f),new Vector2(.35f,.985f)); hud.DistanceLabel.alignment = TextAnchor.MiddleLeft;
            hud.StyleLabel = Label("Style", driving, "STİL 0", 24, new Color(.72f,1,.91f), new Vector2(.035f,.891f),new Vector2(.43f,.929f)); hud.StyleLabel.alignment = TextAnchor.MiddleLeft;
            hud.SpeedLabel = Label("Speed", driving, "", 21, Color.white, new Vector2(.49f,.927f),new Vector2(.84f,.98f));
            ui.PauseButton.transform.SetParent(driving, false); Place(ui.PauseButton.GetComponent<RectTransform>(),new Vector2(.855f,.925f),new Vector2(.975f,.985f));
            hud.ComboLabel = Label("Combo", driving, "", 27, Color.white, new Vector2(.1f,.085f),new Vector2(.9f,.13f));
            var bar = Rect("Combo countdown", driving, new Vector2(.25f,.079f),new Vector2(.75f,.084f));
            var background = bar.gameObject.AddComponent<Image>(); background.color = new Color(.04f,.08f,.08f,.45f); background.raycastTarget = false;
            var fill = Rect("Fill", bar, Vector2.zero,Vector2.one); hud.ComboFill=fill.gameObject.AddComponent<Image>();hud.ComboFill.color=new Color(.35f,1,.85f);hud.ComboFill.raycastTarget=false;
            hud.NoticeLabel=Label("Near miss",driving,"",32,new Color(.4f,1,.9f),new Vector2(.03f,.37f),new Vector2(.97f,.43f));
            hud.WarningLabel=Label("Side ball warning",driving,"",29,new Color(1,.8f,.3f),new Vector2(.03f,.73f),new Vector2(.97f,.78f));
            foreach(var t in driving.GetComponentsInChildren<Text>())t.gameObject.AddComponent<Shadow>().effectDistance=new Vector2(1,-2);
            Place(ui.Overlay.GetComponent<RectTransform>(),new Vector2(.06f,.28f),new Vector2(.94f,.77f));
            ui.Overlay.GetComponent<Image>().color=new Color(.025f,.055f,.07f,.94f);
            ui.Heading.fontSize=61;Place(ui.Heading.rectTransform,new Vector2(.04f,.78f),new Vector2(.96f,.96f));
            ui.Description.fontSize=31;Place(ui.Description.rectTransform,new Vector2(.07f,.35f),new Vector2(.93f,.77f));
            Place(ui.ActionButton.GetComponent<RectTransform>(),new Vector2(.09f,.19f),new Vector2(.91f,.32f));ui.ActionText.fontSize=36;
            hud.SameRouteButton=Button("Same route retry",ui.Overlay.transform,"AYNI PARKURU TEKRARLA",new Vector2(.09f,.07f),new Vector2(.91f,.17f),out var retryText);retryText.fontSize=27;
            var version=ui.Overlay.transform.Find("Prototype version").GetComponent<Text>();version.text="v0.4  /  REFINED ROOM";version.fontSize=19;Place(version.rectTransform,new Vector2(.1f,.005f),new Vector2(.9f,.06f));
            Place(hud.SoundButton.GetComponent<RectTransform>(),new Vector2(.51f,.025f),new Vector2(.96f,.075f));hud.SoundLabel.fontSize=24;
            ui.Overlay.transform.SetAsLastSibling();hud.ImpactFlash.transform.SetAsLastSibling();
        }
        private static RectTransform Rect(string name, Transform parent,Vector2 min,Vector2 max)
        {var r=new GameObject(name,typeof(RectTransform)).GetComponent<RectTransform>();r.SetParent(parent,false);Place(r,min,max);return r;}
        private static void Place(RectTransform r,Vector2 min,Vector2 max){r.anchorMin=min;r.anchorMax=max;r.offsetMin=r.offsetMax=Vector2.zero;}
        private static Text Label(string name,Transform parent,string text,int size,Color color,Vector2 min,Vector2 max)
        {var r=Rect(name,parent,min,max);var t=r.gameObject.AddComponent<Text>();t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.fontSize=size;t.text=text;t.color=color;t.alignment=TextAnchor.MiddleCenter;t.raycastTarget=false;return t;}
        private static Button Button(string name,Transform parent,string text,Vector2 min,Vector2 max,out Text label)
        {var r=Rect(name,parent,min,max);var i=r.gameObject.AddComponent<Image>();i.color=new Color(.035f,.09f,.11f,.94f);var b=r.gameObject.AddComponent<Button>();b.targetGraphic=i;label=Label("Label",r,text,24,Color.white,Vector2.zero,Vector2.one);return b;}
        private static void RoomDetails()
        {
            var room=GameObject.Find("Bedroom Set").transform;
            var details=new GameObject("Refined room joinery — decorative only").transform;details.SetParent(room,false);
            // Real depth at the window, a readable panelled wall and consistent warm ivory joinery.
            Box("Deep window sill",details,new Vector3(-6.8f,3.2f,41.6f),new Vector3(8.1f,.24f,1.8f),Mat("Ivory"),.06f);
            for(int i=0;i<7;i++)Box("Wall panelling stile",details,new Vector3(-16+i*5.3f,1.4f,42.58f),new Vector3(.11f,4.5f,.14f),Mat("Ivory"),.025f);
            foreach(float y in new[]{-.9f,3.65f})Box("Wall panelling rail",details,new Vector3(0,y,42.57f),new Vector3(39,.12f,.15f),Mat("Ivory"),.025f);
            foreach(float x in new[]{1.7f,6.3f})Box("Poster walnut frame",details,new Vector3(x,8,42.12f),new Vector3(.10f,6.05f,.16f),Mat("Wood"),.025f);
            foreach(float y in new[]{5f,11f})Box("Poster walnut frame",details,new Vector3(4,y,42.12f),new Vector3(4.7f,.10f,.16f),Mat("Wood"),.025f);
            // Add fine seams to the storage chest rather than raising overall prop count indiscriminately.
            for(int i=0;i<3;i++)Box("Chest drawer highlight",details,new Vector3(6.9f,-5.45f+i*1.45f,35.22f),new Vector3(3.08f,.035f,.025f),Mat("Ivory"),.008f);
            CombineDetails(details);
        }
        internal static void Box(string name,Transform parent,Vector3 position,Vector3 size,Material material,float radius)
        {
            radius=Mathf.Min(radius,Mathf.Min(size.x,Mathf.Min(size.y,size.z))*.35f);
            var vertices=new List<Vector3>();var normals=new List<Vector3>();var uv=new List<Vector2>();var tris=new List<int>();
            var half=size*.5f;var inner=half-Vector3.one*radius;
            foreach(var n in new[]{Vector3.right,Vector3.left,Vector3.up,Vector3.down,Vector3.forward,Vector3.back})
            {
                var u=Mathf.Abs(n.y)>.5f?Vector3.right:Vector3.up;var v=Vector3.Cross(n,u);float hu=Vector3.Scale(u,half).magnitude,hv=Vector3.Scale(v,half).magnitude;
                float[] cu={-hu,-hu+radius,0,hu-radius,hu},cv={-hv,-hv+radius,0,hv-radius,hv};int first=vertices.Count;
                for(int y=0;y<5;y++)for(int x=0;x<5;x++){var q=Vector3.Scale(n,half)+u*cu[x]+v*cv[y];var c=new Vector3(Mathf.Clamp(q.x,-inner.x,inner.x),Mathf.Clamp(q.y,-inner.y,inner.y),Mathf.Clamp(q.z,-inner.z,inner.z));var normal=(q-c).normalized;vertices.Add(c+normal*radius);normals.Add(normal);uv.Add(new Vector2(x/4f,y/4f));}
                for(int y=0;y<4;y++)for(int x=0;x<4;x++){int a=first+y*5+x,b=a+1,c=a+5,d=c+1;tris.AddRange(new[]{a,b,d,a,d,c});}
            }
            var mesh=new Mesh();mesh.SetVertices(vertices);mesh.SetNormals(normals);mesh.SetUVs(0,uv);mesh.SetTriangles(tris,0);mesh.RecalculateBounds();mesh.RecalculateTangents();
            var obj=new GameObject(name,typeof(MeshFilter),typeof(MeshRenderer));obj.transform.SetParent(parent,false);obj.transform.localPosition=position;obj.GetComponent<MeshFilter>().sharedMesh=mesh;obj.GetComponent<MeshRenderer>().sharedMaterial=material;
        }
        internal static void CombineDetails(Transform root)
        {
            var filters=root.GetComponentsInChildren<MeshFilter>();
            foreach(var group in filters.GroupBy(f=>f.GetComponent<Renderer>().sharedMaterial))
            {
                var mesh=new Mesh{indexFormat=IndexFormat.UInt32};mesh.CombineMeshes(group.Select(f=>new CombineInstance{mesh=f.sharedMesh,transform=root.worldToLocalMatrix*f.transform.localToWorldMatrix}).ToArray());
                AssetDatabase.CreateAsset(mesh,Root+"Meshes/Joinery_"+meshId+++".asset");var obj=new GameObject("Joinery • "+group.Key.name,typeof(MeshFilter),typeof(MeshRenderer));obj.transform.SetParent(root,false);obj.GetComponent<MeshFilter>().sharedMesh=mesh;obj.GetComponent<MeshRenderer>().sharedMaterial=group.Key;
            }
            foreach(var filter in filters){if(!EditorUtility.IsPersistent(filter.sharedMesh))Object.DestroyImmediate(filter.sharedMesh);Object.DestroyImmediate(filter.gameObject);}
        }
    }
}
