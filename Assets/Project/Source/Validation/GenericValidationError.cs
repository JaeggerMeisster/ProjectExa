namespace Exa.Validation {
    public class GenericValidationError : ValidationError {
        public GenericValidationError(string id, string message) {
            Id = id;
            Message = message;
        }

        public GenericValidationError(string id, string message, ErrorType errorType)
            : this(id, message) {
            ErrorType = errorType;
        }

        public override string Id { get; }
    }
}