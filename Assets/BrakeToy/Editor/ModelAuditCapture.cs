using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace BrakeToy.Editor
{
    /// <summary>Actual Unity model inspection renders, not concept art.</summary>
    public static class ModelAuditCapture
    {
        private static Camera camera;
        private static RenderTexture target;
        private static GameManager game;
        private static int stage;
        private static double last;
        public static void Capture()
        {
            EditorSceneManager.OpenScene(File.Exists(LivingSceneBuilder.ScenePath)?LivingSceneBuilder.ScenePath:File.Exists(RefinedSceneBuilder.ScenePath)?RefinedSceneBuilder.ScenePath:WorkshopSceneBuilder.ScenePath);ShaderUtil.allowAsyncCompilation=false;
            game=UnityEngine.Object.FindFirstObjectByType<GameManager>();game.UI.gameObject.SetActive(false);
            camera=Camera.main;camera.GetComponent<PortraitCamera>().enabled=false;
            camera.fieldOfView=38;camera.aspect=4f/3;camera.nearClipPlane=.02f;
            target=new RenderTexture(1200,900,24);target.Create();camera.targetTexture=target;
            stage=0;Setup();last=EditorApplication.timeSinceStartup;EditorApplication.update+=Update;
        }
        private static void Setup()
        {
            if(stage<2)
            {
                camera.transform.position=stage==0?new Vector3(1.6f,1.2f,1.9f):new Vector3(-1.7f,1.1f,-1.8f);
                camera.transform.LookAt(new Vector3(0,.30f,0));
            }
            else if(stage==2)
            {
                game.Player.gameObject.SetActive(false);
                int[] indices={0,6,12,18};
                for(int i=0;i<indices.Length;i++)
                {var h=game.Arcade.Pool[indices[i]];h.Place((i-1.5f)*2.35f,0,0,0);h.Model.localRotation=Quaternion.Euler(0,i*8-12,0);}
                camera.fieldOfView=44;camera.transform.position=new Vector3(2,6.1f,-7.5f);camera.transform.LookAt(new Vector3(0,.35f,0));
            }
            else
            {foreach(var h in game.Arcade.Pool)h.gameObject.SetActive(false);camera.fieldOfView=55;camera.transform.position=stage==3?new Vector3(13,1.8f,16):new Vector3(-13,1,14);camera.transform.LookAt(stage==3?new Vector3(8,-2,26):new Vector3(-8,-2,24));}
        }
        private static void Update()
        {
            if(EditorApplication.timeSinceStartup-last<1)return;
            try
            {
                RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});
                RenderPipeline.SubmitRenderRequest(camera,new UniversalRenderPipeline.SingleCameraRequest{destination=target});
                RenderTexture.active=target;var texture=new Texture2D(1200,900,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,1200,900),0,0);texture.Apply();
                string name=stage==0?"Models-v0.3-front.png":stage==1?"Models-v0.3-rear.png":"Models-v0.3-obstacles.png";
                if(game.Arcade.UseVariedPatterns)name=name.Replace("v0.3","v0.4");
                if(game.Arcade.Choreographed)name=name.Replace("v0.4","v0.5");
                if(stage==3)name="Room-v0.5-detail.png";
                if(stage==4)name="Room-v0.5-chair.png";
                File.WriteAllBytes(Path.GetFullPath(Path.Combine(Application.dataPath,"../"+name)),texture.EncodeToPNG());UnityEngine.Object.DestroyImmediate(texture);RenderTexture.active=null;
                stage++;if(stage<(game.Arcade.Choreographed?5:3)){Setup();last=EditorApplication.timeSinceStartup;return;}
                camera.targetTexture=null;target.Release();UnityEngine.Object.DestroyImmediate(target);EditorApplication.update-=Update;EditorApplication.Exit(0);
            }
            catch(Exception e){Debug.LogException(e);EditorApplication.update-=Update;EditorApplication.Exit(1);}
        }
    }
}
