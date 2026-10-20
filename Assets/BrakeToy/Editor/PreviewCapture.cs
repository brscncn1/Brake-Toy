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

        // Run without -nographics and without -quit. Exits the batch editor after capture.
        public static void CaptureBatch()
        {
            EditorSceneManager.OpenScene("Assets/BrakeToy/Scenes/ToyRoom.unity");
            ShaderUtil.allowAsyncCompilation = false;
            SessionState.SetBool("BrakeToy.PreviewPending", true);
            EditorApplication.EnterPlaymode();
        }

        [InitializeOnLoadMethod]
        private static void ResumeAfterReload()
        {
            if (!SessionState.GetBool("BrakeToy.PreviewPending", false)) return;
            started = EditorApplication.timeSinceStartup;
            EditorApplication.update += CaptureWhenReady;
        }

        private static void CaptureWhenReady()
        {
            if (!EditorApplication.isPlaying || EditorApplication.timeSinceStartup - started < 3) return;
            EditorApplication.update -= CaptureWhenReady;
            SessionState.SetBool("BrakeToy.PreviewPending", false);
            try
            {
                var camera = Camera.main;
                var game = UnityEngine.Object.FindFirstObjectByType<GameManager>();
                ShaderUtil.allowAsyncCompilation = false;
                game.StartRun();
                game.World.Advance(6.4f);
                var target = new RenderTexture(540, 960, 24);
                target.Create();
                camera.targetTexture = target;
                camera.aspect = 540f / 960;
                camera.GetComponent<PortraitCamera>().Reframe();
                var canvas = game.UI.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = camera;
                canvas.planeDistance = 1;
                game.UI.SafeArea.anchorMin = Vector2.zero;
                game.UI.SafeArea.anchorMax = Vector2.one;
                game.UI.Overlay.SetActive(false);
                game.UI.PauseButton.gameObject.SetActive(true);
                Canvas.ForceUpdateCanvases();
                var request = new UniversalRenderPipeline.SingleCameraRequest { destination = target };
                RenderPipeline.SubmitRenderRequest(camera, request);
                RenderPipeline.SubmitRenderRequest(camera, request);
                RenderTexture.active = target;
                var texture = new Texture2D(540, 960, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0, 0, 540, 960), 0, 0);
                texture.Apply();
                string output = Path.GetFullPath(Path.Combine(Application.dataPath, "../Preview.png"));
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
                Debug.LogException(error);
                EditorApplication.Exit(1);
            }
        }
    }
}
