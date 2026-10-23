using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Object=UnityEngine.Object;
namespace BrakeToy.Editor
{
    internal static class JoyfulRoom
    {
        public static void Build(GameManager game)
        {
            var palette=new Dictionary<Material,Material>();
            foreach(var r in Object.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Include,FindObjectsSortMode.None))
            {
                if(r.transform.IsChildOf(game.Player.transform))continue;var mats=r.sharedMaterials;
                for(int i=0;i<mats.Length;i++)
                {
                    var m=mats[i];if(!m)continue;
                    if(m.name=="Daylight"){r.enabled=false;continue;}
                    string key=m.name.ToLowerInvariant();string target=key=="wall"?"Mint":key=="wood"||key.Contains("calmwood")?"Cream":key=="pink"?"Rose":key=="amber"?"Mango":null;
                    if(target!=null)mats[i]=JoyfulArt.M(target);
                    else if(key.Contains("curtainfabric")||key.Contains("duvetfabric"))
                    {if(!palette.TryGetValue(m,out var clone)){clone=new Material(m){name="Joyful "+m.name};clone.SetColor("_BaseColor",key.Contains("curtain")?new Color(1,.39f,.37f):new Color(.18f,.39f,.93f));AssetDatabase.CreateAsset(clone,JoyfulArt.Root+"Materials/"+clone.name+".mat");palette[m]=clone;}mats[i]=clone;}
                }
                r.sharedMaterials=mats;
            }
            foreach(var chunk in game.World.FloorChunks)
            {
                chunk.localScale=new Vector3(.86f,1,1);
                foreach(var r in chunk.GetComponentsInChildren<Renderer>())r.sharedMaterial=JoyfulArt.M("Cream");
                var detail=JoyfulArt.Group("Painted play-table inlay",chunk);
                JoyfulArt.B("Mint inset lane",detail,new Vector3(0,.025f,0),new Vector3(8.25f,.018f,17.98f),"Mint",.008f);
                for(int side=-1;side<=1;side+=2)
                {JoyfulArt.B("Rounded painted edge",detail,new Vector3(side*5.3f,-.10f,0),new Vector3(.13f,.20f,18),"Rose",.045f);for(int i=0;i<8;i++)JoyfulArt.B("Stitched lane edge",detail,new Vector3(side*4.22f,.045f,-8+i*2.2f),new Vector3(.07f,.012f,.78f),"Cream",.005f);}
                // Edge-only doodles are clearly decoration, outside the +/-3.5 driving area.
                for(int i=0;i<5;i++){float x=(i%2==0?-1:1)*4.85f,z=-7+i*3.1f;JoyfulArt.S("Painted dot sticker",detail,new Vector3(x,.045f,z),new Vector3(.38f,.016f,.38f),i%2==0?"Mango":"Blue");}
                JoyfulArt.Combine(detail,"TableInlay");
            }
            var room=JoyfulArt.Group("Joyful room additions — decoration only");
            for(int side=-1;side<=1;side+=2)
            {
                var chest=JoyfulArt.Group("Painted toy chest",room,new Vector3(side*9,-7,16));JoyfulArt.B("Rounded storage base",chest,new Vector3(0,1.6f,0),new Vector3(4.8f,3.2f,4),side<0?"Rose":"Blue",.28f);JoyfulArt.B("Lid",chest,new Vector3(0,3.3f,0),new Vector3(5,.26f,4.2f),"Mango",.12f);JoyfulArt.B("Handle",chest,new Vector3(0,2,-2.08f),new Vector3(1,.28f,.18f),"Cream");
                var toy=JoyfulArt.Bear(chest,side>0);toy.localPosition=new Vector3(0,3.45f,0);toy.localScale=Vector3.one*2.2f;toy.localRotation=Quaternion.Euler(0,side*18,0);
            }
            var rug=JoyfulArt.Group("Rainbow braided rug",room,new Vector3(0,-7.015f,12));for(int i=0;i<7;i++)JoyfulArt.S("Woven oval band",rug,new Vector3(0,i*.008f,0),new Vector3(26-i*1.7f,.03f,21-i*1.4f),i%3==0?"Mango":i%3==1?"Rose":"Blue");
            var play=JoyfulArt.Group("Toy parade on shelf",room,new Vector3(-10,-2,28));for(int i=0;i<3;i++){var toy=i==0?JoyfulArt.Robot(play):i==1?JoyfulArt.Duck(play):JoyfulArt.Bear(play,true);toy.localPosition=new Vector3((i-1)*2,0,0);toy.localScale=Vector3.one*1.6f;}
            for(int i=0;i<8;i++)
            {var b=JoyfulArt.Group("Alphabet block",room,new Vector3(7.2f+(i%3)*1.0f,-6.65f+(i/3)*.75f,24));b.localRotation=Quaternion.Euler(0,i*17,0);JoyfulArt.B("Rounded wooden block",b,Vector3.zero,Vector3.one*.75f,i%2==0?"Rose":"Mango",.09f);JoyfulArt.S("Raised button",b,new Vector3(0,0,-.38f),new Vector3(.35f,.35f,.04f),"Cream");}
            // Original radio prop: mesh grille, speaker rings, tuning scale and antenna.
            var radio=JoyfulArt.Group("Happy FM radio",room,new Vector3(-4.4f,.05f,24));radio.localRotation=Quaternion.Euler(0,-12,0);JoyfulArt.B("Rounded radio casing",radio,new Vector3(0,.72f,0),new Vector3(2.5f,1.4f,.82f),"Rose",.20f);JoyfulArt.B("Inset face",radio,new Vector3(0,.72f,-.43f),new Vector3(2.27f,1.12f,.055f),"Cream",.13f);
            JoyfulArt.S("Speaker cone",radio,new Vector3(-.58f,.70f,-.49f),new Vector3(.88f,.88f,.06f),"Ink");for(int x=0;x<10;x++)for(int y=0;y<10;y++)if((x-4.5f)*(x-4.5f)+(y-4.5f)*(y-4.5f)<20)JoyfulArt.S("Perforated speaker grille",radio,new Vector3(-.58f+(x-4.5f)*.075f,.70f+(y-4.5f)*.075f,-.53f),Vector3.one*.028f,"Cream");
            JoyfulArt.B("Frequency glass",radio,new Vector3(.55f,.96f,-.49f),new Vector3(.92f,.34f,.05f),"Mint");for(int i=0;i<10;i++)JoyfulArt.B("Tuning tick",radio,new Vector3(.17f+i*.08f,.96f,-.53f),new Vector3(.012f,i%2==0?.14f:.08f,.01f),"Cream",.002f);for(int i=0;i<2;i++)JoyfulArt.S("Tuning dial",radio,new Vector3(.28f+i*.55f,.45f,-.50f),new Vector3(.28f,.28f,.11f),"Blue");JoyfulArt.Tube("Telescopic antenna",radio,new[]{new Vector3(.8f,1.4f,0),new Vector3(1.25f,2.5f,0)},.025f,"Cream");
            JoyfulArt.Combine(room,"JoyfulRoomDecor");
            Outside(game);
        }
        private static void Outside(GameManager game)
        {
            var outside=JoyfulArt.Group("Window outdoor diorama",null,new Vector3(-7.1f,7.5f,32.40f));var sky=new Material(Shader.Find("Universal Render Pipeline/Lit")){name="Window animated sky"};sky.SetColor("_BaseColor",new Color(.3f,.7f,1));sky.EnableKeyword("_EMISSION");sky.SetColor("_EmissionColor",new Color(.2f,.4f,.8f));AssetDatabase.CreateAsset(sky,JoyfulArt.Root+"Materials/WindowSky.mat");LivingMeshFactory.Box("Sky panel",outside,new Vector3(0,0,.6f),new Vector3(14,8,.05f),sky,.005f);
            var cycle=game.gameObject.AddComponent<RoomDayCycle>();cycle.Sky=outside.GetComponentInChildren<Renderer>();cycle.SunLight=RenderSettings.sun;
            cycle.Sun=JoyfulArt.S("Sun",outside,Vector3.zero,Vector3.one*.82f,"Mango").transform;cycle.Moon=JoyfulArt.S("Moon",outside,Vector3.zero,Vector3.one*.64f,"White").transform;
            var stars=JoyfulArt.Group("Night stars",outside);for(int i=0;i<42;i++){float x=Mathf.Sin(i*73.1f)*3.25f,y=Mathf.Cos(i*31.7f)*3.1f;JoyfulArt.S("Star",stars,new Vector3(x,y,.13f),Vector3.one*(i%4==0?.055f:.032f),"White");}JoyfulArt.Combine(stars,"WindowStars");cycle.Stars=stars.gameObject;
            var clouds=JoyfulArt.Group("Drifting clouds",outside);for(int i=0;i<3;i++)for(int j=0;j<3;j++)JoyfulArt.S("Cloud puff",clouds,new Vector3(-3+i*3.1f+j*.35f,1.2f+Mathf.Sin(i)*.8f+j%2*.18f,.23f),new Vector3(.9f,.48f,.14f),"White");JoyfulArt.Combine(clouds,"WindowClouds");cycle.Clouds=clouds;
            var birds=JoyfulArt.Group("Morning birds",outside);var wings=new List<Transform>();for(int i=0;i<4;i++){var bird=JoyfulArt.Group("Bird wings",birds,new Vector3(i*.55f,.3f+Mathf.Sin(i)*.25f,-.04f));JoyfulArt.Tube("Wing silhouette",bird,new[]{new Vector3(-.18f,.07f,0),Vector3.zero,new Vector3(.18f,.07f,0)},.017f,"Ink");wings.Add(bird);}cycle.Birds=birds.gameObject;cycle.BirdWings=wings.ToArray();
            var lamp=new GameObject("Warm night-time bounce").AddComponent<Light>();lamp.type=LightType.Point;lamp.transform.position=new Vector3(-3,5,15);lamp.color=new Color(1,.72f,.40f);lamp.range=32;lamp.shadows=LightShadows.None;cycle.RoomLamp=lamp;
            foreach(var r in outside.GetComponentsInChildren<Renderer>()){r.shadowCastingMode=ShadowCastingMode.Off;r.receiveShadows=false;}
        }
    }
}
