using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class LabelAttribute : MetaAttribute {
        public LabelAttribute(string label) {
            Label = label;
        }

        public string Label { get; }
    }
}