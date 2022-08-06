namespace SpiralCircus.Math
{
    public static class Maths
    {
        public const float EPSILON = 0.00001f;
        public const float PI = 3.14159265358979323846f;
        public const float ROOT2 = 1.41421356237f;

        public static float DegreesToRadians => (float)System.Math.PI / 180;
        public static float RadiansToDegrees => 180 / (float)System.Math.PI;

        public static float Sqrt(float x)
        {
            return (float)System.Math.Sqrt(x);
        }

        public static float Abs(float x)
        {
            return x >= 0 ? x : -x;
        }

        public static bool ApproximatelyEqual(float a, float b)
        {
            return Abs(a - b) <= EPSILON;
        }

        public static bool ApproximatelyEqual(float a, float b, float epsilon)
        {
            return Abs(a - b) <= epsilon;
        }

        public static int Abs(int x)
        {
            return x >= 0 ? x : -x;
        }

        public static float Clamp(float x, float min = 0, float max = 1)
        {
            return x < min ? min : x > max ? max : x;
        }

        public static int Clamp(int x, int min = 0, int max = 1)
        {
            return x < min ? min : x > max ? max : x;
        }

        public static float Min(float a, float b)
        {
            return a <= b ? a : b;
        }

        public static float Max(float a, float b)
        {
            return a >= b ? a : b;
        }

        public static float Floor(float x)
        {
            return (float)System.Math.Floor(x);
        }

        public static int Min(int a, int b)
        {
            return a <= b ? a : b;
        }

        public static int Max(int a, int b)
        {
            return a >= b ? a : b;
        }

        public static int FloorToInt(float x)
        {
            return (int)x;
        }

        public static float Ceil(float x)
        {
            return (float)System.Math.Ceiling(x);
        }

        public static int CeilToInt(float x)
        {
            return (int)x + 1;
        }

        public static float Round(float x)
        {
            return (float)System.Math.Round(x);
        }

        public static float RoundToNearest(float x, float nearest)
        {
            return nearest * Round(x / nearest);
        }

        public static int RoundToNearestInt(float x, int nearest)
        {
            return nearest * RoundToInt(x / nearest);
        }

        public static float CeilToNearest(float x, float nearest)
        {
            return nearest * Ceil(x / nearest);
        }

        public static float FloorToNearest(float x, float nearest)
        {
            return nearest * Floor(x / nearest);
        }

        public static int RoundToInt(float x)
        {
            return (int)System.Math.Round(x);
        }

        public static float RoundToFactor(float x, float factor)
        {
            return Round(x / factor) * factor;
        }

        public static int Sign(float x)
        {
            return x >= 0 ? 1 : -1;
        }

        public static float ScaleRange(float input, float inputMin, float inputMax, float outputMin, float outputMax,
            bool clampInput = false)
        {
            if (inputMin > inputMax) Swap(ref inputMin, ref inputMax);

            if (outputMin > outputMax) Swap(ref outputMin, ref outputMax);

            if (clampInput) input = Clamp(input, inputMin, inputMax);

            var inputRange = inputMax - inputMin;
            var outputRange = outputMax - outputMin;

            var result = input - inputMin;
            if (inputRange > 0) result /= inputRange;

            result *= outputRange;
            result += outputMin;
            return result;
        }

        public static float Pow(float value, float exponent)
        {
            return (float)System.Math.Pow(value, exponent);
        }

        private static void Swap(ref float a, ref float b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static float Step(float input, float edge, float outputMin = 0, float outputMax = 1)
        {
            return input < edge ? outputMin : outputMax;
        }

        public static float LinStep(float input, float edge0, float edge1, float outputMin = 0, float outputMax = 1)
        {
            return ScaleRange(input, edge0, edge1, outputMin, outputMax, true);
        }

        public static float SmoothStep(float input, float edge0, float edge1, float outputMin = 0, float outputMax = 1)
        {
            var t = ScaleRange(input, edge0, edge1, 0, 1, true);
            var poly = t * t * (3 - 2 * t);
            return ScaleRange(poly, 0, 1, outputMin, outputMax);
        }

        public static float FastSin(float x, bool precision = true)
        {
            return precision
                ? Approximations.HighPrecisionSin(x)
                : Approximations.LowPrecisionSin(x);
        }

        public static float FastCos(float x, bool precision = true)
        {
            return precision ? Approximations.HighPrecisionCos(x) : Approximations.LowPrecisionCos(x);
        }

        public static float Sin(float x)
        {
            return (float)System.Math.Sin(x);
        }

        public static float Cos(float x)
        {
            return (float)System.Math.Cos(x);
        }

        public static float Tan(float x)
        {
            return (float)System.Math.Tan(x);
        }

        public static float Asin(float x)
        {
            return (float)System.Math.Asin(x);
        }

        public static float Acos(float x)
        {
            return (float)System.Math.Acos(x);
        }

        public static float Atan(float x)
        {
            return (float)System.Math.Atan(x);
        }

        public static float Atan2(float x, float y)
        {
            return (float)System.Math.Atan2(x, y);
        }

        public static float Tanh(float x)
        {
            return (float)System.Math.Tanh(x);
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Clamp(t);
        }

        public static float LerpUnclamped(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float InverseLerp(float a, float b, float v)
        {
            return (v - 1) / (b - a);
        }

        public static float UnsignedAngleDelta(float a, float b)
        {
            a = Wrap(a, 360);
            b = Wrap(b, 360);

            var lowest = Min(a, b);
            var highest = Max(a, b);

            var d1 = highest - lowest;
            var d2 = lowest - (highest - 360);

            return Min(d1, d2);
        }

        public static float LerpAngle(float a, float b, float t)
        {
            var rotation = SignedAngleDelta(a, b);
            return Wrap(Lerp(a, a + rotation, Clamp(t)), 360);
        }

        public static float LerpAngleUnclamped(float a, float b, float t)
        {
            var rotation = SignedAngleDelta(a, b);
            return Wrap(Lerp(a, a + rotation, t), 360);
        }

        public static float ClampAngle(float angle, float centre, float arc)
        {
            centre = Wrap(centre, 360);
            angle = Wrap(angle, 360);
            var distance = SignedAngleDelta(centre, angle);
            if (distance > arc)
                angle = Wrap(centre + arc, 360);
            else if (distance < -arc) angle = Wrap(centre - arc, 360);

            return angle;
        }

        public static float ScaleAngle(float angle, float centre, float factor)
        {
            centre = Wrap(centre, 360);
            angle = Wrap(angle, 360);
            var distance = SignedAngleDelta(centre, angle);
            distance *= factor;
            return Wrap(angle + distance, 360);
        }

        public static float MoveTowardsAngle(float a, float b, float delta)
        {
            var rotation = SignedAngleDelta(a, b);
            return Abs(rotation) < delta ? a : Wrap(a + delta * Sign(rotation), 360);
        }

        public static float WrappedDistance(float from, float to, float min, float max)
        {
            var a = Abs(from - to);
            var b = Abs(from - to + max - min);
            var c = Abs(from - to - max - min);
            return Min(a, Min(b, c));
        }

        public static float SignedAngleDelta(float from, float to)
        {
            var a = to - from;
            return Mod(a + 180, 360) - 180;
        }

        public static float Mod(float a, float n)
        {
            return a - Floor(a / n) * n;
        }

        public static float Wrap(float value, float dividend)
        {
            while (value < 0) value += dividend;
            return value % dividend;
        }

        public static int Wrap(int value, int dividend)
        {
            while (value < 0) value += dividend;
            return value % dividend;
        }

        public static float Wrap(float value, float min, float max)
        {
            value += min;
            var result = Wrap(value, max - min);
            result -= min;
            return result;
        }

        public static int Wrap(int value, int min, int max)
        {
            value += min;
            var result = Wrap(value, max - min);
            result -= min;
            return result;
        }

        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime,
            float deltaTime, float maxSpeed = float.MaxValue)
        {
            smoothTime = Max(0.0001f, smoothTime);
            var inverseSmoothTime = 2f / smoothTime;
            var dt = inverseSmoothTime * deltaTime;
            var acceleration =
                (float)(1.0 / (1.0 + dt + 0.479999989271164 * dt * dt + 0.234999999403954 * dt * dt * dt));
            var delta = current - target;
            var tempTarget = target;
            var max = maxSpeed * smoothTime;
            var clampedDelta = Clamp(delta, -max, max);
            target = current - clampedDelta;
            var deltaVelocity = (currentVelocity + inverseSmoothTime * clampedDelta) * deltaTime;
            currentVelocity = (currentVelocity - inverseSmoothTime * deltaVelocity) * acceleration;
            var stepTowards = target + (clampedDelta + deltaVelocity) * acceleration;
            if (tempTarget - (double)current > 0.0 != stepTowards > (double)tempTarget) return stepTowards;

            stepTowards = tempTarget;
            currentVelocity = (stepTowards - tempTarget) / deltaTime;
            return stepTowards;
        }

        public static float TruncateFloat(float value, int digits)
        {
            var mult = System.Math.Pow(10.0, digits);
            var result = System.Math.Truncate(mult * value) / mult;
            return (float)result;
        }
    }
}