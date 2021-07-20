using System;

namespace UCommandConsole {
    public interface IParameterContext : IParameterInfo {
        Type PropertyType { get; }
        string Name { get; }
    }
}