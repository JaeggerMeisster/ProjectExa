using System;

namespace UCommandConsole.Exceptions {
    public class InputFormatException : Exception {
        public InputFormatException() { }

        public InputFormatException(string message)
            : base(message) { }

        public InputFormatException(string message, Exception innertException)
            : base(message, innertException) { }
    }
}