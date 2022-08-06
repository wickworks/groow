using System.Runtime.InteropServices;

namespace Grow.Math
{
    public static class Approximations
    {
        // Sine approximation using polynominals.
        // Has a low precision.
        public static float LowPrecisionSin(float x)
        {
            //always wrap input angle to -PI..PI
            if (x < -(float)System.Math.PI)
                x += (float)System.Math.PI * 2;
            else if (x > (float)System.Math.PI) x -= (float)System.Math.PI * 2;

            //compute sine
            if (x < 0) return 4 / (float)System.Math.PI * x + 4 / (float)(System.Math.PI * System.Math.PI) * x * x;

            return 4 / (float)System.Math.PI * x - 4 / (float)(System.Math.PI * System.Math.PI) * x * x;
        }

        // Sine approximation using polynominals.
        // Has a high precision.
        public static float HighPrecisionSin(float x)
        {
            //always wrap input angle to -PI..PI
            if (x < -(float)System.Math.PI)
                x += (float)System.Math.PI * 2;
            else if (x > (float)System.Math.PI) x -= (float)System.Math.PI * 2;

            //compute sine
            if (x < 0)
            {
                var sin = 4 / (float)System.Math.PI * x + 4 / (float)(System.Math.PI * System.Math.PI) * x * x;
                if (sin < 0) return .225f * (sin * -sin - sin) + sin;

                return .225f * (sin * sin - sin) + sin;
            }
            else
            {
                var sin = 4 / (float)System.Math.PI * x - 4 / (float)(System.Math.PI * System.Math.PI) * x * x;
                if (sin < 0) return .225f * (sin * -sin - sin) + sin;

                return .225f * (sin * sin - sin) + sin;
            }
        }

        // Cosine approximation using polynominals.
        // Has a low precision.
        public static float LowPrecisionCos(float x)
        {
            //always wrap input angle to -PI..PI
            if (x < -(float)System.Math.PI)
                x += (float)System.Math.PI * 2;
            else if (x > (float)System.Math.PI) x -= (float)System.Math.PI * 2;

            //compute cosine: sin(x + PI/2) = cos(x)
            x += 1.57079632f;
            if (x > (float)System.Math.PI) x -= (float)System.Math.PI * 2;

            if (x < 0) return 4 / (float)System.Math.PI * x + 4 / (float)(System.Math.PI * System.Math.PI) * x * x;

            return 4 / (float)System.Math.PI * x - 4 / (float)(System.Math.PI * System.Math.PI) * x * x;
        }

        // Cosine approximation using polynominals.
        // Has a high precision.
        public static float HighPrecisionCos(float x)
        {
            //always wrap input angle to -PI..PI
            if (x < -(float)System.Math.PI)
                x += (float)System.Math.PI * 2;
            else if (x > (float)System.Math.PI) x -= (float)System.Math.PI * 2;

            //compute cosine: sin(x + PI/2) = cos(x)
            x += 1.57079632f;
            if (x > (float)System.Math.PI) x -= (float)System.Math.PI * 2;

            if (x < 0)
            {
                var cos = 4 / (float)System.Math.PI * x + 4 / (float)(System.Math.PI * System.Math.PI) * x * x;
                if (cos < 0) return .225f * (cos * -cos - cos) + cos;

                return .225f * (cos * cos - cos) + cos;
            }
            else
            {
                var cos = 4 / (float)System.Math.PI * x - 4 / (float)(System.Math.PI * System.Math.PI) * x * x;
                if (cos < 0) return .225f * (cos * -cos - cos) + cos;

                return .225f * (cos * cos - cos) + cos;
            }
        }

        // Sine approximation using Taylor series.
        public static float TaylorSin(float x)
        {
            return x - x * x * x / 6 + x * x * x * x * x / 120 - x * x * x * x * x * x * x / 5040;
        }

        // Takes the inverse square root of x using Newton-Raphson approximation
        // 1 pass after clever initial guess using bitshifting.
        public static float InvSqrt(float x, int iterations = 0)
        {
            var convert = new Convert { x = x };
            var xhalf = 0.5f * x;
            convert.i = 0x5f3759df - (convert.i >> 1);
            x = convert.x;
            x = x * (1.5f - xhalf * x * x);
            for (var i = 0; i < iterations; i++) x = x * (1.5f - xhalf * x * x);
            return x;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Convert
        {
            [FieldOffset(0)] public float x;
            [FieldOffset(0)] public int i;
        }
    }
}