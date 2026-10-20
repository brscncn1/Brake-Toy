using System;
using UnityEngine;

namespace BrakeToy
{
    /// <summary>Near table props share the world's exact contact fraction and restart timing.</summary>
    public sealed class TabletopDecorScroller : MonoBehaviour
    {
        public WorldScroller World;
        public Transform[] Targets = Array.Empty<Transform>();
        [Min(16)] public float RecycleDistance = 64;
        public float DespawnZ = -10;
        private Vector3[] starts;

        private void Awake() => CacheStarts();
        private void OnEnable()
        {
            if (starts == null) CacheStarts();
            if (!World) return;
            World.DistanceAdvanced += Advance;
            World.RunReset += ResetView;
        }
        private void OnDisable()
        {
            if (!World) return;
            World.DistanceAdvanced -= Advance;
            World.RunReset -= ResetView;
        }
        private void CacheStarts()
        {
            starts = new Vector3[Targets.Length];
            for (int i = 0; i < Targets.Length; i++) starts[i] = Targets[i].position;
        }
        private void Advance(float distance)
        {
            foreach (var target in Targets)
            {
                target.position += Vector3.back * distance;
                while (target.position.z < DespawnZ) target.position += Vector3.forward * Mathf.Max(16, RecycleDistance);
            }
        }
        private void ResetView()
        {
            for (int i = 0; i < Targets.Length; i++) Targets[i].position = starts[i];
        }
    }
}
