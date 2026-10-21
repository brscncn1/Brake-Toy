using UnityEngine;

namespace BrakeToy
{
    [RequireComponent(typeof(Camera))]
    public sealed class PortraitCamera : MonoBehaviour
    {
        public GameConfig Config;
        public Transform FollowTarget;
        [Range(0,.6f)] public float FollowAmount;
        private Camera view;
        private float lastAspect;
        private void OnEnable() { view = GetComponent<Camera>(); Reframe(); }
        private void LateUpdate()
        {
            if (!Mathf.Approximately(lastAspect, view.aspect)) Reframe();
            if(FollowTarget){var p=transform.position;p.x=Mathf.Lerp(p.x,FollowTarget.position.x*FollowAmount,1-Mathf.Exp(-8*Time.deltaTime));transform.position=p;}
        }

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
            float depth = (Config.TrackHalfWidth + Config.CameraSidePadding) / (lastAspect * tan)*Config.CameraDepthScale;
            float vertical = (2 * Config.PlayerViewportY - 1) * depth * tan;
            transform.SetPositionAndRotation(Vector3.up * .35f - rotation * Vector3.forward * depth
                - rotation * Vector3.up * vertical, rotation);
        }
    }
}
