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
    public static class PlaygroundCapture
    {
        private static GameManager game;private static Camera camera;private static RenderTexture target;private static bool prepared;private static int stage;private static double last;
        public static void CaptureCars(){SessionState.SetInt("PlaygroundCaptureStart",19);Capture();}
        public static void Capture(){EditorSceneManager.OpenScene(PlaygroundSceneBuilder.ScenePath);SessionState.SetBool("PlaygroundCapture",true);EditorApplication.EnterPlaymode();}
        [InitializeOnLoadMethod]private static void Resume(){if(!SessionState.GetBool("PlaygroundCapture",false))return;stage=SessionState.GetInt("PlaygroundCaptureStart",0);SessionState.SetInt("PlaygroundCaptureStart",0);prepared=false;last=EditorApplication.timeSinceStartup;EditorApplication.update+=Update;}
        private static void Update()
        {
            if(!EditorApplication.isPlaying||EditorApplication.timeSinceStartup-last<(prepared?1.2:3))return;
            try
            {
                if(!prepared){game=UnityEngine.Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Review.Persist=false;game.GetComponent<PlaygroundRun>().PersistPreferences=false;game.Arcade.PersistRecords=false;camera=Camera.main;target=new RenderTexture(720,1280,24);target.Create();camera.targetTexture=target;camera.aspect=720f/1280;camera.GetComponent<PortraitCamera>().Reframe();var canvas=game.UI.GetComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=camera;canvas.planeDistance=1;game.UI.SafeArea.anchorMin=Vector2.zero;game.UI.SafeArea.anchorMax=Vector2.one;prepared=true;Setup();last=EditorApplication.timeSinceStartup;return;}
                foreach(var c in UnityEngine.Object.FindObjectsByType<Camera>(FindObjectsSortMode.None))if(c!=camera&&c.enabled&&c.targetTexture)RenderPipeline.SubmitRenderRequest(c,new UniversalRenderPipeline.SingleCameraRequest{destination=c.targetTexture});
                Canvas.ForceUpdateCanvases();RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderTexture.active=target;var image=new Texture2D(720,1280,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,720,1280),0,0);image.Apply();File.WriteAllBytes(ReportPaths.Visual("Playground-"+stage+".png"),image.EncodeToPNG());UnityEngine.Object.DestroyImmediate(image);RenderTexture.active=null;
                stage++;if(stage<29){Setup();last=EditorApplication.timeSinceStartup;return;}
                camera.targetTexture=null;target.Release();UnityEngine.Object.DestroyImmediate(target);SessionState.SetBool("PlaygroundCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(0);
            }
            catch(Exception e){Debug.LogException(e);SessionState.SetBool("PlaygroundCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(1);}
        }
        private static void Setup()
        {
            var front=game.GetComponent<ReviewFrontend>();var cycle=game.GetComponent<RoomDayCycle>();cycle.Animate=false;cycle.TimeOfDay=stage==4?.75f:.25f;cycle.Apply();game.UI.GetComponent<Canvas>().enabled=stage<3||stage>=5&&stage<=7;
            if(stage<3){front.SetPage(stage==0?"Ana menü":stage==1?"Garaj":"Görevler");return;}
            if(stage>=16){if(stage==16){camera.GetComponent<PortraitCamera>().enabled=true;camera.GetComponent<PortraitCamera>().Reframe();game.Player.gameObject.SetActive(true);front.SetPage("Ayarlar");game.UI.GetComponent<Canvas>().enabled=true;}else if(stage==17||stage==18){front.StartMode(PlayMode.Arcade);foreach(var h in game.Arcade.Pool)h.Release();var run=game.GetComponent<PlaygroundRun>();run.Activate(stage==17?ToyPower.Rocket:ToyPower.Jetpack);for(int i=0;i<45;i++)game.Arcade.Step(1f/60,0,0);game.UI.GetComponent<Canvas>().enabled=true;game.Player.gameObject.SetActive(true);}else {game.Pause();front.SetPage("Garaj");front.Browse(stage-19-game.Review.Data.selectedCar);game.UI.GetComponent<Canvas>().enabled=true;}return;}
            if(stage==3||stage==4){camera.GetComponent<PortraitCamera>().Reframe();return;}
            if(stage==5||stage==6){Witness(stage==5?12:42);return;}
            if(stage==7){front.StartMode(PlayMode.Arcade);foreach(var h in game.Arcade.Pool)h.Release();var jump=game.GetComponent<JoyfulRun>();foreach(var ramp in jump.Ramps)ramp.Release();jump.SpawnRamp(0,3);for(int i=0;i<60;i++)game.Arcade.Step(1f/60,0,0);game.Player.StopAt(0);return;}
            game.Pause();foreach(var h in game.Arcade.Pool)h.Release();foreach(var r in game.GetComponent<JoyfulRun>().Ramps)r.Release();game.Player.gameObject.SetActive(false);camera.GetComponent<PortraitCamera>().enabled=false;camera.fieldOfView=39;
            if(stage==15){game.GetComponent<JoyfulRun>().Ramps[0].Place(0,0);camera.transform.position=new Vector3(3,3,-6);camera.transform.LookAt(new Vector3(0,.2f,-.8f));}
            else{int kind=stage-8;int offset=0;for(int i=0;i<kind;i++)offset+=game.Arcade.Definitions[i].PoolSize;var h=game.Arcade.Pool[offset];h.MotionKind=0;h.Place(0,0,0,0);camera.transform.position=new Vector3(2.2f,1.8f,-3.8f);camera.transform.LookAt(new Vector3(0,.62f,0));}
        }
        private static void Witness(float seconds)
        {
            game.Arcade.VaryRuns=false;game.Arcade.Seed=7331;game.GetComponent<ReviewFrontend>().StartMode(PlayMode.Arcade);var seq=new PlaygroundSequence(game.Arcade.CurrentSeed);var rows=new List<PlaygroundBeat>();for(int i=0;i<100;i++)rows.Add(seq.Next());int row=0;float x=0;
            for(int i=0;i<(int)(seconds*60);i++){while(row<rows.Count-1&&rows[row].Distance+30<game.Arcade.Distance)row++;float next=Mathf.MoveTowards(x,rows[row].SafeX,game.Config.MaxLateralSpeed/60);game.Arcade.Step(1f/60,x,next);if(game.State!=RunState.Running)throw new InvalidOperationException("Capture witness crashed at "+game.Arcade.Elapsed);game.Player.StopAt(next);x=next;}
        }
    }
}
