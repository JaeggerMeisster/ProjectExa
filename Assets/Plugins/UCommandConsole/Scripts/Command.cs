using UCommandConsole.Models;

namespace UCommandConsole {
    public abstract class Command {
        public CommandContext Context { get; set; }
        public CommandParser CurrentParser { get; set; }

        public abstract string GetName();

        public abstract void Execute(Console host);

        public virtual void Parameterize() {
            CommandBuilder.Parameterize(this, CurrentParser);
        }

        public virtual void BuildParameters() {
            CommandBuilder.BuildParameterDictionary(this);
        }

        public virtual void Undo(Console host) {
            host.output.Print($"Undid command {GetName()} without applying any changes");
        }
    }
}