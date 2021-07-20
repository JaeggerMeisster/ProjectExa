using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct PowerGeneratorData : IBlockComponentValues {
        public float powerGeneration;

        public void AddGridTotals(GridTotals totals) {
            totals.UnscaledPowerGeneration += powerGeneration;
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.UnscaledPowerGeneration -= powerGeneration;
        }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            return new ITooltipComponent[] {
                new LabeledValue<object>("Power generation", $"{powerGeneration}")
            };
        }
    }
}