using UCommandConsole;

namespace Exa.Debugging.Commands {
    public class ClsCommand : Command {
        public override string GetName() {
            return "cls";
        }

        public override void Execute(Console host) {
            host.output.Clear();
        }
    }
}