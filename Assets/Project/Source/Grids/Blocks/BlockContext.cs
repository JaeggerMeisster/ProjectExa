using System;
using System.Collections.Generic;

namespace Exa.Grids.Blocks {
    /// <summary>
    ///     Enum used to identify to which group a block belongs
    /// </summary>
    [Flags]
    public enum BlockContext {
        None = 0,
        DefaultGroup = 1 << 0,
        UserGroup = 1 << 1,
        EnemyGroup = 1 << 2,
        Debris = 1 << 3
    }

    public static class BlockContextExtensions {
        public static IEnumerable<BlockContext> GetContexts() {
            yield return BlockContext.DefaultGroup;
            yield return BlockContext.EnemyGroup;
            yield return BlockContext.UserGroup;
        }
    }
}