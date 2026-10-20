using BrakeToy.Core;
using UnityEngine;

namespace BrakeToy
{
    /// <summary>Pooled, deterministic arcade encounter stream. Original prototype path remains available.</summary>
    public sealed class ArcadeDirector : MonoBehaviour
    {
        public ArcadeHazard[] Pool;
        public ArcadeHUD HUD;
        public ArcadeAudio Sound;
        public int Seed = 7331;
        public float Elapsed { get; private set; }
        public float Distance { get; private set; }
        public float Speed => ArcadeRules.Speed(Elapsed);
        public readonly StyleScore Style = new StyleScore();
        public int Gear => Mathf.Min(4, 1 + Mathf.FloorToInt((Speed - 10) / 2));
        public float BestDistance { get; private set; }
        public int BestStyle { get; private set; }
        public int SpawnedRows { get; private set; }
        public int PoolExhaustions { get; private set; }
        public bool PersistRecords { get; set; } = true;
        private GameManager game;
        private ArcadeSequence sequence;
        private ArcadeRow pending;
        private int lastGear;
        public void Initialize(GameManager manager)
        {
            game = manager;
            BestDistance = Mathf.Max(0, PlayerPrefs.GetFloat("BrakeToy.v03.distance", 0));
            BestStyle = Mathf.Max(0, PlayerPrefs.GetInt("BrakeToy.v03.style", 0));
            HUD.Bind(this, game); Sound.Initialize(game, this);
            ResetRun();
        }
        public void ResetRun()
        {
            foreach (var hazard in Pool) hazard.Release();
            Elapsed = Distance = 0; SpawnedRows = PoolExhaustions = 0; lastGear = 1;
            Style.Reset(); sequence = new ArcadeSequence(Seed); pending = sequence.Next();
            HUD.Clear(); FillAhead();
        }
        private void FillAhead()
        {
            while (pending.Distance - Distance < 95)
            {
                float z = pending.Distance - Distance;
                if (pending.Ball) Activate(3, pending.BallX(z), z, pending.BallCrossingX, pending.BallSlope);
                else for (int lane = 0; lane < 3; lane++) if (pending.IsBlocked(lane)) Activate(pending.Variant, ArcadeRules.LaneX(lane), z, 0, 0);
                SpawnedRows++; pending = sequence.Next();
            }
        }
        private void Activate(int variant, float x, float z, float crossing, float slope)
        {
            // Each prefab kind has six reserved instances; no allocations or Instantiate while driving.
            for (int i = variant * 6; i < variant * 6 + 6; i++)
                if (!Pool[i].InUse) { Pool[i].Place(x, z, crossing, slope); return; }
            PoolExhaustions++; Debug.LogError("Arcade pool exhausted; seed=" + Seed + " row=" + SpawnedRows);
        }
        public void Step(float dt, float oldX, float newX)
        {
            if (dt <= 0 || game.State != RunState.Running) return;
            // Substeps bound acceleration error; swept relative collision handles both axes and hitches.
            int steps = Mathf.Max(1, Mathf.CeilToInt(dt * 60));
            float sub = dt / steps;
            for (int step = 0; step < steps; step++)
            {
                float fromX = Mathf.Lerp(oldX, newX, step / (float)steps);
                float toX = Mathf.Lerp(oldX, newX, (step + 1f) / steps);
                float d = ArcadeRules.Distance(Elapsed + sub) - ArcadeRules.Distance(Elapsed);
                bool hit = false; float fraction = 1;
                foreach (var hazard in Pool)
                {
                    if (!hazard.InUse) continue;
                    var a = hazard.transform.position; var b = hazard.PositionAfter(d);
                    float t;
                    bool contact = hazard.RollingBall
                        ? SweptCircleBox.TryHit(fromX-a.x,-a.z,toX-b.x,-b.z,game.Config.PlayerHalfWidth,game.Config.PlayerHalfLength,hazard.HalfWidth,out t)
                        : SweptCollision.TryHit(fromX-a.x,-a.z,toX-b.x,-b.z,game.Config.PlayerHalfWidth+hazard.HalfWidth,game.Config.PlayerHalfLength+hazard.HalfLength,out t);
                    if (contact && t <= fraction)
                    { fraction = t; hit = true; }
                }
                float moved = d * fraction;
                foreach (var hazard in Pool) if (hazard.InUse)
                {
                    var a = hazard.transform.position; var b = hazard.PositionAfter(moved);
                    float halfZ = game.Config.PlayerHalfLength + hazard.HalfLength;
                    // Closest lateral separation only while longitudinal hit volumes overlap.
                    if (a.z >= -halfZ && b.z <= halfZ)
                    {
                        float enter = Mathf.Clamp01((a.z - halfZ) / Mathf.Max(.000001f, moved));
                        float exit = Mathf.Clamp01((a.z + halfZ) / Mathf.Max(.000001f, moved));
                        float r0 = Mathf.Lerp(fromX, toX, fraction * enter) - Mathf.Lerp(a.x, b.x, enter);
                        float r1 = Mathf.Lerp(fromX, toX, fraction * exit) - Mathf.Lerp(a.x, b.x, exit);
                        float separation = r0 * r1 <= 0 ? 0 : Mathf.Min(Mathf.Abs(r0), Mathf.Abs(r1));
                        hazard.BestGap = Mathf.Min(hazard.BestGap, separation - game.Config.PlayerHalfWidth - hazard.HalfWidth);
                    }
                    hazard.Advance(moved);
                    if (!hit && !hazard.Scored && b.z < -halfZ)
                    {
                        hazard.Scored = true;
                        if (hazard.BestGap > .005f && hazard.BestGap <= .48f)
                        { int award = Style.Award(); HUD.Notify("YAKIN GEÇİŞ  +" + award, new Color(.2f, .95f, .85f)); Sound.NearMiss(Style.Multiplier); }
                    }
                    if (b.z < -9) hazard.Release();
                }
                game.World.Advance(moved); Distance += moved; Elapsed += sub * fraction;
                Style.Advance(sub * fraction);
                if (hit)
                {
                    game.Player.StopAt(Mathf.Lerp(fromX, toX, fraction)); Style.Crash();
                    BestDistance = Mathf.Max(BestDistance, Distance); BestStyle = Mathf.Max(BestStyle, Style.Total);
                    if(PersistRecords) { PlayerPrefs.SetFloat("BrakeToy.v03.distance", BestDistance); PlayerPrefs.SetInt("BrakeToy.v03.style", BestStyle); PlayerPrefs.Save(); }
                    Sound.Crash(); HUD.Impact(); game.Crash(); return;
                }
                FillAhead();
            }
            if (Gear != lastGear) { lastGear = Gear; HUD.Notify("VİTES " + Gear + "  •  HIZLANIYORUZ", new Color(1, .8f, .35f)); Sound.Shift(); }
        }
        public bool BallWarning(out float x)
        {
            foreach (var hazard in Pool)
                if (hazard.InUse && hazard.RollingBall && hazard.transform.position.z > 0 && hazard.transform.position.z < Speed * 2.1f)
                { x = hazard.transform.position.x; return true; }
            x = 0; return false;
        }
    }
}
