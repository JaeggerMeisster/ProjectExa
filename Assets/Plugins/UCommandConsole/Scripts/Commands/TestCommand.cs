using UCommandConsole.Attributes;
using UCommandConsole.TypeParsers;

namespace UCommandConsole {
    public class TestCommand : Command {
        [CommandArgument(CustomParser = typeof(StringLiteralParser))] public string Val { get; set; }

        public override string GetName() {
            return "test";
        }

        public override void Execute(Console host) {
            host.output.Print($"Val value: {Val}", OutputColor.accent);
        }
    }
}