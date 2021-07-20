using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class BoxGroupAttribute : MetaAttribute, IGroupAttribute {
        public BoxGroupAttribute(string name = "") {
            Name = name;
        }

        public string Name { get; }
    }
}