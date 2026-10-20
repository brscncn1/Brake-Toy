using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace BrakeToy
{
    /// <summary>Owns a runtime pipeline copy; never edits shared project assets or earlier scenes.</summary>
    public sealed class GraphicsProfileController : MonoBehaviour
    {
        public UniversalRenderPipelineAsset Source;
        public Button ToggleButton;
        public Text Label;
        public bool PersistPreferences = true;
        public bool Low { get; private set; }
        public UniversalRenderPipelineAsset ActivePipeline { get; private set; }
        private RenderPipelineAsset previousDefault, previousQuality;
        private int previousFrameRate;
        private GameManager game;
        private bool initialized;
        private void Start()
        {
            game = GetComponent<GameManager>(); previousDefault = GraphicsSettings.defaultRenderPipeline;
            previousQuality = QualitySettings.renderPipeline; previousFrameRate = Application.targetFrameRate;
            ActivePipeline = Instantiate(Source); ActivePipeline.name = "Refined Room runtime quality";
            initialized = true;
            ToggleButton.onClick.AddListener(() => SetLow(!Low, true));
            SetLow(PlayerPrefs.GetInt("BrakeToy.quality.v1", 0) == 1, false);
        }
        public void SetLow(bool low, bool save = false)
        {
            if (!initialized) return;
            Low = low; ActivePipeline.renderScale = low ? .8f : 1;
            ActivePipeline.msaaSampleCount = low ? 2 : 4;
            ActivePipeline.shadowDistance = low ? 35 : 75;
            ActivePipeline.shadowCascadeCount = low ? 1 : 2;
            ActivePipeline.mainLightShadowmapResolution = low ? 1024 : 2048;
            GraphicsSettings.defaultRenderPipeline = ActivePipeline; QualitySettings.renderPipeline = ActivePipeline;
            Camera.main.GetUniversalAdditionalCameraData().SetRenderer(low ? 1 : 0);
            Application.targetFrameRate = low ? 30 : 60;
            if (save && PersistPreferences) { PlayerPrefs.SetInt("BrakeToy.quality.v1", low ? 1 : 0); PlayerPrefs.Save(); }
            if (Label) Label.text = low ? "GRAFİK: EKONOMİK" : "GRAFİK: YÜKSEK";
        }
        private void LateUpdate() { if (game && ToggleButton) ToggleButton.gameObject.SetActive(game.State != RunState.Running); }
        private void OnDestroy()
        {
            if (!initialized) return;
            // Another scene may have replaced the pipeline: restore only the settings we still own.
            if (GraphicsSettings.defaultRenderPipeline == ActivePipeline) GraphicsSettings.defaultRenderPipeline = previousDefault;
            if (QualitySettings.renderPipeline == ActivePipeline) QualitySettings.renderPipeline = previousQuality;
            Application.targetFrameRate = previousFrameRate;
            if (ActivePipeline) Destroy(ActivePipeline);
        }
    }
}
