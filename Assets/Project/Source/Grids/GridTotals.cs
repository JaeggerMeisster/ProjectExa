using System.Collections.Generic;
using Exa.Data;
using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.UI.Tooltips;

namespace Exa.Grids {
    public class GridTotals : ICloneable<GridTotals> {
        private readonly BlockContext context;

        public GridTotals(BlockContext context) {
            this.context = context;
        }

        public float Mass { get; set; }
        public float Hull { get; set; }
        public float UnscaledPowerGeneration { get; set; }
        public float UnscaledTurningPower { get; set; }
        public float PowerGenerationModifier { get; set; }
        public float TurningPowerModifier { get; set; }
        public BlockMetadata Metadata { get; set; }

        public GridTotals Clone() {
            return new GridTotals(context) {
                Mass = Mass,
                Hull = Hull,
                PowerGenerationModifier = PowerGenerationModifier,
                TurningPowerModifier = TurningPowerModifier
            };
        }

        public BlockContext GetInjectedContext() {
            return context;
        }

        public void Reset() {
            Mass = 0f;
            Hull = 0f;
            PowerGenerationModifier = 0f;
            TurningPowerModifier = 0f;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() {
            return new ITooltipComponent[] {
                new TooltipText($"Mass: {Mass}"),
                new TooltipText($"Hull: {Hull}")
            };
        }
    }
}