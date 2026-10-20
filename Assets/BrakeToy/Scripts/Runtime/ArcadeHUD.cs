using UnityEngine;
using UnityEngine.UI;

namespace BrakeToy
{
    public sealed class ArcadeHUD : MonoBehaviour
    {
        public GameObject Driving;
        public Text DistanceLabel, StyleLabel, SpeedLabel, ComboLabel, NoticeLabel, WarningLabel;
        public Image ComboFill, ImpactFlash;
        public Button SoundButton;
        public Text SoundLabel;
        private ArcadeDirector arcade;
        private GameManager game;
        private float noticeTime, impact;
        private bool muted;
        public void Bind(ArcadeDirector director, GameManager manager)
        {
            arcade = director; game = manager;
            manager.gameObject.AddComponent<ArcadeFeedback>().Initialize(manager,director);
            muted = PlayerPrefs.GetInt("BrakeToy.v03.muted", 0) == 1;
            SoundButton.onClick.AddListener(() => { muted = !muted; PlayerPrefs.SetInt("BrakeToy.v03.muted", muted ? 1 : 0); PlayerPrefs.Save(); });
        }
        public void Clear() { noticeTime = impact = 0; if (NoticeLabel) NoticeLabel.text = ""; }
        public void Notify(string text, Color color) { NoticeLabel.text = text; NoticeLabel.color = color; noticeTime = 1.45f; }
        public void Impact() { impact = .28f; }
        private void LateUpdate()
        {
            if (!arcade || !game) return;
            bool running = game.State == RunState.Running;
            Driving.SetActive(running);
            SoundLabel.text = muted ? "SES KAPALI" : "SES AÇIK";
            arcade.Sound.Muted = muted;
            DistanceLabel.text = Mathf.FloorToInt(arcade.Distance) + " <size=25>m</size>";
            StyleLabel.text = "STİL  " + arcade.Style.Total.ToString("N0");
            SpeedLabel.text = "VİTES " + arcade.Gear + "   /   " + (arcade.Speed / 10).ToString("0.00") + "×";
            ComboLabel.text = arcade.Style.Fever ? "COMBO FEVER   ×5" : arcade.Style.Chain > 0 ? "KOMBO " + arcade.Style.Chain + "   ×" + arcade.Style.Multiplier : "YAKINDAN GEÇ • KOMBO YAP";
            ComboLabel.color = arcade.Style.Fever ? new Color(1, .70f, .2f) : new Color(.65f, .95f, .91f);
            ComboFill.fillAmount = arcade.Style.Remaining / 6;
            ComboFill.rectTransform.anchorMax = new Vector2(arcade.Style.Remaining / 6, 1);
            // Timers use gameplay time; pause cannot consume a combo or notice.
            if (running) noticeTime = Mathf.Max(0, noticeTime - Time.deltaTime);
            NoticeLabel.enabled = noticeTime > 0;
            float ballX = 0;
            bool warning = running && arcade.BallWarning(out ballX);
            WarningLabel.text = warning ? (ballX < 0 ? "◀  SOLDAN TOP GELİYOR" : "SAĞDAN TOP GELİYOR  ▶") : "";
            WarningLabel.color = new Color(1, .75f, .22f, .75f + .25f * Mathf.Sin(Time.unscaledTime * 10));
            if (impact > 0) impact = Mathf.Max(0, impact - Time.unscaledDeltaTime);
            ImpactFlash.color = new Color(1, .32f, .15f, impact * .7f);
            if (game.State == RunState.Ready)
            {
                game.UI.Heading.text = "BRAKE TOY";
                game.UI.Description.text = "KÜÇÜK ARABA. BÜYÜK KARGAŞA.\n\nSürükle, aradan sıyrıl.\nYakın geçişlerle komboyu büyüt.\nYuvarlanan toplara dikkat!";
            }
            else if (game.State == RunState.Crashed)
            {
                game.UI.Heading.text = "BİR TUR DAHA?";
                game.UI.Description.text = Mathf.FloorToInt(arcade.Distance) + " m  •  " + arcade.Style.Total + " STİL\n" + arcade.Style.NearMisses + " yakın geçiş\n\nREKOR  " + Mathf.FloorToInt(arcade.BestDistance) + " m\nEN İYİ STİL  " + arcade.BestStyle;
            }
        }
    }
}
