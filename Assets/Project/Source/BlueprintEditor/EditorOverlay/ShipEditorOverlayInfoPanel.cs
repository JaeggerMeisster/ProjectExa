using Exa.UI.Controls;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.ShipEditor {
    public class ShipEditorOverlayInfoPanel : MonoBehaviour {
        public Button clearButton;
        public TextTooltipTrigger saveButtonTooltipTrigger;
        public Button saveButton;
        public Button exportButton;
        public InputFieldControl blueprintNameInput;
        public BlueprintErrorListController errorListController;
        public CanvasGroup saveButtonCanvasGroup;
        public GridTotalsView gridTotalsView;
        public Transform controlsContainer;

        public void SetNameEditingActive(bool active) {
            blueprintNameInput.gameObject.SetActive(active);
        }
    }
}