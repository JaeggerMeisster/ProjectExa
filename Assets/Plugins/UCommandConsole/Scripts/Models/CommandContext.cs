using System.Collections.Generic;
using System.Linq;

namespace UCommandConsole.Models {
    public class CommandContext {
        public Dictionary<string, ICommandParameter> arguments;
        public CommandInfo info;
        public Dictionary<string, ICommandParameter> properties;

        public string GetArgumentFormat() {
            var argumentNames = arguments
                .Select(
                    item => {
                        var propName = item.Key;
                        var propTypeName = item.Value.Context.PropertyType.Name;
                        var result = $"{propName}: {propTypeName}";

                        if (!item.Value.Context.Required) {
                            result = $"[optional] {result}";
                        }

                        return result;
                    }
                );

            return $"({string.Join(", ", argumentNames)})";
        }
    }
}