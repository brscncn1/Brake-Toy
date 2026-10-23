using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    [DefaultExecutionOrder(400)]
    public sealed class JoyfulRun:MonoBehaviour
    {
        public BookRamp[] Ramps;public float Height{get;private set;}public float AirDistance{get;private set;}public int AirStyle{get;private set;}public int Jumps{get;private set;}
        public bool Airborne=>flight>=0;private float flight=-1;private GameManager game;private int previousStyle;
        private void Awake(){game=GetComponent<GameManager>();}
        public void ResetRun(){Height=AirDistance=0;AirStyle=Jumps=0;flight=-1;previousStyle=0;foreach(var r in Ramps)r.Release();}
        public void SpawnRamp(float x,float z){foreach(var r in Ramps)if(!r.InUse){r.Place(x,z);return;}throw new System.InvalidOperationException("Book ramp pool exhausted");}
        public float PredictHeight(float dt)=>Airborne?JoyfulRules.FlightHeight(flight+dt):Height;
        public void Advance(float dt,float distance,float oldX,float x)
        {
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
        {if(!game||!game.Player.Visual)return;var p=game.Player.Visual.localPosition;p.y=Height;game.Player.Visual.localPosition=p;var angles=game.Player.Visual.localEulerAngles;angles.x=Airborne?Mathf.Cos(flight/1.65f*Mathf.PI)*-14:0;game.Player.Visual.localRotation=Quaternion.Euler(angles);}
    }
}
