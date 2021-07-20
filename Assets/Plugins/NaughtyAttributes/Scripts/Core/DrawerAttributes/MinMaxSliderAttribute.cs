using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class MinMaxSliderAttribute : DrawerAttribute {
        public MinMaxSliderAttribute(float minValue, float maxValue) {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public float MinValue { get; }
        public float MaxValue { get; }
    }
}