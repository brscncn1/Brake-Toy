using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    [DefaultExecutionOrder(400)]
    public sealed class JoyfulRun:MonoBehaviour
    {
        public BookRamp[] Ramps;public float Height{get;private set;}public float AirDistance{get;private set;}public int AirStyle{get;private set;}public int Jumps{get;private set;}
        public bool Airborne=>flight>=0||(playground&&playground.JetHeight()>.05f);private float flight=-1;private GameManager game;private int previousStyle;private PlaygroundRun playground;private float flightDuration=1.65f,launchHeight=.65f;private bool supported;
        private void Awake(){game=GetComponent<GameManager>();playground=GetComponent<PlaygroundRun>();}
        public void ResetRun(){Height=AirDistance=0;AirStyle=Jumps=0;flight=-1;supported=false;flightDuration=1.65f;launchHeight=.65f;previousStyle=0;foreach(var r in Ramps)r.Release();}
        public void LaunchFromToy(float height){if(flight>=0)return;flight=0;flightDuration=1.35f;launchHeight=height;Height=height;supported=false;Jumps++;game.Arcade.HUD.Notify("CETVEL SIÇRAYIŞI · TP ×2",new Color(.08f,.25f,.18f));}
        public void StopFlight(){flight=-1;Height=0;supported=false;}
        public void SpawnRamp(float x,float z){foreach(var r in Ramps)if(!r.InUse){r.Place(x,z);return;}throw new System.InvalidOperationException("Book ramp pool exhausted");}
        public float PredictHeight(float dt)=>PredictHeight(dt,0,game?game.Player.X:0);
        public float PredictHeight(float dt,float distance,float x)
        {if(!playground)return Airborne?JoyfulRules.FlightHeight(flight+dt):Height;float h=flight>=0?Flight(flight+dt):Surface(x,distance,distance);return Mathf.Max(h,playground.JetHeight(dt));}
        private float Flight(float age)=>age<0||age>=flightDuration?0:launchHeight*(1-age/flightDuration)+(flightDuration>1?1.85f:0)*Mathf.Sin(Mathf.PI*age/flightDuration);
        private float Surface(float x,float rampDistance,float toyDistance)
        {float h=GetComponent<AdventureEncounters>()?.SurfaceHeight(x,toyDistance)??0;foreach(var r in Ramps)if(r.InUse&&!r.Launched)h=Mathf.Max(h,PlaygroundRules.RampSurface(x-r.transform.position.x,-r.transform.position.z+rampDistance,game.Config.PlayerHalfWidth,game.Config.PlayerHalfLength));return h;}
        private void AdvancePlayground(float dt,float distance,float oldX,float x)
        {
            bool collecting=Airborne,hadFlight=flight>=0;float before=Height;
            if(flight>=0){AirDistance+=distance*Mathf.Clamp01((flightDuration-flight)/Mathf.Max(dt,.000001f));flight+=dt;if(flight>=flightDuration)flight=-1;}
            foreach(var ramp in Ramps){if(!ramp.InUse)continue;float a=ramp.transform.position.z,b=a-distance;ramp.transform.position-=Vector3.forward*distance;
                bool contact=Mathf.Abs(x-ramp.transform.position.x)<=.95f+game.Config.PlayerHalfWidth-.015f;
                float tip=-.975f-game.Config.PlayerHalfLength;
                if(flight<0&&!ramp.Launched&&contact&&supported&&a>tip&&b<=tip){ramp.Launched=true;flight=0;flightDuration=1.65f;launchHeight=Mathf.Max(.65f,before);Jumps++;game.Arcade.HUD.Notify("TEMİZ KALKIŞ · TP ×2",new Color(.08f,.25f,.18f));}
                if(b< -10)ramp.Release();}
            // Ramps have moved above; Adventure toys move later in PlaygroundRun.Tick.
            // Query each support at this step's endpoint so height matches the visible surface.
            GetComponent<AdventureEncounters>()?.TryRulerLaunch(distance,x);float surface=Surface(x,0,distance);if(flight<0&&supported&&surface==0&&before>.02f){flight=0;flightDuration=.3f;launchHeight=before;}
            Height=Mathf.Max(flight>=0?Flight(flight):surface,playground.JetHeight());supported=flight<0&&surface>.001f;
            if(collecting&&playground.JetHeight()>.05f&&!hadFlight)AirDistance+=distance;
            int delta=game.Arcade.Style.Total-previousStyle;if(collecting&&delta>0)AirStyle+=delta;previousStyle=game.Arcade.Style.Total;
        }
        public void Advance(float dt,float distance,float oldX,float x)
        {
            if(playground){AdvancePlayground(dt,distance,oldX,x);return;}
            bool collectingAir=Airborne;
            if(Airborne){float active=Mathf.Min(dt,Mathf.Max(0,1.65f-flight));AirDistance+=distance*(active/Mathf.Max(.000001f,dt));flight+=dt;Height=JoyfulRules.FlightHeight(flight);if(flight>=1.65f){flight=-1;Height=0;game.Arcade.Sound.Shift();}}
            else Height=0;
            foreach(var ramp in Ramps)
            {
                if(!ramp.InUse)continue;float a=ramp.transform.position.z,b=a-distance;ramp.transform.position-=Vector3.forward*distance;
                if(!Airborne&&!ramp.Launched&&Mathf.Abs(x-ramp.transform.position.x)<.55f)
                {
                    if(b<=2.5f&&b>=-.8f)Height=Mathf.Max(Height,Mathf.Lerp(0,.65f,Mathf.InverseLerp(2.5f,-.8f,b)));
                    if(a>-.8f&&b<=-.8f){ramp.Launched=true;flight=0;Height=.65f;Jumps++;game.Arcade.HUD.Notify("UÇUŞ!  TOY POINTS ×2",new Color(.35f,1,.4f));game.Arcade.Sound.NearMiss(2);}
                }
                if(b< -10)ramp.Release();
            }
            int delta=game.Arcade.Style.Total-previousStyle;if(collectingAir&&delta>0)AirStyle+=delta;previousStyle=game.Arcade.Style.Total;
        }
        private void LateUpdate()
        {if(!game||!game.Player.Visual)return;var p=game.Player.Visual.localPosition;p.y=Height;game.Player.Visual.localPosition=p;var angles=game.Player.Visual.localEulerAngles;angles.x=flight>=0?Mathf.Cos(flight/flightDuration*Mathf.PI)*-14:0;game.Player.Visual.localRotation=Quaternion.Euler(angles);}
    }
}
