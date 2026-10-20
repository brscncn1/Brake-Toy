using UnityEngine;

namespace BrakeToy
{
    [CreateAssetMenu(menuName = "Brake Toy/Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        [Header("World units: one unit is one toy-scale gameplay unit")]
        [Min(1)] public float TrackHalfWidth = 3.5f;
        [Min(.1f)] public float PlayerHalfWidth = .39f;
        [Min(.1f)] public float PlayerHalfLength = .7f;
        [Min(.1f)] public float WorldSpeed = 8;
        [Min(.1f)] public float DragSensitivity = 9;
        [Min(.1f)] public float MaxLateralSpeed = 9.5f;
        [Range(.15f, .35f)] public float PlayerViewportY = .25f;
        [Range(15, 65)] public float CameraPitch = 52;
        [Range(35, 65)] public float VerticalFieldOfView = 50;
        [Range(.2f, 2)] public float CameraSidePadding = .8f;
        [Range(30, 60)] public int TargetFrameRate = 60;
        public float PlayerLimit => Mathf.Max(.1f, TrackHalfWidth - PlayerHalfWidth);
    }
}
