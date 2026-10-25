using UnityEngine;

namespace BrakeToy
{
    public sealed class ArcadeHazard : MonoBehaviour
    {
        public bool RollingBall;
        public float HalfWidth = .8f, HalfLength = .65f;
        public Transform Model;
        public int MotionKind;public float CurveAmplitude,CurveFrequency=.075f,CurvePhase,ForwardFactor=1,Height=1.3f;
        [System.NonSerialized] public bool InUse, Scored;
        [System.NonSerialized] public float Slope, CrossingX, BestGap;
        private Transform[] wheels;
        private void Awake(){if(Model){var found=new System.Collections.Generic.List<Transform>();foreach(var t in Model.GetComponentsInChildren<Transform>())if(t.name.StartsWith("Wheel unit"))found.Add(t);wheels=found.ToArray();}}
        public void Place(float x, float z, float crossing, float slope)
        {
            InUse = true; Scored = false; BestGap = float.PositiveInfinity;
            CrossingX = crossing; Slope = slope;
            transform.position = new Vector3(x, 0, z);
            if (Model) Model.localRotation = Quaternion.identity;
            gameObject.SetActive(true);
        }
        public void Release() { InUse = false; gameObject.SetActive(false); }
        public BrakeToy.Core.BallPath BallPath;
        public void ConfigureRollingBall(float crossingDepth, float laneX, int variant, int stage)
        {
            MotionKind = 3;
            BallPath = BrakeToy.Core.RollingBallRules.Profile(variant);
            ForwardFactor = BrakeToy.Core.RollingBallRules.SpeedFactor(variant, stage);
            CrossingX = laneX;
            CurvePhase = variant * .13f;
            CurveAmplitude = .55f;
            // Match the authored crossing time, despite independent forward motion.
            transform.position = new Vector3(laneX, 0, crossingDepth * ForwardFactor);
            transform.position = PositionAfter(0);
        }
        public Vector3 PositionAfter(float distance)
        {
            float z = transform.position.z - distance;
            if (MotionKind == 3)
            {
                z = transform.position.z - distance * ForwardFactor;
                return new Vector3(CrossingX + BrakeToy.Core.RollingBallRules.Offset(BallPath,
                    z / ForwardFactor, CurvePhase, CurveAmplitude), 0, z);
            }
            if(MotionKind!=0){z=transform.position.z-distance*ForwardFactor;return new Vector3(BrakeToy.Core.JoyfulRules.MotionX(CrossingX,z/ForwardFactor,CurveAmplitude,CurveFrequency,CurvePhase),0,z);}
            return new Vector3(RollingBall ? CrossingX + Slope * z : transform.position.x, 0, z);
        }
        public void Advance(float distance)
        {
            var old = transform.position; transform.position = PositionAfter(distance);
            if(MotionKind==2&&wheels!=null){float angle=(transform.position.x-old.x)/.18f*Mathf.Rad2Deg;foreach(var wheel in wheels)wheel.Rotate(Vector3.forward,-angle,Space.Self);}
            if (RollingBall && Model)
            {
                BallRolling.Apply(Model, BallRolling.TableDisplacement(transform.position - old, distance), HalfWidth);
            }
        }
    }
}
