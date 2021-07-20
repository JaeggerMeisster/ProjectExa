using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowNativePropertyAttribute : SpecialCaseDrawerAttribute { }
}