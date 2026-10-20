using UnityEngine;
using UnityEngine.UI;

namespace BrakeToy
{
    public sealed class PrototypeUI : MonoBehaviour
    {
        public RectTransform SafeArea;
        public GameObject Overlay;
        public Text Heading;
        public Text Description;
        public Text ActionText;
        public Button ActionButton;
        public Button PauseButton;
        private GameManager game;
        private Rect previousSafeArea;
        private Vector2Int previousSize;

        public void Bind(GameManager manager)
        {
            game = manager;
            ActionButton.onClick.AddListener(Act);
            PauseButton.onClick.AddListener(game.Pause);
            ApplySafeArea();
        }
        public void Show(RunState state)
        {
            Overlay.SetActive(state != RunState.Running);
            PauseButton.gameObject.SetActive(state == RunState.Running);
            Heading.text = state == RunState.Ready ? "BRAKE TOY" : state == RunState.Crashed ? "HOP! ÇARPTIK." : "KISA BİR MOLA";
            Description.text = state == RunState.Ready ? "NO BRAKES. BIG TROUBLE.\n\nParmağını sağa sola sürükle.\nOyuncakların arasından kaç!" :
                state == RunState.Crashed ? "Sparky yeniden hazır.\nBir tur daha?" : "Hazır olduğunda yola devam et.";
            ActionText.text = state == RunState.Ready ? "OYNA" : state == RunState.Crashed ? "TEKRAR OYNA" : "DEVAM ET";
        }
        private void Act() { if (game.State == RunState.Paused) game.Resume(); else game.StartRun(); }
        private void LateUpdate()
        {
            if (previousSafeArea != Screen.safeArea || previousSize != new Vector2Int(Screen.width, Screen.height)) ApplySafeArea();
        }
        private void ApplySafeArea()
        {
            if (Screen.width == 0 || Screen.height == 0) return;
            previousSafeArea = Screen.safeArea;
            previousSize = new Vector2Int(Screen.width, Screen.height);
            SafeArea.anchorMin = new Vector2(previousSafeArea.xMin / Screen.width, previousSafeArea.yMin / Screen.height);
            SafeArea.anchorMax = new Vector2(previousSafeArea.xMax / Screen.width, previousSafeArea.yMax / Screen.height);
            SafeArea.offsetMin = SafeArea.offsetMax = Vector2.zero;
        }
    }
}
