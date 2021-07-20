using System;

namespace Exa.Audio {
    [Flags]
    public enum AudioType {
        /// <summary>
        ///     Defines a soundtrack audio type
        /// </summary>
        ST = 1 << 0,

        /// <summary>
        ///     Defines a soundtrack audio type
        /// </summary>
        UI_SFX = 1 << 1
    }
}