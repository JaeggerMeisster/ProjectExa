using System.Linq;
using Exa.Grids.Blueprints;
using Exa.Validation;

namespace Exa.ShipEditor {
    public class BlueprintNameValidator : Validator<BlueprintNameValidationArgs> {
        protected override void AddErrors(ValidationResult errors, BlueprintNameValidationArgs args) {
            // Check if there is any blueprint in the collection that contains the same name as the requested name
            errors.Assert<BlueprintNameDuplicateError>(
                "Blueprint name is already used",
                !args.collectionContext.Any(
                    blueprintContainer =>
                        blueprintContainer.Data.name == args.requestedName &&
                        blueprintContainer != args.blueprintContainer
                )
            );

            // Check if the name isn't empty
            errors.Assert<BlueprintNameEmptyError>(
                "Blueprint name is empty",
                args.requestedName != ""
            );

            // Check if the requested name is the default name
            errors.Assert<BlueprintNameDefaultError>(
                "Blueprint name cannot be default",
                args.requestedName != Blueprint.DEFAULT_BLUEPRINT_NAME
            );
        }
    }
}