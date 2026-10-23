using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    public static class JoyfulSceneBuilder
    {
        public const string ScenePath="Assets/BrakeToy/Scenes/ToyRoomJoyful.unity";
        [MenuItem("Tools/Brake Toy/Open Joyful Room v0.7")]public static void Open(){if(!File.Exists(ScenePath))Create();else EditorSceneManager.OpenScene(ScenePath);}
        public static void Create()
        {
            if(File.Exists(ScenePath))throw new System.InvalidOperationException("Joyful scene exists; preserve edits.");
            foreach(var dir in new[]{"Meshes","Materials","Prefabs"})Directory.CreateDirectory(JoyfulArt.Root+dir);AssetDatabase.Refresh();JoyfulArt.Palette();
            var scene=EditorSceneManager.OpenScene(ReviewSceneBuilder.ScenePath);EditorSceneManager.SaveScene(scene,ScenePath);
            var g=Object.FindFirstObjectByType<GameManager>();g.name="Brake Toy — Joyful Room v0.7";var config=Object.Instantiate(g.Config);config.name="JoyfulConfig";config.CameraPitch=21;config.CameraSidePadding=2.5f;config.CameraDepthScale=1.15f;config.PlayerViewportY=.23f;AssetDatabase.CreateAsset(config,"Assets/BrakeToy/ScriptableObjects/JoyfulConfig.asset");g.Config=config;
            var camera=Camera.main.GetComponent<PortraitCamera>();camera.Config=config;camera.FollowAmount=.16f;camera.Reframe();
            g.Arcade.Joyful=true;g.Arcade.DistanceRecordKey="BrakeToy.v07.distance";g.Arcade.StyleRecordKey="BrakeToy.v07.style";g.Arcade.BuildCatalogPool=true;
            var run=g.gameObject.AddComponent<JoyfulRun>();var ramps=JoyfulArt.Group("Reusable book ramps");var master=JoyfulArt.Ramp(ramps);run.Ramps=new BookRamp[6];run.Ramps[0]=master;for(int i=1;i<6;i++)run.Ramps[i]=Object.Instantiate(master,ramps);
            var catalog=new ObstacleDefinition[7];var staging=JoyfulArt.Group("Joyful authoring staging");
            for(int i=0;i<7;i++)
            {
                var h=JoyfulArt.Group("Joyful obstacle "+i,staging).gameObject.AddComponent<ArcadeHazard>();Transform model;
                if(i==3)model=JoyfulArt.Ball(h.transform);
                else{model=i==0?JoyfulArt.Robot(h.transform):i==1?JoyfulArt.Bear(h.transform,true):i==2?JoyfulArt.Duck(h.transform):i==4?JoyfulArt.Bear(h.transform):i==5?JoyfulArt.Robot(h.transform):JoyfulArt.Bus(h.transform);JoyfulArt.Combine(model,"ToyObstacle"+i);}
                h.Model=model;h.RollingBall=i==3;h.HalfWidth=i==3?.48f:i==6?.9f:i==2?.56f:.63f;h.HalfLength=i==3?.48f:i==6?.60f:.45f;h.Height=i==3?.96f:i==6?.96f:i==2?.98f:1.57f;
                var definition=ScriptableObject.CreateInstance<ObstacleDefinition>();definition.Kind=i;definition.PoolSize=i==3?24:12;definition.HalfWidth=h.HalfWidth;definition.HalfLength=h.HalfLength;definition.ReadabilityNotes="Joyful-r2 swept XZ with height check; decoration has no collider.";
                definition.Prefab=PrefabUtility.SaveAsPrefabAsset(h.gameObject,JoyfulArt.Root+"Prefabs/Toy_"+i+".prefab").GetComponent<ArcadeHazard>();AssetDatabase.CreateAsset(definition,JoyfulArt.Root+"Prefabs/Definition_"+i+".asset");catalog[i]=definition;
            }
            Object.DestroyImmediate(staging.gameObject);g.Arcade.Definitions=catalog;JoyfulRoom.Build(g);
            var music=g.gameObject.AddComponent<ToyMusic>();music.MenuTrack=Clip("menu-leo-loop.ogg");music.RaceTrack=Clip("race-pixel-sprinter.ogg");g.Arcade.Sound.RecordedHorn=Clip("toy-horn.wav");g.Arcade.Sound.RecordedToyCrash=Clip("plastic-toy-clatter.ogg");AudioUI(g);
            foreach(string path in Directory.GetFiles("Assets/BrakeToy/Audio/Joyful").Where(p=>p.EndsWith(".ogg")||p.EndsWith(".wav")||p.EndsWith(".mp3")))
            {var importer=(AudioImporter)AssetImporter.GetAtPath(path.Replace('\\','/'));var settings=importer.defaultSampleSettings;settings.loadType=AudioClipLoadType.DecompressOnLoad;settings.compressionFormat=AudioCompressionFormat.Vorbis;settings.quality=.75f;settings.preloadAudioData=true;importer.defaultSampleSettings=settings;importer.SaveAndReimport();}
            PlayerSettings.bundleVersion="0.7.0";PlayerSettings.Android.bundleVersionCode=7;EditorBuildSettings.scenes=new[]{new EditorBuildSettingsScene(ScenePath,true)}.Concat(EditorBuildSettings.scenes).ToArray();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();JoyfulUpgrade.Apply();JoyfulPolish.Apply();Debug.Log("Joyful Room generated; real render and gameplay acceptance still required.");
        }
        private static AudioClip Clip(string name)=>AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/BrakeToy/Audio/Joyful/"+name);
        private static RectTransform Rect(string name,Transform parent,float x0,float y0,float x1,float y1){var r=new GameObject(name,typeof(RectTransform)).GetComponent<RectTransform>();r.SetParent(parent,false);r.anchorMin=new Vector2(x0,y0);r.anchorMax=new Vector2(x1,y1);r.offsetMin=r.offsetMax=Vector2.zero;return r;}
        private static Slider Slider(Transform parent,string label,float bottom)
        {
            var row=Rect(label,parent,0,bottom,1,bottom+.31f);var text=Rect("Explanation",row,0,0,.48f,1).gameObject.AddComponent<Text>();text.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");text.fontSize=24;text.color=new Color(.08f,.18f,.25f);text.text=label;text.alignment=TextAnchor.MiddleLeft;
            var track=Rect("Slider",row,.52f,.33f,.96f,.67f);var bg=track.gameObject.AddComponent<Image>();bg.color=new Color(.62f,.77f,.76f);var slider=track.gameObject.AddComponent<Slider>();var handle=Rect("Handle",track,0,0,1,1);handle.sizeDelta=new Vector2(36,26);var image=handle.gameObject.AddComponent<Image>();image.color=new Color(.96f,.48f,.12f);slider.handleRect=handle;slider.targetGraphic=image;slider.value=.65f;return slider;
        }
        private static void AudioUI(GameManager game)
        {var settings=game.GetComponent<ToyAudioSettings>();foreach(Transform child in settings.Panel.transform.Cast<Transform>().ToArray())Object.DestroyImmediate(child.gameObject);settings.Engine=Slider(settings.Panel.transform,"MOTOR\nUğultu ve lastik",.67f);settings.Effects=Slider(settings.Panel.transform,"EFEKTLER\nÇarpma, korna, yıldız",.335f);settings.Music=Slider(settings.Panel.transform,"MÜZİK\nMenü ve yarış",0);}
    }
}
