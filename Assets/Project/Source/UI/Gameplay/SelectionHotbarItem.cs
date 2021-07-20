using Exa.Gameplay;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Gameplay {
    public class SelectionHotbarItem : MonoBehaviour {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text text;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color emptyColor;
        private bool selected;
        private ShipSelection shipSelection;

        public bool HasShipSelection {
            get => shipSelection != null && shipSelection.Count > 0;
        }

        public ShipSelection ShipSelection {
            get => shipSelection;
            set {
                shipSelection = value;
                UpdateView();
            }
        }

        public bool Selected {
            get => selected;
            set {
                selected = value;
                UpdateView();
            }
        }

        private void Awake() {
            selected = false;

            UpdateView();
        }

        public void Setup(int index) {
            text.text = index.ToString();
        }

        private void UpdateView() {
            canvasGroup.alpha = selected
                ? 1f
                : 0.6f;

            text.color = shipSelection != null
                ? selected
                    ? activeColor
                    : inactiveColor
                : emptyColor;
        }
    }
}