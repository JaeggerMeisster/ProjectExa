using System.Collections.Generic;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    public interface IBlockComponentValues : IGridTotalsModifier {
        IEnumerable<ITooltipComponent> GetTooltipComponents();
    }
}