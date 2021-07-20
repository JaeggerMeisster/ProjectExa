namespace Exa.Validation {
    /// <summary>
    ///     Represents the result for a single validation operation
    /// </summary>
    public struct ValidationOperationResult {
        /// <summary>
        ///     Error message if applicable
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Whether the operation was valid
        /// </summary>
        public bool Valid { get; set; }
    }
}