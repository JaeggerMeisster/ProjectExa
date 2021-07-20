using Exa.Grids.Blocks;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Tooltips {
    public class TooltipFactory : MonoBehaviour {
        [SerializeField] private GameObject propertyPrefab;
        [SerializeField] private GameObject groupPrefab;
        [SerializeField] private GameObject titlePrefab;
        [SerializeField] private GameObject spacerPrefab;
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private GameObject metadataPrefab;

        private Tooltip currentTooltip;

        public GroupView CreateRootView(Tooltip tooltip, Transform parent) {
            currentTooltip = tooltip;
            var container = tooltip.GetRootData();
            var root = container.InstantiateComponentView(parent) as GroupView;
            currentTooltip = null;

            return root;
        }

        public PropertyView GenerateTooltipProperty(Transform parent, ITooltipComponent value) {
            var view = CreateComponent<PropertyView>(propertyPrefab, parent, value);

            if (currentTooltip.Font) {
                view.SetFont(currentTooltip.Font);
            }

            return view;
        }

        public GroupView GenerateTooltipGroup(Transform parent, TooltipGroup value) {
            return CreateComponent<GroupView>(groupPrefab, parent, value);
        }

        public TitleView GenerateTooltipTitle(Transform parent, TooltipTitle value) {
            var view = CreateComponent<TitleView>(titlePrefab, parent, value);

            if (currentTooltip.Font) {
                view.SetFont(currentTooltip.Font);
            }

            return view;
        }

        public SpacerView GenerateTooltipSpacer(Transform parent, TooltipSpacer value) {
            return CreateComponent<SpacerView>(spacerPrefab, parent, value);
        }

        public BlockCostsView CreateBlockCostsView(Transform parent, BlockCosts metadata) {
            return CreateComponent<BlockCostsView>(metadataPrefab, parent, metadata);
        }

        public TextView GenerateTooltipText(Transform parent, TooltipText value) {
            var view = CreateComponent<TextView>(textPrefab, parent, value);

            if (currentTooltip.Font) {
                view.SetFont(currentTooltip.Font);
            }

            return view;
        }

        private T CreateComponent<T>(GameObject prefab, Transform parent, ITooltipComponent value)
            where T : TooltipComponentView {
            var component = Instantiate(prefab, parent).GetComponent<T>();
            component.Refresh(value);

            return component;
        }
    }
}