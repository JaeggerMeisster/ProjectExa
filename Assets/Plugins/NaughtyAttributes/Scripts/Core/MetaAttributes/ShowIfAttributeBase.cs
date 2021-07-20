namespace NaughtyAttributes {
    public class ShowIfAttributeBase : MetaAttribute {
        public ShowIfAttributeBase(string condition) {
            ConditionOperator = EConditionOperator.And;

            Conditions = new string[1] {
                condition
            };
        }

        public ShowIfAttributeBase(EConditionOperator conditionOperator, params string[] conditions) {
            ConditionOperator = conditionOperator;
            Conditions = conditions;
        }

        public string[] Conditions { get; }
        public EConditionOperator ConditionOperator { get; }
        public bool Inverted { get; protected set; }
    }
}