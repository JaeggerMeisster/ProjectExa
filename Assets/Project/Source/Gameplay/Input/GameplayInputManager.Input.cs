using Exa.Math;
using Exa.Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Gameplay {
    public partial class GameplayInputManager {
        private SelectionBuilder selectionBuilder;
        private bool shiftIsPressed;

        public void OnLeftClick(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    OnStartSelectionArea();

                    break;

                case InputActionPhase.Canceled:
                    OnEndSelectionArea();

                    break;
            }
        }

        public void OnMovement(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Performed:
                    S.CameraController.EscapeTarget();
                    S.CameraController.UserTarget.movementDelta = context.ReadValue<Vector2>();

                    break;

                case InputActionPhase.Canceled:
                    S.CameraController.UserTarget.movementDelta = Vector2.zero;

                    break;

                default:
                    return;
            }
        }

        public void OnRightClick(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    if (CurrentSelection is FriendlyShipSelection selection) {
                        var point = S.Input.MouseWorldPoint;
                        selection.MoveLookAt(point);

                        return;
                    }

                    if (S.GodModeIsEnabled && GS.Raycaster.TryGetTarget<GridInstance>(out var ship)) {
                        var worldPos = ship.transform.position.ToVector2();
                        var direction = (worldPos - S.Input.MouseWorldPoint).normalized * ship.BlockGrid.GetTotals().Mass;
                        ship.Rigidbody2D.AddForce(direction, ForceMode2D.Force);
                    }

                    break;
            }
        }

        public void OnZoom(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Performed:
                    var yScroll = context.ReadValue<Vector2>().y;

                    if (yScroll == 0f) {
                        return;
                    }

                    var target = S.CameraController.GetTarget();
                    target.OnScroll(yScroll);

                    break;
            }
        }

        public void OnEscape(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    if (HasSelection && !IsSelectingArea) {
                        RemoveSelectionArea();

                        return;
                    }

                    GS.UI.gameplayLayer.NavigateTo(GS.UI.pauseMenu);

                    break;
            }
        }

        public void OnSelectGroup(InputAction.CallbackContext context) {
            if (shiftIsPressed) {
                return;
            }

            switch (context.phase) {
                case InputActionPhase.Performed:
                    var index = context.ReadValue<float>().Round();

                    CurrentSelection?.Clear();
                    CurrentSelection = GS.UI.gameplayLayer.selectionHotbar.Select(index);

                    break;
            }
        }

        public void OnSaveGroup(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Performed:
                    var index = context.ReadValue<float>().Round();
                    GS.UI.gameplayLayer.selectionHotbar.Save(CurrentSelection, index);

                    break;
            }
        }

        public void OnSaveGroupModifier(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    shiftIsPressed = true;

                    break;
                case InputActionPhase.Canceled:
                    shiftIsPressed = false;

                    break;
            }
        }

        private void OnStartSelectionArea() {
            var worldPoint = S.Input.MouseWorldPoint;
            selectionBuilder = new SelectionBuilder(worldPoint);
            GS.UI.gameplayLayer.selectionArea.Show(worldPoint);
        }

        private void OnUpdateSelectionArea() {
            var worldPoint = S.Input.MouseWorldPoint;
            GS.UI.gameplayLayer.selectionArea.SetEnd(worldPoint);
        }

        private void OnEndSelectionArea() {
            if (IsSelectingArea) {
                // Clear the current selection
                CurrentSelection?.Clear();

                // Update selection builder
                var worldPoint = S.Input.MouseWorldPoint;
                selectionBuilder.UpdateSelection(worldPoint);

                // Hide the selection area overlay
                GS.UI.gameplayLayer.selectionArea.Hide();

                // Get the selection and save it in the hotbar if possible
                var selection = selectionBuilder.Build();

                CurrentSelection = selection;
                selectionBuilder = null;
            }
        }

        private void RemoveSelectionArea() {
            // Deselect current selection
            var currentHotbarSelection = GS.UI.gameplayLayer.selectionHotbar.CurrentSelection;

            if (currentHotbarSelection != null) // Update the view
            {
                currentHotbarSelection.Selected = false;
            }

            // Clear the selection
            CurrentSelection.Clear();
            CurrentSelection = null;
        }

        private void OnSelectGroup(int index) { }
    }
}