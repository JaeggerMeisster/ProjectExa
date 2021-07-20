using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class MinValueAttribute : ValidatorAttribute {
        public MinValueAttribute(float minValue) {
            MinValue = minValue;
        }

        public MinValueAttribute(int minValue) {
            MinValue = minValue;
        }

        public float MinValue { get; }
    }
}