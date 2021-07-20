using UnityEngine;

namespace Exa.UI.Tooltips {
    public struct TooltipTitle : ITooltipComponent {
        private readonly bool animated;

        public string Text { get; }

        public TooltipTitle(string text, bool animated = true) {
            this.animated = animated;

            Text = text;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            var titleView = S.UI.Tooltips.tooltipGenerator.GenerateTooltipTitle(parent, this);

            if (animated) {
                titleView.AddAnimator();
            }

            return titleView;
        }
    }
}