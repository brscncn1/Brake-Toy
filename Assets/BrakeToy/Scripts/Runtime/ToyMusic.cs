using UnityEngine;
namespace BrakeToy
{
    // Real recorded/composed tracks. Two sources crossfade between menu and driving;
    // author-provided sample-accurate loop is decoded/preloaded before playback.
    public sealed class ToyMusic:MonoBehaviour
    {
        public AudioClip MenuTrack,RaceTrack;[Range(0,1)]public float Level=.65f;
        public AudioSource Menu{get;private set;}public AudioSource Race{get;private set;}
        private GameManager game;private float blend,gain,duck;
        public void DuckForImpact(){duck=.45f;}
        private void Start()
        {game=GetComponent<GameManager>();Menu=Make(MenuTrack);Race=Make(RaceTrack);double start=AudioSettings.dspTime+.15;Menu.PlayScheduled(start);Race.PlayScheduled(start);}
        private AudioSource Make(AudioClip clip){var source=gameObject.AddComponent<AudioSource>();source.clip=clip;source.loop=true;source.playOnAwake=false;source.volume=0;source.spatialBlend=0;source.priority=80;return source;}
        private void Update()
        {
            if(!game||!Menu||!Race)return;bool driving=game.State==RunState.Running||game.State==RunState.Paused;
            gain=Mathf.MoveTowards(gain,1,Time.unscaledDeltaTime/1.2f);duck=Mathf.Max(0,duck-Time.unscaledDeltaTime);
            blend=Mathf.MoveTowards(blend,driving?1:0,Time.unscaledDeltaTime/.65f);float volume=game.Arcade.Sound.Muted?0:Level*.42f*gain*(duck>0?.35f:1);
            Menu.volume=Mathf.Cos(blend*Mathf.PI*.5f)*volume;Race.volume=Mathf.Sin(blend*Mathf.PI*.5f)*volume*(game.State==RunState.Paused?.40f:1);
        }
    }
}
