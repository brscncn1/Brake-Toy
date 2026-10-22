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
        public bool UseVariedPatterns, VaryRuns, BuildCatalogPool;
        public bool Choreographed;
        public RunChallenges Challenges;
        public string StyleRecordKey = "BrakeToy.v03.style";
        public ObstacleDefinition[] Definitions;
        public int CurrentSeed { get; private set; }
        private int runNumber;
        private int[] poolOffsets;
        public float Elapsed { get; private set; }
        public float Distance { get; private set; }
        public float Speed => game && game.Review ? CareerRules.Speed(Elapsed,game.Review.Mode) : ArcadeRules.Speed(Elapsed);
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
            CurrentSeed = Seed;
            if (BuildCatalogPool)
            {
                if (Definitions == null || Definitions.Length != 4) throw new System.InvalidOperationException("Missing encounter catalog.");
                Transform parent = Pool.Length > 0 ? Pool[0].transform.parent : transform;
                foreach (var hazard in Pool) { hazard.Release(); Destroy(hazard.gameObject); }
                var instances = new System.Collections.Generic.List<ArcadeHazard>();
                foreach (var definition in Definitions)
                {
                    if (!definition || !definition.Prefab || definition.PoolSize < 6) throw new System.InvalidOperationException("Invalid catalog prefab.");
                    for (int i = 0; i < definition.PoolSize; i++)
                    { var hazard = Instantiate(definition.Prefab, parent); hazard.Release(); instances.Add(hazard); }
                }
                Pool = instances.ToArray();
            }
            if (Definitions != null && Definitions.Length > 0)
            {
                if (Definitions.Length != 4) throw new System.InvalidOperationException("Four encounter kinds required.");
                poolOffsets = new int[4]; int count = 0;
                for (int kind = 0; kind < 4; kind++)
                {
                    var definition = Definitions[kind];
                    if (!definition || definition.Kind != kind || definition.PoolSize < 6 || !definition.Prefab)
                        throw new System.InvalidOperationException("Invalid encounter definition " + kind);
                    poolOffsets[kind] = count;
                    for (int i = 0; i < definition.PoolSize; i++)
                    {
                        if (count >= Pool.Length) throw new System.InvalidOperationException("Encounter pool is too small.");
                        var h = Pool[count++]; h.HalfWidth = definition.HalfWidth; h.HalfLength = definition.HalfLength;
                        h.RollingBall = kind == 3;
                    }
                }
                if (count != Pool.Length) throw new System.InvalidOperationException("Unexpected encounter pool instances.");
            }
            BestDistance = Mathf.Max(0, PlayerPrefs.GetFloat("BrakeToy.v03.distance", 0));
            BestStyle = Mathf.Max(0, PlayerPrefs.GetInt(StyleRecordKey, 0));
            HUD.Bind(this, game); Sound.Initialize(game, this);
            if(Challenges)Challenges.Initialize(game,this);
            ResetRun();
        }
        public void ResetRun()
        {
            foreach (var hazard in Pool) hazard.Release();
            Elapsed = Distance = 0; SpawnedRows = PoolExhaustions = 0; lastGear = 1;
            Style.Reset(); sequence = new ArcadeSequence(CurrentSeed, UseVariedPatterns, Choreographed,game&&game.Review&&game.Review.Mode==PlayMode.Relax); pending = sequence.Next();
            if(Challenges)Challenges.ResetRun();
            HUD.Clear(); FillAhead();
        }
        public void BeginRun(bool sameRoute)
        {
            if (!sameRoute || runNumber == 0)
            {
                runNumber++;
                CurrentSeed = VaryRuns ? unchecked(Seed + runNumber * 104729 + System.Environment.TickCount) : Seed;
            }
            ResetRun();
        }
        private void FillAhead()
        {
            while (pending.Distance - Distance < 95)
            {
                float z = pending.Distance - Distance;
                if(Challenges)Challenges.SpawnRisk(pending,z);
                if (pending.Ball) Activate(3, pending.BallX(z), z, pending.BallCrossingX, pending.BallSlope);
                else for (int lane = 0; lane < 3; lane++) if (pending.IsBlocked(lane)) Activate(pending.Variant, ArcadeRules.LaneX(lane), z, 0, 0);
                SpawnedRows++; pending = sequence.Next();
            }
        }
        private void Activate(int variant, float x, float z, float crossing, float slope)
        {
            // Each prefab kind has six reserved instances; no allocations or Instantiate while driving.
            int offset = poolOffsets == null ? variant * 6 : poolOffsets[variant];
            int count = poolOffsets == null ? 6 : Definitions[variant].PoolSize;
            for (int i = offset; i < offset + count; i++)
                if (!Pool[i].InUse) { Pool[i].Place(x, z, crossing, slope); return; }
            PoolExhaustions++; Debug.LogError("Arcade pool exhausted; seed=" + Seed + " row=" + SpawnedRows);
        }
        public void Step(float dt, float oldX, float newX)
        {
            if (dt <= 0 || game.State != RunState.Running) return;
            if(game.Review)GetComponent<ReviewOnline>()?.Record(dt,newX);
            // Substeps bound acceleration error; swept relative collision handles both axes and hitches.
            int steps = Mathf.Max(1, Mathf.CeilToInt(dt * 60));
            float sub = dt / steps;
            for (int step = 0; step < steps; step++)
            {
                float fromX = Mathf.Lerp(oldX, newX, step / (float)steps);
                float toX = Mathf.Lerp(oldX, newX, (step + 1f) / steps);
                var mode=game.Review?game.Review.Mode:PlayMode.Arcade;
                float d = CareerRules.Distance(Elapsed + sub,mode) - CareerRules.Distance(Elapsed,mode);
                bool hit = false; float fraction = 1; ArcadeHazard hitHazard = null;
                foreach (var hazard in Pool)
                {
                    if (!hazard.InUse) continue;
                    var a = hazard.transform.position; var b = hazard.PositionAfter(d);
                    float t;
                    bool contact = hazard.RollingBall
                        ? SweptCircleBox.TryHit(fromX-a.x,-a.z,toX-b.x,-b.z,game.Config.PlayerHalfWidth,game.Config.PlayerHalfLength,hazard.HalfWidth,out t)
                        : SweptCollision.TryHit(fromX-a.x,-a.z,toX-b.x,-b.z,game.Config.PlayerHalfWidth+hazard.HalfWidth,game.Config.PlayerHalfLength+hazard.HalfLength,out t);
                    if (contact && t <= fraction)
                    { fraction = t; hit = true; hitHazard = hazard; }
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
                if(Challenges)Challenges.Step(moved,fromX,Mathf.Lerp(fromX,toX,fraction),hit);
                if(game.Review)GetComponent<GhostReplay>().Record(Elapsed,Mathf.Lerp(fromX,toX,fraction));
                if (hit)
                {
                    game.Player.StopAt(Mathf.Lerp(fromX, toX, fraction)); Style.Crash();
                    if(game.Review && game.Review.SurviveHit()){Sound.Crash(true);HUD.Impact();FillAhead();return;}
                    if(!game.Review || game.Review.Mode==PlayMode.Arcade)
                    {
                    BestDistance = Mathf.Max(BestDistance, Distance); BestStyle = Mathf.Max(BestStyle, Style.Total);
                    if(PersistRecords) { PlayerPrefs.SetFloat("BrakeToy.v03.distance", BestDistance); PlayerPrefs.SetInt(StyleRecordKey, BestStyle); PlayerPrefs.Save(); }
                    }
                    Sound.Crash(hitHazard && (hitHazard.RollingBall || hitHazard.HalfLength < .5f)); HUD.Impact();
                    var impact = game.GetComponent<ToyImpactFeedback>(); if (impact) impact.Play();
                    game.Crash(); return;
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
