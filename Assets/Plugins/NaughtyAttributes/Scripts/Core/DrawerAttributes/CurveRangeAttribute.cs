using System;
using UnityEngine;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class CurveRangeAttribute : DrawerAttribute {
        public CurveRangeAttribute(Vector2 min, Vector2 max, EColor color = EColor.Clear) {
            Min = min;
            Max = max;
            Color = color;
        }

        public CurveRangeAttribute(EColor color)
            : this(Vector2.zero, Vector2.one, color) { }

        public CurveRangeAttribute(float minX, float minY, float maxX, float maxY, EColor color = EColor.Clear)
            : this(new Vector2(minX, minY), new Vector2(maxX, maxY), color) { }

        public Vector2 Min { get; }
        public Vector2 Max { get; }
        public EColor Color { get; }
    }
}