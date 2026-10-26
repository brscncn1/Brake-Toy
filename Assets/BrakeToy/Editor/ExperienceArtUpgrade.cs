using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BrakeToy.Editor
{
    // Explicit bounded upgrade. Prior prefabs and materials remain available for rollback.
    public static class ExperienceArtUpgrade
    {
        private const string Root="Assets/BrakeToy/Art/Experience/";
        public static void Upgrade()
        {
            if(AssetDatabase.LoadAssetAtPath<GameObject>(Root+"Car-5.prefab"))
                throw new System.InvalidOperationException("Experience art already exists; use UpgradeFinish for the bounded material change.");
            EditorSceneManager.OpenScene(AdventureSceneBuilder.ScenePath);
            var game=Object.FindFirstObjectByType<GameManager>();
            Directory.CreateDirectory(Root); AssetDatabase.Refresh();
            foreach(int index in new[]{5,6,7})UpgradeCar(game,index);
            var finish=game.gameObject.AddComponent<TableFinishSwitch>();
            finish.Surfaces=game.World.FloorChunks.Select(c=>c.Find("Table surface").GetComponent<Renderer>()).ToArray();
            finish.Original=finish.Surfaces.Select(r=>r.sharedMaterial).ToArray();
            var surface=new Material(finish.Original[0]);surface.name="Satin painted birch reference";
            surface.SetColor("_BaseColor",new Color(.40f,.69f,.71f));surface.SetFloat("_Smoothness",.31f);
            surface.SetFloat("_Metallic",0);
            var grain=new Texture2D(256,256,TextureFormat.RGBA32,true){name="Fine painted grain",wrapMode=TextureWrapMode.Repeat};
            for(int y=0;y<256;y++)for(int x=0;x<256;x++)
            {
                float value=.965f+.035f*Mathf.PerlinNoise(x*.025f,y*.3f);
                grain.SetPixel(x,y,new Color(value,value,value,1));
            }
            grain.Apply();AssetDatabase.CreateAsset(grain,Root+"PaintedGrain.asset");
            surface.SetTexture("_BaseMap",grain);surface.SetTextureScale("_BaseMap",new Vector2(5,7));
            AssetDatabase.CreateAsset(surface,Root+"SatinTable.mat");finish.ReferenceFinish=surface;finish.Apply(true);
            PlayerSettings.bundleVersion="0.10.0";PlayerSettings.Android.bundleVersionCode=11;
            EditorSceneManager.MarkSceneDirty(game.gameObject.scene);
            EditorSceneManager.SaveScene(game.gameObject.scene); AssetDatabase.SaveAssets();
        }
        public static void UpgradeFinish()
        {
            EditorSceneManager.OpenScene(AdventureSceneBuilder.ScenePath);
            var game=Object.FindFirstObjectByType<GameManager>();var finish=game.GetComponent<TableFinishSwitch>();
            var originals=finish.Surfaces.Select((r,i)=>new{r,m=finish.Original[i]}).ToDictionary(x=>x.r,x=>x.m);
            finish.Surfaces=game.World.FloorChunks.SelectMany(c=>c.GetComponentsInChildren<Renderer>())
                .Where(r=>r.name=="Table surface"||r.name.Contains("TableInlay")&&r.sharedMaterial.name.Contains("Mint")).ToArray();
            finish.Original=finish.Surfaces.Select(r=>originals.TryGetValue(r,out var m)?m:r.sharedMaterial).ToArray();
            finish.Apply(true);EditorSceneManager.MarkSceneDirty(game.gameObject.scene);
            foreach(int index in new[]{5,6,7})
            {
                var visual=game.Review.Visuals[index];
                foreach(Transform detail in visual)
                {
                    if(detail.name=="Inset front lamp")detail.localPosition=new Vector3(Mathf.Sign(detail.localPosition.x)*.13f,.29f,.43f);
                    if(detail.name=="Tail lamp")detail.localPosition=new Vector3(Mathf.Sign(detail.localPosition.x)*.14f,.29f,-.43f);
                }
                PrefabUtility.SaveAsPrefabAsset(visual.gameObject,Root+"Car-"+index+".prefab");
            }
            EditorSceneManager.SaveScene(game.gameObject.scene);AssetDatabase.SaveAssets();
        }
        private static void UpgradeCar(GameManager game,int index)
        {
            var old=game.Review.Visuals[index];
            var root=new GameObject(game.Review.Vehicles[index].Title+" sculpted reference").transform;
            root.SetParent(old.parent,false);
            var paint=new Material(Shader.Find("Universal Render Pipeline/Lit"));
            paint.color=game.Review.Vehicles[index].TrailColor;paint.SetFloat("_Smoothness",.68f);paint.SetFloat("_Metallic",.12f);
            AssetDatabase.CreateAsset(paint,Root+"Paint-"+index+".mat");
            var dark=new Material(paint);dark.color=new Color(.025f,.06f,.10f);dark.SetFloat("_Smoothness",.82f);
            AssetDatabase.CreateAsset(dark,Root+"Glass-"+index+".mat");
            var trim=new Material(paint);trim.color=new Color(.91f,.85f,.59f);trim.SetFloat("_Metallic",.65f);
            AssetDatabase.CreateAsset(trim,Root+"Trim-"+index+".mat");
            // Closed continuous hull: distinct top and side silhouettes, no floating roof blocks.
            var mesh=LivingMeshFactory.Grid(48,40,(u,v)=>
            {
                float z=(v*2-1)*.64f, a=-u*Mathf.PI*2;
                float taper=Mathf.Sin(v*Mathf.PI);
                float width=index==5?.27f:index==6?Mathf.Lerp(.39f,.24f,v):.39f;
                float h=index==6?Mathf.Lerp(.19f,.08f,v):index==7?.21f:.14f;
                return new Vector3(Mathf.Cos(a)*width*taper,.27f+Mathf.Sin(a)*h*taper,z);
            },Vector2.one);
            mesh.name="Closed sculpted hull "+index;AssetDatabase.CreateAsset(mesh,Root+"Hull-"+index+".asset");
            LivingMeshFactory.MeshObject("Sculpted hull",root,mesh,paint,Vector3.zero);
            if(index==5)
                foreach(float side in new[]{-1f,1f})
                {
                    LivingMeshFactory.Sphere("Integrated rocket pod",root,new Vector3(side*.27f,.32f,-.05f),new Vector3(.25f,.30f,1.05f),paint);
                    LivingMeshFactory.Sphere("Inset nozzle",root,new Vector3(side*.27f,.32f,-.56f),new Vector3(.16f,.19f,.05f),dark);
                    LivingMeshFactory.Box("Nozzle collar",root,new Vector3(side*.27f,.32f,-.51f),new Vector3(.20f,.22f,.06f),trim,.03f);
                }
            LivingMeshFactory.Sphere(index==7?"Panoramic dome":"Swept canopy",root,
                new Vector3(0,index==7?.43f:.37f,index==6?-.17f:-.08f),
                index==7?new Vector3(.53f,.36f,.63f):new Vector3(.38f,.18f,.43f),dark);
            foreach(float side in new[]{-1f,1f})
            {
                LivingMeshFactory.Sphere("Inset front lamp",root,new Vector3(side*.13f,.29f,.43f),new Vector3(.13f,.055f,.06f),trim);
                LivingMeshFactory.Sphere("Tail lamp",root,new Vector3(side*.14f,.29f,-.43f),new Vector3(.12f,.045f,.045f),trim);
                if(index==6)LivingMeshFactory.Box("Side sill",root,new Vector3(side*.32f,.22f,-.02f),new Vector3(.045f,.065f,.76f),trim,.02f);
            }
            foreach(Transform wheel in old)if(wheel.name.StartsWith("Wheel"))
            {var w=Object.Instantiate(wheel,root);w.name=wheel.name;}
            // Save generated detail meshes before writing prefab references.
            int part=0;
            foreach(var f in root.GetComponentsInChildren<MeshFilter>())
                if(!EditorUtility.IsPersistent(f.sharedMesh))
                    AssetDatabase.CreateAsset(f.sharedMesh,Root+"Detail-"+index+"-"+part+++".asset");
            var definition=Object.Instantiate(game.Review.Vehicles[index]);
            definition.VisualPrefab=PrefabUtility.SaveAsPrefabAsset(root.gameObject,Root+"Car-"+index+".prefab");
            AssetDatabase.CreateAsset(definition,Root+"Car-"+index+".asset");
            game.Review.Vehicles[index]=definition;game.Review.Visuals[index]=root;
            root.gameObject.SetActive(false);Object.DestroyImmediate(old.gameObject);
        }
    }
}
