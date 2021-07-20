using System;
using System.Collections.Generic;
using Exa.Data;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct PowerConsumerData : IBlockComponentValues {
        public Scalar powerConsumption;

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            return new ITooltipComponent[] {
                new LabeledValue<object>("Power consumption", $"{powerConsumption} KW")
            };
        }
    }
}