using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace BrakeToy.Editor
{
    public static class PreviewCapture
    {
        private static double started;
        private static bool prepared;
        private static RenderTexture target;
        private static float simulatedSeconds;

        // Run without -nographics and without -quit. Exits the batch editor after capture.
        public static void CaptureBatch()
        {
            string scene = File.Exists(RefinedSceneBuilder.ScenePath) ? RefinedSceneBuilder.ScenePath : File.Exists(WorkshopSceneBuilder.ScenePath) ? WorkshopSceneBuilder.ScenePath : File.Exists(TabletopSceneBuilder.ScenePath) ? TabletopSceneBuilder.ScenePath : "Assets/BrakeToy/Scenes/ToyRoom.unity";
            EditorSceneManager.OpenScene(scene);
            ShaderUtil.allowAsyncCompilation = false;
            SessionState.SetBool("BrakeToy.PreviewPending", true);
            EditorApplication.EnterPlaymode();
        }

        public static void RefineAndCapture()
        {
            TabletopSceneBuilder.Refine();
            CaptureBatch();
        }

        [InitializeOnLoadMethod]
        private static void ResumeAfterReload()
        {
            if (!SessionState.GetBool("BrakeToy.PreviewPending", false)) return;
            started = EditorApplication.timeSinceStartup;
            prepared = false;
            EditorApplication.update += CaptureWhenReady;
        }

        private static void CaptureWhenReady()
        {
            if (!EditorApplication.isPlaying || EditorApplication.timeSinceStartup - started < (prepared ? .3 : 3)) return;
            try
            {
                var camera = Camera.main;
                var game = UnityEngine.Object.FindFirstObjectByType<GameManager>();
                ShaderUtil.allowAsyncCompilation = false;
                if (!prepared)
                {
                    if(game.Arcade) { game.Arcade.PersistRecords=false; game.Arcade.VaryRuns=false; }
                    game.StartRun();
                    game.enabled = false;
                    if (game.Arcade)
                    {
                        // Capture a real deterministic risk-taking drive, including earned scores and a moving ball.
                        var stream = new BrakeToy.Core.ArcadeSequence(game.Arcade.CurrentSeed, game.Arcade.UseVariedPatterns);
                        var rows = new System.Collections.Generic.List<BrakeToy.Core.ArcadeRow>();
                        for (int i=0;i<20;i++) rows.Add(stream.Next());
                        int index=0;float x=0;simulatedSeconds=6.3f;
                        var previewArgs=Environment.GetCommandLineArgs();int secondsArg=Array.IndexOf(previewArgs,"-brakeToyPreviewSeconds");
                        if(secondsArg>=0&&secondsArg+1<previewArgs.Length)simulatedSeconds=float.Parse(previewArgs[secondsArg+1],System.Globalization.CultureInfo.InvariantCulture);
                        for(int i=0;i<Mathf.RoundToInt(simulatedSeconds*60);i++)
                        {
                            while(index<rows.Count-1 && rows[index].Distance+3<game.Arcade.Distance)index++;
                            var row=rows[index];float desired=BrakeToy.Core.ArcadeRules.LaneX(row.SafeLane);
                            if(!row.Ball)desired+=row.SafeLane==0?.72f:row.SafeLane==2?-.72f:row.IsBlocked(0)?-.72f:.72f;
                            float next=Mathf.MoveTowards(x,desired,game.Config.MaxLateralSpeed/60);
                            game.Arcade.Step(1f/60,x,next);x=next;game.Player.StopAt(x);
                            if(game.State!=RunState.Running)throw new InvalidOperationException("Preview witness collided at "+game.Arcade.Distance);
                        }
                        game.Arcade.HUD.SendMessage("LateUpdate");
                        game.GetComponent<ArcadeFeedback>().SendMessage("LateUpdate");
                        Debug.Log("Preview actual run: "+game.Arcade.Distance+"m / style="+game.Arcade.Style.Total+" chain="+game.Arcade.Style.Chain);
                    }
                    else game.World.Advance(6.4f);
                    string[] arguments = Environment.GetCommandLineArgs();
                    int pitchArg = Array.IndexOf(arguments, "-brakeToyCameraPitch");
                    if (pitchArg >= 0 && pitchArg + 1 < arguments.Length)
                    {
                        float pitch = float.Parse(arguments[pitchArg + 1], System.Globalization.CultureInfo.InvariantCulture);
                        var previewConfig = UnityEngine.Object.Instantiate(game.Config);
                        previewConfig.CameraPitch = Mathf.Clamp(pitch, 15, 65);
                        camera.GetComponent<PortraitCamera>().Config = previewConfig;
                    }
                    target = new RenderTexture(540, 960, 24); target.Create();
                    camera.targetTexture = target;
                    camera.aspect = 540f / 960;
                    camera.GetComponent<PortraitCamera>().Reframe();
                    var canvas = game.UI.GetComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceCamera; canvas.worldCamera = camera; canvas.planeDistance = 1;
                    game.UI.SafeArea.anchorMin = Vector2.zero; game.UI.SafeArea.anchorMax = Vector2.one;
                    game.UI.Overlay.SetActive(false); game.UI.PauseButton.gameObject.SetActive(true);
                    Canvas.ForceUpdateCanvases();
                    prepared = true; started = EditorApplication.timeSinceStartup;
                    return;
                }
                EditorApplication.update -= CaptureWhenReady;
                SessionState.SetBool("BrakeToy.PreviewPending", false);
                var request = new UniversalRenderPipeline.SingleCameraRequest { destination = target };
                RenderPipeline.SubmitRenderRequest(camera, request);
                RenderPipeline.SubmitRenderRequest(camera, request);
                RenderTexture.active = target;
                var texture = new Texture2D(540, 960, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0, 0, 540, 960), 0, 0);
                texture.Apply();
                float activePitch = camera.GetComponent<PortraitCamera>().Config.CameraPitch;
                string filename = gameObjectSceneIsTabletop() ? (Mathf.Approximately(activePitch, 20) ? "../Preview-v0.2.png" : "../Preview-v0.2-" + activePitch.ToString("0", System.Globalization.CultureInfo.InvariantCulture) + "deg.png") : "../Preview.png";
                if (game.Arcade) filename = simulatedSeconds>10 ? "../Preview-v0.3-late-run.png" : "../Preview-v0.3.png";
                if (game.Arcade && game.Arcade.UseVariedPatterns) filename = simulatedSeconds>10 ? "../Preview-v0.4-late-run.png" : "../Preview-v0.4.png";
                string output = Path.GetFullPath(Path.Combine(Application.dataPath, filename));
                File.WriteAllBytes(output, texture.EncodeToPNG());
                RenderTexture.active = null;
                camera.targetTexture = null;
                UnityEngine.Object.DestroyImmediate(texture);
                target.Release();
                UnityEngine.Object.DestroyImmediate(target);
                Debug.Log("Brake Toy preview saved: " + output);
                EditorApplication.Exit(0);
            }
            catch (Exception error)
            {
                SessionState.SetBool("BrakeToy.PreviewPending", false);
                EditorApplication.update -= CaptureWhenReady;
                Debug.LogException(error);
                EditorApplication.Exit(1);
            }
        }

        private static bool gameObjectSceneIsTabletop() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ToyRoomTabletop";
    }
}
