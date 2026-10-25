using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;
using PlayMode = BrakeToy.Core.PlayMode;

namespace BrakeToy.Tests
{
    public sealed class OpeningRuntimeAuditTests
    {
        [Serializable] private sealed class Sample
        {
            public int seed, seconds, lives, referenceCrossings, spawnedIncludingLookahead;
            public bool relax;
            public float distance, speed;
        }
        [Serializable] private sealed class Report { public List<Sample> samples = new List<Sample>(); }

        [UnityTest]
        public IEnumerator FirstMinuteSafeWitnessMatchesScheduleAndExportsThreeEngineFrames()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var report = new Report();
            foreach (bool relax in new[] { false, true })
            foreach (int seed in new[] { 7331, 109, 917 })
            {
                game.Review.Mode = relax ? PlayMode.Relax : PlayMode.Arcade;
                game.Arcade.VaryRuns = false;
                game.Arcade.Seed = seed;
                game.StartRun();
                var sequence = new AdventureSequence(seed, relax);
                var beats = new List<PlaygroundBeat>();
                for (int n = 0; n < 100; n++) beats.Add(sequence.Next());
                int row = 0;
                float x = 0;
                for (int frame = 1; frame <= 3600; frame++)
                {
                    while (row < beats.Count - 1 && beats[row].Distance + 30 < game.Arcade.Distance) row++;
                    float next = Mathf.MoveTowards(x, beats[row].SafeX, game.Config.MaxLateralSpeed / 60);
                    game.Arcade.Step(1f / 60, x, next);
                    game.Player.StopAt(next);
                    x = next;
                    Assert.That(game.State, Is.EqualTo(RunState.Running), "seed=" + seed + " frame=" + frame);
                    if (frame != 600 && frame != 1800 && frame != 3600) continue;
                    int seconds = frame / 60, crossings = 0;
                    foreach (var beat in beats) if (beat.Distance <= game.Arcade.Distance) crossings++;
                    Assert.That(game.Arcade.Distance, Is.EqualTo(JoyfulRules.Distance(seconds, relax)).Within(.15f));
                    Assert.That(game.Review.Lives, Is.EqualTo(relax ? 3 : 1));
                    Assert.That(game.Arcade.PoolExhaustions + game.GetComponent<AdventureEncounters>().PoolExhaustions, Is.Zero);
                    report.samples.Add(new Sample { seed = seed, seconds = seconds, relax = relax,
                        distance = game.Arcade.Distance, speed = game.Arcade.Speed, lives = game.Review.Lives,
                        referenceCrossings = crossings, spawnedIncludingLookahead = game.Arcade.SpawnedRows });
                    if (!relax && seed == 7331)
                    {
                        yield return null; // Refresh UI and visual height without advancing the disabled GameManager.
                        Capture(game, seconds);
                    }
                }
            }
            Directory.CreateDirectory("Reports/Local");
            File.WriteAllText("Reports/Local/OpeningRuntime.json", JsonUtility.ToJson(report, true));
            Assert.That(report.samples.Count, Is.EqualTo(18));
        }

        private static void Capture(GameManager game, int seconds)
        {
            var camera = Camera.main;
            var target = new RenderTexture(720, 1280, 24);
            var previous = camera.targetTexture;
            var active = RenderTexture.active;
            float aspect = camera.aspect;
            var canvas = game.UI.GetComponent<Canvas>();
            var mode = canvas.renderMode;
            var worldCamera = canvas.worldCamera;
            try
            {
                target.Create();
                camera.targetTexture = target;
                camera.aspect = 720f / 1280;
                var framing = camera.GetComponent<PortraitCamera>();
                framing.Reframe();
                var position = camera.transform.position;
                position.x = game.Player.X * framing.FollowAmount;
                camera.transform.position = position;
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = camera;
                canvas.planeDistance = 1;
                Canvas.ForceUpdateCanvases();
                for (int n = 0; n < 2; n++) RenderPipeline.SubmitRenderRequest(camera,
                    new UniversalRenderPipeline.SingleCameraRequest { destination = target });
                RenderTexture.active = target;
                var image = new Texture2D(720, 1280, TextureFormat.RGB24, false);
                image.ReadPixels(new Rect(0, 0, 720, 1280), 0, 0);
                image.Apply();
                Directory.CreateDirectory("Reports/Local");
                File.WriteAllBytes("Reports/Local/Opening-" + seconds + ".png", image.EncodeToPNG());
                Object.Destroy(image);
            }
            finally
            {
                canvas.renderMode = mode;
                canvas.worldCamera = worldCamera;
                camera.targetTexture = previous;
                camera.aspect = aspect;
                RenderTexture.active = active;
                target.Release();
                Object.Destroy(target);
            }
        }
    }
}
