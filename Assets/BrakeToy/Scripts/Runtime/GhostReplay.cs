using System;
using System.IO;
using System.IO.Compression;
using BrakeToy.Core;
using UnityEngine;
using UnityEngine.Rendering;
namespace BrakeToy
{
    public sealed class GhostReplay:MonoBehaviour
    {
        public bool Visible=true;public Material GhostMaterial;public int RecordedCount{get;private set;}
        private const int Capacity=12000;private readonly short[] recorded=new short[Capacity];private short[] playback;private readonly short[] elevations=new short[Capacity];private short[] playbackElevations;private float lastHeight;
        private int seed;private PlayMode mode;private float next,lastTime,lastX,best;private GameObject ghost;private Material material;
        private ReviewSession session;private GameManager game;
        private string FilePath=>Path.Combine(Application.persistentDataPath,(GetComponent<PlaygroundRun>()?"ghost-playground-latest-":GetComponent<JoyfulRun>()?"ghost-joyful-latest-":"ghost-latest-")+(int)mode+".gz");
        public void Begin(int route,PlayMode playMode)
        {
            session=GetComponent<ReviewSession>();game=GetComponent<GameManager>();seed=route;mode=playMode;RecordedCount=0;next=lastTime=lastX=best=0;playback=null;playbackElevations=null;lastHeight=0;
            if(ghost)Destroy(ghost);if(material)Destroy(material);
            if(session.Persist)try{Read(FilePath,seed,(int)mode,out best,out playback,out playbackElevations);}catch{playback=null;playbackElevations=null;lastHeight=0;}
            if(playback==null)return;
            ghost=Instantiate(game.Player.Visual.gameObject);ghost.name="Same-route personal ghost — no collision";foreach(var collider in ghost.GetComponentsInChildren<Collider>())Destroy(collider);
            material=GhostMaterial?new Material(GhostMaterial):new Material(Shader.Find("Universal Render Pipeline/Unlit"));material.SetColor("_BaseColor",new Color(.2f,.9f,1,.24f));material.SetFloat("_Surface",1);material.SetFloat("_SrcBlend",(float)BlendMode.SrcAlpha);material.SetFloat("_DstBlend",(float)BlendMode.OneMinusSrcAlpha);material.SetFloat("_ZWrite",0);material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");material.renderQueue=3000;
            foreach(var r in ghost.GetComponentsInChildren<Renderer>()){r.sharedMaterial=material;r.shadowCastingMode=ShadowCastingMode.Off;r.receiveShadows=false;}
        }
        public void Record(float time,float x)
        {
            while(next<=time&&RecordedCount<Capacity){float t=Mathf.InverseLerp(lastTime,time,next);recorded[RecordedCount]=(short)Mathf.RoundToInt(Mathf.Lerp(lastX,x,t)*1000);elevations[RecordedCount++]=(short)Mathf.RoundToInt(Mathf.Lerp(lastHeight,GetComponent<JoyfulRun>()?GetComponent<JoyfulRun>().Height:0,t)*1000);next+=.05f;}lastTime=time;lastX=x;lastHeight=GetComponent<JoyfulRun>()?GetComponent<JoyfulRun>().Height:0;
        }
        private void LateUpdate()
        {
            if(!ghost||!game)return;float t=game.Arcade.Elapsed/.05f;int i=Mathf.FloorToInt(t);ghost.SetActive(Visible&&(!GetComponent<PlaygroundRun>()||!GetComponent<PlaygroundRun>().Assisted)&&game.State==RunState.Running&&i<playback.Length-1);
            if(ghost.activeSelf)ghost.transform.SetPositionAndRotation(new Vector3(Mathf.Lerp(playback[i],playback[i+1],t-i)/1000,.018f+(playbackElevations!=null?Mathf.Lerp(playbackElevations[i],playbackElevations[i+1],t-i)/1000:0),.35f),Quaternion.identity);
        }
        public void Finish(float distance)
        {
            if((GetComponent<PlaygroundRun>()&&GetComponent<PlaygroundRun>().Assisted)||!session.Persist||distance<=best||RecordedCount<2)return;
            try{Write(FilePath,seed,(int)mode,distance,recorded,RecordedCount,GetComponent<JoyfulRun>()?elevations:null);}catch(Exception e){Debug.LogWarning("Ghost save failed: "+e.GetType().Name);}
        }
        public static void Write(string path,int seed,int mode,float distance,short[] values,int count,short[] heights=null)
        {using(var f=File.Create(path+".pending"))using(var zip=new GZipStream(f,CompressionMode.Compress))using(var w=new BinaryWriter(zip)){w.Write(heights==null?1:2);w.Write(seed);w.Write(mode);w.Write(distance);w.Write(count);for(int i=0;i<count;i++){w.Write(values[i]);if(heights!=null)w.Write(heights[i]);}}File.Copy(path+".pending",path,true);}
        public static void Read(string path,int seed,int mode,out float best,out short[] values)
        {Read(path,seed,mode,out best,out values,out _);}
        public static void Read(string path,int seed,int mode,out float best,out short[] values,out short[] heights)
        {best=0;values=null;heights=null;if(!File.Exists(path)||new FileInfo(path).Length>128000)return;using(var f=File.OpenRead(path))using(var zip=new GZipStream(f,CompressionMode.Decompress))using(var r=new BinaryReader(zip)){int version=r.ReadInt32();if((version!=1&&version!=2)||r.ReadInt32()!=seed||r.ReadInt32()!=mode)return;float distance=r.ReadSingle();int n=r.ReadInt32();if(n<2||n>Capacity||!float.IsFinite(distance)||distance<0)return;var data=new short[n];var elevation=version==2?new short[n]:null;for(int i=0;i<n;i++){data[i]=r.ReadInt16();if(Math.Abs((int)data[i])>3111)return;if(version==2){elevation[i]=r.ReadInt16();if(elevation[i]<0||elevation[i]>2500)return;}}best=distance;values=data;heights=elevation;}}
        private void OnDestroy(){if(ghost)Destroy(ghost);if(material)Destroy(material);}
    }
}
