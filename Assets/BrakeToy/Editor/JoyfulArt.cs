using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    internal static class JoyfulArt
    {
        public const string Root="Assets/BrakeToy/Art/Joyful/";
        private static readonly Dictionary<string,Material> materials=new Dictionary<string,Material>();
        public static void Palette()
        {
            materials.Clear();New("Cream",new Color(1,.91f,.67f));New("Mint",new Color(.13f,.73f,.66f));New("Blue",new Color(.10f,.47f,.91f));New("Rose",new Color(.97f,.22f,.40f));New("Mango",new Color(1,.61f,.065f));New("Purple",new Color(.47f,.28f,.84f));New("White",new Color(.96f,.97f,1));New("Ink",new Color(.028f,.055f,.10f));New("Brown",new Color(.46f,.24f,.10f));New("Skin",new Color(1,.70f,.47f));New("Hair",new Color(.18f,.065f,.025f));New("Green",new Color(.19f,.95f,.21f));
            M("Green").EnableKeyword("_EMISSION");M("Green").SetColor("_EmissionColor",new Color(.05f,.45f,.015f));
            M("Ink").SetFloat("_Smoothness",.65f);M("Mint").SetFloat("_Smoothness",.40f);
        }
        private static void New(string name,Color color){var m=new Material(Shader.Find("Universal Render Pipeline/Lit")){name="Joyful "+name};m.SetColor("_BaseColor",color);m.SetFloat("_Smoothness",.30f);AssetDatabase.CreateAsset(m,Root+"Materials/"+name+".mat");materials[name]=m;}
        public static Material M(string name)=>materials[name];
        public static void LoadPalette(){materials.Clear();foreach(string name in new[]{"Cream","Mint","Blue","Rose","Mango","Purple","White","Ink","Brown","Skin","Hair","Green"})materials[name]=AssetDatabase.LoadAssetAtPath<Material>(Root+"Materials/"+name+".mat");}
        public static Transform Group(string name,Transform parent=null,Vector3 p=default){var t=new GameObject(name).transform;t.SetParent(parent,false);t.localPosition=p;return t;}
        public static void B(string name,Transform p,Vector3 pos,Vector3 size,string mat,float radius=.06f)=>LivingMeshFactory.Box(name,p,pos,size,M(mat),radius);
        public static GameObject S(string name,Transform p,Vector3 pos,Vector3 size,string mat)=>LivingMeshFactory.Sphere(name,p,pos,size,M(mat));
        public static void Tube(string name,Transform p,Vector3[] path,float radius,string mat)=>LivingMeshFactory.Tube(name,p,path,radius,M(mat),10);
        public static void Combine(Transform root,string name)
        {
            var filters=root.GetComponentsInChildren<MeshFilter>().Where(f=>f.GetComponent<Renderer>().sharedMaterials.Length==1&&(!f.transform.parent||!f.transform.parent.name.StartsWith("Wheel unit"))).ToArray();int part=0;
            foreach(var group in filters.GroupBy(f=>f.GetComponent<Renderer>().sharedMaterial))
            {var mesh=new Mesh{name=name,indexFormat=IndexFormat.UInt32};mesh.CombineMeshes(group.Select(f=>new CombineInstance{mesh=f.sharedMesh,transform=root.worldToLocalMatrix*f.transform.localToWorldMatrix}).ToArray(),true,true);mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,AssetDatabase.GenerateUniqueAssetPath(Root+"Meshes/"+name+"_"+part+++".asset"));LivingMeshFactory.MeshObject(name+" • "+group.Key.name,root,mesh,group.Key,Vector3.zero);}
            foreach(var f in filters)if(f)Object.DestroyImmediate(f.gameObject);
        }
        public static Transform Bear(Transform parent,bool doll=false)
        {
            var t=Group(doll?"Lily rag doll":"Honey teddy",parent);string body=doll?"Rose":"Mango";
            S("Soft pear body",t,new Vector3(0,.52f,0),new Vector3(.72f,.86f,.55f),body);S("Cream bib",t,new Vector3(0,.51f,-.24f),new Vector3(.43f,.48f,.10f),"Cream");
            S("Head",t,new Vector3(0,1.04f,-.03f),new Vector3(.66f,.62f,.57f),doll?"Skin":"Mango");
            for(int side=-1;side<=1;side+=2)
            {
                S("Seated foot",t,new Vector3(side*.28f,.16f,-.12f),new Vector3(.38f,.29f,.52f),doll?"Purple":"Mango");var arm=S("Soft arm",t,new Vector3(side*.39f,.58f,-.03f),new Vector3(.27f,.62f,.29f),body);arm.transform.localRotation=Quaternion.Euler(-12,0,side*25);
                S("Eye",t,new Vector3(side*.135f,1.09f,-.294f),new Vector3(.075f,.10f,.035f),"Ink");S("Eye glint",t,new Vector3(side*.13f,1.115f,-.312f),Vector3.one*.022f,"White");S("Cheek",t,new Vector3(side*.23f,.97f,-.26f),new Vector3(.11f,.06f,.035f),"Rose");
                if(!doll){S("Round ear",t,new Vector3(side*.29f,1.28f,0),Vector3.one*.28f,"Mango");S("Ear lining",t,new Vector3(side*.29f,1.28f,-.10f),new Vector3(.17f,.17f,.05f),"Cream");}
                else for(int i=0;i<6;i++)S("Braided yarn hair",t,new Vector3(side*(.26f+.025f*i),1.21f-i*.09f,.02f),new Vector3(.20f,.18f,.23f),"Hair");
            }
            if(doll){S("Yarn cap",t,new Vector3(0,1.27f,.035f),new Vector3(.64f,.23f,.52f),"Hair");S("Nose",t,new Vector3(0,1,-.32f),Vector3.one*.065f,"Skin");}
            else{S("Muzzle",t,new Vector3(0,.97f,-.27f),new Vector3(.32f,.23f,.18f),"Cream");S("Nose",t,new Vector3(0,1.02f,-.366f),new Vector3(.105f,.07f,.055f),"Ink");}
            Tube("Smile embroidery",t,new[]{new Vector3(-.09f,.94f,-.315f),new Vector3(0,.91f,-.34f),new Vector3(.09f,.94f,-.315f)},.012f,"Brown");
            B("Bow tie",t,new Vector3(0,.76f,-.285f),new Vector3(.27f,.10f,.045f),"Blue",.02f);for(int i=0;i<2;i++)S("Sewn button",t,new Vector3(0,.55f-i*.16f,-.305f),Vector3.one*.055f,"Blue");return t;
        }
        public static Transform Robot(Transform parent)
        {
            var t=Group("Buddy wind-up robot",parent);B("Rounded enamel body",t,new Vector3(0,.53f,0),new Vector3(.78f,.72f,.58f),"Mint",.13f);B("Head shell",t,new Vector3(0,1.04f,0),new Vector3(.70f,.48f,.55f),"Blue",.11f);B("Face glass",t,new Vector3(0,1.04f,-.286f),new Vector3(.55f,.27f,.035f),"Ink",.07f);
            for(int s=-1;s<=1;s+=2){S("Lens eye",t,new Vector3(s*.14f,1.05f,-.31f),new Vector3(.10f,.12f,.03f),"Cream");B("Boot",t,new Vector3(s*.23f,.09f,-.06f),new Vector3(.34f,.18f,.48f),"Purple");S("Arm joint",t,new Vector3(s*.45f,.63f,0),Vector3.one*.26f,"Cream");B("Toy hand",t,new Vector3(s*.48f,.41f,-.02f),new Vector3(.20f,.32f,.26f),"Mango");}
            for(int i=0;i<3;i++)S("Control button",t,new Vector3((i-1)*.19f,.62f,-.30f),Vector3.one*.10f,i==0?"Rose":i==1?"Mango":"White");B("Battery hatch",t,new Vector3(0,.35f,-.30f),new Vector3(.43f,.16f,.025f),"Cream",.02f);Tube("Antenna",t,new[]{new Vector3(0,1.28f,0),new Vector3(.10f,1.47f,0)},.025f,"Cream");S("Antenna tip",t,new Vector3(.10f,1.49f,0),Vector3.one*.13f,"Rose");return t;
        }
        public static Transform Rocket(Transform parent)
        {
            var t=Group("Wind-up space rocket",parent);LivingMeshFactory.Lathe("Smooth rocket shell",t,Vector3.zero,new[]{new Vector2(0,.12f),new Vector2(.27f,.12f),new Vector2(.34f,.24f),new Vector2(.34f,.95f),new Vector2(.29f,1.2f),new Vector2(.15f,1.48f),new Vector2(0,1.60f)},M("White"),48);
            S("Porthole trim",t,new Vector3(0,.98f,-.315f),new Vector3(.35f,.35f,.09f),"Mango");S("Porthole glass",t,new Vector3(0,.98f,-.365f),new Vector3(.24f,.24f,.035f),"Blue");S("Porthole glint",t,new Vector3(-.06f,1.04f,-.386f),new Vector3(.06f,.08f,.01f),"Cream");
            for(int i=0;i<3;i++){float a=i*Mathf.PI*2/3;var fin=Group("Landing fin",t,new Vector3(Mathf.Cos(a)*.29f,.27f,Mathf.Sin(a)*.29f));fin.localRotation=Quaternion.Euler(0,-a*Mathf.Rad2Deg,20);B("Rounded rocket fin",fin,Vector3.zero,new Vector3(.18f,.48f,.30f),i==0?"Rose":i==1?"Mint":"Blue",.04f);}return t;
        }
        public static Transform Duck(Transform parent)
        {
            var t=Group("Rubber duck",parent);S("Body",t,new Vector3(0,.28f,.02f),new Vector3(1.05f,.56f,.85f),"Mango");S("Head",t,new Vector3(0,.69f,-.19f),Vector3.one*.55f,"Mango");S("Beak",t,new Vector3(0,.60f,-.48f),new Vector3(.37f,.14f,.33f),"Rose");for(int s=-1;s<=1;s+=2){S("Wing",t,new Vector3(s*.42f,.32f,0),new Vector3(.25f,.25f,.52f),"Cream");S("Eye",t,new Vector3(s*.16f,.75f,-.41f),Vector3.one*.075f,"Ink");}return t;
        }
        public static Transform Bus(Transform parent)
        {
            var t=Group("Sunny toy bus",parent);B("Diecast lower shell",t,new Vector3(0,.32f,0),new Vector3(1.70f,.43f,.87f),"Mango",.12f);B("Roof cab",t,new Vector3(0,.66f,0),new Vector3(1.56f,.50f,.82f),"Rose",.13f);B("Cream roof",t,new Vector3(0,.91f,0),new Vector3(1.53f,.06f,.81f),"Cream",.03f);
            for(int i=0;i<5;i++){B("Side window",t,new Vector3(-.60f+i*.30f,.69f,-.414f),new Vector3(.23f,.24f,.025f),"Ink",.035f);B("Window reflection",t,new Vector3(-.63f+i*.30f,.75f,-.432f),new Vector3(.07f,.025f,.012f),"White",.005f);}
            for(int x=-1;x<=1;x+=2)for(int z=-1;z<=1;z+=2){var wheel=Group("Wheel unit",t,new Vector3(x*.55f,.18f,z*.44f));S("Rubber tire",wheel,Vector3.zero,new Vector3(.36f,.36f,.12f),"Ink");S("Wheel hub",wheel,new Vector3(0,0,z*.065f),new Vector3(.18f,.18f,.025f),"White");S("Rotation marker",wheel,new Vector3(.085f,.08f,z*.083f),Vector3.one*.04f,"Mango");}
            B("Bumper",t,new Vector3(0,.24f,-.46f),new Vector3(1.55f,.09f,.06f),"White",.03f);for(int side=-1;side<=1;side+=2)S("Headlamp",t,new Vector3(side*.74f,.37f,-.45f),new Vector3(.10f,.10f,.035f),"Cream");return t;
        }
        public static Transform Ball(Transform parent)
        {
            var t=Group("Beach ball — center pivot",parent,new Vector3(0,.48f,0));const int slices=48,rings=24;var vertices=new List<Vector3>();var normals=new List<Vector3>();var uv=new List<Vector2>();var triangles=new List<int>[6];for(int k=0;k<6;k++)triangles[k]=new List<int>();
            for(int r=0;r<=rings;r++)for(int s=0;s<=slices;s++){float a=r*Mathf.PI/rings,b=s*Mathf.PI*2/slices;var n=new Vector3(Mathf.Sin(a)*Mathf.Cos(b),Mathf.Cos(a),Mathf.Sin(a)*Mathf.Sin(b));vertices.Add(n*.48f);normals.Add(n);uv.Add(new Vector2(s/(float)slices,r/(float)rings));if(r<rings&&s<slices){int c=r*(slices+1)+s,d=c+slices+1;triangles[s/8].AddRange(new[]{c,c+1,d,c+1,d+1,d});}}
            var mesh=new Mesh{name="Stitched six panel play ball"};mesh.SetVertices(vertices);mesh.SetNormals(normals);mesh.SetUVs(0,uv);mesh.subMeshCount=6;for(int i=0;i<6;i++)mesh.SetTriangles(triangles[i],i);mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,Root+"Meshes/BeachBall.asset");var ball=LivingMeshFactory.MeshObject("Six vinyl panels",t,mesh,M("White"),Vector3.zero);ball.GetComponent<Renderer>().sharedMaterials=new[]{M("Rose"),M("Cream"),M("Blue"),M("White"),M("Mango"),M("Mint")};
            for(int s=0;s<6;s++){var path=new Vector3[25];float b=s*Mathf.PI/3;for(int r=0;r<25;r++){float a=r*Mathf.PI/24;path[r]=new Vector3(Mathf.Sin(a)*Mathf.Cos(b),Mathf.Cos(a),Mathf.Sin(a)*Mathf.Sin(b))*.483f;}Tube("Raised vinyl seam",t,path,.006f,"White");}Combine(t,"BallSeams");return t;
        }
        public static BookRamp Ramp(Transform parent)
        {
            var t=Group("Book jump — green arrow",parent);var ramp=t.gameObject.AddComponent<BookRamp>();B("Stack lower book",t,new Vector3(0,.17f,.35f),new Vector3(1.92f,.34f,1.25f),"Blue",.055f);B("Paper pages",t,new Vector3(0,.36f,.35f),new Vector3(1.78f,.10f,1.15f),"Cream",.025f);
            var cover=Group("Inclined book cover",t,new Vector3(0,.296f,-.85f));cover.localRotation=Quaternion.Euler(-11.15f,0,0);B("Hardback launch surface",cover,Vector3.zero,new Vector3(1.90f,.09f,3.36f),"Purple",.035f);B("Inset binding",cover,new Vector3(-.78f,.055f,0),new Vector3(.10f,.015f,3.2f),"Mango",.006f);
            var points=new[]{new Vector3(-.17f,.03f,-4.35f),new Vector3(.17f,.03f,-4.35f),new Vector3(.17f,.03f,-3.55f),new Vector3(.52f,.03f,-3.55f),new Vector3(0,.03f,-2.98f),new Vector3(-.52f,.03f,-3.55f),new Vector3(-.17f,.03f,-3.55f)};var arrow=new Mesh();arrow.vertices=points;arrow.triangles=new[]{0,2,1,0,6,2,6,5,4,6,4,2,2,4,3};arrow.RecalculateNormals();arrow.RecalculateBounds();LivingMeshFactory.MeshObject("GREEN approach arrow",t,arrow,M("Green"),Vector3.zero);Combine(t,"BookRamp");ramp.Release();return ramp;
        }
    }
}
