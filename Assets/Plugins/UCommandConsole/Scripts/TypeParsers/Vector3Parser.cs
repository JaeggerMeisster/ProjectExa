using UnityEngine;

namespace UCommandConsole.TypeParsers {
    public class Vector3Parser : CustomTypeParser<Vector3> {
        public override string GetFormatString() {
            return "[x],[y],[z]";
        }

        public override Vector3 Parse(CommandParser tokenizer) {
            var float0 = tokenizer.AsValue<float>();
            tokenizer.Require(',', true);
            var float1 = tokenizer.AsValue<float>();
            tokenizer.Require(',', true);
            var float2 = tokenizer.AsValue<float>();

            return new Vector3 {
                x = float0,
                y = float1,
                z = float2
            };
        }
    }
}