namespace Exa.Ships {
    public interface IGridOverlay {
        public void SetEnergyFill(float current, float max);

        public void SetHullFill(float current, float max);
    }
}