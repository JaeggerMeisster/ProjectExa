using System.Collections.Generic;
using Exa.Camera;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using Exa.UI.Controls;
using Exa.Validation;
using UnityEngine;
using static Exa.Input.GameControls;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    [RequireComponent(typeof(GridEditorNavigateable))]
    public partial class GridEditor : MonoBehaviour, IEditorActions, IUIGroup {
        [SerializeField] private EditorGrid editorGrid;
        public GridEditorNavigateable navigateable;

        [SerializeField] private GridEditorStopwatch stopwatch;
        [SerializeField] private CameraTargetSettings cameraSettings;
        private GameControls gameControls;
        private ShipEditorOverlay overlay;

        public Blueprint ActiveBlueprint {
            get => editorGrid.blueprintLayer.ActiveBlueprint;
        }

        public GridTotals ActiveBlueprintTotals {
            get => ActiveBlueprint.Grid.GetTotals(ImportArgs.BlockContext);
        }

        private void Awake() {
            overlay = S.UI.EditorOverlay;
            EditorCameraTarget = new EditorCameraTarget(cameraSettings);

            gameControls = new GameControls();
            gameControls.Editor.SetCallbacks(this);

            overlay.infoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
            overlay.inventory.blockSelected.AddListener(editorGrid.OnBlockSelected);
            overlay.inventory.Init();
            overlay.onPointerEnter.AddListener(OnOverlayPointerEnter);
            overlay.onPointerExit.AddListener(OnOverlayPointerExit);
            overlay.infoPanel.blueprintNameInput.inputField.onValueChanged.AddListener(OnBlueprintNameInputChanged);
            overlay.infoPanel.saveButton.onClick.AddListener(OnBlueprintSave);

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(OnBlueprintChanged);

            stopwatch.onTime.AddListener(OnBlueprintGridValidationRequested);

        #if UNITY_EDITOR
            var button = ButtonControl.Create(overlay.infoPanel.controlsContainer, "Save as asset");

            button.OnClick.AddListener(
                () => {
                    var blueprint = editorGrid.blueprintLayer.ActiveBlueprint;
                    var staticBlueprint = ScriptableObject.CreateInstance<StaticBlueprint>();
                    staticBlueprint.Save(blueprint);
                }
            );

            button.LayoutElement.preferredHeight = 32;
        #endif
        }

        private void Update() {
            if (leftButtonPressed) {
                editorGrid.OnLeftClickPressed();

                return;
            }

            if (rightButtonPressed) {
                editorGrid.OnRightClickPressed();
            }
        }

        private void OnEnable() {
            overlay.gameObject.SetActive(true);
            gameControls.Enable();

            S.CameraController.SetTarget(EditorCameraTarget, true);
        }

        private void OnDisable() {
            overlay.gameObject.SetActive(false);
            gameControls.Disable();

            foreach (var validator in customValidators) {
                validator.Remove();
            }
        }

        public void Import(GridEditorImportArgs importArgs) {
            ImportArgs = importArgs;
            customValidators = new List<IPlugableValidator>();
            customValidators.AddRange(importArgs.Validators);

            ImportImpl(importArgs.GetBlueprint(), importArgs.ValidateName);
        }

        private void ImportImpl(Blueprint blueprint, bool enableNameChanging) {
            ResetState();

            editorGrid.turretLayer.Init();

            gridValidator = new BlueprintGridValidator();
            nameValidator = enableNameChanging ? new BlueprintNameValidator() : null;
            overlay.infoPanel.SetNameEditingActive(enableNameChanging);

            var newBlueprint = blueprint.Clone();
            editorGrid.Import(newBlueprint);

            if (enableNameChanging) {
                ValidateName(newBlueprint.name);
            }

            foreach (var validator in customValidators) {
                validator.Add();
            }

            overlay.infoPanel.blueprintNameInput.SetValue(newBlueprint.name, false);
            overlay.inventory.SetFilter(blueprint.BlueprintType.allowedBlockCategory);
            overlay.inventory.SetSelected(null);
            overlay.inventory.CloseTabs();

            FlipState = BlockFlip.None;
        }

        public void ExportToClipboard() {
            var json = IOUtils.JsonSerializeWithSettings(editorGrid.blueprintLayer.ActiveBlueprint);
            GUIUtility.systemCopyBuffer = json;
        }
    }
}