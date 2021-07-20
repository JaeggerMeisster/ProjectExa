using System;

namespace Exa.Misc {
    /// <summary>
    ///     Base class for any exception that should be logged directly to the user
    /// </summary>
    public class UserException : Exception {
        public UserException() { }

        public UserException(string message)
            : base(message) { }

        public UserException(string message, bool fatal)
            : base(message) {
            Fatal = fatal;
        }

        public bool Fatal { get; }
    }
}