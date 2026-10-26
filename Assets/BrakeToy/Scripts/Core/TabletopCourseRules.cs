using System;

namespace BrakeToy.Core
{
    // SafeLane names only the entrance to the original toy. Later gates require new decisions.
    public static class TabletopCourseRules
    {
        public const string Version = "tabletop-r5-choices";
        public static bool OpenRampExit(PlaygroundBeat beat) => (beat.Variant / 11) % 2 == 0;
        public static float GateOffset(PlaygroundBeat beat, int gate)
            => 22 + gate * 18 + (beat.Variant / (gate == 0 ? 1 : gate == 1 ? 5 : 25)) % 5 - 2;
        public static int Pattern(PlaygroundBeat beat) => beat.Index % 6;
        public static int GapLane(PlaygroundBeat beat, int gate)
        {
            int start = beat.SafeLane;
            int neighbour = start == 1 ? (beat.Variant % 2) * 2 : 1;
            if (gate == 0) return start;
            if (gate == 1) return neighbour;
            switch (Pattern(beat))
            {
                case 0: case 3: return start; // short slalom / bus and return
                case 1: case 4: return neighbour; // narrow corridor / offset exit
                default: return start == 1 ? 1 : 2 - start; // sweeping S
            }
        }
        public static float Spacing(int index, bool relax)
            => (relax ? 86 : 78) + (index % 3) * 3;

        // A witness used by automated validation, never an auto-steering aid in the game.
        public static float WitnessX(PlaygroundBeat beat, float distance)
        {
            float local = distance - beat.Distance;
            int gate = local < GateOffset(beat, 0) + 4 ? 0 : local < GateOffset(beat, 1) + 4 ? 1 : 2;
            return ArcadeRules.LaneX(GapLane(beat, gate));
        }
    }
}
