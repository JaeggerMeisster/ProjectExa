using DG.Tweening;
using UnityEngine;

namespace Exa.Math {
    public static partial class MathUtils {
        public static EaseFunction CubicBezier(float p0, float p1, float p2, float p3) {
            return (c, s, e, d) => {
                var t = c / d;
                var it = 1f - t;
                var r = Mathf.Pow(it, 3f) * p0 + 3f * Mathf.Pow(it, 2f) * t * p1 + 3f * it * Mathf.Pow(t, 2f) * p2 + Mathf.Pow(t, 3f) * p3;

                return s + e * r;
            };
        }
    }
}