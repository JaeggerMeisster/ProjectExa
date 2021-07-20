namespace Exa.Validation {
    /// <summary>
    ///     Supports a validation error
    /// </summary>
    public abstract class ValidationError {
        public ErrorType ErrorType { get; set; } = ErrorType.Error;
        public string Message { get; set; }

        public virtual string Id {
            get => GetType().Name;
        }
    }
}