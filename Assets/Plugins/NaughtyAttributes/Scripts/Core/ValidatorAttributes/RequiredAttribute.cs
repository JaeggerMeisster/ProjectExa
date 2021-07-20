using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredAttribute : ValidatorAttribute {
        public RequiredAttribute(string message = null) {
            Message = message;
        }

        public string Message { get; }
    }
}