using System.IO;
using System.Linq;
using BrakeToy.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    public static class ReviewSceneBuilder
    {
        public const string ScenePath="Assets/BrakeToy/Scenes/ToyRoomReview.unity";
        private const string Root="Assets/BrakeToy/Art/Review/";
        [MenuItem("Tools/Brake Toy/Open Review Candidate v0.6")]
        public static void Open(){if(!File.Exists(ScenePath))Create();else EditorSceneManager.OpenScene(ScenePath);}
        public static void Create()
        {
            if(File.Exists(ScenePath)){Debug.Log("Review scene already exists; preserved.");return;}
            foreach(var d in new[]{"Vehicles","Meshes","Materials"})Directory.CreateDirectory(Root+d);AssetDatabase.Refresh();
            var scene=EditorSceneManager.OpenScene(LivingSceneBuilder.ScenePath);EditorSceneManager.SaveScene(scene,ScenePath);
            var g=Object.FindFirstObjectByType<GameManager>();g.name="Brake Toy — Complete Review Candidate";
            foreach(var root in scene.GetRootGameObjects())foreach(var t in root.GetComponentsInChildren<Transform>(true))if(t&&PrefabUtility.IsAnyPrefabInstanceRoot(t.gameObject))PrefabUtility.UnpackPrefabInstance(t.gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
            var session=g.gameObject.AddComponent<ReviewSession>();g.Review=session;g.gameObject.AddComponent<GhostReplay>();g.gameObject.AddComponent<ReviewFrontend>();ConfigureOnline(g);
            var config=Object.Instantiate(g.Config);config.name="ReviewConfig";AssetDatabase.CreateAsset(config,"Assets/BrakeToy/ScriptableObjects/ReviewConfig.asset");g.Config=config;Camera.main.GetComponent<PortraitCamera>().Config=config;Camera.main.cullingMask&=~(1<<30);
            string[] names={"Sparky","Bubble Bug","Mini Muscle","Candy Cruiser","Pixel Kart","Rocket Pop","Thunder Volt","Galaxy Glider","Golden Comet","Crown Racer"};
            Color[] colors={new Color(.78f,.025f,.025f),new Color(.03f,.66f,.65f),new Color(.95f,.29f,.035f),new Color(.95f,.24f,.48f),new Color(.30f,.33f,.85f),new Color(.9f,.22f,.10f),new Color(.17f,.75f,.30f),new Color(.20f,.05f,.43f),new Color(.8f,.52f,.09f),new Color(.10f,.17f,.24f)};
            float[] width={1,.95f,1,.91f,.88f,.85f,.98f,.92f,1,1};float[] length={1,.87f,1,.95f,.84f,1,.96f,.94f,1,1};float[] roof={1,1.08f,.83f,1.04f,.52f,.74f,.70f,.97f,.89f,1.03f};
            var template=g.Player.Visual;session.Visuals=new Transform[10];session.Vehicles=new VehicleDefinition[10];
            for(int i=0;i<10;i++)
            {
                var visual=Object.Instantiate(template.gameObject,g.Player.transform,false).transform;visual.name=names[i]+" visual";
                var enamel=new Material(AssetDatabase.LoadAssetAtPath<Material>("Assets/BrakeToy/Art/Living/Materials/Enamel.mat"));enamel.name=names[i]+" enamel";enamel.SetColor("_BaseColor",colors[i]);enamel.SetFloat("_Smoothness",i==8?.75f:.48f);AssetDatabase.CreateAsset(enamel,Root+"Materials/Car_"+i+".mat");
                int part=0;
                foreach(var f in visual.GetComponentsInChildren<MeshFilter>())
                {
                    bool wheel=f.transform.parent&&f.transform.parent.name.StartsWith("Wheel");if(wheel)continue;
                    var mesh=Object.Instantiate(f.sharedMesh);var vertices=mesh.vertices;
                    for(int j=0;j<vertices.Length;j++)
                    {var v=vertices[j];v.x*=width[i];v.z*=length[i];if(v.y>.355f)v.y=.355f+(v.y-.355f)*roof[i];if(i==2&&v.z>.2f&&v.y>.31f)v.y+=.025f;if(i==4)v.x=Mathf.Sign(v.x)*Mathf.Pow(Mathf.Abs(v.x)/.38f,.85f)*.38f;vertices[j]=v;}
                    mesh.vertices=vertices;mesh.RecalculateNormals();mesh.RecalculateTangents();mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,Root+"Meshes/Car_"+i+"_"+part+++".asset");f.sharedMesh=mesh;
                    var renderer=f.GetComponent<Renderer>();if(renderer.sharedMaterial.name=="Enamel")renderer.sharedMaterial=enamel;
                }
                if(i==5||i==6||i==9)
                {foreach(float sign in new[]{-1f,1f})LivingMeshFactory.Box("Signature rear fin",visual,new Vector3(sign*.22f,.39f,-.51f),new Vector3(.025f,i==9?.14f:.10f,.18f),enamel,.006f);}
                if(i==1||i==3)LivingMeshFactory.Sphere("Rounded nose badge",visual,new Vector3(0,.30f,.65f*length[i]),new Vector3(.10f,.08f,.018f),enamel);
                foreach(var f in visual.GetComponentsInChildren<MeshFilter>())if(!EditorUtility.IsPersistent(f.sharedMesh)){var mesh=f.sharedMesh;AssetDatabase.CreateAsset(mesh,Root+"Meshes/Car_"+i+"_"+part+++".asset");}
                var d=ScriptableObject.CreateInstance<VehicleDefinition>();d.Title=names[i];d.Cost=CareerRules.Costs[i];d.Silhouette="width="+width[i]+" length="+length[i]+" canopy="+roof[i];d.TrailColor=colors[i];
                var prefab=PrefabUtility.SaveAsPrefabAsset(visual.gameObject,Root+"Vehicles/"+names[i]+".prefab");d.VisualPrefab=prefab;AssetDatabase.CreateAsset(d,Root+"Vehicles/"+names[i]+".asset");session.Visuals[i]=visual;session.Vehicles[i]=d;visual.gameObject.SetActive(i==0);
            }
            Object.DestroyImmediate(template.gameObject);g.Player.Visual=session.Visuals[0];g.Player.GetComponent<ToyWheelAnimation>().Wheels=session.Visuals[0].Cast<Transform>().Where(t=>t.name.StartsWith("Wheel")).ToArray();
            var ghostMat=new Material(Shader.Find("Universal Render Pipeline/Unlit")){name="Ghost shader reference"};AssetDatabase.CreateAsset(ghostMat,Root+"Materials/Ghost.mat");g.GetComponent<GhostReplay>().GhostMaterial=ghostMat;
            PlayerSettings.bundleVersion="0.6.0";PlayerSettings.Android.bundleVersionCode=6;
            EditorBuildSettings.scenes=new[]{ScenePath,LivingSceneBuilder.ScenePath,RefinedSceneBuilder.ScenePath,WorkshopSceneBuilder.ScenePath,TabletopSceneBuilder.ScenePath,"Assets/BrakeToy/Scenes/ToyRoom.unity"}.Select(p=>new EditorBuildSettingsScene(p,true)).ToArray();
            EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();Debug.Log("Review candidate created with 10 vehicle definitions, career, Relax, menus and ghost.");
        }
        public static void UpgradeOnline(){var scene=EditorSceneManager.OpenScene(ScenePath);ConfigureOnline(Object.FindFirstObjectByType<GameManager>());EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();}
        private static void ConfigureOnline(GameManager game)
        {var online=game.GetComponent<ReviewOnline>();if(!online)online=game.gameObject.AddComponent<ReviewOnline>();var config=AssetDatabase.LoadAssetAtPath<OnlineConfiguration>("Assets/BrakeToy/ScriptableObjects/OnlineConfiguration.asset");if(!config){config=ScriptableObject.CreateInstance<OnlineConfiguration>();AssetDatabase.CreateAsset(config,"Assets/BrakeToy/ScriptableObjects/OnlineConfiguration.asset");}online.Configuration=config;}
    }
}
