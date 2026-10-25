using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace BrakeToy.Editor
{
    public static class ReviewCapture
    {
        private static double last;private static int stage;private static bool prepared;
        private static RenderTexture target;private static GameManager game;
        private static readonly string[] Pages={"Ana menü","Garaj","Görevler","Ayarlar","Sıralama","Bilgi"};
        public static void Capture(){EditorSceneManager.OpenScene(ReviewSceneBuilder.ScenePath);SessionState.SetBool("ReviewCapture",true);EditorApplication.EnterPlaymode();}
        [InitializeOnLoadMethod]private static void Resume(){if(!SessionState.GetBool("ReviewCapture",false))return;stage=0;prepared=false;last=EditorApplication.timeSinceStartup;EditorApplication.update+=Update;}
        private static void Update()
        {
            if(!EditorApplication.isPlaying||EditorApplication.timeSinceStartup-last<(prepared?1:3))return;
            try
            {
                if(!prepared){game=UnityEngine.Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Review.Persist=false;game.Arcade.PersistRecords=false;var camera=Camera.main;target=new RenderTexture(540,960,24);target.Create();camera.targetTexture=target;camera.aspect=540f/960;camera.GetComponent<PortraitCamera>().Reframe();var canvas=game.UI.GetComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=camera;canvas.planeDistance=1;game.UI.SafeArea.anchorMin=Vector2.zero;game.UI.SafeArea.anchorMax=Vector2.one;prepared=true;game.GetComponent<ReviewFrontend>().SetPage(Pages[stage]);last=EditorApplication.timeSinceStartup;return;}
                foreach(var camera in UnityEngine.Object.FindObjectsByType<Camera>(FindObjectsSortMode.None))if(camera!=Camera.main&&camera.enabled&&camera.targetTexture)RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=camera.targetTexture});
                Canvas.ForceUpdateCanvases();RenderPipeline.SubmitRenderRequest(Camera.main,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderPipeline.SubmitRenderRequest(Camera.main,new UniversalRenderPipeline.SingleCameraRequest{destination=target});RenderTexture.active=target;var image=new Texture2D(540,960,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,540,960),0,0);image.Apply();File.WriteAllBytes(ReportPaths.Visual("UI-review-"+stage+".png"),image.EncodeToPNG());UnityEngine.Object.DestroyImmediate(image);RenderTexture.active=null;
                stage++;if(stage<18){var frontend=game.GetComponent<ReviewFrontend>();if(stage<Pages.Length)frontend.SetPage(Pages[stage]);else if(stage==6)frontend.SetPage("Garaj");else if(stage<16)frontend.Browse(1);else{frontend.StartMode(stage==16?Core.PlayMode.Arcade:Core.PlayMode.Relax);game.Arcade.Step(.1f,0,0);}last=EditorApplication.timeSinceStartup;return;}
                Camera.main.targetTexture=null;target.Release();UnityEngine.Object.DestroyImmediate(target);SessionState.SetBool("ReviewCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(0);
            }
            catch(Exception e){Debug.LogException(e);SessionState.SetBool("ReviewCapture",false);EditorApplication.update-=Update;EditorApplication.Exit(1);}
        }
    }
}
