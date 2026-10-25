using System;
using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    [Serializable]public sealed class AdventureSolid
    {public Transform Part;public Vector3 Centre,Half;public bool Sphere;}
    public sealed class AdventureToy:MonoBehaviour
    {
        public TrackToy Kind;public Transform[] Moving;public Transform Signal,ReadySignal,Shadow,Link;public Transform[] ExitArrows;public Transform[] Options;public AdventureSolid[] Solids;
        [NonSerialized]public bool InUse,Launched,Notified;[NonSerialized]public float Phase;[NonSerialized]public int Variant,SafeLane,RiskLane;
        private Vector3[] oldCentres;
        private void Awake(){oldCentres=new Vector3[Moving.Length];}
        public Vector3 Motion(int index,float z)
        {
            float p=AdventureRules.Phase(z,Phase)+index*1.8f;
            switch(Kind){case TrackToy.Tunnel:return new Vector3((index-1)*2.25f,0,0);case TrackToy.Windup:return new Vector3(Mathf.Sin(p)*.34f,Variant%2==0?AdventureRules.Hop(p):Mathf.Abs(Mathf.Sin(p*2))*.07f,0);
            case TrackToy.Crane:return new Vector3(Mathf.Sin(p)*.9f,3.18f-Mathf.Sqrt(2.38f*2.38f-Mathf.Sin(p)*Mathf.Sin(p)*.9f*.9f),0);
            case TrackToy.Train:return new Vector3(SafeLane==1?Mathf.Sin(AdventureRules.Phase(z,Phase))*.78f:(SafeLane==0?1:-1)*(.95f+Mathf.Sin(AdventureRules.Phase(z,Phase))*.95f)+(index-1)*.66f,0,0);
            case TrackToy.Marbles:return new Vector3(RollingBallRules.Offset(RollingBallRules.Profile(Variant + index * 3),z/TravelFactor,Phase + index * 1.8f,.48f),.29f,index*3.6f);
            case TrackToy.Garage:return index==0?new Vector3(0,GarageOpening(z)*1.65f,0):new Vector3(Mathf.Sin(p)*.36f,0,5+index*2-Mathf.Clamp((35-z)*.42f,0,10));
            case TrackToy.Seesaw:return new Vector3(0,.32f,0);default:return Vector3.zero;}
        }
        public float GarageOpening(float z)=>Mathf.SmoothStep(0,1,Mathf.InverseLerp(41,32,z))*Mathf.SmoothStep(0,1,Mathf.InverseLerp(-9,-3,z));
        public float TravelFactor=>Kind==TrackToy.Marbles?RollingBallRules.SpeedFactor(Variant,Variant%4):1;
        public void Pose()
        {
            for(int i=0;i<Moving.Length;i++){Moving[i].localPosition=Motion(i,transform.position.z);}
            if(Kind==TrackToy.Seesaw&&Moving.Length>0)Moving[0].localRotation=Quaternion.Euler(Launched?7:-6.1f,0,0);
            if(Link&&Kind==TrackToy.Crane){var end=Motion(0,transform.position.z);var top=new Vector3(0,3.18f,0);Link.localPosition=(top+end)*.5f;Link.localRotation=Quaternion.FromToRotation(Vector3.up,top-end);Link.localScale=new Vector3(1,(top-end).magnitude,1);}
            if(Shadow&&Kind==TrackToy.Crane)Shadow.localPosition=new Vector3(Motion(0,transform.position.z).x,.025f,0);
            if(ReadySignal){bool ready=Kind==TrackToy.Garage?GarageOpening(transform.position.z)>.94f:transform.position.z< -1;ReadySignal.gameObject.SetActive(ready);Signal.gameObject.SetActive(!ready);}
            if(Signal){float pulse=.78f+.22f*Mathf.Sin(transform.position.z*.8f);Signal.localScale=Vector3.one*pulse;}
        }
        public void Move(float moved)
        {if(oldCentres==null)oldCentres=new Vector3[Moving.Length];for(int i=0;i<Moving.Length;i++)oldCentres[i]=Moving[i].position;transform.position-=Vector3.forward*(moved*TravelFactor);Pose();if(Kind==TrackToy.Marbles)for(int i=0;i<Moving.Length;i++)BallRolling.Apply(Moving[i],BallRolling.TableDisplacement(Moving[i].position-oldCentres[i],moved),.29f);}
        public void Release(){InUse=false;gameObject.SetActive(false);}
    }
}
