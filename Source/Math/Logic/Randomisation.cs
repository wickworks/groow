using System;
using System.Collections.Generic;

namespace SpiralCircus.Math
{
    public static class Randomisation
    {
        [ThreadStatic] private static Random _rng;
        private static Random Rng => _rng ??= new Random();

        public static float RandomFloat(float lowerBound, float upperBound)
        {
            return lowerBound + RandomFloat() * (upperBound - lowerBound);
        }

        public static float RandomFloat()
        {
            return (float)Rng.NextDouble();
        }

        public static int RandomInt(int min, int max, bool includeMax = false)
        {
            return (int)RandomFloat(min, includeMax ? max + 1 : max);
        }

        public static float RandomWalk(float initial, float walkSpeed, float min = 0, float max = 1)
        {
            var next = initial + (RandomFloat() * 2 - 1) * walkSpeed;
            if (next < min) return min;
            return next > max ? max : next;
        }

        public static float RandomGaussian(float mean = 0, float sigma = 1)
        {
            var uniformA = Rng.NextDouble();
            var uniformB = Rng.NextDouble();
            var normal = System.Math.Sqrt(-2.0 * System.Math.Log(uniformA)) *
                         System.Math.Sin(2.0 * System.Math.PI * uniformB);
            var result = mean + sigma * normal;
            return (float)result;
        }

        public static int MeanNdx(int n, int x)
        {
            var accumulator = 0;
            for (var i = 0; i < n; i++) accumulator += RandomInt(0, x, true);
            return accumulator / n;
        }

        public static bool RandomBool(float trueChance = 0.5f)
        {
            return RandomFloat(0, 1) < Maths.Clamp(trueChance);
        }

        public static float RandomSign()
        {
            return RandomFloat(-1, 1) >= 0 ? 1 : -1;
        }

        public static T PickAtRandom<T>(params T[] choices)
        {
            return choices.SelectRandom();
        }

        public static float Randomise(float value, float amount)
        {
            var delta = value * amount * 0.5f;
            return value + RandomFloat(-delta, delta);
        }

        public static float RandomisePct(float value, float pct)
        {
            var delta = value * (value * pct) * 0.5f;
            return value + RandomFloat(-delta, delta);
        }

        public static T SelectRandom<T>(this IReadOnlyList<T> data)
        {
            return data[RandomInt(0, data.Count)];
        }
    }
}