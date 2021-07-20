using System.Text;
using UCommandConsole.Exceptions;

namespace UCommandConsole.TypeParsers {
    public class StringParser : CustomTypeParser<string> {
        public override string GetFormatString() {
            return "\n[x]\n";
        }

        public override string Parse(CommandParser tokenizer) {
            var sb = new StringBuilder();

            if (tokenizer.Matches('"')) {
                tokenizer.NextChar();

                var hasClosedParenthesis = false;

                while (!tokenizer.IsEOF) {
                    if (tokenizer.Matches('"')) {
                        hasClosedParenthesis = true;
                        tokenizer.NextChar();

                        break;
                    }

                    sb.Append(tokenizer.CurrentChar);
                    tokenizer.NextChar();
                }

                if (!hasClosedParenthesis) {
                    throw new InputFormatException("String parenthesis not closed");
                }
            }

            return sb.ToString();
        }
    }
}