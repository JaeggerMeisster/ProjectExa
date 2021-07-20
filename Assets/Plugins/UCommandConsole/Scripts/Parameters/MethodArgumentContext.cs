using System;

namespace UCommandConsole {
    public class MethodArgumentContext : IParameterContext {
        public Type CustomParser {
            get => null;
        }

        public Type PropertyType { get; set; }

        public bool Required {
            get => true;
        }

        public string Name { get; set; }
    }
}