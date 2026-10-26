using System.Collections;
using System.Reflection;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace BrakeToy.Tests
{
    public sealed class RoomAndRampChoiceSceneTests
    {
        private GameManager game;
        private AdventureEncounters toys;
        private IEnumerator Load()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            game = Object.FindFirstObjectByType<GameManager>();
            toys = game.GetComponent<AdventureEncounters>();
            game.Arcade.VaryRuns = false;
        }
        private void Clear()
        {
            foreach (var hazard in game.Arcade.Pool) hazard.Release();
            foreach (var ramp in game.GetComponent<JoyfulRun>().Ramps) ramp.Release();
            game.GetComponent<PlaygroundEncounters>().ResetRun();
            game.Arcade.Challenges.ResetRun();
            toys.ResetRun();
        }
        private void Place(PlaygroundBeat beat, float z)
        {
            var activate = typeof(ArcadeDirector).GetMethod("Activate", BindingFlags.Instance | BindingFlags.NonPublic);
            toys.Spawn(beat, z, (kind, x, depth, crossing, slope) =>
                (ArcadeHazard)activate.Invoke(game.Arcade, new object[] { kind, x, depth, crossing, slope }));
        }
        [UnityTest]
        public IEnumerator DayNightOnlyChangesOutsideAndLocalLamps()
        {
            yield return Load();
            var cycle = game.GetComponent<RoomDayCycle>();
            cycle.Animate = false;
            cycle.TimeOfDay = .25f;
            cycle.Apply();
            yield return null;
            float intensity = cycle.SunLight.intensity, bounce = cycle.RoomLamp.intensity;
            Color ambient = RenderSettings.ambientSkyColor, equator = RenderSettings.ambientEquatorColor;
            Color keyColour = cycle.SunLight.color;
            Quaternion keyAngle = cycle.SunLight.transform.rotation;
            game.StartRun();
            yield return null;
            OpeningRuntimeAuditTests.Capture(game, 0, "Choices-room-day");
            cycle.TimeOfDay = .75f;
            cycle.Apply();
            yield return null;
            Assert.That(cycle.SunLight.intensity, Is.EqualTo(intensity));
            Assert.That(cycle.RoomLamp.intensity, Is.EqualTo(bounce));
            Assert.That(RenderSettings.ambientSkyColor, Is.EqualTo(ambient));
            Assert.That(RenderSettings.ambientEquatorColor, Is.EqualTo(equator));
            Assert.That(cycle.SunLight.color, Is.EqualTo(keyColour));
            Assert.That(Quaternion.Angle(keyAngle, cycle.SunLight.transform.rotation), Is.LessThan(.001f));
            Assert.That(cycle.Stars.activeSelf, Is.True);
            Assert.That(cycle.Sun.gameObject.activeSelf, Is.False);
            Assert.That(game.GetComponent<NightRoomLamps>().CornerLight.intensity, Is.GreaterThan(0));
            OpeningRuntimeAuditTests.Capture(game, 0, "Choices-room-night");
        }
        [UnityTest]
        public IEnumerator EveryRampFamilyHasOpenAndJumpableExitsWithoutLosingObjects()
        {
            yield return Load();
            for (int family = 0; family < 8; family++)
            {
                if (family == (int)TrackToy.Seesaw) continue;
                foreach (int variant in new[] { 0, 11 })
                {
                    game.StartRun();
                    Clear();
                    var beat = new PlaygroundBeat(0, 0, 1, family, variant, 20);
                    Place(beat, 20);
                    int count = 0, exitBlockers = 0;
                    float gate = 20 + TabletopCourseRules.GateOffset(beat, 0);
                    foreach (var hazard in game.Arcade.Pool)
                    {
                        if (!hazard.InUse) continue;
                        count++;
                        if (Mathf.Abs(hazard.transform.position.x - beat.RiskX) < .01f
                            && Mathf.Abs(hazard.transform.position.z - gate) < 2)
                        {
                            exitBlockers++;
                            Assert.That(hazard.Height, Is.LessThan(.7f));
                        }
                    }
                    Assert.That(count, Is.EqualTo(6), "Objects must move, not vanish.");
                    Assert.That(exitBlockers, Is.EqualTo(variant == 0 ? 0 : 1), "family=" + family);
                    if (family == (int)TrackToy.Windup)
                    {
                        game.Player.StopAt(-2.25f);
                        yield return null;
                        OpeningRuntimeAuditTests.Capture(game, 0, "Choices-ramp-" + (variant == 0 ? "open" : "obstacle"));
                    }
                }
            }
        }
        [UnityTest]
        public IEnumerator OptionalRampAndFirstGateBypassBothWorkWithoutPowers()
        {
            yield return Load();
            foreach (bool takeRamp in new[] { false, true })
            foreach (int variant in new[] { 0, 11 })
            foreach (int fps in new[] { 30, 60, 120 })
            {
                game.StartRun();
                Clear();
                var beat = new PlaygroundBeat(0, 0, 1, (int)TrackToy.Windup, variant, 20);
                Place(beat, 20);
                float x = beat.SafeX;
                while (game.Arcade.Distance < 84)
                {
                    float local = game.Arcade.Distance - beat.Distance;
                    float target = TabletopCourseRules.WitnessX(beat, game.Arcade.Distance);
                    if (takeRamp && local >= 11 && local < TabletopCourseRules.GateOffset(beat, 0) + 4)
                        target = beat.RiskX;
                    float next = Mathf.MoveTowards(x, target, game.Config.MaxLateralSpeed / fps);
                    game.Arcade.Step(1f / fps, x, next);
                    game.Player.StopAt(next);
                    x = next;
                    Assert.That(game.State, Is.EqualTo(RunState.Running), "ramp=" + takeRamp + " variant=" + variant + " fps=" + fps + " distance=" + game.Arcade.Distance);
                }
                Assert.That(game.GetComponent<PlaygroundRun>().Assisted, Is.False);
                Assert.That(game.GetComponent<JoyfulRun>().Jumps, Is.EqualTo(takeRamp ? 1 : 0));
            }
        }
    }
}
