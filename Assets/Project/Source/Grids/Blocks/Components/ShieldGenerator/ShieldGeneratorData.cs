using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct ShieldGeneratorData : IBlockComponentValues {
        public float shieldRadius;
        public float recoverTime;
        public float health;

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            yield return new LabeledValue<object>("Shield radius", $"{shieldRadius}m");
            yield return new LabeledValue<object>("Recover time", $"{recoverTime}s");
            yield return new LabeledValue<object>("Health", $"{health}");
        }
    }
}