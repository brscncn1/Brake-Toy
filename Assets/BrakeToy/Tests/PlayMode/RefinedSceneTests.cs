using System.Collections;
using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BrakeToy.Tests
{
    public sealed class RefinedSceneTests
    {
        private IEnumerator Load()
        {yield return SceneManager.LoadSceneAsync("ToyRoomRefined");yield return null;Object.FindFirstObjectByType<GameManager>().Arcade.PersistRecords=false;Object.FindFirstObjectByType<GraphicsProfileController>().PersistPreferences=false;}
        [UnityTest]
        public IEnumerator ProfilesArePrivateAndMenuHasOnePanel()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();var q=g.GetComponent<GraphicsProfileController>();g.enabled=false;
            Assert.That(q.ActivePipeline,Is.Not.SameAs(q.Source));float scale=q.Source.renderScale;int msaa=q.Source.msaaSampleCount;
            q.SetLow(true);Assert.That(q.ActivePipeline.renderScale,Is.EqualTo(.8f));Assert.That(Application.targetFrameRate,Is.EqualTo(30));
            q.SetLow(false);Assert.That(q.ActivePipeline.renderScale,Is.EqualTo(1));Assert.That(Application.targetFrameRate,Is.EqualTo(60));
            Assert.That(q.Source.renderScale,Is.EqualTo(scale));Assert.That(q.Source.msaaSampleCount,Is.EqualTo(msaa));
            Assert.That(g.UI.Overlay.transform.Find("Menu card"),Is.Null);Assert.That(g.Arcade.Definitions.Length,Is.EqualTo(4));
            for(int i=0;i<4;i++){var d=g.Arcade.Definitions[i];Assert.That(d.Kind,Is.EqualTo(i));Assert.That(d.HalfWidth,Is.EqualTo(g.Arcade.Pool[i*6].HalfWidth));}
            g.StartRun();yield return null;Assert.That(g.Arcade.HUD.SoundButton.gameObject.activeSelf,Is.False);Assert.That(q.ToggleButton.gameObject.activeSelf,Is.False);
        }
        [UnityTest]
        public IEnumerator SameRouteReplaysAndNewRunChangesSeed()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.StartRun();int seed=g.Arcade.CurrentSeed;
            var positions=new Vector3[24];for(int i=0;i<24;i++)positions[i]=g.Arcade.Pool[i].transform.position;
            g.Arcade.Step(.1f,0,0);g.StartSameRoute();Assert.That(g.Arcade.CurrentSeed,Is.EqualTo(seed));
            for(int i=0;i<24;i++)if(g.Arcade.Pool[i].InUse)Assert.That(g.Arcade.Pool[i].transform.position,Is.EqualTo(positions[i]));
            g.StartRun();Assert.That(g.Arcade.CurrentSeed,Is.Not.EqualTo(seed));
        }
        [UnityTest]
        public IEnumerator ImpactKeepsCollisionRootFixedAndRestoresVisuals()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.StartRun();
            var root=g.Player.transform.position;var local=g.Player.Visual.localPosition;
            g.GetComponent<ToyImpactFeedback>().Play();g.Crash();yield return null;
            Assert.That(g.GetComponent<ToyImpactFeedback>().IsPlaying,Is.True);Assert.That(g.UI.Overlay.activeSelf,Is.False);
            Assert.That(g.Player.transform.position,Is.EqualTo(root));
            yield return new WaitForSecondsRealtime(.5f);Assert.That(g.GetComponent<ToyImpactFeedback>().IsPlaying,Is.False);
            Assert.That(g.Player.Visual.localPosition,Is.EqualTo(local));Assert.That(g.UI.Overlay.activeSelf,Is.True);
            g.StartRun();Assert.That(g.Player.Visual.localPosition,Is.EqualTo(local));
        }
        [UnityTest]
        public IEnumerator StartupPoolAndDetailedModelsAreValid()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();var a=g.Arcade;
            Assert.That(g.State,Is.EqualTo(RunState.Ready));Assert.That(a.Pool.Length,Is.EqualTo(24));Assert.That(g.World.Obstacles.Length,Is.Zero);
            Assert.That(g.Player.GetComponent<ToyWheelAnimation>().Wheels.Length,Is.EqualTo(4));
            Assert.That(GameObject.Find("Bedroom Set").GetComponentsInChildren<Collider>().Length,Is.Zero);
            var bounds=new Bounds(g.Player.transform.position,Vector3.zero);
            foreach(var r in g.Player.GetComponentsInChildren<Renderer>()){Assert.That(r.sharedMaterial.shader.name,Is.Not.EqualTo("Hidden/InternalErrorShader"));bounds.Encapsulate(r.bounds);}
            Assert.That(bounds.min.y,Is.GreaterThanOrEqualTo(-.005f));Assert.That(bounds.max.y,Is.LessThanOrEqualTo(.61f));
            Assert.That(bounds.size.x,Is.LessThanOrEqualTo(.785f));Assert.That(bounds.size.z,Is.LessThanOrEqualTo(1.405f));
            var checkedMeshes=new HashSet<Mesh>();
            foreach(var f in Object.FindObjectsByType<MeshFilter>(FindObjectsInactive.Include,FindObjectsSortMode.None))if(checkedMeshes.Add(f.sharedMesh))
            {
                Assert.That(f.sharedMesh,Is.Not.Null);
                foreach(var v in f.sharedMesh.vertices)Assert.That(float.IsNaN(v.x)||float.IsInfinity(v.x)||float.IsNaN(v.y)||float.IsNaN(v.z),Is.False,f.name);
                foreach(var index in f.sharedMesh.triangles)Assert.That(index,Is.InRange(0,f.sharedMesh.vertexCount-1));
            }
            foreach(var h in a.Pool)if(h.RollingBall)
            {
                Assert.That(h.Model.localPosition.y,Is.EqualTo(.51f).Within(.002f));h.gameObject.SetActive(true);h.Place(0,20,0,.12f);
                foreach(float angle in new[]{0,90,180,270})
                {
                    h.Model.localRotation=Quaternion.Euler(angle,angle*.7f,angle*.3f);
                    var r=h.Model.GetComponentInChildren<Renderer>();Assert.That(r.bounds.center.y,Is.EqualTo(.51f).Within(.025f));
                    // Renderer.bounds rotates a conservative cube; inspect actual vertices for a sphere's floor contact.
                    foreach(var f in h.Model.GetComponentsInChildren<MeshFilter>())foreach(var v in f.sharedMesh.vertices)
                        Assert.That(f.transform.TransformPoint(v).y,Is.GreaterThan(-.015f));
                }
                h.Release();
            }
        }
        [UnityTest]
        public IEnumerator AccelerationPauseAndDeterministicRetryReuseInstances()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();var a=g.Arcade;g.enabled=false;g.StartRun();
            var ids=new int[24];for(int i=0;i<24;i++)ids[i]=a.Pool[i].GetInstanceID();
            a.Step(.1f,0,0);float elapsed=a.Elapsed,distance=a.Distance;
            g.Pause();a.Step(1,0,0);Assert.That(a.Elapsed,Is.EqualTo(elapsed));Assert.That(a.Distance,Is.EqualTo(distance));
            g.Resume();a.Step(.1f,0,0);Assert.That(a.Speed,Is.GreaterThan(10));
            g.StartRun();Assert.That(a.Distance,Is.Zero);Assert.That(a.Style.Total,Is.Zero);
            for(int i=0;i<24;i++)Assert.That(a.Pool[i].GetInstanceID(),Is.EqualTo(ids[i]));
            yield return null;
        }
        [UnityTest]
        public IEnumerator FirstChallengeArrivesQuicklyAndRetryIsImmediate()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();var a=g.Arcade;g.enabled=false;g.StartRun();
            for(int i=0;i<180&&g.State==RunState.Running;i++)a.Step(1f/60,0,0);
            Assert.That(g.State,Is.EqualTo(RunState.Crashed));Assert.That(a.Elapsed,Is.InRange(1.5f,2.5f));
            g.StartRun();Assert.That(g.State,Is.EqualTo(RunState.Running));Assert.That(a.Elapsed,Is.Zero);Assert.That(g.Player.X,Is.Zero);
        }
        [UnityTest]
        public IEnumerator NearMissAwardsOnceAndMovingBallUsesRelativeSweep()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.StartRun();var a=g.Arcade;
            foreach(var h in a.Pool)h.Release();var hazard=a.Pool[0];float half=g.Config.PlayerHalfWidth+hazard.HalfWidth;
            hazard.Place(half+.2f,2,0,0);
            for(int i=0;i<30;i++)a.Step(1f/60,0,0);
            Assert.That(a.Style.NearMisses,Is.EqualTo(1));int total=a.Style.Total;
            for(int i=0;i<30;i++)a.Step(1f/60,0,0);
            Assert.That(a.Style.Total,Is.EqualTo(total));
            foreach(var h in a.Pool)h.Release();var ball=a.Pool[18];ball.Place(0,1.5f,0,.12f);
            a.Step(.1f,-2,2);Assert.That(g.State,Is.EqualTo(RunState.Crashed));Assert.That(a.Style.Chain,Is.Zero);
        }
        [UnityTest]
        public IEnumerator TwoMinuteWitnessDriveHandlesEveryRowWithoutAllocatingHazards()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.StartRun();var a=g.Arcade;
            var sequence=new ArcadeSequence(a.CurrentSeed,true);var rows=new List<ArcadeRow>();for(int i=0;i<110;i++)rows.Add(sequence.Next());
            int index=0;float x=0;int count=Object.FindObjectsByType<ArcadeHazard>(FindObjectsInactive.Include,FindObjectsSortMode.None).Length;
            const float dt=1f/60;
            for(int i=0;i<7200;i++)
            {
                while(index<rows.Count-1&&rows[index].Distance+3<a.Distance)index++;
                float target=ArcadeRules.LaneX(rows[index].SafeLane);float next=Mathf.MoveTowards(x,target,g.Config.MaxLateralSpeed*dt);
                a.Step(dt,x,next);x=next;
                Assert.That(g.State,Is.EqualTo(RunState.Running),"row="+index+" distance="+a.Distance);
            }
            Assert.That(a.PoolExhaustions,Is.Zero);Assert.That(a.SpawnedRows,Is.GreaterThan(60));
            Assert.That(Object.FindObjectsByType<ArcadeHazard>(FindObjectsInactive.Include,FindObjectsSortMode.None).Length,Is.EqualTo(count));
        }
        [UnityTest]
        public IEnumerator FeverIsVisualOnlyAndStopsOnCrash()
        {
            yield return Load();var g=Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.StartRun();
            var bounds=g.Player.GetComponent<BoxCollider>().size;float speed=g.Arcade.Speed;
            for(int i=0;i<8;i++)g.Arcade.Style.Award();yield return null;
            var feedback=g.GetComponent<ArcadeFeedback>();Assert.That(feedback.FeverVisible,Is.True);
            foreach(var normal in feedback.GetComponentInChildren<MeshFilter>().sharedMesh.normals)
                Assert.That(normal.y,Is.GreaterThan(.9f),"Fever ribbon must face the gameplay camera, not the underside of the table.");
            Assert.That(g.Arcade.Speed,Is.EqualTo(speed));Assert.That(g.Player.GetComponent<BoxCollider>().size,Is.EqualTo(bounds));
            g.Arcade.Style.Crash();g.Crash();yield return null;Assert.That(feedback.FeverVisible,Is.False);
        }
        [UnityTest]
        public IEnumerator MaxSpeedReactionDistanceRemainsVisibleInFourPortraitRatios()
        {
            yield return Load();var camera=Camera.main;
            foreach(float aspect in new[]{9f/16,9f/19.5f,9f/21,3f/4})
            {
                camera.aspect=aspect;camera.GetComponent<PortraitCamera>().Reframe();
                foreach(float x in new[]{-3.5f,3.5f})
                {
                    var p=camera.WorldToViewportPoint(new Vector3(x,.35f,18*1.5f));Assert.That(p.x,Is.InRange(0f,1f));Assert.That(p.y,Is.InRange(.25f,.81f));
                }
            }
            camera.ResetAspect();
        }
    }
}
