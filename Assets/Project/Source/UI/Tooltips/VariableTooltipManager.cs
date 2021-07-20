using Exa.Grids.Blocks;
using Exa.Ships;
using UnityEngine;

namespace Exa.UI.Tooltips {
    public class VariableTooltipManager : MonoBehaviour {
        public TooltipFactory tooltipGenerator;
        public BlockTemplateTooltip blockTemplateTooltip;
        public BlueprintTypeTooltip blueprintTypeTooltip;
        public ShipDebugTooltip shipAIDebugTooltip;
        public TextTooltip textTooltip;
    }
}