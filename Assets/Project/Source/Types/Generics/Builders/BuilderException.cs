using System;

namespace Exa.Generics {
    public class BuilderException : Exception {
        public BuilderException(string message)
            : base(message) { }
    }
}