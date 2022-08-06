using System;

namespace SpiralCircus.Math
{
    [Serializable]
    public struct Vector2
    {
        // MEMBERS

        #region MEMBERS

        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Magnitude()
        {
            return Magnitude(this);
        }

        public float SquareMagnitude()
        {
            return SquareMagnitude(this);
        }

        public bool ApproximatelyZero()
        {
            return ApproximatelyZero(this);
        }

        public Vector2 Normalized()
        {
            return Normalized(this);
        }

        public float Angle()
        {
            return Angle(this);
        }

        public Vector2 Perpendicular()
        {
            return Perpendicular(this);
        }

        public float FastMagnitude()
        {
            return FastMagnitude(this);
        }

        public Vector2 FastNormalized()
        {
            return FastNormalized(this);
        }

        #endregion

        // STATIC METHODS

        #region STATIC METHODS

        public static float Magnitude(Vector2 v)
        {
            return Maths.Sqrt(v.x * v.x + v.y * v.y);
        }

        public static float SquareMagnitude(Vector2 v)
        {
            return v.x * v.x + v.y * v.y;
        }

        public static Vector2 Normalized(Vector2 v)
        {
            return ApproximatelyZero(v) ? Zero : v / Magnitude(v);
        }

        public static float FastMagnitude(Vector2 v)
        {
            return 1 / Approximations.InvSqrt(v.x * v.x + v.y * v.y, SqrtIterations);
        }

        public static bool ApproximatelyZero(Vector2 v)
        {
            return ApproximatelyEqual(v, Zero);
        }

        public static bool ApproximatelyEqual(Vector2 a, Vector2 b)
        {
            return Maths.ApproximatelyEqual(a.x, b.x) && Maths.ApproximatelyEqual(a.y, b.y);
        }

        public static Vector2 FastNormalized(Vector2 v)
        {
            return ApproximatelyZero(v) ? Zero : v * Approximations.InvSqrt(v.x * v.x + v.y * v.y, SqrtIterations);
        }

        public static float Angle(Vector2 v)
        {
            var result = Maths.Atan2(v.y, v.x) * Maths.RadiansToDegrees;
            if (result < 0) result += 360;
            return result;
        }

        public static Vector2 FromAngle(float angle = 0, float magnitude = 1)
        {
            while (angle < 0) angle += 360;
            angle %= 360;
            angle *= Maths.DegreesToRadians;
            return new Vector2(Maths.Cos(angle), Maths.Sin(angle)) * magnitude;
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static float Cross(Vector2 a, Vector2 b, Vector2 o)
        {
            return (a.x - o.x) * (b.y - o.y) - (a.y - o.y) * (b.x - o.x);
        }

        public static float AngleBetween(Vector2 from, Vector2 to)
        {
            return Maths.Acos(Maths.Clamp(Dot(from.Normalized(), to.Normalized()), -1f)) * RadiansToDegrees;
        }

        public static Vector2 Random(float range, bool noNegative = false)
        {
            return new Vector2(
                Randomisation.RandomFloat(noNegative ? 0 : -range, range),
                Randomisation.RandomFloat(noNegative ? 0 : -range, range));
        }

        public static Vector2 RandomInsideCircle(float radius = 0, bool onCircumference = false)
        {
            var length = onCircumference ? radius : Randomisation.RandomFloat(0, radius);
            return FromAngle(Randomisation.RandomFloat(0, 360)) * length;
        }

        public static Vector2 Random(float min, float max)
        {
            return new Vector2(Randomisation.RandomFloat(min, max), Randomisation.RandomFloat(-min, max));
        }

        public static Vector2 Random(float xMin, float xMax, float yMin, float yMax)
        {
            return new Vector2(Randomisation.RandomFloat(xMin, xMax), Randomisation.RandomFloat(yMin, yMax));
        }

        public static Vector2 Random(Rect rect)
        {
            return Random(rect.xMin, rect.xMax, rect.yMin, rect.yMax);
        }

        public static float SignedAngleBetween(Vector2 from, Vector2 to)
        {
            var fromNorm = from.Normalized();
            var toNorm = to.Normalized();
            var angle = Maths.Acos(Maths.Clamp(Dot(fromNorm, toNorm), -1f)) * RadiansToDegrees;
            float sign = Maths.Sign(fromNorm.x * toNorm.y - fromNorm.y * toNorm.x);
            return angle * sign;
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Magnitude();
        }

        public static float FastDistance(Vector2 a, Vector2 b)
        {
            return (a - b).FastMagnitude();
        }

        public static Vector2 Rotate(Vector2 vector, float degrees)
        {
            var cos = Maths.Cos(degrees * Maths.DegreesToRadians);
            var sin = Maths.Sin(degrees * Maths.DegreesToRadians);

            var newX = vector.x * cos - vector.y * sin;
            var newY = vector.x * cos + vector.y * sin;

            return new Vector2(newX, newY);
        }

        public static Vector2 RotateAround(Vector2 vector, Vector2 origin, float degrees)
        {
            var cos = Maths.Cos(degrees * Maths.DegreesToRadians);
            var sin = Maths.Sin(degrees * Maths.DegreesToRadians);

            vector -= origin;

            var newX = vector.x * cos - vector.y * sin;
            var newY = vector.x * cos + vector.y * sin;

            return new Vector2(newX, newY) + origin;
        }

        public static Vector2 Perpendicular(Vector2 v)
        {
            return new Vector2(v.y, -v.x);
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            Vector2 result;
            if (vector.SquareMagnitude() > maxLength * maxLength)
                result = vector.Normalized() * maxLength;
            else
                result = vector;

            return result;
        }

        public static Vector2 FastClampMagnitude(Vector2 vector, float maxLength)
        {
            Vector2 result;
            if (vector.SquareMagnitude() > maxLength * maxLength)
                result = vector.FastNormalized() * maxLength;
            else
                result = vector;

            return result;
        }

        public static Vector2 SetMagnitude(Vector2 vector, float maxLength)
        {
            return vector.Normalized() * maxLength;
        }

        public static Vector2 FastSetMagnitude(Vector2 vector, float maxLength)
        {
            return vector.FastNormalized() * maxLength;
        }

        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            return new Vector2(Maths.Min(a.x, b.x), Maths.Min(a.y, b.y));
        }

        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(Maths.Max(a.x, b.x), Maths.Max(a.y, b.y));
        }

