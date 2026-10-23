using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    public static class JoyfulPolish
    {
        public static void Apply()
        {
            var scene=EditorSceneManager.OpenScene(JoyfulSceneBuilder.ScenePath);var game=Object.FindFirstObjectByType<GameManager>();JoyfulArt.LoadPalette();
            if(GameObject.Find("Joyful final room dressing")){FinalizeDepth(game);return;}
            // Only this scene receives cloned meshes; historical source meshes stay immutable.
            var old=GameObject.Find("Bedroom Set");int part=0;
            foreach(var f in old.GetComponentsInChildren<MeshFilter>())
            {
                if(f.name.Contains("• Wall")){f.GetComponent<Renderer>().enabled=false;continue;}
                var vertices=f.sharedMesh.vertices;bool changed=false;var mat=f.GetComponent<Renderer>().sharedMaterial;bool curtain=mat&&mat.name.Contains("CurtainFabric");
                for(int i=0;i<vertices.Length;i++){Vector3 world=f.transform.TransformPoint(vertices[i]);if(world.z>30&&(world.y>3.5f||curtain)){world.z+=20;vertices[i]=f.transform.InverseTransformPoint(world);changed=true;}}
                if(changed){var mesh=Object.Instantiate(f.sharedMesh);mesh.vertices=vertices;mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,JoyfulArt.Root+"Meshes/DeepRoom_"+part+++".asset");f.sharedMesh=mesh;}
            }
            var outdoor=GameObject.Find("Window outdoor diorama");outdoor.transform.position+=Vector3.forward*20;
            var root=JoyfulArt.Group("Joyful final room dressing");var wall=new Material(JoyfulArt.M("Mint")){name="Pastel blue wall"};wall.SetColor("_BaseColor",new Color(.43f,.68f,.79f));wall.SetFloat("_Smoothness",.10f);AssetDatabase.CreateAsset(wall,JoyfulArt.Root+"Materials/PastelWall.mat");
            void Wall(string name,Vector3 pos,Vector3 size)=>LivingMeshFactory.Box(name,root,pos,size,wall,.005f);
            Wall("Back wall left",new Vector3(-15.3f,5,52),new Vector3(9.4f,24,.4f));Wall("Back wall right",new Vector3(8.2f,5,52),new Vector3(23.6f,24,.4f));Wall("Below window",new Vector3(-7.1f,-1.5f,52),new Vector3(7,11,.4f));Wall("Above window",new Vector3(-7.1f,14,52),new Vector3(7,6,.4f));Wall("Left wall",new Vector3(-20,5,15),new Vector3(.4f,24,74));Wall("Right wall",new Vector3(20,5,15),new Vector3(.4f,24,74));
            JoyfulArt.B("Cream dado rail",root,new Vector3(0,1.8f,51.73f),new Vector3(39,.15f,.16f),"Cream",.03f);JoyfulArt.B("Low warm skirting",root,new Vector3(0,-6.5f,51.7f),new Vector3(39,.7f,.2f),"Cream",.05f);
            for(int i=0;i<11;i++)
            {
                float x=-17+i*3.4f,y=12.8f-Mathf.Sin(i/10f*Mathf.PI)*1.8f;JoyfulArt.Tube("Bunting cord",root,new[]{new Vector3(x,y,51.65f),new Vector3(x+3.4f,12.8f-Mathf.Sin((i+1)/10f*Mathf.PI)*1.8f,51.65f)},.025f,"Cream");
                var flag=new Mesh();flag.vertices=new[]{new Vector3(x+.3f,y,51.61f),new Vector3(x+2.5f,y-.2f,51.61f),new Vector3(x+1.4f,y-1.7f,51.61f)};flag.triangles=new[]{0,1,2};flag.RecalculateNormals();LivingMeshFactory.MeshObject("Cloth pennant",root,flag,JoyfulArt.M(i%3==0?"Rose":i%3==1?"Mango":"Blue"),Vector3.zero);
            }
            var mobile=JoyfulArt.Group("Hanging solar system mobile",root,new Vector3(5,11,23));JoyfulArt.Tube("Wooden mobile arch",mobile,new[]{new Vector3(-3,1,0),new Vector3(0,1.8f,0),new Vector3(3,1,0)},.045f,"Cream");
            for(int i=0;i<5;i++){float x=-2.7f+i*1.35f,y=-.5f-Mathf.Sin(i)*.8f;JoyfulArt.Tube("Fine hanging thread",mobile,new[]{new Vector3(x,1.3f,0),new Vector3(x,y,0)},.012f,"Cream");JoyfulArt.S("Toy planet",mobile,new Vector3(x,y,0),Vector3.one*(i==2?1.0f:.55f),i%3==0?"Rose":i%3==1?"Blue":"Mango");if(i==2){var ring=new Vector3[49];for(int j=0;j<49;j++){float a=j*Mathf.PI*2/48;ring[j]=new Vector3(x+Mathf.Cos(a)*.85f,y+Mathf.Sin(a)*.20f,Mathf.Sin(a)*.85f);}JoyfulArt.Tube("Saturn ring",mobile,ring,.055f,"Cream");}}
            JoyfulArt.Combine(root,"JoyfulFinalRoom");
            // Keep decorative radio within the painted table shoulder, outside driving collision space.
            var addition=GameObject.Find("Joyful room additions — decoration only");
            foreach(var f in addition.GetComponentsInChildren<MeshFilter>())
            {var v=f.sharedMesh.vertices;bool changed=false;for(int i=0;i<v.Length;i++){var p=v[i];if(p.x< -2.8f&&p.x> -6.2f&&p.y>0&&p.y<3&&p.z>22.5f&&p.z<25.5f){v[i]=new Vector3(-4.5f+(p.x+4.4f)*.65f,p.y*.65f,24+(p.z-24)*.65f);changed=true;}}if(changed){var mesh=Object.Instantiate(f.sharedMesh);mesh.vertices=v;mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,JoyfulArt.Root+"Meshes/RadioShoulder_"+part+++".asset");f.sharedMesh=mesh;}}
            Surface();EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();Debug.Log("Room enlarged for 1.5s visual lead at max speed; wall/mobile/table surface polished.");
        }
        private static void FinalizeDepth(GameManager game)
        {
            var outdoor=GameObject.Find("Window outdoor diorama");float delta=52.40f-outdoor.transform.position.z;
            if(Mathf.Abs(delta)<.01f)return;
            foreach(string name in new[]{"Bedroom Set","Joyful final room dressing"})foreach(var f in GameObject.Find(name).GetComponentsInChildren<MeshFilter>())
            {var v=f.sharedMesh.vertices;bool changed=false;for(int i=0;i<v.Length;i++){var p=f.transform.TransformPoint(v[i]);if(p.z>46){p.z+=delta;v[i]=f.transform.InverseTransformPoint(p);changed=true;}}if(changed){var mesh=Object.Instantiate(f.sharedMesh);mesh.vertices=v;mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,AssetDatabase.GenerateUniqueAssetPath(JoyfulArt.Root+"Meshes/FinalDepth.asset"));f.sharedMesh=mesh;}}
            outdoor.transform.position+=Vector3.forward*delta;EditorSceneManager.MarkSceneDirty(game.gameObject.scene);EditorSceneManager.SaveScene(game.gameObject.scene);AssetDatabase.SaveAssets();
        }
        private static void Surface()
        {
            Directory.CreateDirectory(JoyfulArt.Root+"Textures");var tex=new Texture2D(128,128,TextureFormat.RGBA32,false);for(int y=0;y<128;y++)for(int x=0;x<128;x++){float p=Mathf.PerlinNoise(x*.35f,y*.35f),n=.94f+p*.06f;tex.SetPixel(x,y,new Color(n,n,n,1));}tex.Apply();string path=JoyfulArt.Root+"Textures/playmat-grain.png";File.WriteAllBytes(path,tex.EncodeToPNG());Object.DestroyImmediate(tex);AssetDatabase.ImportAsset(path);var importer=(TextureImporter)AssetImporter.GetAtPath(path);importer.wrapMode=TextureWrapMode.Repeat;importer.mipmapEnabled=true;importer.SaveAndReimport();var mint=JoyfulArt.M("Mint");mint.SetTexture("_BaseMap",AssetDatabase.LoadAssetAtPath<Texture2D>(path));mint.SetTextureScale("_BaseMap",new Vector2(12,20));mint.SetFloat("_Smoothness",.22f);mint.SetColor("_BaseColor",new Color(.13f,.65f,.57f));EditorUtility.SetDirty(mint);
        }
    }
}
