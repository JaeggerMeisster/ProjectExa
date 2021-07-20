using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Utils;

namespace Exa.Validation {
    /// <summary>
    ///     Used as an error container for multiple validators
    /// </summary>
    public class ValidationState {
        internal Action<ValidationError> defaultErrorCleaner;

        // Default implementation for the error handlers or cleaners
        internal Action<ValidationError> defaultErrorHandler;

        internal Dictionary<IValidator, IDictionary<string, Action<ValidationError>>> errorCleaners;
        // Keep track of error handlers in a nested dictionary
        // Access the error handler or cleaner by [validator][error]
        internal Dictionary<IValidator, IDictionary<string, Action<ValidationError>>> errorHandlers;

        // Keep track of errors thrown by a specific validator
        internal Dictionary<IValidator, IEnumerable<ValidationError>> lastControlErrors;

        public ValidationState() {
            errorHandlers = new Dictionary<IValidator, IDictionary<string, Action<ValidationError>>>();
            errorCleaners = new Dictionary<IValidator, IDictionary<string, Action<ValidationError>>>();
            lastControlErrors = new Dictionary<IValidator, IEnumerable<ValidationError>>();
        }

        public ValidationResult Control<TArgs>(IValidator<TArgs> validator, TArgs args) {
            // Get errors
            var errors = validator.Validate(args);

            ApplyResults(errors);

            return errors;
        }

        public void ApplyResults(ValidationResult errors) {
            var validator = errors.Validator;
            var currErrorIds = errors.Select(error => error.Id);

            // Error handlers
            foreach (var currentError in errors) // Check if there is a specific error handler for the error, otherwise use the default one
            {
                if (errorHandlers.NestedContainsKey(validator, currentError.Id)) {
                    errorHandlers[validator][currentError.Id](currentError);
                } else {
                    defaultErrorHandler(currentError);
                }
            }

            if (lastControlErrors.ContainsKey(validator)) // Get errors from the same validator that were handled last control, but are no longer present
            {
                foreach (var lastControlError in lastControlErrors[validator]) {
                    // If error wasn't thrown in the last control, it doesn't need to be cleaned up
                    if (currErrorIds.Contains(lastControlError.Id)) {
                        continue;
                    }

                    // Check if there is a specific error handler for the error, otherwise use the default one
                    if (errorHandlers.NestedContainsKey(validator, lastControlError.Id)) {
                        errorCleaners[validator][lastControlError.Id](lastControlError);
                    } else {
                        defaultErrorCleaner(lastControlError);
                    }
                }
            }

            // Keep track of the thrown errors for the current validator
            lastControlErrors[validator] = errors;
        }

        public IEnumerable<ValidationError> GetActiveErrors() {
            return lastControlErrors.SelectMany(kvp => kvp.Value);
        }
    }
}