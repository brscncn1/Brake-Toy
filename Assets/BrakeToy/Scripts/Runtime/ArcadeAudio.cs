using UnityEngine;

namespace BrakeToy
{
    /// <summary>Original synthesized toy motor and short cues. No external audio licence dependency.</summary>
    public sealed class ArcadeAudio : MonoBehaviour
    {
        public bool Muted;
        public bool EnhancedLayers;
        private AudioSource rolling;
        private float previousX, lastShift;
        private AudioSource engine, effects;
        private AudioClip near, shift, crash, softImpact;
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
            if (EnhancedLayers)
            {
                Destroy(engine.clip); engine.clip = LayeredClip("Die-cast flywheel", 1, 0); engine.volume = .032f; engine.Play();
                Destroy(near); Destroy(shift); Destroy(crash);
                near = LayeredClip("Clean pass shimmer", .24f, 2);
                shift = LayeredClip("Spring ratchet", .12f, 3);
                crash = LayeredClip("Wood and metal impact", .42f, 4);
                softImpact = LayeredClip("Rubber toy impact", .32f, 5);
                rolling = gameObject.AddComponent<AudioSource>(); rolling.playOnAwake = false; rolling.loop = true;
                rolling.clip = LayeredClip("Rubber on desk", 1, 1); rolling.volume = .016f; rolling.Play();
            }
        }
        // Deterministic modal synthesis: filtered friction, flywheel harmonics, and a dry wooden attack.
        public static AudioClip LayeredClip(string name, float duration, int kind)
        {
            const int rate = 22050; var data = new float[Mathf.RoundToInt(duration * rate)];
            var random = new System.Random(456 + kind); float filtered = 0;
            for (int i = 0; i < data.Length; i++)
            {
                float t = i / (float)rate, n = (float)random.NextDouble() * 2 - 1;
                filtered += .11f * (n - filtered); float sample;
                if (kind == 0) sample = .44f * Mathf.Sin(2 * Mathf.PI * 92 * t) + .12f * Mathf.Sin(2 * Mathf.PI * 184 * t) + .06f * filtered;
                else if (kind == 1) sample = filtered * .38f * (.75f + .25f * Mathf.Sin(2 * Mathf.PI * 17 * t));
                else if (kind == 2) sample = (Mathf.Sin(2 * Mathf.PI * 880 * t) + .45f * Mathf.Sin(2 * Mathf.PI * 1320 * t)) * .3f * Mathf.Exp(-t * 17);
                else if (kind == 3) sample = (.5f * n + .4f * Mathf.Sin(2 * Mathf.PI * 290 * t)) * Mathf.Exp(-t * 52);
                else if (kind == 5) sample = (.48f * Mathf.Sin(2 * Mathf.PI * (95 * t - 60 * t * t)) + .22f * filtered) * Mathf.Exp(-t * 24);
                else sample = .38f * filtered * Mathf.Exp(-t * 38) + .34f * Mathf.Sin(2 * Mathf.PI * 177 * t) * Mathf.Exp(-t * 23)
                    + .14f * Mathf.Sin(2 * Mathf.PI * 593 * t) * Mathf.Exp(-t * 16)
                    + (t > .065f ? .12f * Mathf.Sin(2 * Mathf.PI * 1040 * (t - .065f)) * Mathf.Exp(-(t - .065f) * 29) : 0);
                float edge = Mathf.Min(1, Mathf.Min(i, data.Length - 1 - i) / 110f);
                // Loop layers have a periodic sine bed; only the quiet noise seam is cross-faded.
                data[i] = Mathf.Clamp(sample * edge, -.9f, .9f);
            }
            var clip = AudioClip.Create(name, data.Length, 1, rate, false); clip.SetData(data, 0); return clip;
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
            if (EnhancedLayers)
            {
                float shiftDip = Mathf.Clamp01((Time.unscaledTime - lastShift) / .12f);
                engine.pitch = (.92f + (arcade.Speed - 10) * .065f) * Mathf.Lerp(.84f, 1, shiftDip);
                float lateral = Mathf.Abs(game.Player.X - previousX) / Mathf.Max(.001f, Time.deltaTime);
                rolling.mute = Muted || game.State != RunState.Running;
                rolling.volume = .012f + Mathf.Min(.018f, lateral * .002f); rolling.pitch = .85f + arcade.Speed * .025f;
                previousX = game.Player.X;
            }
        }
        public void NearMiss(int multiplier) { if (effects) { effects.pitch = 1 + multiplier * .07f; effects.PlayOneShot(near); } }
        public void Shift() { lastShift = Time.unscaledTime; if (effects) { effects.pitch = 1; effects.PlayOneShot(shift); } }
        public void Crash(bool soft = false) { if (effects) { effects.pitch = 1; effects.PlayOneShot(EnhancedLayers && soft ? softImpact : crash); } }
        private void OnDestroy()
        {
            if (engine && engine.clip) Destroy(engine.clip);
            if (rolling && rolling.clip) Destroy(rolling.clip);
            if (near) Destroy(near); if (shift) Destroy(shift); if (crash) Destroy(crash);
            if (softImpact) Destroy(softImpact);
        }
    }
}
