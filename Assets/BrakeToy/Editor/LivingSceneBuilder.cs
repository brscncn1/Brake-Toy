using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Object=UnityEngine.Object;

namespace BrakeToy.Editor
{
    public static class LivingSceneBuilder
    {
        public const string ScenePath="Assets/BrakeToy/Scenes/ToyRoomLiving.unity";
        private const string Root="Assets/BrakeToy/Art/Living/";
        [MenuItem("Tools/Brake Toy/Open Living Workshop v0.5")]
        public static void Open(){if(!File.Exists(ScenePath))Create();else EditorSceneManager.OpenScene(ScenePath);}
        public static void PolishRoom()
        {EditorSceneManager.OpenScene(ScenePath);LivingRoomBuilder.Build(Object.FindFirstObjectByType<GameManager>());var s=UnityEngine.SceneManagement.SceneManager.GetActiveScene();EditorSceneManager.MarkSceneDirty(s);EditorSceneManager.SaveScene(s);AssetDatabase.SaveAssets();}
        public static void Create()
        {
            if(File.Exists(ScenePath)){Debug.Log("Living scene exists; preserved.");return;}
            foreach(var d in new[]{"Materials","Meshes","Prefabs"})Directory.CreateDirectory(Root+d);
            AssetDatabase.Refresh();ImportSources();
            foreach(string path in Directory.GetFiles("Assets/BrakeToy/Art/Refined/Materials","*.mat"))
                AssetDatabase.CopyAsset(path.Replace('\\','/'),Root+"Materials/"+Path.GetFileName(path));
            AssetDatabase.Refresh();
            Fabric("CurtainFabric",new Color(.9f,.68f,.29f));Fabric("DuvetFabric",new Color(.15f,.39f,.54f));
            Fabric("PillowFabric",new Color(.9f,.85f,.72f));Fabric("TeddyFabric",new Color(.42f,.24f,.12f));
            var chair=NewMat("ScannedChair",Color.white);string c=Root+"ThirdParty/painted_wooden_chair_02/painted_wooden_chair_02_";
            chair.SetTexture("_BaseMap",AssetDatabase.LoadAssetAtPath<Texture2D>(c+"diff_1k.jpg"));chair.SetTexture("_BumpMap",AssetDatabase.LoadAssetAtPath<Texture2D>(c+"nor_gl_1k.jpg"));chair.EnableKeyword("_NORMALMAP");chair.SetFloat("_BumpScale",.4f);chair.SetFloat("_Smoothness",.22f);
            chair.SetTexture("_OcclusionMap",AssetDatabase.LoadAssetAtPath<Texture2D>(c+"ao_1k.jpg"));
            var headlamp=NewMat("Headlamp",new Color(.95f,.91f,.72f));headlamp.SetFloat("_Smoothness",.85f);headlamp.EnableKeyword("_EMISSION");headlamp.SetColor("_EmissionColor",new Color(.25f,.22f,.12f));
            var daylight=NewMat("Daylight",new Color(.71f,.84f,.93f));daylight.EnableKeyword("_EMISSION");daylight.SetColor("_EmissionColor",new Color(.36f,.46f,.55f));
            var scene=EditorSceneManager.OpenScene(RefinedSceneBuilder.ScenePath);EditorSceneManager.SaveScene(scene,ScenePath);
            foreach(var root in scene.GetRootGameObjects())foreach(var t in root.GetComponentsInChildren<Transform>(true))
                if(t && PrefabUtility.IsAnyPrefabInstanceRoot(t.gameObject))PrefabUtility.UnpackPrefabInstance(t.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
            foreach(var renderer in Object.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Include,FindObjectsSortMode.None))
                renderer.sharedMaterials=renderer.sharedMaterials.Select(m=>m?Mat(m.name)??m:m).ToArray();
            var game=Object.FindFirstObjectByType<GameManager>();game.name="Brake Toy — Living Workshop v0.5";
            var config=Object.Instantiate(game.Config);config.name="LivingConfig";config.CameraPitch=17;config.CameraDepthScale=.88f;
            AssetDatabase.CreateAsset(config,"Assets/BrakeToy/ScriptableObjects/LivingConfig.asset");game.Config=config;
            LivingCarBuilder.Build(game);LivingRoomBuilder.Build(game);
            var camera=Camera.main.GetComponent<PortraitCamera>();camera.Config=config;camera.FollowTarget=game.Player.transform;camera.FollowAmount=.35f;camera.Reframe();
            var challenges=game.gameObject.AddComponent<RunChallenges>();game.Arcade.Challenges=challenges;game.Arcade.Choreographed=true;game.Arcade.StyleRecordKey="BrakeToy.v05.style";
            var pool=new GameObject("Reusable risk stars — no collision").transform;challenges.RiskStars=new Transform[12];
            var starMesh=AssetDatabase.LoadAssetAtPath<Mesh>("Assets/BrakeToy/Art/Tabletop/Meshes/star.asset");
            for(int i=0;i<12;i++){var star=LivingMeshFactory.MeshObject("Risk star "+i,pool,starMesh,Mat("Amber"),Vector3.zero);star.transform.localScale=Vector3.one*.28f;star.transform.localRotation=Quaternion.Euler(-65,0,0);star.SetActive(false);challenges.RiskStars[i]=star.transform;}
            challenges.GoalLabel=Label("Run goal",game.Arcade.HUD.Driving.transform,"",22,new Vector2(.02f,.135f),new Vector2(.98f,.18f));
            var audio=game.Arcade.Sound;audio.UseRecordedSamples=true;
            audio.RecordedRoll=Clip("qubodup-bowling_roll-nofadeout");audio.RecordedWood=Clip("impactWood_medium_001");audio.RecordedMetal=Clip("impactMetal_light_002");audio.RecordedSoft=Clip("impactSoft_medium_001");
            BuildAudioUI(game);
            var version=game.UI.Overlay.transform.Find("Prototype version").GetComponent<Text>();version.text="v0.5  /  LIVING WORKSHOP";
            var profile=game.GetComponent<GraphicsProfileController>();
            AssetDatabase.CopyAsset("Assets/BrakeToy/Art/Refined/RefinedPipeline.asset",Root+"LivingPipeline.asset");profile.Source=AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(Root+"LivingPipeline.asset");profile.ManagePostProcessing=true;
            var volume=new GameObject("Living color grade").AddComponent<Volume>();volume.isGlobal=true;
            var vp=ScriptableObject.CreateInstance<VolumeProfile>();AssetDatabase.CreateAsset(vp,Root+"LivingVolume.asset");volume.sharedProfile=vp;
            var tone=vp.Add<Tonemapping>(true);tone.mode.Override(TonemappingMode.ACES);
            var bloom=vp.Add<Bloom>(true);bloom.intensity.Override(.12f);bloom.threshold.Override(1.1f);
            var color=vp.Add<ColorAdjustments>(true);color.contrast.Override(5);color.saturation.Override(4);
            Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing=true;
            RenderSettings.sun.color=new Color(1,.91f,.76f);RenderSettings.sun.intensity=1.35f;RenderSettings.sun.shadowStrength=.6f;RenderSettings.sun.transform.rotation=Quaternion.Euler(29,135,0);
            var fill=GameObject.Find("Soft room bounce fill");if(fill)fill.GetComponent<Light>().intensity=.46f;
            RenderSettings.ambientMode=AmbientMode.Trilight;RenderSettings.ambientSkyColor=new Color(.52f,.58f,.67f);RenderSettings.ambientEquatorColor=new Color(.38f,.40f,.42f);RenderSettings.ambientGroundColor=new Color(.22f,.17f,.12f);
            var hdr=AssetDatabase.LoadAssetAtPath<Cubemap>(Root+"ThirdParty/studio_small_09/studio_small_09_1k.hdr");if(hdr){RenderSettings.defaultReflectionMode=DefaultReflectionMode.Custom;RenderSettings.customReflectionTexture=hdr;RenderSettings.reflectionIntensity=.65f;}
            PlayerSettings.bundleVersion="0.5.0";PlayerSettings.Android.bundleVersionCode=5;
            EditorBuildSettings.scenes=new[]{ScenePath,RefinedSceneBuilder.ScenePath,WorkshopSceneBuilder.ScenePath,TabletopSceneBuilder.ScenePath,"Assets/BrakeToy/Scenes/ToyRoom.unity"}.Select(p=>new EditorBuildSettingsScene(p,true)).ToArray();
            EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();Debug.Log("Living Workshop v0.5 generated.");
        }
        private static AudioClip Clip(string name)=>AssetDatabase.LoadAssetAtPath<AudioClip>(Root+"ThirdParty/Audio/"+name+".ogg");
        private static Material Mat(string name)=>AssetDatabase.LoadAssetAtPath<Material>(Root+"Materials/"+name+".mat");
        private static Material NewMat(string name,Color color){var m=new Material(Shader.Find("Universal Render Pipeline/Lit")){name=name};m.SetColor("_BaseColor",color);m.SetFloat("_Smoothness",.18f);AssetDatabase.CreateAsset(m,Root+"Materials/"+name+".mat");return m;}
        private static void Fabric(string name,Color tint)
        {var m=NewMat(name,tint);string p=Root+"ThirdParty/cotton_jersey/cotton_jersey_";m.SetTexture("_BaseMap",AssetDatabase.LoadAssetAtPath<Texture2D>(p+"diff_1k.jpg"));m.SetTexture("_BumpMap",AssetDatabase.LoadAssetAtPath<Texture2D>(p+"nor_gl_1k.jpg"));m.EnableKeyword("_NORMALMAP");m.SetFloat("_BumpScale",.23f);m.SetFloat("_Cull",0);}
        private static void ImportSources()
        {
            foreach(var path in Directory.GetFiles(Root+"ThirdParty","*.jpg",SearchOption.AllDirectories))
            {var t=(TextureImporter)AssetImporter.GetAtPath(path.Replace('\\','/'));t.maxTextureSize=1024;t.mipmapEnabled=true;if(path.Contains("nor_gl")){t.textureType=TextureImporterType.NormalMap;t.sRGBTexture=false;}else t.sRGBTexture=!path.Contains("_ao_");t.SaveAndReimport();}
            string f=Root+"ThirdParty/painted_wooden_chair_02/painted_wooden_chair_02_1k.fbx";var model=(ModelImporter)AssetImporter.GetAtPath(f);model.isReadable=true;model.materialImportMode=ModelImporterMaterialImportMode.None;model.importLights=false;model.importCameras=false;model.SaveAndReimport();
            var hdr=(TextureImporter)AssetImporter.GetAtPath(Root+"ThirdParty/studio_small_09/studio_small_09_1k.hdr");hdr.textureShape=TextureImporterShape.TextureCube;hdr.generateCubemap=TextureImporterGenerateCubemap.AutoCubemap;hdr.maxTextureSize=256;hdr.SaveAndReimport();
        }
        private static RectTransform Rect(string name,Transform parent,Vector2 min,Vector2 max)
        {var r=new GameObject(name,typeof(RectTransform)).GetComponent<RectTransform>();r.SetParent(parent,false);r.anchorMin=min;r.anchorMax=max;r.offsetMin=r.offsetMax=Vector2.zero;return r;}
        private static Text Label(string name,Transform parent,string text,int size,Vector2 min,Vector2 max)
        {var t=Rect(name,parent,min,max).gameObject.AddComponent<Text>();t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.fontSize=size;t.text=text;t.alignment=TextAnchor.MiddleCenter;t.color=new Color(.94f,.96f,1);t.raycastTarget=false;return t;}
        private static void BuildAudioUI(GameManager game)
        {
            var settings=game.gameObject.AddComponent<ToyAudioSettings>();var panel=Rect("Audio mix",game.UI.SafeArea,new Vector2(.10f,.125f),new Vector2(.9f,.205f));settings.Panel=panel.gameObject;
            settings.Engine=Slider(panel,"MOTOR",.53f,1,.65f);settings.Effects=Slider(panel,"EFEKT",0,.47f,.75f);
            var probe=game.GetComponent<PerformanceProbe>();var r=probe.CaptureButton.GetComponent<RectTransform>();r.anchorMin=new Vector2(.23f,.075f);r.anchorMax=new Vector2(.77f,.115f);r.offsetMin=r.offsetMax=Vector2.zero;
        }
        private static Slider Slider(Transform parent,string name,float min,float max,float value)
        {
            var row=Rect(name,parent,new Vector2(0,min),new Vector2(1,max));Label("Label",row,name,18,new Vector2(0,0),new Vector2(.25f,1));
            var track=Rect("Slider",row,new Vector2(.29f,.3f),new Vector2(.98f,.7f));var slider=track.gameObject.AddComponent<Slider>();var image=track.gameObject.AddComponent<Image>();image.color=new Color(.13f,.23f,.27f,.95f);
            var handle=Rect("Handle",track,Vector2.zero,Vector2.one);handle.sizeDelta=new Vector2(22,12);var hi=handle.gameObject.AddComponent<Image>();hi.color=new Color(.9f,.75f,.35f);slider.handleRect=handle;slider.targetGraphic=hi;slider.value=value;return slider;
        }
    }
}
