using System;
using UCommandConsole.Exceptions;
using UCommandConsole.TypeParsers;
using UnityEngine;

namespace UCommandConsole {
    // TODO: Add named arguments
    // TODO: Add command output as command
    public class Console : MonoBehaviour {
        [Header("References")]
        public ConsoleOutput output;
        public ConsoleInput input;
        public CommandParserContext parserContext;

        public CommandContainer Container { get; set; }

        public void Initialize() {
            Container = new CommandContainer(this);
        }

        public void OnSubmit(string input) {
            output.BeginPrint($"Command output: {input}");

            void HandleException(Exception e, string message) {
                output.Print(message, OutputColor.error);
                output.DumpExceptionLogRecursively(e);
            }

            // Run command
            try {
                output.Print($"> {input}");
                Run(input);
            } catch (InputFormatException e) {
                HandleException(e, "Unhandled exception encoutered while parsing command string");
            } catch (Exception e) {
                HandleException(e, "Unhandled exception encoutered while executing command");
            }

            output.EndPrint();
        }

        public bool Run(string commandString) {
            var parser = new CommandParser(commandString, parserContext);
            var commandName = parser.AsValue(new StringLiteralParser());

            // Select command
            if (!Container.ContainsCommand(commandName)) {
                output.Print($"Command: \"{commandName}\" doesn't exist", OutputColor.warning);

                return false;
            }

            var command = Container.GetCommand(commandName);

            return Run(command, parser);
        }

        protected bool Run(Command command, CommandParser parser) {
            command.CurrentParser = parser;

            // Parameterize
            try {
                command.Parameterize();
            } catch (ParameterizationException e) {
                output.Print(e.Message, OutputColor.warning);

                return false;
            } catch (Exception e) {
                throw new InputFormatException($"Unhandled error while parsing parameters on command: \"{name}\"", e);
            }

            // Execute
            try {
                command.Execute(this);
            } catch (Exception e) {
                output.Print("Encountered an exception while executing command");
                output.DumpExceptionLogRecursively(e);

                return false;
            }

            // Add command history to queue
            if (!command.Context.info.ignoresHistory) {
                Container.AddToHistory(command);
            }

            command.CurrentParser = null;

            return true;
        }
    }
}