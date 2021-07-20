namespace Exa.Validation {
    public abstract class PlugableValidator<T> : Validator<T>, IPlugableValidator {
        public ValidationResult Result { get; protected set; }

        public abstract void Add();

        public abstract void Remove();
    }
}