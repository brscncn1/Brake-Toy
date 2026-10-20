using UnityEngine;

namespace BrakeToy
{
    public sealed class WorldScroller : MonoBehaviour
    {
        public Transform[] FloorChunks;
        public ObstacleView[] Obstacles;
        public float ChunkLength = 18;
        public float ObstacleSpacing = 14;
        private Vector3[] floorStarts;
        private Vector3[] obstacleStarts;

        public void Initialize()
        {
            floorStarts = new Vector3[FloorChunks.Length];
            obstacleStarts = new Vector3[Obstacles.Length];
            for (int i = 0; i < FloorChunks.Length; i++) floorStarts[i] = FloorChunks[i].position;
            for (int i = 0; i < Obstacles.Length; i++) obstacleStarts[i] = Obstacles[i].transform.position;
        }
        public void ResetRun()
        {
            for (int i = 0; i < FloorChunks.Length; i++) FloorChunks[i].position = floorStarts[i];
            for (int i = 0; i < Obstacles.Length; i++) Obstacles[i].transform.position = obstacleStarts[i];
        }
        public void Advance(float distance)
        {
            Vector3 movement = Vector3.back * distance;
            foreach (var chunk in FloorChunks)
            {
                chunk.position += movement;
                while (chunk.position.z < -ChunkLength)
                    chunk.position += Vector3.forward * (FloorChunks.Length * ChunkLength);
            }
            foreach (var obstacle in Obstacles)
            {
                obstacle.transform.position += movement;
                while (obstacle.transform.position.z < -8)
                    obstacle.transform.position += Vector3.forward * (Obstacles.Length * ObstacleSpacing);
            }
        }
    }
}
