﻿using Exa.Grids.Blocks;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using Exa.Utils;
using System;
using UnityEngine;
using static Exa.Input.GameControls;

namespace Exa.Grids.Blueprints.Editor
{
    public delegate void BlockSelectedDelegate(BlockTemplate id);

    [RequireComponent(typeof(ShipEditorNavigateable))]
    public partial class ShipEditor : MonoBehaviour, IEditorActions, IUIGroup
    {
        [HideInInspector] public ObservableBlueprintCollection blueprintCollection;
        public EditorGrid editorGrid;
        public ShipEditorNavigateable navigateable;

        [SerializeField] private ShipEditorStopwatch stopwatch;
        [SerializeField] private GameObject editorGridBackground;
        [SerializeField] private float zoomSpeed;
        private float zoom;
        private GameControls gameControls;
        private ShipEditorOverlay shipEditorOverlay;

        private EventRef blueprintNameEditEventRef;
        private EventRef saveButtonOnClickEventRef;

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;

                editorGrid.ZoomScale = value / 5f;
            }
        }

        private void Awake()
        {
            shipEditorOverlay = Systems.MainUI.shipEditorOverlay;

            gameControls = new GameControls();
            gameControls.Editor.SetCallbacks(this);
            SetGridBackground();

            shipEditorOverlay.blueprintInfoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
            shipEditorOverlay.inventory.BlockSelected += editorGrid.OnBlockSelected;

            shipEditorOverlay.onPointerEnter.AddListener(() =>
            {
                BlockedByUI = true;
            });

            shipEditorOverlay.onPointerExit.AddListener(() =>
            {
                BlockedByUI = false;
            });

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(() =>
            {
                IsSaved = false;
                UpdateSaveButtonActive();
            });

            stopwatch.onTime.AddListener(ValidateGrid);
        }

        private void Update()
        {
            if (leftButtonPressed)
            {
                editorGrid.OnLeftClickPressed();
                return;
            }

            if (rightButtonPressed)
            {
                editorGrid.OnRightClickPressed();
                return;
            }
        }

        private void OnEnable()
        {
            Active = true;
            Zoom = 5f;
            Camera.main.orthographicSize = Zoom;
            gameControls.Enable();
        }

        private void OnDisable()
        {
            Active = false;
            gameControls.Disable();
        }

        public void Import(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            this.blueprintContainer = blueprintContainer;
            var newBlueprint = blueprintContainer.Data.Clone();

            editorGrid.Import(newBlueprint);
            shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.text = newBlueprint.name;

            SetCallbacks(blueprintContainer, saveCallback);
            ValidateName(blueprintContainer, newBlueprint.name);
            UpdateSaveButtonActive();

            IsSaved = true;
        }

        // TODO: Fix this horribleness
        public void SetCallbacks(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            // Set blueprint name input field callback
            var onValueChanged = shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.onValueChanged;
            onValueChanged.AddListenerOnce((value) =>
            {
                ValidateName(blueprintContainer, value);
                IsSaved = false;
                UpdateSaveButtonActive();
            }, ref blueprintNameEditEventRef);

            // Set save button callback
            var onClick = shipEditorOverlay.blueprintInfoPanel.saveButton.onClick;
            onClick.AddListenerOnce(() =>
            {
                ValidateAndSave(blueprintContainer, saveCallback);
            }, ref saveButtonOnClickEventRef);
        }

        public void ValidateGrid()
        {
            var args = new BlueprintGridValidationArgs
            {
                blueprintBlocks = editorGrid.blueprintLayer.ActiveBlueprint.Blocks
            };

            shipEditorOverlay.blueprintInfoPanel.errorListController.Validate(new BlueprintGridValidator(), args);
        }

        public void ValidateName(ObservableBlueprint blueprintContainer, string name)
        {
            var args = new BlueprintNameValidationArgs
            {
                collectionContext = blueprintCollection,
                requestedName = name,
                blueprintContainer = blueprintContainer
            };

            var result = shipEditorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(new BlueprintNameValidator(), args);

            NameIsValid = result.Valid;
        }

        public void ValidateAndSave(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            // Don't to save twice
            if (IsSaved) return;

            var args = new BlueprintNameValidationArgs
            {
                collectionContext = blueprintCollection,
                requestedName = shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.text,
                blueprintContainer = blueprintContainer
            };

            var result = shipEditorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(new BlueprintNameValidator(), args);

            if (result.Valid)
            {
                IsSaved = true;
                editorGrid.blueprintLayer.ActiveBlueprint.name = args.requestedName;

                // Set the value of the observable
                blueprintContainer.SetData(editorGrid.blueprintLayer.ActiveBlueprint, false);

                // Save the blueprint, generate the thumbnail
                saveCallback(blueprintContainer);

                // Notify after saving as observers require the thumbnail to be generated
                blueprintContainer.Notify();
            }
            else
            {
                Systems.MainUI.promptController.PromptOk(result[0].Message, this);
            }
        }

        public void ExportToClipboard()
        {
            var json = IOUtils.JsonSerializeWithSettings(editorGrid.blueprintLayer.ActiveBlueprint);
            GUIUtility.systemCopyBuffer = json;
        }

        private void SetGridBackground()
        {
            var screenHeightInUnits = Camera.main.orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
            editorGridBackground.transform.localScale = new Vector3(screenWidthInUnits, screenHeightInUnits);
        }
    }
}