using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BrakeToy.Tests
{
    public sealed class InputIntegrationTests
    {
        private InputDevice virtualDevice;

        [TearDown]
        public void Cleanup()
        {
            if (virtualDevice != null && virtualDevice.added) InputSystem.RemoveDevice(virtualDevice);
            virtualDevice = null;
        }

        [UnityTest]
        public IEnumerator MouseDragReleaseRetouchAndUIBlocking()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoom");
            yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            var mouse = InputSystem.AddDevice<Mouse>();
            virtualDevice = mouse;
            // Ready UI blocks the entire screen. Underlying steering must not receive this gesture.
            var start = new Vector2(Screen.width * .3f, Screen.height * .3f);
            MouseEvent(mouse, game, start, true);
            MouseEvent(mouse, game, start + Vector2.right * Screen.width * .2f, true);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(0));
            MouseEvent(mouse, game, start, false);
            game.UI.Overlay.SetActive(false);
            Canvas.ForceUpdateCanvases();
            MouseEvent(mouse, game, start, true);
            game.Player.Move(.1f);
            Assert.That(game.Player.X, Is.EqualTo(0));
            MouseEvent(mouse, game, start + Vector2.right * Screen.width * .1f, true);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
            MouseEvent(mouse, game, start, false);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
            MouseEvent(mouse, game, new Vector2(Screen.width * .8f, Screen.height * .3f), true);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
        }

        [UnityTest]
        public IEnumerator TouchDragIgnoresSecondFingerAndHoldsOnRelease()
        {
            yield return SceneManager.LoadSceneAsync("ToyRoom");
            yield return null;
            var game = Object.FindFirstObjectByType<GameManager>();
            game.UI.Overlay.SetActive(false);
            Canvas.ForceUpdateCanvases();
            var screen = InputSystem.AddDevice<Touchscreen>();
            virtualDevice = screen;
            Vector2 start = new Vector2(Screen.width * .3f, Screen.height * .3f);
            TouchEvent(screen, game, 81, UnityEngine.InputSystem.TouchPhase.Began, start);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(0));
            TouchEvent(screen, game, 81, UnityEngine.InputSystem.TouchPhase.Moved, start + Vector2.right * Screen.width * .1f);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
            TouchEvent(screen, game, 82, UnityEngine.InputSystem.TouchPhase.Began, start + Vector2.right * Screen.width * .4f);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
            TouchEvent(screen, game, 81, UnityEngine.InputSystem.TouchPhase.Ended, start + Vector2.right * Screen.width * .1f);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
            TouchEvent(screen, game, 82, UnityEngine.InputSystem.TouchPhase.Moved, start);
            game.Player.Move(1);
            Assert.That(game.Player.X, Is.EqualTo(.9f).Within(.002f));
        }

        private static void MouseEvent(Mouse mouse, GameManager game, Vector2 position, bool pressed)
        {
            InputSystem.QueueStateEvent(mouse, new MouseState { position = position, buttons = (ushort)(pressed ? 1 : 0) });
            InputSystem.Update();
            game.Input.Sample(game.Player);
        }
        private static void TouchEvent(Touchscreen screen, GameManager game, int id, UnityEngine.InputSystem.TouchPhase phase, Vector2 position)
        {
            InputSystem.QueueStateEvent(screen, new TouchState { touchId = id, phase = phase, position = position });
            InputSystem.Update();
            game.Input.Sample(game.Player);
        }
    }
}
