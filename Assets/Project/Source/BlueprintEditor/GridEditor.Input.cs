using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.ShipEditor {
    public partial class GridEditor {
        public EditorCameraTarget EditorCameraTarget { get; private set; }

        public void OnMovement(InputAction.CallbackContext context) {
            if (S.Input.inputIsCaptured) {
                editorGrid.MovementVector = Vector2.zero;

                return;
            }

            switch (context.phase) {
                case InputActionPhase.Performed:
                    editorGrid.MovementVector = context.ReadValue<Vector2>();

                    break;

                case InputActionPhase.Canceled:
                    editorGrid.MovementVector = Vector2.zero;

                    break;

                default:
                    return;
            }
        }

        public void OnLeftClick(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    leftButtonPressed = true;

                    break;

                case InputActionPhase.Canceled:
                    leftButtonPressed = false;

                    break;

                default:
                    return;
            }
        }

        public void OnRightClick(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    rightButtonPressed = true;

                    break;

                case InputActionPhase.Canceled:
                    rightButtonPressed = false;

                    break;

                default:
                    return;
            }
        }

        public void OnRotateLeft(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    editorGrid.OnRotateLeft();

                    break;

                default:
                    return;
            }
        }

        public void OnRotateRight(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    editorGrid.OnRotateRight();

                    break;

                default:
                    return;
            }
        }

        public void OnToggleVerticalMirror(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    FlipState ^= BlockFlip.FlipY;

                    break;

                default:
                    return;
            }
        }

        public void OnZoom(InputAction.CallbackContext context) {
            if (MouseOverUI) {
                return;
            }

            if (context.phase == InputActionPhase.Performed) {
                var delta = context.ReadValue<Vector2>();
                EditorCameraTarget.OnScroll(delta.y);
            }
        }
    }
}