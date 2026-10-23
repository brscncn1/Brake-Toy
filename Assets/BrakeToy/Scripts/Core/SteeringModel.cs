using System;

namespace BrakeToy.Core
{
    /// <summary>Resolution-independent relative drag. No Unity or scene dependency.</summary>
    public sealed class SteeringModel
    {
        public float Position { get; private set; }
        public float Target { get; private set; }
        public bool IsDragging { get; private set; }
        public float Limit { get; }
        public float Sensitivity { get; }
        public float MaxSpeed { get; }

        public SteeringModel(float limit, float sensitivity, float maxSpeed)
        {
            if (!(limit > 0) || !(sensitivity > 0) || !(maxSpeed > 0))
                throw new ArgumentOutOfRangeException(nameof(limit));
            Limit = limit;
            Sensitivity = sensitivity;
            MaxSpeed = maxSpeed;
        }

        public void BeginDrag() { IsDragging = true; Target = Position; }
        public void Drag(float pixelDelta, float screenWidth)
        {
            if (IsDragging && screenWidth > 0)
                Target = Clamp(Target + pixelDelta / screenWidth * Sensitivity, -Limit, Limit);
        }
        public void EndDrag() { IsDragging = false; Target = Position; }
        public void Reset() { Position = Target = 0; iceVelocity=0; IsDragging = false; }

        private float iceVelocity;
        public float Step(float deltaTime,float grip=1)
        {
            if (deltaTime < 0) throw new ArgumentOutOfRangeException(nameof(deltaTime));
            float distance = MaxSpeed * deltaTime;
            if(grip<1){float desired=deltaTime>0?Clamp((Target-Position)/deltaTime,-MaxSpeed,MaxSpeed):0;float blend=1-(float)Math.Exp(-4*deltaTime);iceVelocity+=(desired-iceVelocity)*blend;Position=Clamp(Position+iceVelocity*deltaTime,-Limit,Limit);}else {iceVelocity=0;Position += Clamp(Target - Position, -distance, distance);}
            return Position;
        }

        // Only used to stop precisely at the first swept contact of a frame.
        public void StopAt(float position)
        {
            iceVelocity=0;Position = Target = Clamp(position, -Limit, Limit);
            IsDragging = false;
        }

        private static float Clamp(float value, float min, float max) => Math.Max(min, Math.Min(max, value));
    }
}
