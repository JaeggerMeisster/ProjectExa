using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using UnityEngine;

namespace Exa.Grids {
    public interface IGridInstance {
        BlockGrid BlockGrid { get; }
        Rigidbody2D Rigidbody2D { get; }
        Transform Transform { get; }
        BlockContext BlockContext { get; }
        GridInstanceConfiguration Configuration { get; }
    }

    public static class IGridInstanceExtensions {
        public static void AddBlock(this IGridInstance instance, Block block, bool mockSetValues) {
            // Set the parent without triggering any calls to listeners
            // This is because the side effects caused by setting component values and parent of the block,
            // may use each others underlying values
            block.SetParentWithoutNotify(instance);
            block.transform.SetParent(instance.Transform);

            if (!mockSetValues) {
                S.Blocks.Values.SetValues(instance.BlockContext, block.aBpBlock.Template, block);
            }
            
            instance.BlockGrid.Add(block);
            block.NotifyAdded(mockSetValues);
        }

        public static void RemoveBlock(this IGridInstance instance, Block block) {
            block.NotifyRemoved();
            instance.BlockGrid.Remove(block);
            block.SetParentWithoutNotify(null);
        }
    }
}