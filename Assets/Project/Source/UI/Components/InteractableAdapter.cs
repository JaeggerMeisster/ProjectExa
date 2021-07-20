using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public class InteractableAdapter : MonoBehaviour, IUIGroup {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool setAlpha;
        [SerializeField] private bool interactable = true;

        private void Awake() {
            Interactable = interactable;
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public bool Interactable {
            get => interactable;
            set {
                if (interactable == value) {
                    return;
                }

                interactable = value;

                if (canvasGroup != null) {
                    canvasGroup.blocksRaycasts = value;
                    canvasGroup.interactable = value;

                    if (setAlpha) {
                        canvasGroup.alpha = value ? 1f : 0.5f;
                    }
                }
            }
        }
    }
}