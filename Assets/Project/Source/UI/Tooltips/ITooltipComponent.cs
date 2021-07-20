using UnityEngine;

namespace Exa.UI.Tooltips {
    public interface ITooltipComponent {
        TooltipComponentView InstantiateComponentView(Transform parent);
    }
}