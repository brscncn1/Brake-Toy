using System;

namespace BrakeToy.Core
{
    /// <summary>Versioned local arcade rules. No dependency on frame rate or Unity random state.</summary>
    public static class ArcadeRules
    {
        public const float MaxSpeed = 18f;
        public const float LaneWidth = 2.25f;
        public const float ReactionSeconds = 1.5f;
        public static float Speed(float seconds) => 10f + 8f * (1f - (float)Math.Exp(-Math.Max(0, seconds) / 18f));
        public static float Distance(float seconds)
        {
            seconds = Math.Max(0, seconds);
            return 18f * seconds - 144f * (1f - (float)Math.Exp(-seconds / 18f));
        }
        public static float LaneX(int lane) => (lane - 1) * LaneWidth;
    }

    public readonly struct ArcadeRow
    {
        public readonly int Index, SafeLane, BlockedMask, Variant;
        public readonly bool Ball;
        public readonly float Distance, BallCrossingX, BallSlope;
        public ArcadeRow(int index, int safe, int mask, int variant, bool ball, float distance, float crossing, float slope)
        { Index = index; SafeLane = safe; BlockedMask = mask; Variant = variant; Ball = ball; Distance = distance; BallCrossingX = crossing; BallSlope = slope; }
        public float BallX(float z) => BallCrossingX + BallSlope * z;
        public bool IsBlocked(int lane) => (BlockedMask & (1 << lane)) != 0;
    }

    public sealed class ArcadeSequence
    {
        private uint state;
        private int index, safe = 1, previousVariant = -1, sameLaneCount, nextBall = 2;
        private readonly bool varied, choreographed,relaxed;
        private readonly bool mirrored;
        private float distance = 24;
        public ArcadeSequence(int seed, bool varied = false, bool choreographed = false,bool relaxed=false) { this.relaxed=relaxed; state = (uint)seed; if (state == 0) state = 1; this.varied = varied; this.choreographed=choreographed; mirrored=(seed&1)==0; }
        private int Next(int count) { state ^= state << 13; state ^= state >> 17; state ^= state << 5; return (int)(state % (uint)count); }
        public ArcadeRow Next()
        {
            int i = index++;
            // First encounter asks for a visible steering decision, not a waiting period.
            int candidate = i == 0 ? (Next(2) == 0 ? 0 : 2) : Math.Max(0, Math.Min(2, safe + Next(3) - 1));
            if(choreographed)
            {
                // Short authored sweeps/holds create readable decisions; transitions stay one lane apart.
                int beat=i%8,family=(i/8)%3;
                int desired=family==0?(beat<=2?beat:beat<=4?4-beat:beat==5?0:beat-5):family==1?(beat/2%2==0?0:1):(beat%4<=2?beat%4:1);
                if(mirrored)desired=2-desired;
                candidate=Math.Max(safe-1,Math.Min(safe+1,desired));
                if(i==0)candidate=mirrored?2:0;
            }
            if (varied && candidate == safe && sameLaneCount >= 2) candidate = safe == 1 ? Next(2) * 2 : 1;
            sameLaneCount = candidate == safe ? sameLaneCount + 1 : 1;
            safe = candidate;
            int variant = (previousVariant + 1 + Next(2)) % 3; previousVariant = variant;
            bool ball = varied ? i == nextBall : i >= 2 && i % 7 == 2;
            if (varied && ball) nextBall += 5 + Next(4);
            int mask = 7 ^ (1 << safe);
            // Single obstacles and breathing rows break up the double-gate cadence.
            bool breathing = varied ? i > 0 && Next(5) == 0 : i % 6 == 4;
            if (breathing || i == 0) mask = 1 << (safe == 1 ? Next(2) * 2 : 1);
            if(relaxed)mask=1<<(safe==0?2:0);
            int ballLane = safe == 0 ? 2 : 0;
            float slope = ballLane == 2 ? .06f : -.06f;
            if (varied)
            {
                ballLane = (safe + 1 + Next(2)) % 3;
                slope = (Next(2) == 0 ? -1 : 1) * (.035f + Next(31) * .001f);
            }
            var row = new ArcadeRow(i, safe, ball ? 0 : mask, variant, ball, distance, ArcadeRules.LaneX(ballLane), slope);
            distance += varied ? (breathing ? 34 : 25 + Next(7)) : i % 6 == 4 ? 34 : 27;
            return row;
        }
    }

    public sealed class StyleScore
    {
        public int Total { get; private set; }
        public int Chain { get; private set; }
        public int NearMisses { get; private set; }
        public float Remaining { get; private set; }
        public int Multiplier => Chain >= 8 ? 5 : Chain >= 5 ? 3 : Chain >= 3 ? 2 : 1;
        public bool Fever => Chain >= 8;
        public void Reset() { Total = Chain = NearMisses = 0; Remaining = 0; }
        public void Advance(float dt) { Remaining = Math.Max(0, Remaining - Math.Max(0, dt)); if (Remaining <= 0) Chain = 0; }
        public int Award() { Chain++; NearMisses++; Remaining = 6; int points = 100 * Multiplier; Total += points; return points; }
        public void Crash() { Chain = 0; Remaining = 0; }
        public void AwardRiskStar() { Total += 75; }
        public void AwardMastery(int points) { Total += Math.Max(0,points); }
    }
}
