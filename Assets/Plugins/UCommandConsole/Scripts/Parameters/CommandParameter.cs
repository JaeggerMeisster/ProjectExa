using System;

namespace UCommandConsole {
    internal class CommandParameter : ICommandParameter {
        private readonly Func<object, object> getter;
        private readonly Action<object, object> setter;

        public CommandParameter(IParameterContext argumentContext, Func<object, object> getter, Action<object, object> setter) {
            Context = argumentContext;
            this.getter = getter;
            this.setter = setter;
        }

        public IParameterContext Context { get; }

        public void SetValue(object target, object value) {
            setter(target, value);
        }

        public object GetValue(object target) {
            return getter(target);
        }
    }
}