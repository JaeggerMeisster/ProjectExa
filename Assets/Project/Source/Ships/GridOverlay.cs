using UnityEngine;

namespace Exa.Ships {
    public class GridOverlay : MonoBehaviour, IGridOverlay {
        [SerializeField] private RectTransform rectContainer;
        [SerializeField] private ShipOverlayHullBar overlayHullBar;
        [SerializeField] private ShipOverlayEnergyBar overlayEnergyBar;
        [SerializeField] private ShipOverlayCircle overlayCircle;
        private GridInstance gridInstance;

        public void Update() {
            transform.position = gridInstance.GetPosition();
        }

        public void SetEnergyFill(float current, float max) {
            overlayEnergyBar.SetFill(current / max);
        }

        public void SetHullFill(float current, float max) {
            overlayHullBar.SetFill(current / max);
        }

        public void SetGrid(GridInstance instance) {
            gridInstance = instance;
            var size = instance.Blueprint.Grid.MaxSize * 10;
            rectContainer.sizeDelta = new Vector2(size, size);
        }

        public void SetSelected(bool value) {
            overlayCircle.IsSelected = value;
        }

        public void SetHovered(bool value) {
            overlayCircle.IsHovered = value;
        }
    }
}