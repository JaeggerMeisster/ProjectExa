using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Types.Binding;
using Exa.Validation;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.UI {
    public class FleetBuilder : MonoBehaviour {
        [Header("References")]
        [SerializeField] private FleetView fleetView;
        [SerializeField] private FleetBlueprintViewBinder viewBinder;
        [SerializeField] private FleetBuilderBlueprintTypes blueprintTypes;

        [Header("Events")]
        public UnityEvent<ValidationResult> fleetValidation = new UnityEvent<ValidationResult>();

        private FleetValidator fleetValidator;

        public Fleet Fleet {
            get => fleetView.Fleet;
        }

        private void OnDisable() {
            fleetView.Clear(5);
        }

        public void Init(IObservableEnumerable<BlueprintContainer> source) {
            blueprintTypes.BuildList(viewBinder.CreateTab);
            viewBinder.Init(fleetView.Toggle, fleetView.Remove, source);

            // NOTE: placeholder value, should be updated later
            var unityCapacity = 5;
            fleetView.Create(unityCapacity, viewBinder.GetView);
            fleetValidator = new FleetValidator();

            Validate();
        }

        public void Validate() {
            var validationResult = fleetValidator.Validate(Fleet);
            fleetValidation.Invoke(validationResult);
        }
    }
}