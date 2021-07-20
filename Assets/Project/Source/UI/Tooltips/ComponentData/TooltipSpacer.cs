using UnityEngine;

namespace Exa.UI.Tooltips {
    public struct TooltipSpacer : ITooltipComponent {
        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return S.UI.Tooltips.tooltipGenerator.GenerateTooltipSpacer(parent, this);
        }
    }
}