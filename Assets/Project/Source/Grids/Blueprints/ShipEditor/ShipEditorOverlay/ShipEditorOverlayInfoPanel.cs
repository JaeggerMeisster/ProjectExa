﻿using Exa.UI.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Grids.Blueprints.Editor
{
    public class ShipEditorOverlayInfoPanel : MonoBehaviour
    {
        public Button clearButton;
        public Button saveButton;
        public Button exportButton;
        public UI.Controls.ExaInputField blueprintNameInput;
        public ErrorListController errorListController;
        public CanvasGroup saveButtonCanvasGroup;
    }
}