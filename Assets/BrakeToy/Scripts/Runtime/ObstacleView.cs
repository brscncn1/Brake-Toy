using UnityEngine;

namespace BrakeToy
{
    public sealed class ObstacleView : MonoBehaviour
    {
        public BoxCollider Hitbox;
        public float HalfWidth => Hitbox.size.x * .5f;
        public float HalfLength => Hitbox.size.z * .5f;
        public float X => transform.position.x + Hitbox.center.x;
        public float Z => transform.position.z + Hitbox.center.z;
    }
}
