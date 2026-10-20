using BrakeToy.Core;
using UnityEngine;

namespace BrakeToy
{
    public sealed class PlayerController : MonoBehaviour
    {
        public Transform Visual;
        private SteeringModel steering;
        public float X => steering.Position;
        public void Initialize(GameConfig config) => steering = new SteeringModel(config.PlayerLimit, config.DragSensitivity, config.MaxLateralSpeed);
        public void BeginDrag() => steering.BeginDrag();
        public void Drag(float delta, float width) => steering.Drag(delta, width);
        public void EndDrag() => steering.EndDrag();
        public void ResetRun() { steering.Reset(); transform.position = Vector3.zero; if (Visual) Visual.localRotation = Quaternion.identity; }
        public void Move(float dt)
        {
            float old = X;
            transform.position = new Vector3(steering.Step(dt), 0, 0);
            if (Visual && dt > 0)
            {
                float lean = -(X - old) / dt * 1.4f;
                Visual.localRotation = Quaternion.Lerp(Visual.localRotation, Quaternion.Euler(0, -lean * .7f, lean), 1 - Mathf.Exp(-20 * dt));
            }
        }
        public void StopAt(float x) { steering.StopAt(x); transform.position = new Vector3(x, 0, 0); }
    }
}
