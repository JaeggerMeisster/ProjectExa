using System.Collections.Generic;
using UCommandConsole.Attributes;
using UCommandConsole.TypeParsers;

namespace UCommandConsole {
    [IgnoreHistory]
    public class HelpCommand : Command {
        [CommandArgument(ParameterType.Optional, CustomParser = typeof(StringLiteralParser))]
        public string CommandName { get; set; }

        public override void Execute(Console host) {
            if (CommandName == default) {
                HandleCommand(host);
            } else {
                HandleCommandName(host);
            }
        }

        public override string GetName() {
            return "help";
        }

        private void HandleCommand(Console host) {
            host.output.Print($"Availible commands: {host.Container.Count}", OutputColor.accent);

            foreach (var item in host.Container.commandContext) {
                var commandName = item.Key;
                var argumentFormat = item.Value.GetArgumentFormat();
                host.output.Print($"{commandName}: {argumentFormat}", 1, OutputColor.accent, true);
            }
        }

        private void HandleCommandName(Console host) {
            var context = host.Container.commandContext[CommandName];
            host.output.Print($"Command: {CommandName}", OutputColor.accent);
            host.output.Print($"arguments: {context.GetArgumentFormat()}", 1, OutputColor.accent, true);
            PrintParameters(host, context.properties, "properties");
        }

        private void PrintParameters(Console host, Dictionary<string, ICommandParameter> dictionary, string name) {
            var argumentCount = dictionary.Count;
            var argumentCountString = argumentCount > 0 ? argumentCount.ToString() : "none";
            host.output.Print($"{name}: {argumentCountString}", 1, OutputColor.accent, true);

            foreach (var item in dictionary) {
                PrintProperty(host, item.Key, item.Value);
            }
        }

        private void PrintProperty(Console host, string propName, ICommandParameter property) {
            var propHasCustomParser = property.Context.CustomParser != null;
            var parserContext = CurrentParser.context;

            var typeParser = propHasCustomParser
                ? parserContext.GetTypeParser(property.Context.CustomParser)
                : parserContext.GetDefaultTypeParser(property.Context.PropertyType);

            var message = $"{propName}: {property.Context.PropertyType.Name} (Format: {typeParser.GetFormatString()})";

            if (propHasCustomParser) {
                message = $"{message} (Custom parser: {property.Context.CustomParser.Name})";
            }

            host.output.Print(message, 2, OutputColor.accent, true);
        }
    }
}