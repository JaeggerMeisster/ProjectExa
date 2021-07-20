namespace Exa.Grids.Blocks {
    public interface IGridTotalsModifier {
        void AddGridTotals(GridTotals totals);

        void RemoveGridTotals(GridTotals totals);
    }
}