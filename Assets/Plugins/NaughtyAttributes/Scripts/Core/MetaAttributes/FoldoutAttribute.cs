using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class FoldoutAttribute : MetaAttribute, IGroupAttribute {
        public FoldoutAttribute(string name) {
            Name = name;
        }

        public string Name { get; }
    }
}