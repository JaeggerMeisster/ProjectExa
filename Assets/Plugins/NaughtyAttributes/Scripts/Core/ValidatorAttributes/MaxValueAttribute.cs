using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class MaxValueAttribute : ValidatorAttribute {
        public MaxValueAttribute(float maxValue) {
            MaxValue = maxValue;
        }

        public MaxValueAttribute(int maxValue) {
            MaxValue = maxValue;
        }

        public float MaxValue { get; }
    }
}