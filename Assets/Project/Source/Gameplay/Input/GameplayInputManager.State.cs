namespace Exa.Gameplay {
    public partial class GameplayInputManager {
        private ShipSelection currentSelection;

        private bool HasSelection {
            get => currentSelection != null;
        }

        private bool IsSelectingArea {
            get => selectionBuilder != null;
        }

        public ShipSelection CurrentSelection {
            get => currentSelection;
            set {
                currentSelection = value;
                GS.UI.gameplayLayer.selectionOverlay.Reflect(value);

                if (value != null) {
                    S.CameraController.SetSelectionTarget(value);
                }
            }
        }
    }
}