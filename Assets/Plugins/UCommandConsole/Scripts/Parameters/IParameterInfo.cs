using System;

namespace UCommandConsole {
    public interface IParameterInfo {
        Type CustomParser { get; }
        bool Required { get; }
    }
}