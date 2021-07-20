using System;

namespace Exa.ShipEditor {
    [Flags]
    public enum BlockFlip {
        None = 0,
        FlipX = 1 << 0,
        FlipY = 2 << 0,
        Both = FlipX | FlipY
    }
}