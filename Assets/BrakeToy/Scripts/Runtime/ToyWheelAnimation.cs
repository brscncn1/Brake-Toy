using UnityEngine;

namespace BrakeToy
{
    public sealed class ToyWheelAnimation : MonoBehaviour
    {
        public GameManager Game;
        public Transform[] Wheels;
        public Vector3 LocalAxis = Vector3.up;
        private void Update()
        {
            if (!Game || Game.State != RunState.Running) return;
            float speed = Game.Arcade ? Game.Arcade.Speed : Game.Config.WorldSpeed;
            float angle = speed * Mathf.Min(Time.deltaTime, .1f) / .17f * Mathf.Rad2Deg;
            foreach (var wheel in Wheels) wheel.Rotate(LocalAxis, angle, Space.Self);
        }
    }
}
