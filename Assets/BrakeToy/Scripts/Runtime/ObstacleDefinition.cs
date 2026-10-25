using UnityEngine;

namespace BrakeToy
{
    [CreateAssetMenu(menuName = "Brake Toy/Obstacle definition")]
    public sealed class ObstacleDefinition : ScriptableObject
    {
        [Range(0, 9)] public int Kind;
        public ArcadeHazard Prefab;
        [Min(6)] public int PoolSize = 6;
        [Min(.1f)] public float HalfWidth = .8f, HalfLength = .6f;
        [TextArea] public string ReadabilityNotes;
    }
}
