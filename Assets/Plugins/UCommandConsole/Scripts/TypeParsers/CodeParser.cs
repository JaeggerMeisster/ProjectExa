using System;
using UCommandConsole;

namespace Assets.Scripts.TypeParsers {
    public class CodeParser : CustomTypeParser<object> {
        public override string GetFormatString() {
            return "{[x]}";
        }

        public override object Parse(CommandParser tokenizer) {
            throw new NotImplementedException();
        }
    }
}