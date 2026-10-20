using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BrakeToy.Tests
{
    public sealed class TabletopSceneTests
    {
        [UnityTest]
        public IEnumerator RoomPreservesGameplayAndDecorHasNoColliders()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoomTabletop"); yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            Assert.That(game.State, Is.EqualTo(RunState.Ready));
            Assert.That(GameObject.Find("Bedroom Set"), Is.Not.Null);
            Assert.That(GameObject.Find("Table structure"), Is.Not.Null);
            Assert.That(GameObject.Find("Window and curtains"), Is.Not.Null);
            Assert.That(GameObject.Find("Blue star bed"), Is.Not.Null);
            Assert.That(GameObject.Find("Toy shelf"), Is.Not.Null);
            Assert.That(GameObject.Find("Bedroom Set").GetComponentsInChildren<Collider>().Length, Is.Zero);
            Assert.That(game.World.FloorChunks.Length, Is.EqualTo(7));
            Assert.That(game.World.Obstacles.Length, Is.EqualTo(8));
            var collider = game.Player.GetComponent<BoxCollider>();
            Assert.That(collider.size.x * .5f, Is.EqualTo(game.Config.PlayerHalfWidth).Within(.001f));
            Assert.That(collider.size.z * .5f, Is.EqualTo(game.Config.PlayerHalfLength).Within(.001f));
            Assert.That(game.Player.GetComponent<ToyWheelAnimation>().Game, Is.SameAs(game));
        }

        [UnityTest]
        public IEnumerator RoomCameraKeepsTrackAndReactionDistanceVisible()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoomTabletop"); yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var camera = Camera.main;
            foreach (float aspect in new[] { 9f / 16, 9f / 19.5f, 9f / 21, 3f / 4 })
            {
                camera.aspect = aspect; camera.GetComponent<PortraitCamera>().Reframe();
                var player = camera.WorldToViewportPoint(new Vector3(0, .35f, 0));
                Assert.That(player.y, Is.EqualTo(.25f).Within(.002f));
                foreach (float x in new[] { -3.5f, 3.5f })
                {
                    var edge = camera.WorldToViewportPoint(new Vector3(x, .35f, 0));
                    Assert.That(edge.x, Is.InRange(0f, 1f));
                    var warning = camera.WorldToViewportPoint(new Vector3(x, .35f, game.Config.WorldSpeed * 1.5f));
                    Assert.That(warning.x, Is.InRange(0f, 1f));
                    Assert.That(warning.y, Is.InRange(.25f, .9f));
                }
            }
            camera.ResetAspect();
        }

        [UnityTest]
        public IEnumerator NearDecorSharesWorldMovementPauseAndRestart()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoomTabletop"); yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var decor = Object.FindFirstObjectByType<TabletopDecorScroller>();
            Assert.That(decor.Targets.Length, Is.GreaterThan(0));
            var first = decor.Targets[0]; Vector3 start = first.position;
            game.StartRun();
            game.World.Advance(2);
            Assert.That(first.position.z, Is.EqualTo(start.z - 2).Within(.001f));
            game.Pause(); Vector3 paused = first.position;
            yield return null;
            Assert.That(first.position, Is.EqualTo(paused));
            game.StartRun();
            Assert.That(first.position, Is.EqualTo(start));
            Assert.That(decor.Targets[0], Is.SameAs(first));
        }

        [UnityTest]
        public IEnumerator NewSceneStillCrashesAndReusesWorldOnRestart()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoomTabletop"); yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var floor = game.World.FloorChunks[0]; var obstacle = game.World.Obstacles[0];
            game.StartRun();
            float deadline = Time.realtimeSinceStartup + 6;
            while (game.State == RunState.Running && Time.realtimeSinceStartup < deadline) yield return null;
            Assert.That(game.State, Is.EqualTo(RunState.Crashed));
            game.StartRun();
            Assert.That(game.World.FloorChunks[0], Is.SameAs(floor));
            Assert.That(game.World.Obstacles[0], Is.SameAs(obstacle));
            Assert.That(obstacle.transform.position.z, Is.EqualTo(22));
            Assert.That(game.Player.X, Is.EqualTo(0));
        }
    }
}
