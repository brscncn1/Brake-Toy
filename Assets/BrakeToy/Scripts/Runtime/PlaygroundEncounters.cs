using System;
using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    [Serializable] public sealed class PlaygroundProp
    {public Transform Root,Gate,Signal;public Transform[] Dominoes;[NonSerialized]public ArcadeHazard Barrier;[NonSerialized]public bool InUse,Triggered;[NonSerialized]public int Kind;[NonSerialized]public float Age,Phase;}
    public sealed class PlaygroundEncounters:MonoBehaviour
    {
        public PlaygroundProp[] Props;public Transform[] RouteMarkers;
        private GameManager game;private PlaygroundRun run;
        private void Bind(){if(!game){game=GetComponent<GameManager>();run=GetComponent<PlaygroundRun>();}}
        public void ResetRun(){Bind();if(Props!=null)foreach(var p in Props){p.InUse=false;p.Barrier=null;p.Root.gameObject.SetActive(false);}if(RouteMarkers!=null)foreach(var m in RouteMarkers)m.gameObject.SetActive(false);}
        public void Spawn(PlaygroundBeat b,float z,Func<int,float,float,float,float,ArcadeHazard> spawn)
        {
            Bind();var adventure=GetComponent<AdventureEncounters>();if(adventure){adventure.Spawn(b,z,spawn);return;}int third=3-b.SafeLane-b.RiskLane;float thirdX=ArcadeRules.LaneX(third);int kind=(b.Variant%5);if(kind==3)kind=4;
            spawn(kind,thirdX,z+2,0,0);spawn((kind+1)%3,thirdX,z+12,0,0);
            // The green route stays open. Gold is an optional, rewarded timing/airborne route.
            Marker(b.SafeX,z,false);Marker(b.RiskX,z,true);
            game.Arcade.Challenges.SpawnAt(b.RiskX,z-7);game.Arcade.Challenges.SpawnAt(b.RiskX,z-3);float rampOffset=b.Family==0?4:b.Family==1?14:b.Family==3?17:15;bool removedDomino=b.Family==4&&GetComponent<TidyRoom>();game.Arcade.Challenges.SpawnAt(b.RiskX,z+(b.Family==4?(removedDomino?21:24):rampOffset+16),b.Family==4&&!removedDomino?.25f:1.8f);
            switch(b.Family)
            {
                case 0: // Read a low toy, choose a book jump over the next one.
                    GetComponent<JoyfulRun>().SpawnRamp(b.RiskX,z+4);spawn(2,b.RiskX,z+14,0,0);break;
                case 1: // Bus sweep, followed by an offset jump.
                    var bus=spawn(6,b.RiskX,z,b.RiskX,0);bus.MotionKind=2;bus.CurveAmplitude=.58f;bus.CurveFrequency=.12f;bus.CurvePhase=b.Variant*.13f;bus.transform.position=bus.PositionAfter(0);GetComponent<JoyfulRun>().SpawnRamp(b.RiskX,z+14);break;
                case 2: // Garage gate with a visible red/green signal; green lane bypass.
                    var gate=spawn(5,b.RiskX,z+2,0,0);gate.HalfWidth=1.02f;gate.HalfLength=.18f;gate.Height=1.8f;Prop(b.RiskX,z+2,gate,0,b.Variant);GetComponent<JoyfulRun>().SpawnRamp(b.RiskX,z+15);break;
                case 3: // Crossing toy train: two coupled carriages stay on the risk half.
                    for(int n=0;n<2;n++){var train=spawn(6,b.RiskX,z+n*5,b.RiskX,0);train.MotionKind=2;train.CurveAmplitude=.55f;train.CurveFrequency=.15f;train.CurvePhase=b.Variant*.1f;train.transform.position=train.PositionAfter(0);}GetComponent<JoyfulRun>().SpawnRamp(b.RiskX,z+17);break;
                case 4: // Optional domino switch opens a later toy barrier.
                    if(run.DominoEnabled&&!GetComponent<TidyRoom>()){var barrier=spawn(4,b.RiskX,z+20,0,0);Prop(b.RiskX,z,barrier,1,b.Variant);}else GetComponent<JoyfulRun>().SpawnRamp(b.RiskX,z+5);break;
                default: // Rolling ball approach followed by a jump/landing challenge.
                    var ball=spawn(3,b.RiskX,z*1.12f,b.RiskX,0);ball.MotionKind=1;ball.ForwardFactor=1.12f;ball.CurveAmplitude=.42f;ball.CurveFrequency=.09f;ball.CurvePhase=b.Variant;ball.transform.position=ball.PositionAfter(0);GetComponent<JoyfulRun>().SpawnRamp(b.RiskX,z+15);spawn(2,b.RiskX,z+26,0,0);break;
            }
        }
        public void MarkRoute(float x,float z,bool risk,float height=.025f)=>Marker(x,z,risk,height);
        private void Marker(float x,float z,bool risk,float height=.025f)
        {foreach(var m in RouteMarkers)if(!m.gameObject.activeSelf){m.position=new Vector3(x,height,z-5);m.localScale=new Vector3(risk?.7f:.5f,1,1);m.GetComponent<Renderer>().sharedMaterial=risk?RiskMaterial:SafeMaterial;m.gameObject.SetActive(true);return;}throw new InvalidOperationException("Route marker pool exhausted");}
        public Material RiskMaterial,SafeMaterial;
        private void Prop(float x,float z,ArcadeHazard h,int kind,int phase)
        {foreach(var p in Props)if(!p.InUse){p.InUse=true;p.Kind=kind;p.Barrier=h;p.Triggered=false;p.Age=0;p.Phase=phase*.1f;p.Root.position=new Vector3(x,0,z);p.Root.gameObject.SetActive(true);p.Gate.gameObject.SetActive(kind==0);p.Signal.gameObject.SetActive(kind==0);foreach(var d in p.Dominoes){d.gameObject.SetActive(kind==1);d.localRotation=Quaternion.identity;}if(kind==0)h.Model.gameObject.SetActive(false);return;}throw new InvalidOperationException("Interactive prop pool exhausted");}
        public bool IsOpen(ArcadeHazard h)
        {foreach(var p in Props)if(p.InUse&&p.Barrier==h&&p.Kind==0)return Opening(p)>.94f;return false;}
        private float Opening(PlaygroundProp p)=>Mathf.SmoothStep(0,1,Mathf.InverseLerp(-.2f,.35f,Mathf.Sin(game.Arcade.Elapsed*2.1f+p.Phase)));
        public void Advance(float dt,float moved,float oldX,float x)
        {
            Bind();foreach(var m in RouteMarkers)if(m.gameObject.activeSelf){m.position-=Vector3.forward*moved;if(m.position.z< -7)m.gameObject.SetActive(false);}
            foreach(var p in Props)
            {
                if(!p.InUse)continue;float z=p.Root.position.z;p.Root.position-=Vector3.forward*moved;
                if(p.Kind==0){float open=Opening(p);p.Gate.localPosition=new Vector3(0,open*2.3f,0);p.Signal.GetComponent<Renderer>().sharedMaterial=open>.94f?SafeMaterial:RiskMaterial;}
                else
                {
                    if(!p.Triggered&&SweptCircleBox.TryHit(oldX-p.Root.position.x,-z,x-p.Root.position.x,-p.Root.position.z,game.Config.PlayerHalfWidth,game.Config.PlayerHalfLength,.28f,out _)&&GetComponent<JoyfulRun>().Height<.5f){p.Triggered=true;p.Age=0;run.Skill(16);game.Arcade.Style.AwardMastery(120);game.Arcade.HUD.Notify("DOMİNO! · YOL AÇILIYOR +120",new Color(.25f,.12f,.3f));}
                    if(p.Triggered){p.Age+=dt;for(int i=0;i<p.Dominoes.Length;i++)p.Dominoes[i].localRotation=Quaternion.Euler(Mathf.SmoothStep(0,83,Mathf.Clamp01((p.Age-i*.065f)/.17f)),0,0);if(p.Age>.52f&&p.Barrier&&p.Barrier.InUse)p.Barrier.Release();}
                }
                if(p.Root.position.z<(p.Kind==0?-8:-28)){if(p.Barrier&&p.Barrier.Model)p.Barrier.Model.gameObject.SetActive(true);p.InUse=false;p.Root.gameObject.SetActive(false);}
            }
        }
        public void DisableDomino(){foreach(var p in Props)if(p.InUse&&p.Kind==1){if(p.Barrier)p.Barrier.Release();p.InUse=false;p.Root.gameObject.SetActive(false);}}
        public void ClearLanding(float distance){foreach(var p in Props)if(p.InUse&&p.Root.position.z<distance){if(p.Barrier)p.Barrier.Release();p.InUse=false;p.Root.gameObject.SetActive(false);}}
    }
}
