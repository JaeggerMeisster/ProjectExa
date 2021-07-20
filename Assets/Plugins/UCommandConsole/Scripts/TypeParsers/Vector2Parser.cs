using UnityEngine;

namespace UCommandConsole.TypeParsers {
    public class Vector2Parser : CustomTypeParser<Vector2> {
        public override string GetFormatString() {
            return "[x],[y]";
        }

        public override Vector2 Parse(CommandParser tokenizer) {
            var float0 = tokenizer.AsValue<float>();
            tokenizer.Require(',', true);
            var float1 = tokenizer.AsValue<float>();

            return new Vector2 {
                x = float0,
                y = float1
            };
        }
    }
}