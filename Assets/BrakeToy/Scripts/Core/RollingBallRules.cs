using System;

namespace BrakeToy.Core
{
    public enum BallPath { Straight, Diagonal, Curve, DoubleCurve }

    public static class RollingBallRules
    {
        public const string Version = "tabletop-r4-rolling";
        public static BallPath Profile(int variant) => (BallPath)((variant / 3) % 4);
        public static float SpeedFactor(int variant, int stage)
            => 1.2f + Math.Min(3, Math.Max(0, stage)) * .06f + (variant % 3) * .035f;
        public static float Offset(BallPath path, float depth, float phase, float amplitude = .55f)
        {
            switch (path)
            {
                case BallPath.Diagonal: return amplitude * (float)Math.Tanh(depth * .09f) * (Math.Sin(phase) < 0 ? -1 : 1);
                case BallPath.Curve: return amplitude * (float)Math.Sin(depth * .085f + phase);
                case BallPath.DoubleCurve: return amplitude * (float)(.7 * Math.Sin(depth * .12f + phase) + .3 * Math.Sin(depth * .24f + phase));
                default: return 0;
            }
        }
    }
}
