using System;

namespace SpiralCircus.Math
{
    [Serializable]
    public struct BezierPoint
    {
        public Vector2 origin, position, direction, normal;
        public float parameter;

        public BezierPoint(Vector2 position, Vector2 direction, float parameter)
        {
            this.position = position;
            this.direction = direction;
            this.parameter = Maths.Clamp(parameter);
            origin = position;
            normal = direction.Perpendicular();
        }
    }
}