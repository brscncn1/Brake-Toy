using System.Collections;
using System.Linq;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace BrakeToy.Tests
{
    public sealed class RollingBallSceneTests
    {
        private GameManager game;
        private ArcadeHazard ball;
        private IEnumerator Load()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            game = Object.FindFirstObjectByType<GameManager>();
            game.StartRun();
            foreach (var hazard in game.Arcade.Pool) hazard.Release();
            game.GetComponent<AdventureEncounters>().ResetRun();
            foreach (var ramp in game.GetComponent<JoyfulRun>().Ramps) ramp.Release();
            game.Arcade.Challenges.ResetRun();
            ball = game.Arcade.Pool.First(h => h.RollingBall);
        }
        [UnityTest]
        public IEnumerator AStationaryBallDoesNotSpinAndMovingBallRollsByDistanceOverRadius()
        {
            yield return Load();
            ball.MotionKind = 0;
            ball.Place(0, 20, 0, 0);
            ball.Advance(1);
            Assert.That(Quaternion.Angle(Quaternion.identity, ball.Model.localRotation), Is.LessThan(.001f));
            ball.Place(0, 20, 0, 0);
            ball.ConfigureRollingBall(20, 0, 0, 0);
            float startZ = ball.transform.position.z;
            var predicted = ball.PositionAfter(1);
            ball.Advance(1);
            Assert.That(ball.transform.position, Is.EqualTo(predicted));
            Assert.That(startZ - ball.transform.position.z, Is.EqualTo(1.2f).Within(.00001f));
            Assert.That(Quaternion.Angle(Quaternion.identity, ball.Model.localRotation),
                Is.EqualTo(.2f / ball.HalfWidth * Mathf.Rad2Deg).Within(.001f));
        }
        [UnityTest]
        public IEnumerator FourTrajectoriesMoveOverTheTableAndAreStableAcrossStepRates()
        {
            yield return Load();
            for (int profile = 0; profile < 4; profile++)
            {
                Vector3 reference = default;
                Quaternion rotation = default;
                foreach (int fps in new[] { 30, 60, 120 })
                {
                    ball.Place(0, 30, 0, 0);
                    ball.ConfigureRollingBall(30, 0, profile * 3, 2);
                    Vector3 start = ball.transform.position;
                    for (int n = 0; n < fps; n++) ball.Advance(10f / fps);
                    Vector3 overTable = ball.transform.position - start + Vector3.forward * 10;
                    Assert.That(overTable.magnitude, Is.GreaterThan(3));
                    if (profile > 0) Assert.That(Mathf.Abs(overTable.x), Is.GreaterThan(.01f));
                    if (fps == 30) { reference = ball.transform.position; rotation = ball.Model.rotation; }
                    else
                    {
                        Assert.That(Vector3.Distance(reference, ball.transform.position), Is.LessThan(.002f));
                        Assert.That(Quaternion.Angle(rotation, ball.Model.rotation), Is.LessThan(2));
                    }
                }
            }
        }
        [UnityTest]
        public IEnumerator MovingBallStillCollidesAndExportsMotionFrames()
        {
            yield return Load();
            game.Player.StopAt(-2.25f);
            ball.Place(0, 20, 0, 0);
            ball.ConfigureRollingBall(20, 0, 6, 1);
            yield return null; // Refresh frontend visibility after StartRun.
            OpeningRuntimeAuditTests.Capture(game, 0, "Rolling-before");
            Vector3 before = ball.transform.position;
            for (int n = 0; n < 40; n++) game.Arcade.Step(1f / 60, -2.25f, -2.25f);
            Assert.That(before.z - ball.transform.position.z, Is.GreaterThan(10));
            yield return null;
            OpeningRuntimeAuditTests.Capture(game, 0, "Rolling-after");
            ball.Place(0, 1, 0, 0);
            ball.ConfigureRollingBall(1, 0, 0, 0);
            game.Player.StopAt(0);
            game.Arcade.Step(.1f, 0, 0);
            Assert.That(game.State, Is.Not.EqualTo(RunState.Running));
        }
        [UnityTest]
        public IEnumerator TwoNightLampsDimByDayAndCanLimitTrackSpill()
        {
            yield return Load();
            var cycle = game.GetComponent<RoomDayCycle>();
            var lamps = game.GetComponent<NightRoomLamps>();
            Assert.That(lamps, Is.Not.Null);
            var lampBounds = TidyRoom.BoundsOf(lamps.CornerLight.transform.parent);
            Assert.That(lampBounds.min.x, Is.GreaterThanOrEqualTo(6.3f), "Fixture stays outside the table.");
            Assert.That(lampBounds.max.z, Is.LessThan(31.8f), "Shade stays in front of the wall.");
            Assert.That(lampBounds.min.y, Is.EqualTo(1.38f).Within(.01f), "Base rests just above the headboard top.");
            cycle.Animate = false;
            cycle.TimeOfDay = .25f;
            cycle.Apply();
            Assert.That(lamps.ReadingLight.intensity + lamps.CornerLight.intensity, Is.EqualTo(0));
            yield return null;
            OpeningRuntimeAuditTests.Capture(game, 0, "Lamps-day");
            cycle.TimeOfDay = .75f;
            lamps.TrackSpill = false;
            cycle.Apply();
            yield return null;
            Assert.That(lamps.ReadingLight.intensity, Is.EqualTo(0));
            Assert.That(lamps.CornerLight.intensity, Is.GreaterThan(0));
            OpeningRuntimeAuditTests.Capture(game, 0, "Lamps-night-room-only");
            lamps.TrackSpill = true;
            cycle.Apply();
            yield return null;
            Assert.That(lamps.ReadingLight.intensity, Is.InRange(1f, 2f));
            Assert.That(lamps.ReadingLight.shadows, Is.EqualTo(LightShadows.None));
            OpeningRuntimeAuditTests.Capture(game, 0, "Lamps-night-track-spill");
        }
    }
}
