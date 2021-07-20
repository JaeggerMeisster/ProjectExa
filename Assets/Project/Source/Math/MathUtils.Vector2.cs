using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        public static float GetAngle(this Vector2 vector) {
            var theta = Mathf.Atan2(vector.y, vector.x);

            return NormalizeAngle360(theta * Mathf.Rad2Deg);
        }

        public static Vector2 RandomVector2(float magnitude) {
            float RandomComponent() {
                return Random.Range(-magnitude, magnitude);
            }

            return new Vector2(RandomComponent(), RandomComponent());
        }

        public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max) {
            return new Vector2 {
                x = Mathf.Clamp(value.x, min.x, max.x),
                y = Mathf.Clamp(value.y, min.y, max.y)
            };
        }

        public static Vector2 GrowDirectionToMax(Vector2 direction, Vector2 max) {
            var minGrowth = AbsMin(max.x / direction.x, max.y / direction.y);

            return direction * minGrowth;
        }

        public static Vector2 Average(IEnumerable<Vector2> positions) {
            var count = positions.Count();
            var total = positions.Aggregate(Vector2.zero, (current, position) => current + position);

            return total / count;
        }

        public static Vector2 RandomFromAngledMagnitude(float distance) {
            return FromAngledMagnitude(distance, Random.Range(0f, 360f));
        }

        public static Vector2 FromAngledMagnitude(float magnitude, float angle) {
            var vector = Vector2.right;
            Rotate(ref vector, angle);

            return vector * magnitude;
        }

        public static Vector2 Rotate(this Vector2 vector, float angle) {
            var sin = Mathf.Sin(angle * Mathf.Deg2Rad);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);

            var tx = vector.x;
            var ty = vector.y;

            return new Vector2 {
                x = cos * tx - sin * ty,
                y = sin * tx + cos * ty
            };
        }

        public static void Rotate(ref Vector2 vector, float angle) {
            var sin = Mathf.Sin(angle * Mathf.Deg2Rad);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);

            var tx = vector.x;
            var ty = vector.y;

            vector.x = cos * tx - sin * ty;
            vector.y = sin * tx + cos * ty;
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDelta) {
            if (target == current) {
                return current;
            }

            return new Vector2 {
                x = Mathf.MoveTowards(current.x, target.x, maxDelta),
                y = Mathf.MoveTowards(current.y, target.y, maxDelta)
            };
        }

        public static void MoveTowards(ref Vector2 current, Vector2 target, float maxDelta) {
            if (target == current) {
                return;
            }

            current.x = Mathf.MoveTowards(current.x, target.x, maxDelta);
            current.y = Mathf.MoveTowards(current.y, target.y, maxDelta);
        }

        /// <summary>
        ///     Rotate a vector2 by the given count of quarter turns
        /// </summary>
        /// <param name="vector">Vector2 to be rotated</param>
        /// <param name="quarterTurns">Amount of 90 degree turns</param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vector, int quarterTurns) {
            // if the vector is rotated by 360 degrees we dont need to calculate anything
            if (quarterTurns % 4 == 0) {
                return vector;
            }

            var sin = Mathf.Sin(quarterTurns * 90 * Mathf.Deg2Rad);
            var cos = Mathf.Cos(quarterTurns * 90 * Mathf.Deg2Rad);

            var tx = vector.x;
            var ty = vector.y;

            vector.x = cos * tx - sin * ty;
            vector.y = sin * tx + cos * ty;

            return vector;
        }

        public static int GetRotation(this Vector2Int vector) {
            var angle = ((Vector2) vector).GetAngle();

            return Mathf.RoundToInt(angle / 90f);
        }

        /// <summary>
        ///     Rotate a vector2int by the given count of quarter turns
        /// </summary>
        /// <param name="vector">Vector2int to be rotated</param>
        /// <param name="quarterTurns">Amount of 90 degree turns</param>
        /// <returns></returns>
        public static Vector2Int Rotate(this Vector2Int vector, int quarterTurns) {
            // if the vector is rotated by 360 degrees we dont need to calculate anything
            if (quarterTurns % 4 == 0) {
                return vector;
            }

            var sin = Mathf.Sin(quarterTurns * 90 * Mathf.Deg2Rad);
            var cos = Mathf.Cos(quarterTurns * 90 * Mathf.Deg2Rad);

            var tx = vector.x;
            var ty = vector.y;

            vector.x = Mathf.RoundToInt(cos * tx - sin * ty);
            vector.y = Mathf.RoundToInt(sin * tx + cos * ty);

            return vector;
        }

        public static Vector2Int GetRatio(Vector2Int value) {
            var gdc = GreatestCommonDivisor(value.x, value.y);

            return new Vector2Int {
                x = value.x / gdc,
                y = value.y / gdc
            };
        }

        /// <summary>
        ///     Convert a vector2int to a vector3 with the optional z angle
        /// </summary>
        /// <param name="from"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2Int from, float z = 0f) {
            return new Vector3(from.x, from.y, z);
        }

        /// <summary>
        ///     Convert a vector2 to a vector3 with the optional z angle
        /// </summary>
        /// <param name="from"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2 from, float z = 0f) {
            return new Vector3(from.x, from.y, z);
        }

        public static Vector2Int ToVector2Int(this Vector2 from) {
            return new Vector2Int((int) from.x, (int) from.y);
        }

        public static Vector2 ToVector2(this Vector2Int from) {
            return new Vector2(from.x, from.y);
        }

        public static Vector2 SetX(this Vector2 vector, float value) {
            return new Vector2(value, vector.y);
        }

        public static Vector2 SetY(this Vector2 vector, float value) {
            return new Vector2(vector.x, value);
        }

        public static Vector3 WithZ(this Vector2 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }

        public static string ToShortString(this Vector2Int vector) {
            return $"{vector.x},{vector.y}";
        }

        public static bool GetPointIsInViewport(Vector2 viewportPoint) {
            return !(
                viewportPoint.x < 0f ||
                viewportPoint.x > 1f ||
                viewportPoint.y < 0f ||
                viewportPoint.y > 1f);
        }

        public static Vector2 ViewportPointToCentreOffset(Vector2 viewportPoint) {
            return viewportPoint * 2 - new Vector2(1f, 1f);
        }
    }
}