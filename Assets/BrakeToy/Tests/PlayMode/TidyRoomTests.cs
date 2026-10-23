using System.Collections;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
namespace BrakeToy.Tests
{
    public sealed class TidyRoomTests
    {
        private GameManager game;private TidyRoom tidy;
        private IEnumerator Load(){yield return SceneManager.LoadSceneAsync("ToyRoomTidy");yield return null;game=Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Review.Persist=false;game.Arcade.PersistRecords=false;game.GetComponent<PlaygroundRun>().PersistPreferences=false;tidy=game.GetComponent<TidyRoom>();}
        [UnityTest]public IEnumerator AllThreeZonesReserveNotebookAndPencilSpaceAcrossFourFullCycles()
        {yield return Load();for(float distance=0;distance<=256;distance+=.5f){tidy.Apply(distance);foreach(var cell in tidy.Cells){var b=TidyRoom.BoundsOf(cell);Assert.That(Mathf.Min(Mathf.Abs(b.min.x),Mathf.Abs(b.max.x)),Is.GreaterThan(3.6f));foreach(var prop in tidy.NearProps){var a=TidyRoom.BoundsOf(prop);bool x=b.max.x+.15f>a.min.x&&b.min.x-.15f<a.max.x;bool z=b.max.z+.35f>a.min.z&&b.min.z-.35f<a.max.z;Assert.That(x&&z,Is.False,"distance="+distance+" cell="+cell.name+" prop="+prop.name);Assert.That(Mathf.Max(Mathf.Abs(a.min.x),Mathf.Abs(a.max.x)),Is.LessThan(5.16f));Assert.That(Mathf.Min(Mathf.Abs(a.min.x),Mathf.Abs(a.max.x)),Is.GreaterThan(3.55f));}}}tidy.Apply(0);}
        [UnityTest]public IEnumerator RadioStaysOnShelfAndDominoHasNoGeometryOrMenuSwitch()
        {yield return Load();var b=TidyRoom.BoundsOf(tidy.Radio);Assert.That(b.min.y,Is.EqualTo(tidy.ShelfTop+.025f).Within(.005f));Assert.That(b.min.x,Is.GreaterThan(-13.04f));Assert.That(b.max.x,Is.LessThan(-7.36f));Assert.That(b.min.z,Is.GreaterThan(25.6f));Assert.That(b.max.z,Is.LessThan(28.4f));Vector3 at=tidy.Radio.position;tidy.Apply(123);Assert.That(tidy.Radio.position,Is.EqualTo(at));foreach(var p in game.GetComponent<PlaygroundEncounters>().Props)Assert.That(p.Dominoes.Length,Is.Zero);Assert.That(game.UI.SafeArea.Find("Ayarlar/DOMİNO"),Is.Null);Assert.That(game.GetComponent<PlaygroundRun>().DominoEnabled,Is.False);Assert.That(game.Arcade.Speed,Is.EqualTo(JoyfulRules.Speed(0)).Within(.001f));}
    }
}
