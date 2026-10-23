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
    public sealed class JoyfulSceneTests
    {
        private GameManager game;
        private IEnumerator Load(){yield return SceneManager.LoadSceneAsync("ToyRoomJoyful");yield return null;game=Object.FindFirstObjectByType<GameManager>();game.enabled=false;game.Arcade.PersistRecords=false;game.Review.Persist=false;game.GetComponent<ToyAudioSettings>().PersistPreferences=false;game.GetComponent<GraphicsProfileController>().PersistPreferences=false;}
        [UnityTest]public IEnumerator AudioHasRealDistinctTracksAndExplainedThreeChannelSettings()
        {yield return Load();var music=game.GetComponent<ToyMusic>();Assert.That(music.MenuTrack,Is.Not.Null);Assert.That(music.RaceTrack,Is.Not.Null);Assert.That(music.MenuTrack,Is.Not.SameAs(music.RaceTrack));Assert.That(music.MenuTrack.length,Is.GreaterThan(80));Assert.That(music.RaceTrack.length,Is.GreaterThan(30));Assert.That(music.Menu.loop&&music.Race.loop,Is.True);Assert.That(game.Arcade.Sound.RecordedHorn,Is.Not.Null);Assert.That(game.Arcade.Sound.RecordedToyCrash,Is.Not.Null);var audio=game.GetComponent<ToyAudioSettings>();Assert.That(audio.Music,Is.Not.Null);audio.Music.value=.25f;audio.Engine.value=.20f;audio.Effects.value=.30f;Assert.That(music.Level,Is.EqualTo(.25f));Assert.That(game.Arcade.Sound.EngineLevel,Is.EqualTo(.20f));Assert.That(game.Arcade.Sound.EffectsLevel,Is.EqualTo(.30f));}
        [UnityTest]public IEnumerator NewPoolContainsToysCurvedBallsAndMovingBusWithNoNullModels()
        {yield return Load();Assert.That(game.Arcade.Definitions.Length,Is.EqualTo(7));foreach(var h in game.Arcade.Pool){Assert.That(h.Model,Is.Not.Null);Assert.That(h.GetComponentsInChildren<Collider>().Length,Is.Zero);}Assert.That(game.GetComponent<JoyfulRun>().Ramps.Length,Is.EqualTo(6));Assert.That(game.GetComponent<ReviewOnline>().Configured,Is.False);}
        [UnityTest]public IEnumerator RampLaunchHeightLandingAndDoubleTpDistanceAreReal()
        {yield return Load();game.Arcade.VaryRuns=false;game.StartRun();foreach(var h in game.Arcade.Pool)h.Release();var jump=game.GetComponent<JoyfulRun>();foreach(var r in jump.Ramps)r.Release();jump.SpawnRamp(0,2.7f);bool flew=false;for(int i=0;i<130;i++){game.Arcade.Step(1f/60,0,0);if(jump.Airborne)flew=true;Assert.That(game.State,Is.EqualTo(RunState.Running));}Assert.That(flew,Is.True);Assert.That(jump.Jumps,Is.EqualTo(1));Assert.That(jump.AirDistance,Is.GreaterThan(20));Assert.That(jump.Airborne,Is.False);Assert.That(jump.Height,Is.Zero);}
        [UnityTest]public IEnumerator SafeWitnessSurvivesGrowingArcadeAndRelaxWithReuse()
        {
            yield return Load();game.Arcade.VaryRuns=false;
            foreach(var mode in new[]{PlayMode.Arcade,PlayMode.Relax})foreach(int seed in new[]{7331,109,917})
            {game.Arcade.Seed=seed;game.Review.Mode=mode;game.StartRun();var seq=new JoyfulSequence(seed,mode==PlayMode.Relax);var rows=new List<JoyfulRow>();for(int i=0;i<100;i++)rows.Add(seq.Next());int row=0;float x=0;for(int i=0;i<7200;i++){while(row<rows.Count-1&&rows[row].Distance+10<game.Arcade.Distance)row++;float next=Mathf.MoveTowards(x,rows[row].SafeX,game.Config.MaxLateralSpeed/60);game.Arcade.Step(1f/60,x,next);x=next;Assert.That(game.State,Is.EqualTo(RunState.Running),"seed="+seed+" t="+game.Arcade.Elapsed+" row="+row);}Assert.That(game.Arcade.PoolExhaustions,Is.Zero);Assert.That(game.Review.Lives,Is.EqualTo(mode==PlayMode.Relax?3:1));}
        }
        [UnityTest]public IEnumerator DayNightChangesOutsideWhileKeepingTrackLit()
        {yield return Load();var cycle=game.GetComponent<RoomDayCycle>();cycle.Animate=false;cycle.TimeOfDay=.25f;cycle.Apply();Assert.That(cycle.Birds.activeSelf,Is.True);Assert.That(cycle.Stars.activeSelf,Is.False);Assert.That(cycle.SunLight.intensity,Is.GreaterThan(1));cycle.TimeOfDay=.75f;cycle.Apply();Assert.That(cycle.Stars.activeSelf,Is.True);Assert.That(cycle.Birds.activeSelf,Is.False);Assert.That(cycle.SunLight.intensity,Is.GreaterThan(.25f));Assert.That(cycle.RoomLamp.intensity,Is.GreaterThan(2));Assert.That(GameObject.Find("Joyful room additions — decoration only").GetComponentsInChildren<Collider>().Length,Is.Zero);}
    }
}
