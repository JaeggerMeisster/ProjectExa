using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Ships {
    public class ShipOverlayCircle : MonoBehaviour {
        [SerializeField] private CanvasGroup canvasGroup;
        private bool isHovered;
        private bool isSelected;

        public bool IsSelected {
            private get => isSelected;
            set {
                isSelected = value;
                CalculateState();
            }
        }

        public bool IsHovered {
            private get => isHovered;
            set {
                isHovered = value;
                CalculateState();
            }
        }

        private void Awake() {
            CalculateState();
        }

        private void CalculateState() {
            gameObject.SetActive(IsSelected || IsHovered);

            if (IsSelected) {
                canvasGroup.alpha = 0.5f;
            } else if (IsHovered) {
                canvasGroup.alpha = 0.2f;
            }
        }
    }
}