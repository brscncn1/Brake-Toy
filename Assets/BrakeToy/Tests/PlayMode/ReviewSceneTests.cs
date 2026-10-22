using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using PlayMode=BrakeToy.Core.PlayMode;
namespace BrakeToy.Tests
{
    public sealed class ReviewSceneTests
    {
        [Serializable]private sealed class Parity{public int seed;public float distance;public int style;public List<ReviewOnline.InputStep> steps;}
        private IEnumerator Load(){yield return SceneManager.LoadSceneAsync("ToyRoomReview");yield return null;var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.Arcade.PersistRecords=false;g.Review.Persist=false;g.GetComponent<ToyAudioSettings>().PersistPreferences=false;g.GetComponent<GraphicsProfileController>().PersistPreferences=false;}
        [UnityTest]public IEnumerator TenModelsUseSameColliderAndFourAnimatedWheels()
        {yield return Load();var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();g.Review.Data.points=100000;var collider=g.Player.GetComponent<BoxCollider>().size;var meshIds=new HashSet<int>();for(int i=0;i<10;i++){Assert.That(g.Review.BuyOrSelect(i),Is.True);Assert.That(g.Player.Visual,Is.SameAs(g.Review.Visuals[i]));Assert.That(g.Player.GetComponent<ToyWheelAnimation>().Wheels.Length,Is.EqualTo(4));Assert.That(g.Player.GetComponent<BoxCollider>().size,Is.EqualTo(collider));var b=new Bounds(Vector3.zero,Vector3.zero);foreach(var r in g.Player.Visual.GetComponentsInChildren<Renderer>())b.Encapsulate(r.bounds);Assert.That(b.size.x,Is.LessThan(.79f));Assert.That(b.max.y,Is.LessThan(.62f));Assert.That(b.size.z,Is.LessThan(1.405f));meshIds.Add(g.Player.Visual.GetComponentsInChildren<MeshFilter>().First(f=>f.name.Contains("Enamel")).sharedMesh.GetInstanceID());}Assert.That(meshIds.Count,Is.EqualTo(10));}
        [UnityTest]public IEnumerator RelaxLosesExactlyThreeLivesAndNeverWritesArcadeRecords()
        {yield return Load();var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();g.Review.Mode=PlayMode.Relax;float best=g.Arcade.BestDistance;g.StartRun();Assert.That(g.Arcade.Speed,Is.EqualTo(7));for(int hit=1;hit<=3;hit++){foreach(var h in g.Arcade.Pool)h.Release();g.Arcade.Pool[0].Place(0,.8f,0,0);g.Arcade.Step(.1f,0,0);Assert.That(g.Review.Lives,Is.EqualTo(3-hit));Assert.That(g.State,Is.EqualTo(hit<3?RunState.Running:RunState.Crashed));}Assert.That(g.Arcade.BestDistance,Is.EqualTo(best));int points=g.Review.Data.points;g.Review.Finish();Assert.That(g.Review.Data.points,Is.EqualTo(points));}
        [UnityTest]public IEnumerator AllPagesAndDailyOfflinePracticeWorkWithoutOnlineEligibility()
        {yield return Load();var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();var frontend=g.GetComponent<ReviewFrontend>();foreach(string page in new[]{"Garaj","Görevler","Ayarlar","Sıralama","Bilgi","Ana menü"}){frontend.SetPage(page);yield return null;Assert.That(frontend.Page,Is.EqualTo(page));}var online=g.GetComponent<ReviewOnline>();Assert.That(online.Configured,Is.False);online.StartOnline(PlayMode.Arcade);yield return null;Assert.That(g.State,Is.EqualTo(RunState.Ready));frontend.StartMode(PlayMode.Daily);int seed=g.Arcade.CurrentSeed;g.Crash();g.StartSameRoute();Assert.That(g.Arcade.CurrentSeed,Is.EqualTo(seed));Assert.That(online.Recording,Is.False);}
        [UnityTest]public IEnumerator ArcadeAndRelaxWitnessesKeepFairCorridorsAndPools()
        {
            yield return Load();var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();g.Arcade.VaryRuns=false;
            foreach(var mode in new[]{PlayMode.Arcade,PlayMode.Relax})
            {g.Review.Mode=mode;g.StartRun();var stream=new ArcadeSequence(g.Arcade.CurrentSeed,true,true,mode==PlayMode.Relax);var rows=new List<ArcadeRow>();for(int i=0;i<100;i++)rows.Add(stream.Next());float x=0;int row=0;
                for(int i=0;i<7200;i++){while(row<rows.Count-1&&rows[row].Distance+3<g.Arcade.Distance)row++;float next=Mathf.MoveTowards(x,ArcadeRules.LaneX(rows[row].SafeLane),g.Config.MaxLateralSpeed/60);g.Arcade.Step(1f/60,x,next);x=next;Assert.That(g.State,Is.EqualTo(RunState.Running));}Assert.That(g.Review.Lives,Is.EqualTo(mode==PlayMode.Relax?3:1));Assert.That(g.Arcade.PoolExhaustions,Is.Zero);}
        }
        [UnityTest]public IEnumerator ExportActualUnityTraceForServerParity()
        {
            yield return Load();var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();g.Arcade.VaryRuns=false;Directory.CreateDirectory("Tests/ServerParity");
            foreach(int seed in new[]{7331,7332})
            {g.Arcade.Seed=seed;g.Review.Mode=PlayMode.Arcade;g.StartRun();var sequence=new ArcadeSequence(seed,true,true);var rows=new List<ArcadeRow>();for(int i=0;i<32;i++)rows.Add(sequence.Next());var trace=new List<ReviewOnline.InputStep>();int row=0;float x=0;
                for(int i=0;i<1800;i++){while(row<rows.Count-1&&rows[row].Distance+3<g.Arcade.Distance)row++;var r=rows[row];float target=ArcadeRules.LaneX(r.SafeLane);if(!r.Ball)target+=r.SafeLane==0?.72f:r.SafeLane==2?-.72f:r.IsBlocked(0)?-.72f:.72f;float next=Mathf.MoveTowards(x,target,g.Config.MaxLateralSpeed/60);trace.Add(new ReviewOnline.InputStep{dt=1f/60,x=next});g.Arcade.Step(1f/60,x,next);x=next;Assert.That(g.State,Is.EqualTo(RunState.Running));}
                File.WriteAllText("Tests/ServerParity/"+seed+".json",JsonUtility.ToJson(new Parity{seed=seed,distance=g.Arcade.Distance,style=g.Arcade.Style.Total,steps=trace}));}
        }
    }
}
