using Exa.Utils;
using Exa.Validation;
using UnityEngine;

namespace Exa.ShipEditor {
    public partial class GridEditor {
        private BlockFlip flipState;
        private bool interactible = true;
        private bool leftButtonPressed;
        private bool mouseOverUI;
        private bool rightButtonPressed;

        public bool IsSaved { get; private set; }
        public ValidationResult NameValidationResult { get; private set; }
        public ValidationResult GridValidationResult { get; private set; }
        public GridEditorImportArgs ImportArgs { get; set; }

        private BlockFlip FlipState {
            get => flipState;
            set {
                flipState = value;

                overlay.verticalMirrorView.SetState(value.HasValue(BlockFlip.FlipY));
                editorGrid.ghostLayer.SetFlip(value);
            }
        }

        public bool MouseOverUI {
            get => mouseOverUI;
            set {
                mouseOverUI = value;

                editorGrid.MouseOverUI = value;
            }
        }

        private bool ShouldSave { get; set; }

        public bool Interactable {
            get => interactible;
            set {
                interactible = value;

                editorGrid.Interactable = value;
                navigateable.Interactable = value;

                if (value) {
                    gameControls.Enable();
                } else {
                    gameControls.Disable();
                    editorGrid.MovementVector = Vector2.zero;
                }
            }
        }

        private void ResetState() {
            IsSaved = true;
            NameValidationResult = null;
            GridValidationResult = null;
        }

        private void UpdateSaveButton() {
            ShouldSave = GetShouldSave(out var message);

            overlay.infoPanel.saveButtonTooltipTrigger.SetText(message);
            overlay.infoPanel.saveButtonCanvasGroup.interactable = ShouldSave;
            overlay.infoPanel.saveButtonCanvasGroup.alpha = ShouldSave ? 1f : 0.5f;
        }
    }
}