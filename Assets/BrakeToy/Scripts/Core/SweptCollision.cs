using System;

namespace BrakeToy.Core
{
    /// <summary>Continuous XZ AABB intersection in relative space, including initial overlap.</summary>
    public static class SweptCollision
    {
        public static bool TryHit(float fromX, float fromZ, float toX, float toZ,
            float combinedHalfX, float combinedHalfZ, out float hitTime)
        {
            float entry = 0, exit = 1;
            bool hit = Axis(fromX, toX - fromX, combinedHalfX, ref entry, ref exit)
                    && Axis(fromZ, toZ - fromZ, combinedHalfZ, ref entry, ref exit);
            hitTime = hit ? entry : 1;
            return hit;
        }

        private static bool Axis(float origin, float delta, float halfSize, ref float entry, ref float exit)
        {
            if (Math.Abs(delta) < 1e-7f) return Math.Abs(origin) <= halfSize;
            float a = (-halfSize - origin) / delta;
            float b = (halfSize - origin) / delta;
            if (a > b) { float swap = a; a = b; b = swap; }
            entry = Math.Max(entry, a);
            exit = Math.Min(exit, b);
            return entry <= exit;
        }
    }
}
