using System;
using UCommandConsole.Exceptions;

namespace UCommandConsole.Attributes {
    [AttributeUsage(AttributeTargets.Property)]
    public class CommandPropertyAttribute : Attribute, IParameterInfo {
        public CommandPropertyAttribute() {
            CustomParser = null;
        }

        public CommandPropertyAttribute(Type customParserType) {
            if (!typeof(TypeParser).IsAssignableFrom(customParserType)) {
                throw new TypeMismatchException(typeof(TypeParser), customParserType);
            }

            CustomParser = customParserType;
        }

        public Type CustomParser { get; }

        public bool Required {
            get => false;
        }
    }
}