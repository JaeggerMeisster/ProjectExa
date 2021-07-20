using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.IO.Json;
using Newtonsoft.Json;

namespace Exa.Grids.Blueprints {
    [JsonConverter(typeof(BlueprintBlocksConverter))]
    public class BlueprintGrid : Grid<ABpBlock>, ICloneable<BlueprintGrid> {
        public BlueprintGrid Clone() {
            var newBlocks = new BlueprintGrid();

            foreach (var block in GridMembers) {
                newBlocks.Add(block.Clone());
            }

            return newBlocks;
        }

        public GridTotals GetTotals(BlockContext context = BlockContext.DefaultGroup) {
            return S.Blocks.Totals.GetGridTotalsSafe(this, context);
        }
    }
}