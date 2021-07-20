using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UCommandConsole.Attributes;
using UCommandConsole.Exceptions;
using UCommandConsole.Models;

namespace UCommandConsole {
    internal static class CommandBuilder {
        internal static void Parameterize(Command command, CommandParser parser) {
            var position = 1;

            foreach (var item in command.Context.arguments) {
                var argument = item.Value;
                var argumentContext = argument.Context;

                parser.RemovePadding();

                if (parser.IsEOF) {
                    if (argumentContext.Required) {
                        var message = $"Required argument with name: \"{item.Key}\" is not set";

                        throw new ParameterizationException(message);
                    }

                    return;
                }

                try {
                    TypeParser typeParser = null;

                    if (argumentContext.CustomParser != null) {
                        typeParser = parser.context.GetTypeParser(argumentContext.CustomParser);
                    }

                    var parsedObj = parser.AsValue(argumentContext.PropertyType, typeParser);
                    argument.SetValue(command, parsedObj);
                } catch (InputFormatException e) {
                    var message = $"Error while parsing property {item.Key} (parameter position: {position}): {e.Message}";

                    throw new ParameterizationException(message);
                }

                position++;
            }
        }

        internal static void Build(Command command) {
            var commandType = command.GetType();

            var commandContext = new CommandContext {
                info = new CommandInfo {
                    ignoresHistory = commandType.IsDefined(typeof(IgnoreHistoryAttribute), false)
                },
                arguments = new Dictionary<string, ICommandParameter>(),
                properties = new Dictionary<string, ICommandParameter>()
            };

            command.Context = commandContext;
            command.BuildParameters();
        }

        internal static void BuildParameterDictionary(Command command) {
            var propertyType = command.GetType();

            var props = propertyType.GetProperties()
                .Where(prop => prop.CanWrite && prop.CanRead);

            ICommandParameter previousParameter = null;

            foreach (var prop in props) {
                HandleCommandProp(command, prop, out var parameter);

                if (parameter != null) {
                    if (previousParameter != null) {
                        var prevRequired = previousParameter.Context.Required;
                        var currRequired = parameter.Context.Required;

                        if (!prevRequired && currRequired) {
                            var prevName = previousParameter.Context.Name;
                            var message = $"Required parameter: \"{prop.Name}\" is preceded by a non required parameter: \"{prevName}\"";

                            throw new CommandParameterException(message, prop.Name);
                        }
                    }

                    previousParameter = parameter;
                }
            }
        }

        internal static void HandleCommandProp(Command command, PropertyInfo prop, out ICommandParameter parameter) {
            parameter = null;

            var definedArgument = prop.IsDefined(typeof(CommandArgumentAttribute), false);
            var definedProperty = prop.IsDefined(typeof(CommandPropertyAttribute), false);

            if (definedArgument && definedProperty) {
                var message = "Command parameter cannot be defined as argument and property simultaneously";

                throw new CommandParameterException(message, prop.Name);
            }

            if (definedArgument) {
                var attribute = prop.GetCustomAttribute<CommandArgumentAttribute>();
                parameter = GetCommandParameter(command, prop, attribute);
                command.Context.arguments[prop.Name] = parameter;
            }

            if (definedProperty) {
                var attribute = prop.GetCustomAttribute<CommandPropertyAttribute>();
                parameter = GetCommandParameter(command, prop, attribute);
                command.Context.properties[prop.Name] = parameter;
            }
        }

        internal static ICommandParameter GetCommandParameter(Command command, PropertyInfo prop, IParameterInfo parameterInfo) {
            var context = new ParameterContext(prop.PropertyType) {
                CustomParser = parameterInfo.CustomParser,
                Required = parameterInfo.Required
            };

            var parameter = new CommandParameter(context, prop.GetValue, prop.SetValue);

            return parameter;
        }
    }
}