using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;

namespace BrakeToy.Editor
{
    internal static class LivingRoomBuilder
    {
        private static Material M(string name)=>AssetDatabase.LoadAssetAtPath<Material>("Assets/BrakeToy/Art/Living/Materials/"+name+".mat");
        private static Transform Group(string name,Transform parent,Vector3 p=default)
        {var t=new GameObject(name).transform;t.SetParent(parent,false);t.localPosition=p;return t;}
        private static void B(string name,Transform parent,Vector3 p,Vector3 size,string mat,float r=.06f)=>LivingMeshFactory.Box(name,parent,p,size,M(mat),r);
        public static void Build(GameManager game)
        {
            var old=GameObject.Find("Bedroom Set");if(old)Object.DestroyImmediate(old);
            var room=Group("Bedroom Set",null);
            B("Oak floor",room,new Vector3(0,-7.3f,15),new Vector3(40,.5f,70),"Wood");
            // Four flush wall pieces leave a true aperture behind the window.
            B("Wall left",room,new Vector3(-15.3f,5,32),new Vector3(9.4f,24,.4f),"Wall",.005f);
            B("Wall right",room,new Vector3(8.2f,5,32),new Vector3(23.6f,24,.4f),"Wall",.005f);
            B("Wall below window",room,new Vector3(-7.1f,-1.5f,32),new Vector3(7,11,.4f),"Wall",.005f);
            B("Wall above window",room,new Vector3(-7.1f,14,32),new Vector3(7,6,.4f),"Wall",.005f);
            B("Left wall",room,new Vector3(-20,5,9),new Vector3(.4f,24,46),"Wall",.005f);
            B("Right wall",room,new Vector3(20,5,9),new Vector3(.4f,24,46),"Wall",.005f);
            B("Ivory skirting",room,new Vector3(0,-6.5f,31.7f),new Vector3(39,1,.18f),"Ivory");
            for(int i=0;i<9;i++)B("Subtle floor joint",room,new Vector3(-16+i*4,-7.04f,15),new Vector3(.012f,.008f,70),"Ink",.001f);
            Window(room);Bed(room);Shelf(room);Chair(room);Poster(room);Furniture(room);
            foreach(string name in new[]{"Blue star bed","Toy shelf","Poly Haven wooden chair — CC0"})
            {var furniture=room.Find(name);foreach(var renderer in furniture.GetComponentsInChildren<Renderer>())if(renderer.bounds.min.x<6.3f && renderer.bounds.max.x> -6.3f)throw new System.InvalidOperationException("Furniture overlaps the table footprint: "+name);}
            var decor=Decor(room,game.World);
            LivingMeshFactory.Combine(room,"LivingRoom",new[]{decor});
            foreach(var renderer in room.GetComponentsInChildren<Renderer>())if(renderer.sharedMaterial==M("Daylight"))renderer.shadowCastingMode=ShadowCastingMode.Off;
        }
        private static void Window(Transform room)
        {
            var w=Group("Window and curtains",room,new Vector3(-7.1f,7.5f,31.75f));
            B("Window daylight",w,new Vector3(0,0,.38f),new Vector3(7,7,.08f),"Daylight",.01f);
            foreach(float x in new[]{-3.5f,0,3.5f})B("Painted window mullion",w,new Vector3(x,0,-.12f),new Vector3(.16f,7.3f,.34f),"Ivory",.035f);
            foreach(float y in new[]{-3.5f,0,3.5f})B("Painted window rail",w,new Vector3(0,y,-.12f),new Vector3(7.15f,.15f,.34f),"Ivory",.03f);
            B("Deep sill",w,new Vector3(0,-3.66f,-.50f),new Vector3(8.3f,.26f,1.5f),"Ivory",.06f);
            foreach(float x in new[]{-.22f,.22f})B("Window latch",w,new Vector3(x,-.5f,-.34f),new Vector3(.07f,.48f,.07f),"Chrome",.015f);
            LivingMeshFactory.Tube("Brass curtain rail",w,new[]{new Vector3(-6,4,-.4f),new Vector3(6,4,-.4f)},.07f,M("Chrome"));
            foreach(float side in new[]{-1f,1f})
            {
                var cloth=LivingMeshFactory.Grid(64,40,(u,v)=>
                {
                    float width=2.15f+.30f*(1-v),x=side*4.5f+(u-.5f)*width;
                    float z=-.50f+Mathf.Cos(u*Mathf.PI*10)*(.20f+.08f*(1-v))+.05f*Mathf.Sin(v*5+u*7);
                    return new Vector3(x,-5.8f+v*9.6f+.08f*Mathf.Cos(u*Mathf.PI*10)*(1-v),z);
                },new Vector2(2.4f,9));
                LivingMeshFactory.MeshObject("Soft cotton curtain",w,cloth,M("CurtainFabric"),Vector3.zero);
                for(int i=0;i<7;i++)LivingMeshFactory.Sphere("Curtain eyelet",w,new Vector3(side*4.5f+(i/6f-.5f)*2.15f,3.82f,-.45f),new Vector3(.16f,.20f,.06f),M("Chrome"));
            }
            LivingMeshFactory.Lathe("Window ceramic planter",w,new Vector3(-2.1f,-3.54f,-.5f),new[]{new Vector2(0,0),new Vector2(.32f,0),new Vector2(.43f,.65f),new Vector2(.44f,.70f),new Vector2(.37f,.70f),new Vector2(.32f,.10f)},M("Ivory"));
            for(int i=0;i<8;i++)
            {float a=i*2.4f;var leaf=LivingMeshFactory.Sphere("Plant leaf",w,new Vector3(-2.1f+Mathf.Cos(a)*.32f,-2.55f+i%3*.18f,-.5f+Mathf.Sin(a)*.24f),new Vector3(.19f,1,.065f),M("Leaf green"));leaf.transform.localRotation=Quaternion.Euler(20*Mathf.Sin(a),a*Mathf.Rad2Deg,35*Mathf.Cos(a));}
        }
        private static void Bed(Transform room)
        {
            var bed=Group("Blue star bed",room,new Vector3(9.5f,-7,24.5f));bed.localScale=new Vector3(.7f,1,1);
            B("Shaped bed frame",bed,new Vector3(0,2.1f,0),new Vector3(8.1f,.8f,14),"Wood",.22f);
            B("Mattress piping",bed,new Vector3(0,3,0),new Vector3(7.7f,1.2f,13.6f),"PillowFabric",.34f);
            B("Rounded headboard",bed,new Vector3(0,5.1f,6.7f),new Vector3(8.4f,6.5f,.46f),"Wood",.23f);
            foreach(float x in new[]{-3.8f,3.8f})
            {
                LivingMeshFactory.Lathe("Turned bed post",bed,new Vector3(x,0,6.7f),new[]{new Vector2(.26f,0),new Vector2(.26f,1),new Vector2(.20f,1.2f),new Vector2(.20f,7.3f),new Vector2(.30f,7.45f),new Vector2(.25f,7.6f)},M("Wood"),24);
                LivingMeshFactory.Sphere("Bed post cap",bed,new Vector3(x,7.9f,6.7f),Vector3.one*.64f,M("Wood"));
            }
            float BlanketHeight(float x,float z)=>3.82f-.9f*Mathf.SmoothStep(0,1,Mathf.InverseLerp(3.50f,4.2f,Mathf.Abs(x)))-.55f*Mathf.SmoothStep(0,1,Mathf.InverseLerp(-5.6f,-7.1f,z))+.065f*Mathf.Sin(x*3.5f+z*.65f)+.04f*Mathf.Cos(x*5-z*1.5f);
            var blanket=LivingMeshFactory.Grid(72,80,(u,v)=>{float x=Mathf.Lerp(-4.2f,4.2f,u),z=Mathf.Lerp(-7.1f,2.8f,v);return new Vector3(x,BlanketHeight(x,z),z);},new Vector2(7,9));
            LivingMeshFactory.MeshObject("Draped cotton duvet",bed,blanket,M("DuvetFabric"),Vector3.zero);
            for(int row=0;row<3;row++)for(int col=0;col<4;col++)
            {
                float x=-2.7f+col*1.8f,z=-5.4f+row*2.7f;var star=Object.Instantiate(AssetDatabase.LoadAssetAtPath<Mesh>("Assets/BrakeToy/Art/Tabletop/Meshes/star.asset"));
                var points=star.vertices;for(int i=0;i<points.Length;i++){var v=points[i]*.26f;points[i]=new Vector3(x+v.x,BlanketHeight(x+v.x,z+v.z)+.025f+v.y,z+v.z);}star.vertices=points;star.RecalculateNormals();star.RecalculateBounds();star.RecalculateTangents();
                LivingMeshFactory.MeshObject("Stitched duvet star",bed,star,M(row%2==0?"Ivory":"Amber"),Vector3.zero);
            }
            for(int pillow=0;pillow<2;pillow++)
            {
                var mesh=LivingMeshFactory.Grid(48,28,(u,v)=>
                {
                    float a=u*Mathf.PI*2,b=v*Mathf.PI;
                    float Power(float value,float exponent)=>Mathf.Sign(value)*Mathf.Pow(Mathf.Abs(value),exponent);
                    return new Vector3(Power(Mathf.Sin(b)*Mathf.Cos(a),.5f)*1.65f,Power(Mathf.Cos(b),.7f)*.50f,Power(Mathf.Sin(b)*Mathf.Sin(a),.5f)*1.18f);
                },new Vector2(3,2));
                var indices=mesh.triangles;for(int i=0;i<indices.Length;i+=3){int k=indices[i+1];indices[i+1]=indices[i+2];indices[i+2]=k;}mesh.triangles=indices;
                var points=mesh.vertices;var normals=new Vector3[points.Length];for(int i=0;i<points.Length;i++){var p=points[i];normals[i]=new Vector3(Mathf.Pow(p.x/1.65f,3)/1.65f,p.y/.25f,Mathf.Pow(p.z/1.18f,3)/1.18f).normalized;}mesh.normals=normals;mesh.RecalculateTangents();
                var obj=LivingMeshFactory.MeshObject("Soft sewn pillow",bed,mesh,M("PillowFabric"),new Vector3(pillow==0?-1.75f:1.75f,4.1f,4.55f));obj.transform.localRotation=Quaternion.Euler(-8,0,pillow==0?6:-5);
            }
            B("Woven floor rug",room,new Vector3(10,-7.01f,10),new Vector3(11,.05f,13),"DuvetFabric",.01f);
        }
        private static void Chair(Transform room)
        {
            var model=AssetDatabase.LoadAssetAtPath<GameObject>("Assets/BrakeToy/Art/Living/ThirdParty/painted_wooden_chair_02/painted_wooden_chair_02_1k.fbx");
            var chair=Object.Instantiate(model,room,false);chair.name="Poly Haven wooden chair — CC0";chair.transform.localRotation=Quaternion.Euler(0,-8,0)*chair.transform.localRotation;
            var renderers=chair.GetComponentsInChildren<Renderer>();var bounds=renderers[0].bounds;foreach(var renderer in renderers){bounds.Encapsulate(renderer.bounds);renderer.sharedMaterial=M("ScannedChair");}
            chair.transform.localScale*=8.1f/bounds.size.y;foreach(var renderer in renderers)renderer.ResetBounds();bounds=renderers[0].bounds;foreach(var renderer in renderers)bounds.Encapsulate(renderer.bounds);
            if(Mathf.Abs(bounds.size.y-8.1f)>.05f)throw new System.InvalidOperationException("Imported chair scale is incorrect: "+bounds.size);
            chair.transform.position+=new Vector3(-9f-bounds.center.x,-7.04f-bounds.min.y,20-bounds.center.z);
        }
        private static void Shelf(Transform room)
        {
            var shelf=Group("Toy shelf",room,new Vector3(-10.2f,-7,27));shelf.localScale=new Vector3(.7f,1,1);
            B("Shelf backing",shelf,new Vector3(0,5,1.15f),new Vector3(7.7f,9.8f,.18f),"Ivory");
            foreach(float x in new[]{-3.9f,3.9f})B("Rounded shelf upright",shelf,new Vector3(x,5,0),new Vector3(.3f,10,2.7f),"Wood");
            foreach(float y in new[]{.25f,3.4f,6.7f,9.9f})B("Shelf board",shelf,new Vector3(0,y,0),new Vector3(8.1f,.26f,2.7f),"Wood");
            string[] colors={"Teal","Amber","Blue","Coral","Ivory","Lavender"};
            for(int i=0;i<7;i++)
            {
                float h=1.6f+(i%3)*.35f;B("Bound shelf book",shelf,new Vector3(-3+i*.64f,6.85f+h/2,0),new Vector3(.54f,h,1.5f),colors[i%6],.04f);
                for(int stripe=0;stripe<2;stripe++)B("Book spine foil",shelf,new Vector3(-3+i*.64f,7.15f+stripe*.9f,-.758f),new Vector3(.34f,.045f,.012f),"Ivory",.002f);
            }
            foreach(float x in new[]{-1.95f,1.95f}){B("Fabric storage bin",shelf,new Vector3(x,1.65f,0),new Vector3(3.4f,2.4f,2.1f),x<0?"DuvetFabric":"CurtainFabric",.13f);B("Inset bin handle",shelf,new Vector3(x,2.3f,-1.06f),new Vector3(.8f,.16f,.02f),"Ink",.04f);}
            var teddy=Group("Sewn teddy",shelf,new Vector3(1.9f,4.6f,-.2f));
            LivingMeshFactory.Sphere("Teddy body",teddy,Vector3.zero,new Vector3(1.4f,1.6f,1.1f),M("TeddyFabric"));LivingMeshFactory.Sphere("Teddy head",teddy,new Vector3(0,1,0),Vector3.one*1.3f,M("TeddyFabric"));
            foreach(float x in new[]{-.55f,.55f}){LivingMeshFactory.Sphere("Teddy ear",teddy,new Vector3(x,1.5f,0),Vector3.one*.45f,M("TeddyFabric"));LivingMeshFactory.Sphere("Teddy paw",teddy,new Vector3(x,-.55f,-.2f),new Vector3(.6f,.48f,.7f),M("TeddyFabric"));LivingMeshFactory.Sphere("Teddy eye",teddy,new Vector3(x*.45f,1.1f,-.58f),Vector3.one*.10f,M("Ink"));}
            LivingMeshFactory.Sphere("Teddy muzzle",teddy,new Vector3(0,.87f,-.56f),new Vector3(.65f,.43f,.20f),M("PillowFabric"));LivingMeshFactory.Sphere("Teddy nose",teddy,new Vector3(0,.94f,-.69f),new Vector3(.14f,.1f,.08f),M("Ink"));
        }
        private static void Furniture(Transform room)
        {
            B("Bedside cabinet",room,new Vector3(14.4f,-4.8f,28.6f),new Vector3(3.5f,4.4f,3.0f),"Wood",.16f);
            for(int i=0;i<3;i++){B("Inset drawer",room,new Vector3(14.4f,-6.2f+i*1.35f,27.05f),new Vector3(3.15f,1.2f,.12f),"Wood");LivingMeshFactory.Sphere("Ceramic drawer knob",room,new Vector3(14.4f,-6.2f+i*1.35f,26.92f),Vector3.one*.18f,M("Ivory"));}
            LivingMeshFactory.Lathe("Turned lamp foot",room,new Vector3(14.4f,-2.55f,28.5f),new[]{new Vector2(0,0),new Vector2(.72f,0),new Vector2(.72f,.12f),new Vector2(.26f,.30f),new Vector2(.12f,1.15f),new Vector2(.40f,1.27f)},M("Chrome"));
            LivingMeshFactory.Lathe("Cotton lamp shade",room,new Vector3(14.4f,-1.45f,28.5f),new[]{new Vector2(1.18f,0),new Vector2(.70f,1.6f),new Vector2(.65f,1.6f),new Vector2(1.12f,0)},M("PillowFabric"));
            var light=new GameObject("Warm bedside bulb").AddComponent<Light>();light.transform.SetParent(room,false);light.transform.localPosition=new Vector3(14.4f,-.5f,28.5f);light.type=LightType.Point;light.color=new Color(1,.67f,.32f);light.intensity=1.8f;light.range=9;light.shadows=LightShadows.None;
            var table=Group("Table structure",room);
            foreach(float x in new[]{-5.85f,5.85f}){B("Table apron",table,new Vector3(x,-.8f,10),new Vector3(.3f,1.3f,60),"Wood");foreach(float z in new[]{-9f,27f})B("Table leg",table,new Vector3(x,-3.6f,z),new Vector3(.75f,6.7f,.75f),"Wood",.12f);}
        }
        private static void Poster(Transform room)
        {
            var p=Group("Space poster",room,new Vector3(2.4f,7.8f,31.65f));
            B("Poster mat",p,Vector3.zero,new Vector3(4.4f,6.2f,.08f),"Ivory");B("Printed navy paper",p,new Vector3(0,0,-.05f),new Vector3(4.05f,5.85f,.025f),"Blue");
            B("Rocket body",p,new Vector3(0,.2f,-.09f),new Vector3(.8f,2.8f,.06f),"Coral",.12f);LivingMeshFactory.Sphere("Rocket porthole",p,new Vector3(0,.7f,-.14f),new Vector3(.40f,.40f,.02f),M("Ivory"));
            var rocket=new Mesh();rocket.vertices=new[]{new Vector3(-.4f,1.6f,-.14f),new Vector3(0,2.25f,-.14f),new Vector3(.4f,1.6f,-.14f),new Vector3(-.4f,-.3f,-.14f),new Vector3(-.9f,-1.3f,-.14f),new Vector3(-.4f,-1.05f,-.14f),new Vector3(.4f,-.3f,-.14f),new Vector3(.4f,-1.05f,-.14f),new Vector3(.9f,-1.3f,-.14f)};rocket.triangles=new[]{0,1,2,3,4,5,6,7,8};rocket.RecalculateNormals();LivingMeshFactory.MeshObject("Printed rocket nose and fins",p,rocket,M("Coral"),Vector3.zero);
            var flame=new Mesh();flame.vertices=new[]{new Vector3(-.26f,-1.23f,-.15f),new Vector3(.26f,-1.23f,-.15f),new Vector3(0,-2.1f,-.15f)};flame.triangles=new[]{0,1,2};flame.RecalculateNormals();LivingMeshFactory.MeshObject("Printed rocket exhaust",p,flame,M("Amber"),Vector3.zero);
            foreach(var point in new[]{new Vector3(-1.4f,1.6f,-.12f),new Vector3(1.2f,2.1f,-.12f),new Vector3(-1.1f,-2.1f,-.12f),new Vector3(1.3f,-.4f,-.12f)})LivingMeshFactory.Sphere("Printed poster star",p,point,new Vector3(.07f,.07f,.012f),M("Ivory"));
            foreach(float x in new[]{-2.23f,2.23f})B("Poster frame",p,new Vector3(x,0,-.05f),new Vector3(.09f,6.25f,.12f),"Wood");foreach(float y in new[]{-3.12f,3.12f})B("Poster frame",p,new Vector3(0,y,-.05f),new Vector3(4.5f,.09f,.12f),"Wood");
        }
        private static Transform Decor(Transform room,WorldScroller world)
        {
            var root=Group("Table decorations — no collision",room);var cup=Group("Pencil cup",root,new Vector3(5.15f,0,18));
            LivingMeshFactory.Lathe("Hollow glazed cup",cup,Vector3.zero,new[]{new Vector2(0,0),new Vector2(.54f,0),new Vector2(.62f,.10f),new Vector2(.62f,1.36f),new Vector2(.58f,1.40f),new Vector2(.53f,1.36f),new Vector2(.53f,.10f),new Vector2(0,.10f)},M("Ivory"));
            string[] colors={"Enamel","Blue","Amber","Teal","Lavender"};
            for(int i=0;i<5;i++)
            {
                var pencil=Group("Sharpened hex pencil",cup,new Vector3((i-2)*.18f,.3f,i%2*.20f-.1f));pencil.localRotation=Quaternion.Euler((i-2)*4,0,(i-2)*-5);
                LivingMeshFactory.Lathe("Painted hex barrel",pencil,Vector3.zero,new[]{new Vector2(.075f,0),new Vector2(.075f,2.65f)},M(colors[i]),6);
                LivingMeshFactory.Lathe("Exposed wood tip",pencil,Vector3.zero,new[]{new Vector2(.075f,2.65f),new Vector2(.015f,2.94f)},M("Wood"),12);
                LivingMeshFactory.Lathe("Graphite point",pencil,Vector3.zero,new[]{new Vector2(.015f,2.94f),new Vector2(0,3.02f)},M("Ink"),12);
            }
            var notebook=Group("Spiral notebook",root,new Vector3(-5.15f,.10f,14));notebook.localRotation=Quaternion.Euler(0,-10,0);
            B("Notebook cover",notebook,Vector3.zero,new Vector3(2.3f,.16f,3.4f),"Blue");B("Page stack",notebook,new Vector3(0,.125f,0),new Vector3(2.12f,.12f,3.2f),"Paper",.025f);
            for(int i=0;i<9;i++)
            {
                B("Printed page ruling",notebook,new Vector3(.1f,.190f,-1.25f+i*.30f),new Vector3(1.7f,.005f,.012f),"Teal",.001f);
                var ring=new List<Vector3>();for(int j=0;j<=24;j++){float a=j*Mathf.PI*2/24;ring.Add(new Vector3(-1.10f+Mathf.Cos(a)*.14f,.18f+Mathf.Sin(a)*.14f,-1.25f+i*.30f));}LivingMeshFactory.Tube("Metal spiral binding",notebook,ring,.021f,M("Chrome"),8);
            }
            LivingMeshFactory.Combine(cup,"PencilCup");LivingMeshFactory.Combine(notebook,"Notebook");
            var scroller=root.gameObject.AddComponent<TabletopDecorScroller>();scroller.World=world;scroller.Targets=new[]{cup,notebook};scroller.RecycleDistance=64;
            return root;
        }
    }
}
