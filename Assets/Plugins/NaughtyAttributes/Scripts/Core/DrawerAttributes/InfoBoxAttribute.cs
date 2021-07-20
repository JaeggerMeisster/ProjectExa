using System;

namespace NaughtyAttributes {
    public enum EInfoBoxType {
        Normal,
        Warning,
        Error
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class InfoBoxAttribute : DrawerAttribute {
        public InfoBoxAttribute(string text, EInfoBoxType type = EInfoBoxType.Normal) {
            Text = text;
            Type = type;
        }

        public string Text { get; }
        public EInfoBoxType Type { get; }
    }
}