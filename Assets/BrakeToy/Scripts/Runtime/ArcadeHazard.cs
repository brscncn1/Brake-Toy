using UnityEngine;

namespace BrakeToy
{
    public sealed class ArcadeHazard : MonoBehaviour
    {
        public bool RollingBall;
        public float HalfWidth = .8f, HalfLength = .65f;
        public Transform Model;
        [System.NonSerialized] public bool InUse, Scored;
        [System.NonSerialized] public float Slope, CrossingX, BestGap;
        public void Place(float x, float z, float crossing, float slope)
        {
            InUse = true; Scored = false; BestGap = float.PositiveInfinity;
            CrossingX = crossing; Slope = slope;
            transform.position = new Vector3(x, 0, z);
            if (Model) Model.localRotation = Quaternion.identity;
            gameObject.SetActive(true);
        }
        public void Release() { InUse = false; gameObject.SetActive(false); }
        public Vector3 PositionAfter(float distance)
        {
            float z = transform.position.z - distance;
            return new Vector3(RollingBall ? CrossingX + Slope * z : transform.position.x, 0, z);
        }
        public void Advance(float distance)
        {
            var old = transform.position; transform.position = PositionAfter(distance);
            if (RollingBall && Model)
            {
                Vector3 delta = transform.position - old;
                Vector3 axis = Vector3.Cross(Vector3.up, delta).normalized;
                Model.Rotate(axis, delta.magnitude / HalfWidth * Mathf.Rad2Deg, Space.World);
            }
        }
    }
}
