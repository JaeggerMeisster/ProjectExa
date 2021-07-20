using System;

namespace Exa.Debugging {
    [Flags]
    [Serializable]
    public enum DebugMode {
        /// <summary>
        ///     Describes general debugging
        /// </summary>
        Global = 1 << 0,

        /// <summary>
        ///     Describes general ship debugging, like viewing ship state as a tooltip when hovering around it
        /// </summary>
        Ships = 1 << 1,

        /// <summary>
        ///     Describes ship navigation visual debugging
        /// </summary>
        Navigation = 1 << 2,

        /// <summary>
        ///     Enables the debug dragger
        /// </summary>
        Dragging = 1 << 3,

        /// <summary>
        ///     Enables dumping exceptions to the console
        /// </summary>
        ConsoleDump = 1 << 4
    }
}