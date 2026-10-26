using System.Collections;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace BrakeToy.Tests
{
    public sealed class FreshEyesReviewSceneTests
    {
        [UnityTest]
        public IEnumerator CaptureFirstMinuteAndProductPagesWithoutSpendingOrSaving()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");
            yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var front = game.GetComponent<ReviewFrontend>();
            var cycle = game.GetComponent<RoomDayCycle>();
            cycle.Animate = false;
            cycle.TimeOfDay = .25f;
            cycle.Apply();
            front.SetPage("Ana menü");
            yield return null;
            OpeningRuntimeAuditTests.Capture(game, 0, "Review-menu");
            game.Review.Mode = BrakeToy.Core.PlayMode.Arcade;
            game.Arcade.VaryRuns = false;
            game.Arcade.Seed = 7331;
            game.StartRun();
            var sequence = new AdventureSequence(7331, false, true);
            var beats = new List<PlaygroundBeat>();
            for (int n = 0; n < 100; n++) beats.Add(sequence.Next());
            int row = 0;
            float x = 0;
            for (int frame = 1; frame <= 3600; frame++)
            {
                while (row < beats.Count - 1 && beats[row].Distance + 64 < game.Arcade.Distance) row++;
                float target = TabletopCourseRules.WitnessX(beats[row], game.Arcade.Distance);
                float next = Mathf.MoveTowards(x, target, game.Config.MaxLateralSpeed / 60);
                game.Arcade.Step(1f / 60, x, next);
                game.Player.StopAt(next);
                x = next;
                Assert.That(game.State, Is.EqualTo(RunState.Running));
                if (frame != 600 && frame != 1800 && frame != 3600) continue;
                yield return null;
                OpeningRuntimeAuditTests.Capture(game, frame / 60, "Review-run-" + frame / 60);
            }
            game.Pause();
            yield return null;
            OpeningRuntimeAuditTests.Capture(game, 0, "Review-pause");
            foreach (string page in new[] { "Görevler", "Ayarlar", "Sıralama", "Garaj" })
            {
                front.SetPage(page);
                yield return null;
                OpeningRuntimeAuditTests.Capture(game, 0, "Review-page-" + (page == "Görevler" ? "missions" : page == "Ayarlar" ? "settings" : page == "Sıralama" ? "records" : "garage-0"));
            }
            for (int car = 1; car < 3; car++)
            {
                front.Browse(1);
                yield return null;
                OpeningRuntimeAuditTests.Capture(game, 0, "Review-page-garage-" + car);
            }
            game.Resume();
            game.Crash();
            yield return null;
            OpeningRuntimeAuditTests.Capture(game, 0, "Review-result");
            Assert.That(game.Review.Persist, Is.False);
            Assert.That(game.GetComponent<PlaygroundRun>().Assisted, Is.False);
        }
    }
}
