using Exa.Utils;

namespace Exa.UI.Tooltips {
    public abstract class TooltipView<T> : FloatingTooltip
        where T : ITooltipPresenter {
        protected Tooltip tooltip;

        public GroupView Root { get; set; }

        public void Show(T presenter) {
            gameObject.SetActive(true);
            Rebuild(presenter);
            UpdatePosition(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void Rebuild(T data) {
            itemsContainer.DestroyChildren();
            tooltip = data.GetTooltip();
            Root = S.UI.Tooltips.tooltipGenerator.CreateRootView(tooltip, itemsContainer);
            tooltip.ShouldRefresh = false;
        }
    }
}