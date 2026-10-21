using System.Collections;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
namespace BrakeToy.Tests
{
    public sealed class LivingSceneTests
    {
        private IEnumerator Load()
        {yield return SceneManager.LoadSceneAsync("ToyRoomLiving");yield return null;var g=Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.Arcade.PersistRecords=false;g.GetComponent<ToyAudioSettings>().PersistPreferences=false;g.GetComponent<GraphicsProfileController>().PersistPreferences=false;}
        [UnityTest] public IEnumerator RecordedAudioAndPrivateQualityProfilesAreWired()
        {yield return Load();var g=Object.FindFirstObjectByType<GameManager>();var a=g.Arcade.Sound;Assert.That(a.UseRecordedSamples,Is.True);foreach(var clip in new[]{a.RecordedRoll,a.RecordedWood,a.RecordedMetal,a.RecordedSoft}){Assert.That(clip,Is.Not.Null);Assert.That(clip.length,Is.GreaterThan(.01f));}Assert.That(g.Arcade.StyleRecordKey,Is.EqualTo("BrakeToy.v05.style"));var q=g.GetComponent<GraphicsProfileController>();var source=q.Source.renderScale;q.SetLow(true);Assert.That(Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing,Is.False);q.SetLow(false);Assert.That(Camera.main.GetUniversalAdditionalCameraData().renderPostProcessing,Is.True);Assert.That(q.Source.renderScale,Is.EqualTo(source));var mix=g.GetComponent<ToyAudioSettings>();mix.Engine.value=.2f;mix.Effects.value=.7f;Assert.That(a.EngineLevel,Is.EqualTo(.2f));Assert.That(a.EffectsLevel,Is.EqualTo(.7f));g.StartRun();yield return null;Assert.That(mix.Panel.activeSelf,Is.False);g.Pause();yield return null;Assert.That(mix.Panel.activeSelf,Is.True);}
        [UnityTest] public IEnumerator RiskRewardOccursOnceAndNeverOnImpactThenResets()
        {yield return Load();var g=Object.FindFirstObjectByType<GameManager>();g.StartRun();var c=g.Arcade.Challenges;foreach(var s in c.RiskStars)s.gameObject.SetActive(false);var star=c.RiskStars[0];star.position=new Vector3(0,.25f,.8f);star.gameObject.SetActive(true);c.Step(.1f,0,0,true);Assert.That(c.Goals.Pickups,Is.Zero);c.Step(.1f,0,0,false);Assert.That(c.Goals.Pickups,Is.EqualTo(1));Assert.That(g.Arcade.Style.Total,Is.EqualTo(75));c.Step(.1f,0,0,false);Assert.That(c.Goals.Pickups,Is.EqualTo(1));g.Pause();var before=star.position;g.Arcade.Step(1,0,0);Assert.That(star.position,Is.EqualTo(before));g.StartSameRoute();Assert.That(c.Goals.Pickups,Is.Zero);Assert.That(g.Arcade.Style.Total,Is.Zero);}
        [UnityTest] public IEnumerator CenterAndRiskWitnessesSurviveTwoMinutesAcrossSeedsWithoutGrowingPools()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();var a=g.Arcade;int count=Object.FindObjectsByType<ArcadeHazard>(FindObjectsInactive.Include,FindObjectsSortMode.None).Length;a.VaryRuns=false;
            foreach(int seed in new[]{7331,7332,9911,118})foreach(bool risk in new[]{false,true})
            {a.Seed=seed;g.StartRun();var sequence=new ArcadeSequence(seed,true,true);var rows=new List<ArcadeRow>();for(int i=0;i<110;i++)rows.Add(sequence.Next());int index=0;float x=0;
                for(int i=0;i<7200;i++){while(index<rows.Count-1&&rows[index].Distance+3<a.Distance)index++;var row=rows[index];float target=ArcadeRules.LaneX(row.SafeLane);if(risk&&!row.Ball)target+=row.SafeLane==0?.72f:row.SafeLane==2?-.72f:row.IsBlocked(0)?-.72f:.72f;float next=Mathf.MoveTowards(x,target,g.Config.MaxLateralSpeed/60);a.Step(1f/60,x,next);x=next;Assert.That(g.State,Is.EqualTo(RunState.Running),"seed="+seed+" risk="+risk+" row="+index+" m="+a.Distance);}
                Assert.That(a.PoolExhaustions+a.Challenges.PoolExhaustions,Is.Zero);if(risk){Assert.That(a.Challenges.Goals.Completed,Is.EqualTo(3));Assert.That(a.Challenges.Goals.Pickups,Is.GreaterThan(20));}
            }
            Assert.That(Object.FindObjectsByType<ArcadeHazard>(FindObjectsInactive.Include,FindObjectsSortMode.None).Length,Is.EqualTo(count));Assert.That(a.Challenges.RiskStars.Length,Is.EqualTo(12));
        }
        [UnityTest] public IEnumerator CameraPreservesReactionVisibilityAndCarAtSteeringExtremes()
        {
            yield return Load();var camera=Camera.main;var portrait=camera.GetComponent<PortraitCamera>();
            foreach(float aspect in new[]{9f/16,9f/19.5f,9f/21,3f/4})foreach(float x in new[]{-3.11f,3.11f})
            {camera.aspect=aspect;portrait.Reframe();camera.transform.position+=Vector3.right*(x*portrait.FollowAmount);foreach(float edge in new[]{-3.5f,3.5f}){var p=camera.WorldToViewportPoint(new Vector3(edge,.35f,27));Assert.That(p.x,Is.InRange(0f,1f));Assert.That(p.y,Is.InRange(.25f,.84f));}foreach(float edge in new[]{x-.39f,x+.39f}){var p=camera.WorldToViewportPoint(new Vector3(edge,.25f,0));Assert.That(p.x,Is.InRange(0f,1f));Assert.That(p.y,Is.InRange(.05f,.5f));}}
            camera.ResetAspect();
        }
        [UnityTest] public IEnumerator RebuiltCarHasFiniteGeometryAndRoomHasNoGameplayColliders()
        {yield return Load();var g=Object.FindFirstObjectByType<GameManager>();Assert.That(g.Player.GetComponent<ToyWheelAnimation>().Wheels.Length,Is.EqualTo(4));Assert.That(GameObject.Find("Bedroom Set").GetComponentsInChildren<Collider>(true).Length,Is.Zero);var b=new Bounds(g.Player.transform.position,Vector3.zero);foreach(var r in g.Player.GetComponentsInChildren<Renderer>()){b.Encapsulate(r.bounds);Assert.That(r.sharedMaterial,Is.Not.Null);}Assert.That(b.min.y,Is.GreaterThanOrEqualTo(-.005f));Assert.That(b.max.y,Is.LessThan(.61f));Assert.That(b.size.x,Is.LessThan(.79f));Assert.That(b.size.z,Is.LessThan(1.405f));foreach(var f in g.Player.GetComponentsInChildren<MeshFilter>()){foreach(var p in f.sharedMesh.vertices)Assert.That(float.IsFinite(p.x)&&float.IsFinite(p.y)&&float.IsFinite(p.z),Is.True);foreach(var n in f.sharedMesh.normals)Assert.That(float.IsFinite(n.x)&&float.IsFinite(n.y)&&float.IsFinite(n.z),Is.True);}}
        [UnityTest] public IEnumerator ImportedChairKeepsItsIntendedRoomScaleAfterMeshMerge()
        {yield return Load();var chair=GameObject.Find("Bedroom Set").transform.Find("LivingRoom • ScannedChair").GetComponent<Renderer>();Assert.That(chair.bounds.size.y,Is.EqualTo(8.1f).Within(.05f));Assert.That(chair.bounds.min.y,Is.EqualTo(-7.04f).Within(.05f));Assert.That(chair.bounds.max.x,Is.LessThan(-6.3f));}
    }
}
