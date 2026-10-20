using UnityEngine;

namespace BrakeToy
{
    [RequireComponent(typeof(Camera))]
    public sealed class PortraitCamera : MonoBehaviour
    {
        public GameConfig Config;
        private Camera view;
        private float lastAspect;
        private void OnEnable() { view = GetComponent<Camera>(); Reframe(); }
        private void LateUpdate() { if (!Mathf.Approximately(lastAspect, view.aspect)) Reframe(); }

        public void Reframe()
        {
            if (!Config) return;
            if (!view) view = GetComponent<Camera>();
            lastAspect = Mathf.Max(.1f, view.aspect);
            view.orthographic = false;
            view.fieldOfView = Config.VerticalFieldOfView;
            view.nearClipPlane = .1f;
            view.farClipPlane = 160;
            Quaternion rotation = Quaternion.Euler(Config.CameraPitch, 0, 0);
            float tan = Mathf.Tan(view.fieldOfView * .5f * Mathf.Deg2Rad);
            float depth = (Config.TrackHalfWidth + .8f) / (lastAspect * tan);
            float vertical = (2 * Config.PlayerViewportY - 1) * depth * tan;
            transform.SetPositionAndRotation(Vector3.up * .35f - rotation * Vector3.forward * depth
                - rotation * Vector3.up * vertical, rotation);
        }
    }
}
