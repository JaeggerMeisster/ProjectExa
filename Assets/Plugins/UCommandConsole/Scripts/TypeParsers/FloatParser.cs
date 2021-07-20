using System.Text;
using UCommandConsole.Exceptions;

namespace UCommandConsole.TypeParsers {
    public class FloatParser : CustomTypeParser<float> {
        public override string GetFormatString() {
            return "[x]";
        }

        public override float Parse(CommandParser parser) {
            var sb = new StringBuilder();

            if (char.IsDigit(parser.CurrentChar) || parser.CurrentChar == '.') {
                // Capture digits/decimal point
                var haveDecimalPoint = false;

                while (char.IsDigit(parser.CurrentChar) || !haveDecimalPoint && parser.CurrentChar == '.') {
                    sb.Append(parser.CurrentChar);
                    haveDecimalPoint = parser.CurrentChar == '.';
                    parser.NextChar();
                }

                var stringResult = sb.ToString();

                if (float.TryParse(stringResult, out var result)) {
                    return result;
                }

                throw new InputFormatException($"Cannot parse string: \"{stringResult}\" into a float");
            }

            throw new InputFormatException("Cannot parse an empty string");
        }
    }
}