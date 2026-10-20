using BrakeToy.Core;

namespace BrakeToy
{
    public static class CollisionManager
    {
        public static bool FirstHit(GameConfig config, ObstacleView[] obstacles, float oldX, float newX,
            float worldDistance, out float hitTime)
        {
            hitTime = 1;
            bool hit = false;
            foreach (var obstacle in obstacles)
            {
                // Phase 1 obstacles are axis-aligned, unscaled, ground-level static boxes.
                // Player and obstacle are both swept; lateral movement cannot tunnel either.
                if (SweptCollision.TryHit(oldX - obstacle.X, -obstacle.Z,
                    newX - obstacle.X, -obstacle.Z + worldDistance,
                    config.PlayerHalfWidth + obstacle.HalfWidth,
                    config.PlayerHalfLength + obstacle.HalfLength, out float time) && time <= hitTime)
                { hitTime = time; hit = true; }
            }
            return hit;
        }
    }
}
