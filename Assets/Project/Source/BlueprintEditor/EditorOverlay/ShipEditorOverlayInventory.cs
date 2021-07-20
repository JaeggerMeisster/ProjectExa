using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Types.Binding;
using Exa.UI.Components;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    public class BlockSelectedEvent : UnityEvent<BlockTemplate> { }

    public class ShipEditorOverlayInventory : ViewBinder<BlockTemplateView, BlockTemplateContainer, BlockTemplate> {
        [SerializeField] private GameObject expandableItemPrefab;

        private readonly Dictionary<BlockCategory, ExpandableItem> tabs =
            new Dictionary<BlockCategory, ExpandableItem>();

        private BlockTemplateView activeView;
        public BlockSelectedEvent blockSelected = new BlockSelectedEvent();
        private BlockCategory filter = BlockCategory.All;

        public void Init() {
            Source = S.Blocks.blockTemplates;
        }

        public void SelectFirst() {
            var selectedBlock = Source.First(value => filter.HasValue(value.Data.category));
            SetSelected(selectedBlock);
            tabs[selectedBlock.Data.category].Expanded = true;
        }

        public void SetFilter(BlockCategory blockCategoryFilter) {
            filter = blockCategoryFilter;

            foreach (var (category, item) in tabs.Unpack()) {
                item.gameObject.SetActive(blockCategoryFilter.HasValue(category));
            }
        }

        public override void OnAdd(BlockTemplateContainer value) {
            var category = value.Data.category;
            var categoryString = category.ToFriendlyString();

            if (!tabs.ContainsKey(category)) {
                var newExpandableItemObject = Instantiate(expandableItemPrefab, viewContainer);
                var newExpandableItem = newExpandableItemObject.GetComponent<ExpandableItem>();
                newExpandableItem.HeaderText = categoryString;
                tabs[category] = newExpandableItem;
                newExpandableItem.gameObject.SetActive(filter.HasValue(category));
            }

            var categoryItem = tabs[category];
            var view = base.CreateView(value, categoryItem.Content);

            view.button.onClick.AddListener(() => SetSelected(value));
        }

        public void SetSelected(BlockTemplateContainer value) {
            if (activeView != null) {
                activeView.Selected = false;
            }

            if (value == null) {
                activeView = null;
                blockSelected?.Invoke(null);

                return;
            }

            activeView = GetView(value);
            activeView.Selected = true;

            blockSelected?.Invoke(value.Data);
        }

        public void CloseTabs() {
            tabs.Values.ForEach(expandableItem => expandableItem.Expanded = false);
        }
    }
}