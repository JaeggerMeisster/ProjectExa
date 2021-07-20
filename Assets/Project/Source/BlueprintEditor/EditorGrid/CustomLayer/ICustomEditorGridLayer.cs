using Exa.Grids.Blueprints;

namespace Exa.ShipEditor {
    public interface ICustomEditorGridLayer {
        public void TryAddToGrid(ABpBlock block);

        public void TryRemoveFromGrid(ABpBlock block);
    }
}