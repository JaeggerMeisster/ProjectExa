using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class ShowIfAttribute : ShowIfAttributeBase {
        public ShowIfAttribute(string condition)
            : base(condition) {
            Inverted = false;
        }

        public ShowIfAttribute(EConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions) {
            Inverted = false;
        }
    }
}