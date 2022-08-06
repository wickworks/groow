using System.Collections.Generic;

namespace Grow.Math
{
    public static class Geometry
    {
        private static readonly List<Vector2> _lowerHull = new List<Vector2>();
        private static readonly List<Vector2> _upperHull = new List<Vector2>();

        public static Rect GetCameraFrustum(Vector2 position, float cameraZ, float targetZ, float fov,
            float aspect = 16f / 9f)
        {
            var d = targetZ - cameraZ;
            var h = 2 * d * Maths.Tan(fov * 0.5f * Maths.DegreesToRadians);
            var w = h * aspect;
            return new Rect(position, new Vector2(w, h) / 2f);
        }

        public static float GetCameraFov(Rect rect, float cameraZ, float targetZ)
        {
            var d = targetZ - cameraZ;
            return 2 * Maths.Atan(rect.Height * 0.5f / d) * Maths.RadiansToDegrees;
        }

        public static bool PointInsideCircle(Vector2 point, Vector2 circleCentre, float radius)
        {
            return (point - circleCentre).SquareMagnitude() < radius * radius;
        }

        public static bool PointInsideBox(Vector2 point, Vector2 boxCentre, Vector2 boxSize, float boxDirection = 0)
        {
            point -= boxCentre;

            if (!Maths.ApproximatelyEqual(boxDirection, 0)) point = Vector2.Rotate(point, -boxDirection);

            var xResult = Maths.Abs(point.x) <= boxSize.x * 0.5f;
            var yResult = Maths.Abs(point.y) <= boxSize.y * 0.5f;

            return xResult && yResult;
        }

        public static bool PointInsideRect(Vector2 point, Rect rect)
        {
            return point.x >= rect.xMin && point.x <= rect.xMax && point.y >= rect.yMin && point.y <= rect.yMax;
        }

        public static float PerpendicularDistanceToLine(Vector2 point, Vector2 lineA, Vector2 lineB)
        {
            var triangleArea = Maths.Abs((lineB.y - lineA.y) * point.x
                                         - (lineB.x - lineA.x) * point.y
                                         + lineB.x * lineA.y
                                         - lineB.y * lineA.x);
            return triangleArea / (lineB - lineA).Magnitude();
        }

        public static void ConvexHull(List<Vector2> points, List<Vector2> hull)
        {
            // check for null inputs
            if (points == null) return;

            if (hull == null) return;

            // require three points
            if (points.Count < 3) return;

            // sort lexicographically (first by x-coordinate, and in case of a tie, by y-coordinate)
            points.Sort((a, b) => System.Math.Abs(a.x - b.x) < Maths.EPSILON ? a.y.CompareTo(b.y) : a.x > b.x ? 1 : -1);

            // build the lower hull, starting at the leftmost point
            _lowerHull.Clear();
            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];
                // WHILE the lower hull contains at least two points
                // AND the sequence of last two points and the point P[i] does not make a counter-clockwise turn
                // REMOVE the last point from L
                while (_lowerHull.Count >= 2 && Vector2.Cross(_lowerHull[_lowerHull.Count-1], _lowerHull[_lowerHull.Count-1], point) <= 0)
                    _lowerHull.RemoveAt(_lowerHull.Count - 1);
                // add the current point to L
                _lowerHull.Add(point);
            }

            // build the upper hull , starting at the rightmost point
            _upperHull.Clear();
            for (var i = points.Count - 1; i >= 0; i--)
            {
                var point = points[i];
                // WHILE the upper hull contains at least two points
                // AND the sequence of last two points and the point P[i] does not make a counter-clockwise turn
                // REMOVE the last point from L
                while (_upperHull.Count >= 2 &&
                       Vector2.Cross(_upperHull[_upperHull.Count-1], _upperHull[_upperHull.Count-1], point) <= 0)
                    _upperHull.RemoveAt(_upperHull.Count - 1);
                _upperHull.Add(point);
            }

            // remove the LAST point of each list (it's the same as the first point of the other list)
            _lowerHull.RemoveAt(_lowerHull.Count - 1);
            _upperHull.RemoveAt(_upperHull.Count - 1);

            // clear the buffer and fill it with the results
            // concatenating the lower and upper hulls gives the complete hull in counter-clockwise order
            hull.Clear();
            hull.AddRange(_lowerHull);
            hull.AddRange(_upperHull);
        }
    }
}