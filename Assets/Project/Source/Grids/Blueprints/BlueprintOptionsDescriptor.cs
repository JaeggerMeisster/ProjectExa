using Exa.UI;
using Exa.UI.Controls;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    public class BlueprintOptionsDescriptor : ModelDescriptor<BlueprintOptions> {
        private string blueprintName;
        private BlueprintType blueprintType;

        public override BlueprintOptions FromDescriptor() {
            return new BlueprintOptions(blueprintName, blueprintType.typeGuid);
        }

        public override void GenerateView(Transform container) {
            var blueprintTypes = S.Blueprints.blueprintTypes;
            InputFieldControl.Create(container, "Name", SetBlueprintName);

            DropdownControl.Create(
                container,
                "Class",
                blueprintTypes,
                SetBlueprintType,
                OnOptionCreation
            );
        }

        private void SetBlueprintName(string blueprintName) {
            this.blueprintName = blueprintName;
        }

        private void SetBlueprintType(BlueprintType blueprintType) {
            this.blueprintType = blueprintType;
        }

        private void OnOptionCreation(BlueprintType value, DropdownTab tab) {
            var hoverable = tab.gameObject.AddComponent<Hoverable>();
            hoverable.onPointerEnter.AddListener(() => S.UI.Tooltips.blueprintTypeTooltip.Show(value));
            hoverable.onPointerExit.AddListener(() => S.UI.Tooltips.blueprintTypeTooltip.Hide());
        }
    }
}