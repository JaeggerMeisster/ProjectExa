using System.Linq;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        public static int GreatestCommonDivisor(int a, int b) {
            while (b != 0) {
                var i = a % b;
                a = b;
                b = i;
            }

            return a;
        }

        public static float Increment(float from, float to, float by) {
            if (from == to) {
                return from;
            }

            if (from < to) {
                var result = from + by;

                return result < to ? result : to;
            } else {
                var result = from - by;

                return result > to ? result : to;
            }
        }

        public static float Remap(this float value, MinMax<float> from, MinMax<float> to) {
            return value.Remap(from.min, from.max, to.min, to.max);
        }

        public static float Remap(
            this float value,
            float from1,
            float to1,
            float from2,
            float to2
        ) {
            value = Mathf.Clamp(value, from1, to1);

            return
                // Get base value
                (value - from1) /
                // Transform the value in range
                (to1 - from1) *
                (to2 - from2) +
                // Apply base
                from2;
        }

        /// <summary>
        ///     Normalizes a given float value between the float range
        /// </summary>
        /// <para>
        ///     Values over or under the min and max wrap to a value inside the range
        /// </para>
        public static float NormalizeWrap(float value, MinMax<float> minMax) {
            value %= minMax.max;

            if (value < minMax.min) {
                value += minMax.max;
            }

            return value;
        }

        public static float AbsMin(params float[] args) {
            return args
                .OrderBy(Mathf.Abs)
                .First();
        }

        public static float NormalizeAngle360(float angle) {
            return NormalizeWrap(angle, new MinMax<float>(0f, 360f));
        }

        public static float NormalizeAngle180(float angle) {
            return NormalizeWrap(angle, new MinMax<float>(-180f, 180f));
        }

        public static float WrapAngle(float angle) {
            // Wrap
            if (angle > 180f) {
                angle -= 360f;
            }

            return angle;
        }

        public static void WrapAngle(ref float angle) {
            angle = WrapAngle(angle);
        }

        public static int Round(this float value) {
            return Mathf.RoundToInt(value);
        }

        public static bool Between(this float value, float min, float max) {
            return value >= min && value <= max;
        }

        public static int NormalizeAngle04(int quarterAngle) {
            return (int) NormalizeWrap(quarterAngle, new MinMax<float>(0, 4));
        }

        public static int To1(this bool value) {
            return value ? 1 : -1;
        }
    }
}