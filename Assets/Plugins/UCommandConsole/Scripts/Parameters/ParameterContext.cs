using System;

namespace UCommandConsole {
    internal class ParameterContext : IParameterContext {
        public ParameterContext(Type propertyType) {
            PropertyType = propertyType;
        }

        public Type PropertyType { get; }
        public Type CustomParser { get; set; }
        public bool Required { get; set; }
        public string Name { get; set; }
    }
}