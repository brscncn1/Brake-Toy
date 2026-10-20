using UnityEngine;

namespace BrakeToy
{
    public sealed class ToyWheelAnimation : MonoBehaviour
    {
        public GameManager Game;
        public Transform[] Wheels;
        private void Update()
        {
            if (!Game || Game.State != RunState.Running) return;
            float angle = Game.Config.WorldSpeed * Mathf.Min(Time.deltaTime, .1f) / .17f * Mathf.Rad2Deg;
            foreach (var wheel in Wheels) wheel.Rotate(Vector3.up, angle, Space.Self);
        }
    }
}
