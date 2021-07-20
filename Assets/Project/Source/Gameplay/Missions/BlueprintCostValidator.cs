using Exa.Grids.Blocks;
using Exa.ShipEditor;
using Exa.Validation;

namespace Exa.Gameplay.Missions {
    public class BlueprintCostValidator : PlugableValidator<BlueprintCostValidatorArgs> {
        private readonly BlockCosts maxAllowedCosts;
        private BudgetView view;

        public BlueprintCostValidator(BlockCosts budget, BlockCosts currentCosts) {
            maxAllowedCosts = budget + currentCosts;
        }

        protected override void AddErrors(ValidationResult errors, BlueprintCostValidatorArgs args) {
            if (args.currentCosts.creditCost > maxAllowedCosts.creditCost ||
                args.currentCosts.metalsCost > maxAllowedCosts.metalsCost) {
                errors.Throw<BlueprintCostError>("Costs exceed budget");
            }
        }

        private void BlueprintChangedHandler() {
            var args = new BlueprintCostValidatorArgs {
                currentCosts = S.Editor.ActiveBlueprintTotals.Metadata.blockCosts
            };

            view.SetBudget(maxAllowedCosts - args.currentCosts);
            Result = S.Editor.Validate(this, args);
        }

        public override void Add() {
            view = S.UI.EditorOverlay.EnableBudgetView();
            S.Editor.BlueprintChangedEvent += BlueprintChangedHandler;
            BlueprintChangedHandler();
        }

        public override void Remove() {
            S.Editor.BlueprintChangedEvent -= BlueprintChangedHandler;
        }
    }

    public class BlueprintCostValidatorArgs {
        public BlockCosts currentCosts;
    }

    public class BlueprintCostError : ValidationError { }
}