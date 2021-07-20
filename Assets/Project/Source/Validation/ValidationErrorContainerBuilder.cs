using System;
using System.Collections.Generic;
using Exa.Generics;

namespace Exa.Validation {
    public class ValidationErrorContainerBuilder : IBuilder<ValidationState> {
        private readonly ValidationState container;
        private bool onUnhandledErrorSet;

        public ValidationErrorContainerBuilder() {
            container = new ValidationState();
            onUnhandledErrorSet = false;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ValidationState Build() {
            if (!onUnhandledErrorSet) {
                throw new BuilderException("OnUnhandledError must be called");
            }

            return container;
        }

        /// <summary>
        ///     Get the error handler for a given validator
        /// </summary>
        /// <param name="validator">validator identifier</param>
        /// <returns></returns>
        private IDictionary<string, Action<ValidationError>> GetHandlerDictionary(IValidator validator) {
            if (!container.errorHandlers.ContainsKey(validator)) {
                container.errorHandlers[validator] = new Dictionary<string, Action<ValidationError>>();
            }

            return container.errorHandlers[validator];
        }

        /// <summary>
        ///     Get the error cleaner for a given validator
        /// </summary>
        /// <param name="validator">validator identifier</param>
        /// <returns></returns>
        private IDictionary<string, Action<ValidationError>> GetCleanerDictionary(IValidator validator) {
            if (!container.errorHandlers.ContainsKey(validator)) {
                container.errorHandlers[validator] = new Dictionary<string, Action<ValidationError>>();
            }

            return container.errorHandlers[validator];
        }

        /// <summary>
        ///     Add error handler and cleaner callback
        /// </summary>
        /// <typeparam name="TError">Type of error</typeparam>
        /// <param name="validator">Validator identifier</param>
        /// <param name="id">Error identifier</param>
        /// <param name="errorHandler">Error handler callback</param>
        /// <param name="errorCleaner">Error cleaner callback</param>
        /// <returns>Builder</returns>
        public ValidationErrorContainerBuilder OnError<TError>(
            IValidator validator,
            string id,
            Action<TError> errorHandler,
            Action<TError> errorCleaner
        )
            where TError : ValidationError {
            GetHandlerDictionary(validator)[id] = errorHandler as Action<ValidationError>;
            GetCleanerDictionary(validator)[id] = errorCleaner as Action<ValidationError>;

            return this;
        }

        /// <summary>
        ///     Add error handler and cleaner callback
        /// </summary>
        /// <param name="errorHandler">Default error handler callback</param>
        /// <param name="errorCleaner">Default error cleaner callback</param>
        /// <returns>Builder</returns>
        public ValidationErrorContainerBuilder OnUnhandledError(
            Action<ValidationError> fallbackHandler,
            Action<ValidationError> fallbackCleaner
        ) {
            container.defaultErrorHandler = fallbackHandler;
            container.defaultErrorCleaner = fallbackCleaner;
            onUnhandledErrorSet = true;

            return this;
        }
    }
}