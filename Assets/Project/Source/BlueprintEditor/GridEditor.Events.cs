using System;

namespace Exa.ShipEditor {
    public partial class GridEditor {
        public event Action BlueprintChangedEvent;

        public void OnBlueprintClear() {
            // Hide block ghost and ask user for blueprint clear confirmation
            S.UI.Prompts.PromptYesNo(
                "Are you sure you want to clear the blueprint?",
                this,
                yes => {
                    if (yes) {
                        IsSaved = false;
                        editorGrid.ClearBlueprint();
                    }
                }
            );
        }

        public void OnOverlayPointerEnter() {
            MouseOverUI = true;
        }

        public void OnOverlayPointerExit() {
            MouseOverUI = false;
        }

        public void OnBlueprintChanged() {
            BlueprintChangedEvent?.Invoke();
            IsSaved = false;
            UpdateSaveButton();
        }

        public void OnBlueprintNameInputChanged(string value) {
            IsSaved = false;
            ValidateName(value);
            UpdateSaveButton();
        }

        public void OnBlueprintGridValidationRequested() {
            ValidateGrid();
            UpdateSaveButton();
        }

        public void OnBlueprintSave() {
            // Don't to save twice
            if (IsSaved) {
                return;
            }

            // Make sure the grid is validated before saving
            ValidateGrid();
            UpdateSaveButton();

            if (!ShouldSave) {
                return;
            }

            IsSaved = true;
            UpdateSaveButton();

            ImportArgs.Save(ActiveBlueprint);
        }
    }
}