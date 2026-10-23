using UnityEngine;
using UnityEngine.UI;
namespace BrakeToy
{
    public sealed class ToyAudioSettings:MonoBehaviour
    {
        public GameObject Panel;
        public Slider Engine,Effects,Music;
        public bool PersistPreferences=true;
        private GameManager game;
        private void Start()
        {
            game=GetComponent<GameManager>();Engine.SetValueWithoutNotify(PlayerPrefs.GetFloat("BrakeToy.audio.engine.v1",.65f));Effects.SetValueWithoutNotify(PlayerPrefs.GetFloat("BrakeToy.audio.effects.v1",.75f));
            Apply();Engine.onValueChanged.AddListener(_=>{Apply();Save();});Effects.onValueChanged.AddListener(_=>{Apply();Save();});
            if(Music){Music.SetValueWithoutNotify(PlayerPrefs.GetFloat("BrakeToy.audio.music.v1",.65f));Music.onValueChanged.AddListener(_=>{Apply();Save();});Apply();}
        }
        private void Apply(){game.Arcade.Sound.EngineLevel=Engine.value;game.Arcade.Sound.EffectsLevel=Effects.value;if(Music&&GetComponent<ToyMusic>())GetComponent<ToyMusic>().Level=Music.value;}
        private void Save(){if(!PersistPreferences)return;PlayerPrefs.SetFloat("BrakeToy.audio.engine.v1",Engine.value);PlayerPrefs.SetFloat("BrakeToy.audio.effects.v1",Effects.value);if(Music)PlayerPrefs.SetFloat("BrakeToy.audio.music.v1",Music.value);PlayerPrefs.Save();}
        private void LateUpdate(){if(game)Panel.SetActive(game.State!=RunState.Running);}
    }
}
