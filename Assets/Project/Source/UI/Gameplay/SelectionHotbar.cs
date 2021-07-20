using System.Collections.Generic;
using Exa.Gameplay;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Gameplay {
    public class SelectionHotbar : MonoBehaviour {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject selectionHotbarItemPrefab;
        private Dictionary<int, SelectionHotbarItem> itemDict;
        private int selectedIndex;

        public SelectionHotbarItem CurrentSelection {
            get => itemDict.ContainsKey(selectedIndex)
                ? itemDict[selectedIndex]
                : null;
        }

        public bool HasSelected {
            get => IsInRange(selectedIndex);
        }

        private void Awake() {
            itemDict = new Dictionary<int, SelectionHotbarItem>();
            selectedIndex = -1;

            CreatePrefabs();
        }

        public ShipSelection Select(int index = -1) {
            if (IsInRange(selectedIndex)) {
                var item = itemDict[selectedIndex];
                item.Selected = false;
            }

            // If the selected index is the same as the current, treat it as it should be deselected
            if (selectedIndex == index) {
                selectedIndex = -1;

                return null;
            }

            selectedIndex = index;

            if (IsInRange(selectedIndex)) {
                var selection = itemDict[index];
                selection.Selected = true;

                return Clone(selection.ShipSelection);
            }

            return null;
        }

        public void Save(ShipSelection selection, int? pIndex = null) {
            var index = pIndex ?? selectedIndex;

            if (index >= 0 && index <= 9) {
                var item = itemDict[index];
                item.ShipSelection = Clone(selection);
            }
        }

        private void CreatePrefabs() {
            for (var i = 1; i < 10; i++) {
                CreatePrefab(i);
            }

            CreatePrefab(0);
        }

        private void CreatePrefab(int index) {
            var itemGO = Instantiate(selectionHotbarItemPrefab, container);
            var item = itemGO.GetComponent<SelectionHotbarItem>();
            item.Setup(index);
            itemDict[index] = item;
        }

        private ShipSelection Clone(ShipSelection selection) {
            return selection?.Clone();
        }

        private bool IsInRange(int index) {
            return index >= 0 && index <= 9;
        }
    }
}