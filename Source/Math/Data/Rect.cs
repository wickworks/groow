using System;

namespace SpiralCircus.Math
{
    [Serializable]
    public struct Rect
    {
        public Vector2 centre;
        public Vector2 halfSize;
        public float xMin, xMax, yMin, yMax;
        public bool line;

        public Rect(Vector2 centre, Vector2 halfSize)
        {
            this.centre = centre;
            this.halfSize = new Vector2(Maths.Abs(halfSize.x), Maths.Abs(halfSize.y));
            xMin = centre.x - halfSize.x;
            xMax = centre.x + halfSize.x;
            yMin = centre.y - halfSize.y;
            yMax = centre.y + halfSize.y;
            line = Maths.ApproximatelyEqual(xMin, xMax) || Maths.ApproximatelyEqual(yMin, yMax);
        }

        public float Width => xMax - xMin;
        public float Height => yMax - yMin;
        public Vector2 topLeft => centre + new Vector2(-halfSize.x, halfSize.y);
        public Vector2 topRight => centre + halfSize;
        public Vector2 bottomLeft => centre - halfSize;
        public Vector2 bottomRight => centre + new Vector2(halfSize.x, -halfSize.y);
        public float area => halfSize.x * halfSize.y * 4;

        private void Cache()
        {
            xMin = centre.x - halfSize.x;
            xMax = centre.x + halfSize.x;
            yMin = centre.y - halfSize.y;
            yMax = centre.y + halfSize.y;
            line = Maths.ApproximatelyEqual(xMin, xMax) || Maths.ApproximatelyEqual(yMin, yMax);
        }

        public bool Contains(Vector2 point)
        {
            return point.x >= xMin && point.x <= xMax && point.y >= yMin && point.y <= yMax;
        }

        public bool Overlap(Rect other)
        {
            return Overlap(this, other);
        }

        public static bool Overlap(Rect a, Rect b)
        {
            if (a.line || b.line) return false;

            return !(a.xMax < b.xMin || a.xMin > b.xMax || a.yMin > b.yMax || a.yMax < b.yMin);
        }
    }
}