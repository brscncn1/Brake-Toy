using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace BrakeToy
{
    public sealed class InputController : MonoBehaviour
    {
        private readonly List<RaycastResult> uiHits = new List<RaycastResult>(12);
        private PointerEventData pointer;
        private EventSystem pointerEvents;
        private int touchId = -1;
        private bool mouseHeld;
        private bool mouseDown;
        private Vector2 previous;

        private void OnEnable() => EnhancedTouchSupport.Enable();
        private void OnDisable() { EnhancedTouchSupport.Disable(); ResetCapture(); }
        public void ResetCapture()
        {
            touchId = -1;
            mouseHeld = false;
            mouseDown = Mouse.current != null && Mouse.current.leftButton.isPressed;
        }

        public void Sample(PlayerController player)
        {
            var touches = Touch.activeTouches;
            if (touches.Count > 0 || touchId >= 0)
            {
                if (mouseHeld) { player.EndDrag(); mouseHeld = false; }
                if (touchId < 0)
                {
                    for (int i = 0; i < touches.Count; i++)
                    {
                        var touch = touches[i];
                        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began || IsUI(touch.screenPosition)) continue;
                        touchId = touch.touchId;
                        previous = touch.screenPosition;
                        player.BeginDrag();
                        break;
                    }
                    return;
                }
                for (int i = 0; i < touches.Count; i++)
                {
                    var touch = touches[i];
                    if (touch.touchId != touchId) continue;
                    if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended ||
                        touch.phase == UnityEngine.InputSystem.TouchPhase.Canceled)
                    { player.EndDrag(); touchId = -1; return; }
                    player.Drag(touch.screenPosition.x - previous.x, Screen.width);
                    previous = touch.screenPosition;
                    return;
                }
                player.EndDrag(); touchId = -1;
                return;
            }

            var mouse = Mouse.current;
            if (mouse == null) return;
            Vector2 position = mouse.position.ReadValue();
            bool pressed = mouse.leftButton.isPressed;
            // Track the edge ourselves: this also works with several input updates in one frame.
            if (pressed && !mouseDown && !IsUI(position))
            { mouseHeld = true; previous = position; player.BeginDrag(); }
            mouseDown = pressed;
            if (!mouseHeld) return;
            if (!pressed)
            { mouseHeld = false; player.EndDrag(); return; }
            player.Drag(position.x - previous.x, Screen.width);
            previous = position;
        }

        private bool IsUI(Vector2 position)
        {
            var events = EventSystem.current;
            if (events == null) return false;
            if (pointer == null || pointerEvents != events)
            { pointerEvents = events; pointer = new PointerEventData(events); }
            pointer.position = position;
            uiHits.Clear();
            events.RaycastAll(pointer, uiHits);
            return uiHits.Count > 0;
        }
    }
}
