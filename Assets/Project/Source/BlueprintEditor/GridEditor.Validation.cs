using System.Collections.Generic;
using Exa.Validation;
using UnityEngine;

namespace Exa.ShipEditor {
    public partial class GridEditor {
        private List<IPlugableValidator> customValidators;
        private BlueprintGridValidator gridValidator;
        private BlueprintNameValidator nameValidator;

        public void ValidateGrid() {
            var args = new BlueprintGridValidationArgs {
                blueprintGrid = editorGrid.blueprintLayer.ActiveBlueprint.Grid
            };

            GridValidationResult = Validate(gridValidator, args);
        }

        public void ValidateName(string name) {
            if (ImportArgs is ContainerImportArgs containerImportArgs) {
                var args = new BlueprintNameValidationArgs {
                    collectionContext = S.Blueprints.userBlueprints,
                    requestedName = name,
                    blueprintContainer = containerImportArgs.Container
                };

                NameValidationResult = Validate(nameValidator, args);

                if (NameValidationResult) {
                    editorGrid.blueprintLayer.ActiveBlueprint.name = name;
                }
            } else {
                Debug.LogError("Attempted to validate name on unsupported import arguments");
            }
        }

        public ValidationResult Validate<T>(IValidator<T> validator, T args) {
            return overlay.infoPanel.errorListController.Validate(validator, args);
        }

        private bool GetShouldSave(out string message) {
            if (IsSaved) {
                message = "Blueprint is already saved";

                return false;
            }

            if (NameValidationResult != null && !NameValidationResult) {
                message = NameValidationResult.GetFirstBySeverity().Message;

                return false;
            }

            if (GridValidationResult != null && !GridValidationResult) {
                message = GridValidationResult.GetFirstBySeverity().Message;

                return false;
            }

            foreach (var validator in customValidators) {
                if (validator.Result == null || validator.Result) {
                    continue;
                }

                message = validator.Result.GetFirstBySeverity().Message;

                return false;
            }

            message = null;

            return true;
        }
    }
}