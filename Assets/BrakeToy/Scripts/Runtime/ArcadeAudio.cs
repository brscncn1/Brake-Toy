using UnityEngine;

namespace BrakeToy
{
    /// <summary>Original synthesized toy motor and short cues. No external audio licence dependency.</summary>
    public sealed class ArcadeAudio : MonoBehaviour
    {
        public bool Muted;
        private AudioSource engine, effects;
        private AudioClip near, shift, crash;
        private GameManager game;
        private ArcadeDirector arcade;
        public void Initialize(GameManager manager, ArcadeDirector director)
        {
            game = manager; arcade = director;
            engine = gameObject.AddComponent<AudioSource>(); effects = gameObject.AddComponent<AudioSource>();
            engine.playOnAwake = effects.playOnAwake = false;
            engine.loop = true; engine.volume = .045f; effects.volume = .18f;
            engine.clip = Tone("Toy motor", .5f, 96, 96, false); engine.Play();
            near = Tone("Near miss chime", .18f, 880, 1320, false);
            shift = Tone("Gear click", .12f, 220, 420, false);
            crash = Tone("Toy clatter", .24f, 140, 55, true);
        }
        private static AudioClip Tone(string name, float duration, float from, float to, bool noise)
        {
            const int rate = 22050;
            var samples = new float[Mathf.RoundToInt(rate * duration)];
            var random = new System.Random(81); float phase = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                float t = i / (float)samples.Length;
                phase += Mathf.Lerp(from, to, t) / rate * Mathf.PI * 2;
                float wave = Mathf.Sin(phase) * .65f + Mathf.Sin(phase * 2) * .2f;
                if (noise) wave = wave * .4f + ((float)random.NextDouble() * 2 - 1) * .5f;
                float envelope = name == "Toy motor" ? 1 : Mathf.Sin(Mathf.PI * t) * (1 - t);
                samples[i] = wave * envelope;
            }
            var clip = AudioClip.Create(name, samples.Length, 1, rate, false); clip.SetData(samples, 0); return clip;
        }
        private void Update()
        {
            if (!engine) return;
            engine.mute = Muted || game.State != RunState.Running; effects.mute = Muted;
            engine.pitch = .9f + (arcade.Speed - 10) * .09f;
        }
        public void NearMiss(int multiplier) { if (effects) { effects.pitch = 1 + multiplier * .07f; effects.PlayOneShot(near); } }
        public void Shift() { if (effects) { effects.pitch = 1; effects.PlayOneShot(shift); } }
        public void Crash() { if (effects) { effects.pitch = 1; effects.PlayOneShot(crash); } }
        private void OnDestroy()
        {
            if (engine && engine.clip) Destroy(engine.clip);
            if (near) Destroy(near); if (shift) Destroy(shift); if (crash) Destroy(crash);
        }
    }
}
