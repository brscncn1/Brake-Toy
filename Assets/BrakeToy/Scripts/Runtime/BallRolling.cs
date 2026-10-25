using UnityEngine;

namespace BrakeToy
{
    public static class BallRolling
    {
        // World geometry scrolls under the stationary camera. Remove that displacement
        // before computing the no-slip roll: angle = distance over the table / radius.
        public static Vector3 TableDisplacement(Vector3 worldDelta, float tableTravel)
            => worldDelta + Vector3.forward * tableTravel;

        public static void Apply(Transform visual, Vector3 displacement, float radius)
        {
            displacement.y = 0;
            if (displacement.sqrMagnitude < .00000001f || radius <= 0) return;
            visual.Rotate(Vector3.Cross(Vector3.up, displacement).normalized,
                displacement.magnitude / radius * Mathf.Rad2Deg, Space.World);
        }
    }
}
