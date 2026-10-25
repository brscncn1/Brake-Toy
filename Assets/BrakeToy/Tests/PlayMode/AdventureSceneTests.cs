using System.Collections;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using PlayMode=BrakeToy.Core.PlayMode;
namespace BrakeToy.Tests
{
    public sealed class AdventureSceneTests
    {
        private GameManager game;private AdventureEncounters toys;
        private IEnumerator Load(){yield return SceneTestPersistence.Load("ToyRoomAdventure");yield return null;game=Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Review.Persist=false;game.Arcade.PersistRecords=false;game.GetComponent<PlaygroundRun>().PersistPreferences=false;toys=game.GetComponent<AdventureEncounters>();}
        private void Clear(){foreach(var h in game.Arcade.Pool)h.Release();foreach(var r in game.GetComponent<JoyfulRun>().Ramps)r.Release();game.GetComponent<PlaygroundEncounters>().ResetRun();toys.ResetRun();}
        private PlaygroundBeat Beat(int family,int variant=0)=>new PlaygroundBeat(0,0,1,family,variant,0);
        [UnityTest]public IEnumerator EightFamiliesKeepReachableSafeLaneAcrossSeedsAndModes()
        {yield return Load();toys.DenseCourse=false;game.Arcade.VaryRuns=false;foreach(var mode in new[]{PlayMode.Arcade,PlayMode.Relax})foreach(int seed in new[]{7331,109,917,5,42,888}){game.Review.Mode=mode;game.Arcade.Seed=seed;game.StartRun();var seq=new AdventureSequence(seed,mode==PlayMode.Relax);var rows=new List<PlaygroundBeat>();for(int i=0;i<200;i++)rows.Add(seq.Next());int row=0;float x=0;for(int i=0;i<7200;i++){while(row<rows.Count-1&&rows[row].Distance+30<game.Arcade.Distance)row++;float next=Mathf.MoveTowards(x,rows[row].SafeX,game.Config.MaxLateralSpeed/60);game.Arcade.Step(1f/60,x,next);x=next;Assert.That(game.State,Is.EqualTo(RunState.Running),"seed="+seed+" family="+rows[row].Family+" time="+game.Arcade.Elapsed); }Assert.That(game.Review.Lives,Is.EqualTo(mode==PlayMode.Relax?3:1));Assert.That(toys.PoolExhaustions+game.Arcade.PoolExhaustions,Is.Zero);}}
        [UnityTest]public IEnumerator ToyContactsAreSweptAndMatchVisiblePartsIncludingTunnelWalls()
        {yield return Load();game.StartRun();Clear();toys.Place(TrackToy.Windup,Beat(1,1),2);Assert.That(toys.FirstHit(.1f,4,0,0,out float t),Is.True);Assert.That(t,Is.InRange(0f,1f));toys.ResetRun();toys.Place(TrackToy.Tunnel,Beat(0),2);Assert.That(toys.FirstHit(.1f,4,-2.25f,-2.25f,out _),Is.False);Assert.That(toys.FirstHit(.1f,4,-1.125f,-1.125f,out _),Is.True);foreach(var a in toys.Pool)if(a.InUse)foreach(var m in a.Moving)if(m.gameObject.activeSelf)Assert.That(m.localPosition.x,Is.EqualTo(2.25f));}
        [UnityTest]public IEnumerator TapeAndCardsOnlyAffectGroundAndResetOnRetry()
        {yield return Load();game.StartRun();Clear();toys.Place(TrackToy.Surfaces,Beat(5),0);toys.Advance(1f/60,0,0,0);Assert.That(toys.SurfaceFactor,Is.EqualTo(.62f));toys.ResetRun();toys.Place(TrackToy.Surfaces,Beat(5,1),0);toys.Advance(1f/60,0,0,0);Assert.That(toys.OnIce,Is.True);game.Review.Data.jetCharges=1;game.GetComponent<PlaygroundRun>().Activate(ToyPower.Jetpack);for(int i=0;i<30;i++){game.GetComponent<PlaygroundEncounters>().ResetRun();game.Arcade.Step(1f/60,0,0);}toys.Advance(.01f,0,0,0);Assert.That(toys.SurfaceFactor,Is.EqualTo(1));Assert.That(toys.OnIce,Is.False);game.StartRun();Assert.That(toys.OnIce,Is.False);Assert.That(toys.SurfaceFactor,Is.EqualTo(1));}
        [UnityTest]public IEnumerator RulerSupportsBothEdgesLaunchesAndAwardsAirDistance()
        {yield return Load();foreach(float x in new[]{-.7f,0,.7f}){game.StartRun();Clear();toys.Place(TrackToy.Seesaw,Beat(6),3.5f);bool climbed=false;for(int i=0;i<170;i++){game.GetComponent<JoyfulRun>().Advance(1f/60,.3f,x,x);toys.Advance(1f/60,.3f,x,x);climbed|=game.GetComponent<JoyfulRun>().Height>.3f;}var j=game.GetComponent<JoyfulRun>();Assert.That(climbed,Is.True);Assert.That(j.Jumps,Is.EqualTo(1));Assert.That(j.AirDistance,Is.GreaterThan(10));Assert.That(j.Height,Is.LessThan(.01f));}}
        [UnityTest]public IEnumerator RocketClearsNewToysAndPauseFreezesTheirPhase()
        {yield return Load();game.StartRun();Clear();var a=toys.Place(TrackToy.Windup,Beat(1,1),1);var p=a.Moving[0].position;game.Pause();game.Arcade.Step(.1f,0,0);Assert.That(a.Moving[0].position,Is.EqualTo(p));game.Resume();game.Review.Data.rocketCharges=1;Assert.That(game.GetComponent<PlaygroundRun>().Activate(ToyPower.Rocket),Is.True);game.Arcade.Step(.1f,0,0);Assert.That(a.InUse,Is.False);Assert.That(game.State,Is.EqualTo(RunState.Running));}
        [UnityTest]public IEnumerator MarbleCountSpeedGarageExitAndCraneRopeAreReal()
        {yield return Load();game.StartRun();Clear();for(int n=0;n<3;n++){var a=toys.Place(TrackToy.Marbles,Beat(4,n),40);int count=0;foreach(var m in a.Moving)if(m.gameObject.activeSelf)count++;Assert.That(count,Is.EqualTo(n+1));float before=a.transform.position.z;toys.Advance(.1f,2,-2.25f,-2.25f);Assert.That(before-a.transform.position.z,Is.GreaterThan(2));toys.ResetRun();}var train=toys.Place(TrackToy.Train,Beat(3),40);float minimum=100,maximum=-100;for(float z=0;z<80;z+=.5f){float x=train.Motion(1,z).x;minimum=Mathf.Min(minimum,x);maximum=Mathf.Max(maximum,x);}Assert.That(maximum-minimum,Is.GreaterThan(1.8f));toys.ResetRun();var garage=toys.Place(TrackToy.Garage,Beat(7),45);float initial=garage.Moving[1].localPosition.z;toys.Advance(.1f,30,-2.25f,-2.25f);Assert.That(garage.Moving[1].localPosition.z,Is.LessThan(initial-5));Assert.That(garage.Moving[0].localPosition.y,Is.GreaterThan(1.5f));var frog=toys.Place(TrackToy.Windup,Beat(1),25);Assert.That(frog.Options[1].gameObject.activeSelf,Is.True);Assert.That(frog.Options[0].gameObject.activeSelf,Is.False);var crane=toys.Place(TrackToy.Crane,Beat(2),25);Vector3 top=crane.Link.localPosition+crane.Link.localRotation*Vector3.up*crane.Link.localScale.y*.5f;Assert.That(Vector3.Distance(top,new Vector3(0,3.18f,0)),Is.LessThan(.001f));}
    }
}
