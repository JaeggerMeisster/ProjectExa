﻿using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
{
    public partial class ShipEditor
    {
        private bool leftButtonPressed = false;
        private bool rightButtonPressed = false;
        private bool lockMovement = false;
        private bool mirrorEnabled = false;
        private bool interactible = true;
        private bool blockedByUI;
        private ObservableBlueprint blueprintContainer;

        public bool IsSaved { get; private set; }
        public bool NameIsValid { get; private set; }

        // Keep track of time elapsed from last blueprint edit

        public bool MirrorEnabled
        {
            get => mirrorEnabled;
            set
            {
                mirrorEnabled = value;
                editorGrid.MirrorEnabled = value;
                shipEditorOverlay.mirrorView.SetState(value);
            }
        }

        public bool Interactable
        {
            get => interactible;
            set
            {
                interactible = value;

                editorGrid.Interactable = value;
                navigateable.Interactable = value;

                if (value)
                {
                    gameControls.Enable();
                }
                else
                {
                    gameControls.Disable();
                    editorGrid.MovementVector = Vector2.zero;
                }
            }
        }

        public bool Active
        {
            set
            {
                MiscUtils.InvokeIfNotQuitting(() => shipEditorOverlay.gameObject.TrySetActive(value));
            }
        }

        public bool BlockedByUI
        {
            get => blockedByUI;
            set
            {
                blockedByUI = value;

                editorGrid.BlockedByUI = value;
            }
        }

        public void UpdateSaveButtonActive()
        {
            var valid = !IsSaved && NameIsValid;

            shipEditorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.interactable = valid;
            shipEditorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.alpha = valid ? 1f : 0.5f;
        }
    }
}