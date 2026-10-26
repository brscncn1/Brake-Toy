using System;

namespace BrakeToy.Core
{
    public static class RunRewardBreakdown
    {
        public static int AirBonus(float airDistance, int airStyle, float distance, int style)
        {
            int ground = (int)(distance / 10) + style / 100;
            int extra = (int)(Math.Max(0, Math.Min(distance, airDistance)) / 10)
                + Math.Max(0, Math.Min(style, airStyle)) / 100;
            return Math.Min(2500, ground + extra) - Math.Min(2500, ground);
        }
    }
}
