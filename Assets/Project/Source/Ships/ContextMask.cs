using Exa.Grids.Blocks;
using Exa.Utils;
using UnityEngine;

namespace Exa.Ships {
    public readonly struct ContextMask {
        public LayerMask LayerMask { get; }
        public BlockContext BlockContextMask { get; }

        internal ContextMask(BlockContext blockContextMask, LayerMask layerMask) {
            LayerMask = layerMask;
            BlockContextMask = blockContextMask;
        }

        public bool HasValue(BlockContext context) {
            return BlockContextMask.HasValue(context);
        }

        public static implicit operator LayerMask(ContextMask mask) {
            return mask.LayerMask;
        }
    }

    public static class ContextMaskUtils {
        public static ContextMask GetShipMask(this BlockContext context) {
            return new ContextMask(context, LayerMask.GetMask("unit"));
        }

        public static ContextMask GetBlockMask(this BlockContext context) {
            return new ContextMask(context, LayerMask.GetMask("blocks"));
        }
    }
}