using System;
using System.Collections.Generic;
using UCommandConsole.Models;

namespace UCommandConsole {
    public class CommandContainer {
        private readonly Dictionary<string, Func<Command>> commandGetters;
        private readonly Stack<Command> commandHistory;
        private readonly Console host;

        public CommandContainer(Console host) {
            this.host = host;
            commandHistory = new Stack<Command>();
            commandGetters = new Dictionary<string, Func<Command>>(StringComparer.OrdinalIgnoreCase);
            commandContext = new Dictionary<string, CommandContext>(StringComparer.OrdinalIgnoreCase);

            AddCommands();
        }

        public int Count {
            get => commandGetters.Count;
        }

        public Dictionary<string, CommandContext> commandContext { get; }

        protected void AddCommands() {
            AddGetter(() => new UndoCommand());
            AddGetter(() => new HelpCommand());
        }

        public void AddGetter(Func<Command> commandGetter) {
            var command = commandGetter();
            var commandName = command.GetName();

            if (ContainsCommand(commandName)) {
                throw new Exception($"Attempted to add a command with duplicate name: {commandName}");
            }

            CommandBuilder.Build(command);

            commandGetters[commandName] = commandGetter;
            commandContext[commandName] = command.Context;
        }

        public void AddToHistory(Command command) {
            commandHistory.Push(command);
        }

        public Command Undo() {
            var command = commandHistory.Pop();
            command.Undo(host);

            return command;
        }

        public Command PopFromHistory() {
            return commandHistory.Pop();
        }

        public bool ContainsCommand(string name) {
            return commandGetters.ContainsKey(name);
        }

        public Command GetCommand(string name) {
            // Create new command
            var command = commandGetters[name]();

            // Assign the stateless context
            command.Context = commandContext[name];

            return command;
        }
    }
}