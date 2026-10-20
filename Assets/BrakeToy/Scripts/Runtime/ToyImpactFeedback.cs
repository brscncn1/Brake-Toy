using UnityEngine;

namespace BrakeToy
{
    /// <summary>Short grounded die-cast wobble and damped camera impulse. Collision roots never move.</summary>
    public sealed class ToyImpactFeedback : MonoBehaviour
    {
        public bool IsPlaying => remaining > 0;
        public float Duration = .42f;
        private float remaining;
        private GameManager game;
        private Camera view;
        private Vector3 cameraPosition;
        private Quaternion cameraRotation, visualRotation;
        private Vector3 visualPosition;
        private void Awake() { game = GetComponent<GameManager>(); }
        public void Play()
        {
            Restore(); view = Camera.main;
            cameraPosition = view.transform.position; cameraRotation = view.transform.rotation;
            visualPosition = game.Player.Visual.localPosition; visualRotation = game.Player.Visual.localRotation;
            remaining = Duration;
        }
        private void LateUpdate()
        {
            if (remaining <= 0) return;
            if (game.State != RunState.Crashed) { Restore(); return; }
            remaining = Mathf.Max(0, remaining - Time.unscaledDeltaTime);
            float t = Duration - remaining, fade = remaining / Duration;
            float shake = Mathf.Sin(t * 62) * fade * fade;
            view.transform.SetPositionAndRotation(cameraPosition + view.transform.right * shake * .045f, cameraRotation * Quaternion.Euler(shake * .22f, 0, 0));
            // Lift is strictly positive: the car does not sink through the tabletop.
            game.Player.Visual.localPosition = visualPosition + Vector3.up * Mathf.Abs(Mathf.Sin(t * 24)) * fade * .022f;
            game.Player.Visual.localRotation = visualRotation * Quaternion.Euler(0, Mathf.Sin(t * 22) * fade * 5, 0);
            if (remaining <= 0) Restore();
        }
        private void Restore()
        {
            if (view)
            {
                view.transform.SetPositionAndRotation(cameraPosition, cameraRotation);
                if (game && game.Player.Visual)
                { game.Player.Visual.localPosition = visualPosition; if (game.State == RunState.Crashed) game.Player.Visual.localRotation = visualRotation; }
            }
            remaining = 0; view = null;
        }
        private void OnDisable() { Restore(); }
    }
}
