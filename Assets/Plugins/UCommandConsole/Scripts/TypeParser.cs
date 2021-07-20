using System;

namespace UCommandConsole {
    public abstract class CustomTypeParser<T> : TypeParser {
        public override Type Target {
            get => typeof(T);
        }

        public abstract T Parse(CommandParser tokenizer);

        public override object ParseAsObject(CommandParser tokenizer) {
            return Parse(tokenizer);
        }
    }

    public abstract class TypeParser {
        public abstract Type Target { get; }

        public abstract object ParseAsObject(CommandParser tokenizer);

        public abstract string GetFormatString();
    }
}