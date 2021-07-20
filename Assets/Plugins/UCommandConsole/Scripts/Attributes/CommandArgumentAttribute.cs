using System;
using UCommandConsole.Exceptions;

namespace UCommandConsole.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandArgumentAttribute : Attribute, IParameterInfo {
        private Type customParser;

        public CommandArgumentAttribute(ParameterType parameterType = ParameterType.Required) {
            Required = parameterType == ParameterType.Required;
        }

        public Type CustomParser {
            set {
                if (!typeof(TypeParser).IsAssignableFrom(value)) {
                    throw new TypeMismatchException(typeof(TypeParser), value);
                }

                customParser = value;
            }
            get => customParser;
        }

        public bool Required { get; }
    }
}