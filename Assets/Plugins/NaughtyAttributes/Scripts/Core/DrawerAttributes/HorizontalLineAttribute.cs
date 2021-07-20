using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class HorizontalLineAttribute : DrawerAttribute {
        public const float DefaultHeight = 2.0f;
        public const EColor DefaultColor = EColor.Gray;

        public HorizontalLineAttribute(float height = DefaultHeight, EColor color = DefaultColor) {
            Height = height;
            Color = color;
        }

        public float Height { get; }
        public EColor Color { get; }
    }
}