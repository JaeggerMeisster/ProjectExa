using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        /// <summary>
        ///     Convert a vector3int to a vector
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector3Int from) {
            return new Vector3(from.x, from.y, from.z);
        }

        public static Vector2 ToVector2(this Vector3Int from) {
            return new Vector2(from.x, from.y);
        }

        public static Vector2 ToVector2(this Vector3 from) {
            return new Vector2(from.x, from.y);
        }

        public static Vector3 SetZ(this Vector3 from, float value) {
            from.z = value;

            return from;
        }
    }
}