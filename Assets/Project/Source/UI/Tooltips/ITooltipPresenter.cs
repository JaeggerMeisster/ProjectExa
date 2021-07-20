namespace Exa.UI.Tooltips {
    /// <summary>
    ///     Supports objects that may be presented with an in-game tooltip
    /// </summary>
    public interface ITooltipPresenter {
        /// <summary>
        ///     Get the collection of tooltip components
        /// </summary>
        /// <returns></returns>
        Tooltip GetTooltip();
    }
}