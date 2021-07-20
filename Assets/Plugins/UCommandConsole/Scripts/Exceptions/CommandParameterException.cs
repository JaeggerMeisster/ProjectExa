using System;

namespace UCommandConsole.Exceptions {
    public class CommandParameterException : ArgumentException {
        public CommandParameterException() { }

        public CommandParameterException(string message)
            : base(message) { }

        public CommandParameterException(string message, Exception innerException)
            : base(message, innerException) { }

        public CommandParameterException(string message, string paramName)
            : base(message, paramName) { }

        public CommandParameterException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException) { }
    }
}