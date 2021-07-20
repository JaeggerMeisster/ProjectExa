using System;

namespace UCommandConsole.Exceptions {
    public class TypeMismatchException : Exception {
        public Type type0;
        public Type type1;

        public TypeMismatchException(Type type0, Type type1)
            : base($"Type mismatch between {type0} and {type1}") {
            this.type0 = type0;
            this.type1 = type1;
        }

        public TypeMismatchException(string message, Type type0, Type type1)
            : base(message) {
            this.type0 = type0;
            this.type1 = type1;
        }
    }
}