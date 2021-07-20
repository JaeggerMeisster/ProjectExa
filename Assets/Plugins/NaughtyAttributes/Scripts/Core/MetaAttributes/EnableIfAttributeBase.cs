namespace NaughtyAttributes {
    public abstract class EnableIfAttributeBase : MetaAttribute {
        public EnableIfAttributeBase(string condition) {
            ConditionOperator = EConditionOperator.And;

            Conditions = new string[1] {
                condition
            };
        }

        public EnableIfAttributeBase(EConditionOperator conditionOperator, params string[] conditions) {
            ConditionOperator = conditionOperator;
            Conditions = conditions;
        }

        public string[] Conditions { get; }
        public EConditionOperator ConditionOperator { get; }
        public bool Inverted { get; protected set; }
    }
}