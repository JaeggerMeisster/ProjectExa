using Exa.Ships;
using Exa.Validation;

namespace Exa.UI {
    public class FleetValidator : Validator<Fleet> {
        protected override void AddErrors(ValidationResult errors, Fleet args) {
            if (args.mothership == null) {
                errors.Throw("fleet-mothership-null-error", "Fleet must have a mothership");
            }

            if (args.units.Capacity > args.units.Count) {
                var message = "Fleet capacity not fully utilized";
                errors.Throw("fleet-units-capacity-suggestion", message, ErrorType.Suggestion);
            }
        }
    }
}