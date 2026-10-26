using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object=UnityEngine.Object;

namespace BrakeToy.Tests
{
    public sealed class PlayerExperienceSceneTests
    {
        private static void Click(Button button)
        {
            Canvas.ForceUpdateCanvases();
            var canvas=button.GetComponentInParent<Canvas>();
            var r=button.GetComponent<RectTransform>();
            var point=RectTransformUtility.WorldToScreenPoint(canvas.renderMode==RenderMode.ScreenSpaceOverlay?null:canvas.worldCamera,r.TransformPoint(r.rect.center));
            var data=new PointerEventData(EventSystem.current){position=point};
            var hits=new List<RaycastResult>();EventSystem.current.RaycastAll(data,hits);
            Assert.That(hits.Count,Is.GreaterThan(0),button.name);
            Assert.That(hits[0].gameObject.GetComponentInParent<Button>(),Is.EqualTo(button),"Covered button: "+button.name);
            ExecuteEvents.Execute(button.gameObject,data,ExecuteEvents.pointerClickHandler);
        }
        [UnityTest]
        public IEnumerator MenuSettingsBackAndZeroAirResultAreHonestAndClickable()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");yield return null;yield return null;
            var game=Object.FindFirstObjectByType<GameManager>();var front=game.GetComponent<ReviewFrontend>();var safe=game.UI.SafeArea;
            Assert.That(game.GetComponent<PlayerExperiencePresentation>(),Is.Not.Null);
            OpeningRuntimeAuditTests.Capture(game,0,"Experience-menu");
            Click(safe.Find("Ana menü/DAHA FAZLA").GetComponent<Button>());
            yield return null;
            Assert.That(safe.Find("Diğer seçenekler").gameObject.activeSelf,Is.True);
            OpeningRuntimeAuditTests.Capture(game,0,"Experience-more");
            Click(safe.Find("Diğer seçenekler/SIRALAMA").GetComponent<Button>());
            yield return null;
            Assert.That(front.Page,Is.EqualTo("Sıralama"));
            Assert.That(safe.Find("Diğer seçenekler").gameObject.activeSelf,Is.False);
            Click(safe.Find("Sıralama/GERİ").GetComponent<Button>());
            front.SetPage("Ayarlar");yield return null;
            Click(safe.Find("Ayarlar/YAZI BOYUTUNU DEĞİŞTİR").GetComponent<Button>());
            foreach(var pair in new[]{("SES","Ses paneli"),("GÖRÜNTÜ","Görüntü paneli"),("ÖLÇÜM","Ölçüm paneli")})
            {
                Click(safe.Find("Ayarlar/"+pair.Item1).GetComponent<Button>());yield return null;
                OpeningRuntimeAuditTests.Capture(game,0,"Experience-"+pair.Item1);
                Click(safe.Find(pair.Item2+"/GERİ").GetComponent<Button>());yield return null;
                Assert.That(safe.Find(pair.Item2).gameObject.activeSelf,Is.False);
            }
            OpeningRuntimeAuditTests.Capture(game,0,"Experience-settings-large");
            foreach(var size in new[]{new Vector2Int(720,1280),new Vector2Int(1600,2560)})
            {
                front.SetPage("Ayarlar");yield return null;
                OpeningRuntimeAuditTests.Capture(game,0,"Experience-settings-"+size.x,size.x,size.y,
                    ()=>Click(safe.Find("Ayarlar/GERİ").GetComponent<Button>()));
                Assert.That(front.Page,Is.EqualTo("Ana menü"));
            }
            yield return null;
            Assert.That(front.Page,Is.EqualTo("Ana menü"));
            int charges=game.Review.Data.jetCharges;
            game.StartRun();yield return null;game.Crash();yield return null;
            var result=safe.Find("Sonuç/Results").GetComponent<Text>().text;
            Assert.That(result,Does.Contain("Hava ek ödülü: 0 TP"));
            Assert.That(result,Does.Not.Contain("PLAYGROUND"));Assert.That(result,Does.Not.Contain("uygulandı"));
            Assert.That(game.Review.Data.jetCharges,Is.EqualTo(charges));
            OpeningRuntimeAuditTests.Capture(game,0,"Experience-zero-air-result");
        }
        [UnityTest]
        public IEnumerator SculptedCarsAndReversibleTableHaveValidReferencesAndBounds()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");yield return null;yield return null;
            var game=Object.FindFirstObjectByType<GameManager>();var front=game.GetComponent<ReviewFrontend>();
            var finish=game.GetComponent<TableFinishSwitch>();Assert.That(finish,Is.Not.Null);
            finish.Apply(false);Assert.That(finish.Surfaces[0].sharedMaterial,Is.EqualTo(finish.Original[0]));
            OpeningRuntimeAuditTests.Capture(game,0,"Experience-table-original");
            finish.Apply(true);Assert.That(finish.Surfaces[0].sharedMaterial,Is.EqualTo(finish.ReferenceFinish));
            OpeningRuntimeAuditTests.Capture(game,0,"Experience-table-reference");
            front.SetPage("Garaj");front.Browse(-game.Review.Data.selectedCar);yield return null;
            for(int i=0;i<10;i++)
            {
                var visual=game.Review.Visuals[i];
                Assert.That(game.Review.Vehicles[i].VisualPrefab,Is.Not.Null);
                Assert.That(visual.Cast<Transform>().Count(t=>t.name.StartsWith("Wheel")),Is.EqualTo(4));
                foreach(var renderer in visual.GetComponentsInChildren<Renderer>(true))
                    Assert.That(renderer.sharedMaterials.All(m=>m!=null),Is.True);
                var bounds=TidyRoom.BoundsOf(visual);Assert.That(bounds.size.x,Is.LessThan(1.8f));Assert.That(bounds.size.z,Is.LessThan(2f));
                if(i>=5&&i<=7)
                {
                    Assert.That(visual.Find("Sculpted hull"),Is.Not.Null);
                    foreach(Transform lamp in visual)
                        if(lamp.name=="Inset front lamp")Assert.That(lamp.localPosition.z,Is.EqualTo(.43f).Within(.001f));
                    OpeningRuntimeAuditTests.Capture(game,0,"Experience-car-"+i);
                }
                front.Browse(1);yield return null;
            }
        }
        [UnityTest]
        public IEnumerator BenchmarkIsolationDisablesAllPersistenceBeforeWitnessPreparation()
        {
            yield return SceneTestPersistence.Load("ToyRoomAdventure");yield return null;yield return null;
            var game=Object.FindFirstObjectByType<GameManager>();var probe=game.GetComponent<PerformanceProbe>();
            probe.IsolateBenchmark();game.StartRun();probe.PrepareWitness();
            Assert.That(game.Review.Persist,Is.False);Assert.That(game.Arcade.PersistRecords,Is.False);
            Assert.That(game.GetComponent<PlaygroundRun>().PersistPreferences,Is.False);
            Assert.That(game.GetComponent<ToyAudioSettings>().PersistPreferences,Is.False);
            Assert.That(game.GetComponent<GraphicsProfileController>().PersistPreferences,Is.False);
        }
    }
}
