using Exa.UI.Tooltips;
using Exa.Utils;

namespace Exa.Grids.Blocks {
    public class BlockTemplateTooltip : FloatingTooltip {
        public void Show(BlockContext blockContext, BlockTemplate blockTemplate) {
            gameObject.SetActive(true);
            var tooltip = S.Blocks.Values.GetTooltip(blockContext, blockTemplate);
            S.UI.Tooltips.tooltipGenerator.CreateRootView(tooltip, itemsContainer);
            UpdatePosition(true);
        }

        public void Hide() {
            itemsContainer.DestroyChildren();
            gameObject.SetActive(false);
        }
    }
}