using System;
using System.Collections.Generic;
using System.IO;
using BrakeToy.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using PlayMode=BrakeToy.Core.PlayMode;
namespace BrakeToy.Editor
{
    public static class JoyfulCapture
    {
        private static GameManager game;private static Camera camera;private static RenderTexture target;private static bool prepared;private static int stage;private static double last;
        public static void Capture(){EditorSceneManager.OpenScene(JoyfulSceneBuilder.ScenePath);SessionState.SetBool("JoyfulCapture",true);EditorApplication.EnterPlaymode();}
        [InitializeOnLoadMethod]private static void Resume(){if(!SessionState.GetBool("JoyfulCapture",false))return;stage=0;prepared=false;last=EditorApplication.timeSinceStartup;EditorApplication.update+=Update;}
        private static void Update()
        {
            if(!EditorApplication.isPlaying||EditorApplication.timeSinceStartup-last<(prepared?1.2:3))return;
            try
            {
                if(!prepared){game=UnityEngine.Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Review.Persist=false;game.Arcade.PersistRecords=false;camera=Camera.main;target=new RenderTexture(720,1280,24);target.Create();camera.targetTexture=target;camera.aspect=720f/1280;camera.GetComponent<PortraitCamera>().Reframe();var canvas=game.UI.GetComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=camera;canvas.planeDistance=1;game.UI.SafeArea.anchorMin=Vector2.zero;game.UI.SafeArea.anchorMax=Vector2.one;prepared=true;Setup();last=EditorApplication.timeSinceStartup;return;}
                foreach(var c in UnityEngine.Object.FindObjectsByType<Camera>(FindObjectsSortMode.None))if(c!=camera&&c.enabled&&c.targetTexture)RenderPipeline.SubmitRenderRequest(c,new UniversalRenderPipeline.SingleCameraRequest{destination=c.targetTexture});
                Canvas.ForceUpdateCanvases();RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderTexture.active=target;var image=new Texture2D(720,1280,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,720,1280),0,0);image.Apply();File.WriteAllBytes("Joyful-"+stage+".png",image.EncodeToPNG());UnityEngine.Object.DestroyImmediate(image);RenderTexture.active=null;
                stage++;if(stage<16){Setup();last=EditorApplication.timeSinceStartup;return;}
                camera.targetTexture=null;target.Release();UnityEngine.Object.DestroyImmediate(target);SessionState.SetBool("JoyfulCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(0);
            }
            catch(Exception e){Debug.LogException(e);SessionState.SetBool("JoyfulCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(1);}
        }
        private static void Setup()
        {
            var front=game.GetComponent<ReviewFrontend>();var cycle=game.GetComponent<RoomDayCycle>();cycle.Animate=false;cycle.TimeOfDay=stage==4?.75f:.25f;cycle.Apply();game.UI.GetComponent<Canvas>().enabled=stage<3||stage>=5&&stage<=7;
            if(stage<3){front.SetPage(stage==0?"Ana menü":stage==1?"Garaj":"Ayarlar");return;}
            if(stage==3||stage==4){camera.GetComponent<PortraitCamera>().Reframe();return;}
            if(stage==5||stage==6){Witness(stage==5?12:42);return;}
            if(stage==7){front.StartMode(PlayMode.Arcade);foreach(var h in game.Arcade.Pool)h.Release();var jump=game.GetComponent<JoyfulRun>();foreach(var ramp in jump.Ramps)ramp.Release();jump.SpawnRamp(0,3);for(int i=0;i<60;i++)game.Arcade.Step(1f/60,0,0);game.Player.StopAt(0);return;}
            game.Pause();foreach(var h in game.Arcade.Pool)h.Release();foreach(var r in game.GetComponent<JoyfulRun>().Ramps)r.Release();game.Player.gameObject.SetActive(false);camera.GetComponent<PortraitCamera>().enabled=false;camera.fieldOfView=39;
            if(stage==15){game.GetComponent<JoyfulRun>().Ramps[0].Place(0,0);camera.transform.position=new Vector3(3,3,-6);camera.transform.LookAt(new Vector3(0,.2f,-.8f));}
            else{int kind=stage-8;int offset=0;for(int i=0;i<kind;i++)offset+=game.Arcade.Definitions[i].PoolSize;var h=game.Arcade.Pool[offset];h.MotionKind=0;h.Place(0,0,0,0);camera.transform.position=new Vector3(2.2f,1.8f,-3.8f);camera.transform.LookAt(new Vector3(0,.62f,0));}
        }
        private static void Witness(float seconds)
        {
            game.Arcade.VaryRuns=false;game.Arcade.Seed=7331;game.GetComponent<ReviewFrontend>().StartMode(PlayMode.Arcade);var seq=new JoyfulSequence(game.Arcade.CurrentSeed);var rows=new List<JoyfulRow>();for(int i=0;i<100;i++)rows.Add(seq.Next());int row=0;float x=0;
            for(int i=0;i<(int)(seconds*60);i++){while(row<rows.Count-1&&rows[row].Distance+10<game.Arcade.Distance)row++;float next=Mathf.MoveTowards(x,rows[row].SafeX,game.Config.MaxLateralSpeed/60);game.Arcade.Step(1f/60,x,next);if(game.State!=RunState.Running)throw new InvalidOperationException("Capture witness crashed at "+game.Arcade.Elapsed);game.Player.StopAt(next);x=next;}
        }
    }
}
