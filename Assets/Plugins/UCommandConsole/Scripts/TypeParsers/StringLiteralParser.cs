using System.Text;
using UCommandConsole.Exceptions;

namespace UCommandConsole.TypeParsers {
    public class StringLiteralParser : CustomTypeParser<string> {
        public override string GetFormatString() {
            return "[x]";
        }

        public override string Parse(CommandParser tokenizer) {
            var sb = new StringBuilder();

            if (char.IsLetter(tokenizer.CurrentChar)) {
                while (
                    char.IsLetter(tokenizer.CurrentChar) ||
                    char.IsDigit(tokenizer.CurrentChar) ||
                    tokenizer.CurrentChar == '-') {
                    sb.Append(tokenizer.CurrentChar);
                    tokenizer.NextChar();
                }

                if (tokenizer.CurrentChar != ' ' && !tokenizer.IsEOF) {
                    throw new InputFormatException($"Literal string property end must be followed by a space, instead received: \"{tokenizer.CurrentChar}\"");
                }
            } else {
                throw new InputFormatException($"Literal string property is expected to be followed by a letter, instead received: \"{tokenizer.CurrentChar}\"");
            }

            return sb.ToString();
        }
    }
}