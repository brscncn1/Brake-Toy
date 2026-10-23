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
    public static class AdventureCapture
    {
        private static GameManager game;private static Camera camera;private static RenderTexture target;private static bool prepared;private static int stage;private static double last;
        public static void Capture(){EditorSceneManager.OpenScene(AdventureSceneBuilder.ScenePath);SessionState.SetBool("AdventureCapture",true);EditorApplication.EnterPlaymode();}
        [InitializeOnLoadMethod]private static void Resume(){if(!SessionState.GetBool("AdventureCapture",false))return;stage=0;prepared=false;last=EditorApplication.timeSinceStartup;EditorApplication.update+=Update;}
        private static void Update()
        {
            if(!EditorApplication.isPlaying||EditorApplication.timeSinceStartup-last<(prepared?1.2:3))return;
            try
            {
                if(!prepared){game=UnityEngine.Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Review.Persist=false;game.GetComponent<PlaygroundRun>().PersistPreferences=false;game.Arcade.PersistRecords=false;camera=Camera.main;target=new RenderTexture(720,1280,24);target.Create();camera.targetTexture=target;camera.aspect=720f/1280;camera.GetComponent<PortraitCamera>().Reframe();var canvas=game.UI.GetComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=camera;canvas.planeDistance=1;game.UI.SafeArea.anchorMin=Vector2.zero;game.UI.SafeArea.anchorMax=Vector2.one;prepared=true;Setup();last=EditorApplication.timeSinceStartup;return;}
                foreach(var c in UnityEngine.Object.FindObjectsByType<Camera>(FindObjectsSortMode.None))if(c!=camera&&c.enabled&&c.targetTexture)RenderPipeline.SubmitRenderRequest(c,new UniversalRenderPipeline.SingleCameraRequest{destination=c.targetTexture});
                Canvas.ForceUpdateCanvases();RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderTexture.active=target;var image=new Texture2D(720,1280,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,720,1280),0,0);image.Apply();File.WriteAllBytes("Adventure-"+stage+".png",image.EncodeToPNG());UnityEngine.Object.DestroyImmediate(image);RenderTexture.active=null;
                stage++;if(stage<13){Setup();last=EditorApplication.timeSinceStartup;return;}
                camera.targetTexture=null;target.Release();UnityEngine.Object.DestroyImmediate(target);SessionState.SetBool("AdventureCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(0);
            }
            catch(Exception e){Debug.LogException(e);SessionState.SetBool("AdventureCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(1);}
        }
        private static void Setup()
        {
            game.GetComponent<RoomDayCycle>().Animate=false;game.GetComponent<RoomDayCycle>().TimeOfDay=.25f;game.GetComponent<RoomDayCycle>().Apply();camera.GetComponent<PortraitCamera>().enabled=true;camera.GetComponent<PortraitCamera>().Reframe();game.UI.GetComponent<Canvas>().enabled=stage<=1||stage==10;
            if(stage==0){game.GetComponent<ReviewFrontend>().SetPage("Ana menü");return;}
            if(stage==1){Witness(45);return;}
            game.GetComponent<ReviewFrontend>().StartMode(PlayMode.Arcade);game.Pause();foreach(var h in game.Arcade.Pool)h.Release();foreach(var r in game.GetComponent<JoyfulRun>().Ramps)r.Release();game.GetComponent<PlaygroundEncounters>().ResetRun();var toys=game.GetComponent<AdventureEncounters>();toys.ResetRun();int family=stage==10?3:stage==11?1:stage==12?5:stage-2;
            var a=toys.Place((TrackToy)family,new PlaygroundBeat(0,0,1,family,stage>=11?1:2,0),stage==10?14:9);toys.Advance(0,0,-2.25f,-2.25f);
            if(stage==10){game.Player.gameObject.SetActive(true);game.Player.StopAt(-2.25f);game.GetComponent<JoyfulRun>().SpawnRamp(0,30);game.Resume();return;}
            game.Player.gameObject.SetActive(false);camera.GetComponent<PortraitCamera>().enabled=false;camera.fieldOfView=44;camera.transform.position=new Vector3(family==0?6:4,5,-3);camera.transform.LookAt(new Vector3(0,.8f,family==3?11:9));
        }
        private static void Witness(float seconds)
        {
            game.Arcade.VaryRuns=false;game.Arcade.Seed=7331;game.GetComponent<ReviewFrontend>().StartMode(PlayMode.Arcade);var seq=new AdventureSequence(game.Arcade.CurrentSeed);var rows=new List<PlaygroundBeat>();for(int i=0;i<100;i++)rows.Add(seq.Next());int row=0;float x=0;
            for(int i=0;i<(int)(seconds*60);i++){while(row<rows.Count-1&&rows[row].Distance+30<game.Arcade.Distance)row++;float next=Mathf.MoveTowards(x,rows[row].SafeX,game.Config.MaxLateralSpeed/60);game.Arcade.Step(1f/60,x,next);if(game.State!=RunState.Running)throw new InvalidOperationException("Capture witness crashed at "+game.Arcade.Elapsed);game.Player.StopAt(next);x=next;}
        }
    }
}
