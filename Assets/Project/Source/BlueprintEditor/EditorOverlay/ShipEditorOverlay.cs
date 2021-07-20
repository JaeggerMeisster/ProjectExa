using Exa.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    public class ShipEditorOverlay : MonoBehaviour {
        [SerializeField] private Hoverable blueprintInventoryHoverable;
        [SerializeField] private Hoverable blueprintInfoPanelHoverable;
        [SerializeField] private Hoverable editorStatePanelHoverable;
        [SerializeField] private BudgetView budgetView;

        public InteractableAdapter editorOverlay;

        [FormerlySerializedAs("blueprintInfoPanel")]
        public ShipEditorOverlayInfoPanel infoPanel;

        public ShipEditorOverlayInventory inventory;
        [FormerlySerializedAs("mirrorView")]
        public MirrorView verticalMirrorView;

        public UnityEvent onPointerEnter;
        public UnityEvent onPointerExit;

        private void Awake() {
            AddListenerToGroupOnPointerEnter(onPointerEnter.Invoke);
            AddListenerToGroupOnPointerExit(onPointerExit.Invoke);
        }

        private void OnDisable() {
            budgetView.gameObject.SetActive(false);
        }

        public BudgetView EnableBudgetView() {
            budgetView.gameObject.SetActive(true);

            return budgetView;
        }

        private void AddListenerToGroupOnPointerEnter(UnityAction action) {
            blueprintInventoryHoverable.onPointerEnter.AddListener(action);
            blueprintInfoPanelHoverable.onPointerEnter.AddListener(action);
            editorStatePanelHoverable.onPointerEnter.AddListener(action);
        }

        private void AddListenerToGroupOnPointerExit(UnityAction action) {
            blueprintInventoryHoverable.onPointerExit.AddListener(action);
            blueprintInfoPanelHoverable.onPointerExit.AddListener(action);
            editorStatePanelHoverable.onPointerExit.AddListener(action);
        }
    }
}