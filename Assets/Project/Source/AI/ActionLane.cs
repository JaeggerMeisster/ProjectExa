using System;

namespace Exa.AI {
    [Flags]
    public enum ActionLane : uint {
        None = 0,
        Movement = 1 << 0,
        AimTurrets = 1 << 1,
        Rotation = 1 << 2
    }
}