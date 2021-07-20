namespace UCommandConsole {
    /// <summary>
    ///     Supports stateless getter, setter and context for a command parameter
    /// </summary>
    public interface ICommandParameter {
        IParameterContext Context { get; }

        void SetValue(object target, object value);

        object GetValue(object target);
    }
}