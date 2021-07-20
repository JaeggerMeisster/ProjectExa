using System;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class ValidateInputAttribute : ValidatorAttribute {
        public ValidateInputAttribute(string callbackName, string message = null) {
            CallbackName = callbackName;
            Message = message;
        }

        public string CallbackName { get; }
        public string Message { get; }
    }
}