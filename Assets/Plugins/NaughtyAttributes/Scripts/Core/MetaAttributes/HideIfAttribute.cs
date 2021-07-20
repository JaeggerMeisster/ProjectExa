using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class HideIfAttribute : ShowIfAttributeBase {
        public HideIfAttribute(string condition)
            : base(condition) {
            Inverted = true;
        }

        public HideIfAttribute(EConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions) {
            Inverted = true;
        }
    }
}