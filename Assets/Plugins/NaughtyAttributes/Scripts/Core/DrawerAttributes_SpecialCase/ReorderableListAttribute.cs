using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class ReorderableListAttribute : SpecialCaseDrawerAttribute { }
}