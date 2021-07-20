using System;
using Exa.Grids.Blocks;
using Exa.Types.Generics;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    [Serializable]
    [CreateAssetMenu(fileName = "BlueprintType", menuName = "Grids/Blueprints/BlueprintType")]
    public class BlueprintType : ScriptableObject, ITooltipPresenter, ILabeledValue<BlueprintType> {
        public BlueprintTypeGuid typeGuid;
        public string displayName;
        public Vector2Int maxSize;
        public BlockCategory allowedBlockCategory;

        private Tooltip tooltipResult;

        public bool IsMothership {
            get => typeGuid == BlueprintTypeGuid.mothership;
        }

        private void OnEnable() {
            tooltipResult = new Tooltip(GetTooltipGroup);
        }

        public string Label {
            get => displayName;
        }

        public BlueprintType Value {
            get => this;
        }

        public Tooltip GetTooltip() {
            return tooltipResult;
        }

        private TooltipGroup GetTooltipGroup() {
            return new TooltipGroup(
                new ITooltipComponent[] {
                    new LabeledValue<object>("Max size", $"{maxSize.x}x{maxSize.y}")
                }
            );
        }
    }
}