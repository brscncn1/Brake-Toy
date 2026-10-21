using UnityEngine;
using UnityEngine.UI;
namespace BrakeToy
{
    public sealed class ToyAudioSettings:MonoBehaviour
    {
        public GameObject Panel;
        public Slider Engine,Effects;
        public bool PersistPreferences=true;
        private GameManager game;
        private void Start()
        {
            game=GetComponent<GameManager>();Engine.SetValueWithoutNotify(PlayerPrefs.GetFloat("BrakeToy.audio.engine.v1",.65f));Effects.SetValueWithoutNotify(PlayerPrefs.GetFloat("BrakeToy.audio.effects.v1",.75f));
            Apply();Engine.onValueChanged.AddListener(_=>{Apply();Save();});Effects.onValueChanged.AddListener(_=>{Apply();Save();});
        }
        private void Apply(){game.Arcade.Sound.EngineLevel=Engine.value;game.Arcade.Sound.EffectsLevel=Effects.value;}
        private void Save(){if(!PersistPreferences)return;PlayerPrefs.SetFloat("BrakeToy.audio.engine.v1",Engine.value);PlayerPrefs.SetFloat("BrakeToy.audio.effects.v1",Effects.value);PlayerPrefs.Save();}
        private void LateUpdate(){if(game)Panel.SetActive(game.State!=RunState.Running);}
    }
}
