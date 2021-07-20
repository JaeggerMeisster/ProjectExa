using System;
using UCommandConsole;
using Console = UCommandConsole.Console;

namespace Exa.Debugging.Commands {
    public class SpawnCommand : Command {
        public override string GetName() {
            return "spawn-Ship";
        }

        public override void Execute(Console host) {
            throw new NotImplementedException();
        }
    }
}