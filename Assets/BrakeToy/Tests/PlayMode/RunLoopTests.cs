using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BrakeToy.Tests
{
    public sealed class RunLoopTests
    {
        [UnityTest]
        public IEnumerator ReadyCrashAndRestart()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoom");
            yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            Assert.That(game, Is.Not.Null);
            Assert.That(game.State, Is.EqualTo(RunState.Ready));
            Assert.That(game.World.Obstacles.Length, Is.EqualTo(8));
            Vector3 first = game.World.Obstacles[0].transform.position;
            game.StartRun();
            float timeout = Time.realtimeSinceStartup + 6;
            while (game.State == RunState.Running && Time.realtimeSinceStartup < timeout) yield return null;
            Assert.That(game.State, Is.EqualTo(RunState.Crashed), "Center obstacle must stop an untouched car.");
            Vector3 stopped = game.World.Obstacles[0].transform.position;
            yield return null;
            Assert.That(game.World.Obstacles[0].transform.position, Is.EqualTo(stopped));
            for (int i = 0; i < 20; i++)
            {
                game.StartRun();
                Assert.That(game.Player.X, Is.EqualTo(0));
                Assert.That(game.World.Obstacles[0].transform.position, Is.EqualTo(first));
                game.Pause();
                Assert.That(game.State, Is.EqualTo(RunState.Paused));
                game.Resume();
                Assert.That(game.State, Is.EqualTo(RunState.Running));
            }
        }

        [UnityTest]
        public IEnumerator CameraPreservesPortraitFraming()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoom");
            yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var camera = Camera.main;
            var framing = camera.GetComponent<PortraitCamera>();
            foreach (float aspect in new[] { 9f / 16, 9f / 19.5f, 9f / 21, 3f / 4 })
            {
                camera.aspect = aspect;
                framing.Reframe();
                var player = camera.WorldToViewportPoint(new Vector3(0, .35f, 0));
                Assert.That(player.x, Is.EqualTo(.5f).Within(.002f));
                Assert.That(player.y, Is.EqualTo(game.Config.PlayerViewportY).Within(.002f));
                foreach (float x in new[] { -game.Config.TrackHalfWidth, game.Config.TrackHalfWidth })
                {
                    float viewportX = camera.WorldToViewportPoint(new Vector3(x, .35f, 0)).x;
                    Assert.That(viewportX, Is.InRange(0f, 1f));
                }
            }
            camera.ResetAspect();
        }
    }
}
