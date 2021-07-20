namespace Exa.Validation {
    public interface IPlugableValidator : IValidator {
        public ValidationResult Result { get; }

        void Add();

        void Remove();
    }
}