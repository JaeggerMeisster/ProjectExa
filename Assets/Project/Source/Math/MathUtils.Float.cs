using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        public static Vector2 ToVector2(this float value) {
            return new Vector2(value, value);
        }
        
        public static string ToPercentageString(this float value) {
            return $"{value * 100f}%";
        }
    }
}