        public static Vector2 ArrayMean(Vector2[] array)
        {
            var result = Zero;
            for (var i = 0; i < array.Length; i++) result += array[i];
            return result / array.Length;
        }

        public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity,
            float smoothTime,
            float deltaTime)
        {
            smoothTime = Maths.Max(0.0001f, smoothTime);
            var twoOver = 2f / smoothTime;
            var timeStep = twoOver * deltaTime;
            var d = 1f / (1f + timeStep + 0.48f * timeStep * timeStep + 0.235f * timeStep * timeStep * timeStep);
            var targetToCurrent = current - target;
            var targetPos = target;
            target = current - targetToCurrent;
            var speed = (currentVelocity + twoOver * targetToCurrent) * deltaTime;
            currentVelocity = (currentVelocity - twoOver * speed) * d;
            var delta = target + (targetToCurrent + speed) * d;
            if (!(Dot(targetPos - current, delta - targetPos) > 0f)) return delta;

            delta = targetPos;
            currentVelocity = (delta - targetPos) / deltaTime;
            return delta;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = Maths.Clamp(t);
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        public static bool IsNan(Vector2 v)
        {
            return float.IsNaN(v.x) || float.IsNaN(v.y);
        }

        public static Vector2 SampleArrayLinear(Vector2[] points, float t, float tMax = 1, bool lerp = true)
        {
            var pos = points.Length * (t % tMax / tMax);
            var i = Maths.FloorToInt(pos);
            while (i < 0) i += points.Length;
            i %= points.Length;

            var iPlus = Maths.Clamp(i + 1, 0, points.Length - 1);
            var tLerp = pos % 1;

            return lerp ? Lerp(points[i], points[iPlus], tLerp) : points[i];
        }

        #endregion

        // OPERATORS

        #region OPERATORS

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return (lhs - rhs).ApproximatelyZero();
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        #endregion

        // OBJECT OVERRIDES

        #region OVERRIDES

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        public override bool Equals(object other)
        {
            bool result;
            if (!(other is Vector2 vector2D))
                result = false;
            else
                result = x.Equals(vector2D.x) && y.Equals(vector2D.y);

            return result;
        }

        #endregion

        // STANDARD VALUES

        #region STANDARD VALUES

        public static Vector2 Zero { get; } = new Vector2(0f, 0f);
        public static Vector2 One { get; } = new Vector2(1f, 1f);
        public static Vector2 Up { get; } = new Vector2(0f, 1f);
        public static Vector2 Down { get; } = new Vector2(0f, -1f);
        public static Vector2 Left { get; } = new Vector2(-1f, 0f);
        public static Vector2 Right { get; } = new Vector2(1f, 0f);
        public static Vector2 PositiveInfinity { get; } = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        public static Vector2 NegativeInfinity { get; } = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
        public static Vector2 Epsilon { get; } = new Vector2(KEpsilon, KEpsilon);

        #endregion

        // CONSTANTS

        #region CONSTANTS

        private const float RadiansToDegrees = 57.29578f;
        private const int SqrtIterations = 1;
        private const float KEpsilon = 1E-06f;

        #endregion
    }
}