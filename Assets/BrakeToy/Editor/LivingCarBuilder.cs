using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object=UnityEngine.Object;

namespace BrakeToy.Editor
{
    internal static class LivingCarBuilder
    {
        private static Material M(string name)=>AssetDatabase.LoadAssetAtPath<Material>("Assets/BrakeToy/Art/Living/Materials/"+name+".mat");
        private static float Width(float z)=>Profile(z,new[]{.24f,.32f,.35f,.34f,.34f,.35f,.30f,.23f});
        private static float Height(float z)=>Profile(z,new[]{.29f,.36f,.39f,.36f,.36f,.39f,.35f,.29f});
        private static float Profile(float z,float[] values)
        {float[] stops={-.65f,-.56f,-.43f,-.20f,.13f,.42f,.54f,.65f};int i=0;while(i<6&&z>stops[i+1])i++;float t=Mathf.SmoothStep(0,1,Mathf.InverseLerp(stops[i],stops[i+1],z));return Mathf.Lerp(values[i],values[i+1],t);}
        private static float Bottom(float z)
        {float b=.135f;foreach(float center in new[]{-.43f,.42f}){float d=Mathf.Abs(z-center);if(d<.18f)b=Mathf.Max(b,.16f+Mathf.Sqrt(.18f*.18f-d*d));}return b;}
        private static float BodyTop(float x,float z)
        {float cosine=Mathf.Pow(Mathf.Clamp01(Mathf.Abs(x)/Width(z)),1/.45f);return (Height(z)+Bottom(z))*.5f+(Height(z)-Bottom(z))*.5f*Mathf.Pow(Mathf.Sqrt(1-cosine*cosine),.65f);}
        private static Vector3 Canopy(float u,float z)
        {
            float t=Mathf.InverseLerp(-.34f,.31f,z),shape=Mathf.Pow(Mathf.Max(0,Mathf.Sin(t*Mathf.PI)),.72f);
            float width=Mathf.Lerp(.265f,.222f,shape),top=.355f+.218f*shape;
            return new Vector3(u*width,.355f+(top-.355f)*Mathf.Pow(Mathf.Max(0,1-u*u),.36f),z);
        }
        public static void Build(GameManager game)
        {
            var old=game.Player;var root=new GameObject("Sparky — rounded diecast coupe").transform;var visual=new GameObject("Curved body and trim").transform;visual.SetParent(root,false);
            var player=root.gameObject.AddComponent<PlayerController>();player.Visual=visual;
            // A curved closed body, with the lower edge lifted around both wheel circles.
            const int sections=104,sides=32;var v=new List<Vector3>();var uv=new List<Vector2>();var tri=new List<int>();
            for(int s=0;s<=sections;s++)
            {
                float z=Mathf.Lerp(-.65f,.65f,s/(float)sections),w=Width(z),h=Height(z),b=Bottom(z);
                for(int j=0;j<=sides;j++)
                {
                    float a=j*Mathf.PI*2/sides,c=Mathf.Cos(a),n=Mathf.Sin(a);
                    v.Add(new Vector3(w*Mathf.Sign(c)*Mathf.Pow(Mathf.Abs(c),.45f),(h+b)*.5f+(h-b)*.5f*Mathf.Sign(n)*Mathf.Pow(Mathf.Abs(n),.65f),z));uv.Add(new Vector2(j/(float)sides,s/(float)sections));
                    if(s<sections&&j<sides){int k=s*(sides+1)+j,l=k+sides+1;tri.AddRange(new[]{k,k+1,l,k+1,l+1,l});}
                }
            }
            for(int end=0;end<2;end++)
            {
                int source=end==0?0:sections*(sides+1),center=v.Count;float z=end==0?-.65f:.65f;v.Add(new Vector3(0,(Height(z)+Bottom(z))*.5f,z));uv.Add(Vector2.one*.5f);
                for(int j=0;j<sides;j++)if(end==0)tri.AddRange(new[]{center,source+j+1,source+j});else tri.AddRange(new[]{center,source+j,source+j+1});
            }
            var body=new Mesh();body.SetVertices(v);body.SetUVs(0,uv);body.SetTriangles(tri,0);body.RecalculateNormals();body.RecalculateTangents();body.RecalculateBounds();LivingMeshFactory.MeshObject("Continuous curved pressed metal",visual,body,M("Enamel"),Vector3.zero);
            var glass=LivingMeshFactory.Grid(48,48,(u,t)=>Canopy(u*2-1,Mathf.Lerp(-.34f,.31f,t)),Vector2.one);LivingMeshFactory.MeshObject("Curved panoramic glass",visual,glass,M("Glass"),Vector3.zero);
            LivingMeshFactory.MeshObject("Pressed curved roof",visual,LivingMeshFactory.Grid(24,32,(u,t)=>Canopy((u*2-1)*.70f,Mathf.Lerp(-.205f,.105f,t))+Vector3.up*.008f,Vector2.one),M("Enamel"),Vector3.zero);
            foreach(float centerX in new[]{-.061f,.061f})
            {
                LivingMeshFactory.MeshObject("Roof stripe",visual,LivingMeshFactory.Grid(4,32,(u,t)=>{float z=Mathf.Lerp(-.19f,.09f,t),x=centerX+(u-.5f)*.048f;var p=Canopy(x/Mathf.Lerp(.265f,.222f,Mathf.Pow(Mathf.Max(0,Mathf.Sin(Mathf.InverseLerp(-.34f,.31f,z)*Mathf.PI)),.72f)),z);p.y+=.010f;return p;},Vector2.one),M("Ivory"),Vector3.zero);
                LivingMeshFactory.MeshObject("Hood stripe",visual,LivingMeshFactory.Grid(4,26,(u,t)=>{float z=Mathf.Lerp(.33f,.60f,t),x=centerX+(u-.5f)*.048f;return new Vector3(x,BodyTop(x,z)+.003f,z);},Vector2.one),M("Ivory"),Vector3.zero);
            }
            foreach(float sign in new[]{-1f,1f})
            {
                var sill=new List<Vector3>();for(int i=0;i<=24;i++){float z=Mathf.Lerp(-.30f,.27f,i/24f);sill.Add(new Vector3(sign*(Width(z)+.002f),.25f,z));}LivingMeshFactory.Tube("Inset door shut line",visual,sill,.0025f,M("Ink"),6);
                var edge=new List<Vector3>();for(int i=0;i<=40;i++)edge.Add(Canopy(sign*.98f,Mathf.Lerp(-.335f,.305f,i/40f))+Vector3.up*.004f);LivingMeshFactory.Tube("Window beltline",visual,edge,.009f,M("Chrome"));
                var pillar=new List<Vector3>();for(int i=0;i<=14;i++)pillar.Add(Canopy(sign*Mathf.Lerp(.98f,.70f,i/14f),Mathf.Lerp(.29f,.105f,i/14f))+Vector3.up*.009f);LivingMeshFactory.Tube("Swept A pillar",visual,pillar,.013f,M("Enamel"));
                pillar=new List<Vector3>();for(int i=0;i<=14;i++)pillar.Add(Canopy(sign*Mathf.Lerp(.98f,.70f,i/14f),Mathf.Lerp(-.33f,-.205f,i/14f))+Vector3.up*.009f);LivingMeshFactory.Tube("Swept rear pillar",visual,pillar,.014f,M("Enamel"));
                LivingMeshFactory.Box("Flush door handle",visual,new Vector3(sign*.347f,.337f,-.12f),new Vector3(.015f,.015f,.08f),M("Chrome"),.004f);
                LivingMeshFactory.Tube("Mirror arm",visual,new[]{new Vector3(sign*.32f,.37f,.16f),new Vector3(sign*.365f,.39f,.16f)},.007f,M("Ink"));
                LivingMeshFactory.Sphere("Diecast mirror",visual,new Vector3(sign*.365f,.39f,.16f),new Vector3(.041f,.042f,.075f),M("Enamel"));
                LivingMeshFactory.Sphere("Recessed lamp housing",visual,new Vector3(sign*.18f,.254f,.650f),new Vector3(.145f,.100f,.033f),M("Chrome"));
                LivingMeshFactory.Sphere("Headlamp glass",visual,new Vector3(sign*.18f,.254f,.665f),new Vector3(.119f,.079f,.019f),M("Headlamp"));
                LivingMeshFactory.Box("Rear red lens",visual,new Vector3(sign*.195f,.273f,-.657f),new Vector3(.12f,.051f,.019f),M("RedLight"),.009f);
                LivingMeshFactory.Tube("Spoiler support",visual,new[]{new Vector3(sign*.16f,.35f,-.55f),new Vector3(sign*.16f,.405f,-.55f)},.008f,M("Ink"));
            }
            foreach(float z in new[]{-.669f,.669f})LivingMeshFactory.Box("Chrome bumper",visual,new Vector3(0,.176f,z),new Vector3(.55f,.037f,.038f),M("Chrome"),.01f);
            LivingMeshFactory.Box("Soft edged rear wing",visual,new Vector3(0,.418f,-.555f),new Vector3(.59f,.027f,.105f),M("Enamel"),.008f);
            LivingMeshFactory.Box("Number badge",visual,new Vector3(0,.259f,-.673f),new Vector3(.15f,.040f,.009f),M("Ivory"),.004f);
            var wheels=new List<Transform>();
            foreach(var oldWheel in old.GetComponent<ToyWheelAnimation>().Wheels)
            {var wheel=Object.Instantiate(oldWheel.gameObject,visual,false).transform;wheel.localPosition=oldWheel.localPosition;wheel.localRotation=oldWheel.localRotation;wheels.Add(wheel);}
            LivingMeshFactory.Combine(visual,"SparkyBody",wheels.ToArray());
            var collider=root.gameObject.AddComponent<BoxCollider>();collider.center=new Vector3(0,.35f,0);collider.size=new Vector3(.78f,.7f,1.4f);collider.isTrigger=true;
            var animation=root.gameObject.AddComponent<ToyWheelAnimation>();animation.Wheels=wheels.ToArray();animation.LocalAxis=Vector3.up;animation.Game=game;
            Object.DestroyImmediate(old.gameObject);game.Player=player;PrefabUtility.SaveAsPrefabAsset(root.gameObject,"Assets/BrakeToy/Art/Living/Prefabs/SparkyLiving.prefab");
        }
    }
}
