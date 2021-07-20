using System;
using System.Collections.Generic;
using Exa.Grids.Blocks;

namespace Exa.Research {
    public class DynamicBlockComponentModifier : IBlockComponentModifier {
        private readonly Func<BlockTemplate, bool> affectsTemplate;
        private readonly ResearchStep step;

        public DynamicBlockComponentModifier(ResearchStep step, Func<BlockTemplate, bool> affectsTemplate) {
            this.step = step;
            this.affectsTemplate = affectsTemplate;
        }

        public bool AffectsTemplate(BlockTemplate blockTemplate) {
            return affectsTemplate?.Invoke(blockTemplate) ?? true;
        }

        public IEnumerable<ResearchStep> GetResearchSteps() {
            yield return step;
        }
    }
}