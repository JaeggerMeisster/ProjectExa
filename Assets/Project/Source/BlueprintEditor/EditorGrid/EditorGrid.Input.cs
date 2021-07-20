using System;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tweening;
using UnityEngine;

namespace Exa.ShipEditor {
    public partial class EditorGrid {
        [SerializeField] private MovementConfiguration movementCfg;
        private Vector2Int? mouseGridPos;

        /// <summary>
        ///     Current mouse position in the grid
        ///     <para>
        ///         A null value signifies a mouse outside of the grid
        ///     </para>
        /// </summary>
        private Vector2Int? MouseGridPos {
            get => mouseGridPos;
            set {
                if (mouseGridPos == value) {
                    return;
                }

                if (mouseGridPos != null) {
                    SetActiveBackground(mouseGridPos, false);
                }

                mouseGridPos = value;

                if (value != null) {
                    SetActiveBackground(value, true);
                }

                ghostLayer.MoveGhost(size, value);
            }
        }

        public void OnBlockSelected(BlockTemplate template) {
            ghostLayer.ImportTemplate(template);
            turretLayer.OverlayVisibility = template?.GetAnyPartialDataIsOf<ITurretValues>() ?? false;
        }

        public void OnRotateLeft() {
            ghostLayer.RotateGhosts(1);
        }

        public void OnRotateRight() {
            ghostLayer.RotateGhosts(-1);
        }

        public void OnLeftClickPressed() {
            if (MouseGridPos == null || !Interactable || MouseOverUI) {
                return;
            }

            ghostLayer.TryPlace();
        }

        public void OnRightClickPressed() {
            if (MouseGridPos == null || !Interactable) {
                return;
            }

            ghostLayer.TryDelete();
        }

        private void UpdatePosition() {
            // Move the grid to keyboard input
            // Remap zoom scale range to damp scale
            var remappedZoomScale = movementCfg.zoomCurve.Evaluate(S.Editor.EditorCameraTarget.ZoomScale);

            // Calculate movement offset
            playerPos -= MovementVector * (movementCfg.speed * Time.deltaTime * remappedZoomScale);

            // Clamp movement offset to prevent going out of bounds
            playerPos = Vector2.ClampMagnitude(playerPos, 15f);

            // Get position by adding the pivot to the offset
            positionTween.To(centerPos + playerPos, 0.3f);
        }

        private void OnEnterGrid(Vector2Int? gridPos) {
            MouseGridPos = gridPos;
        }

        private void OnExitGrid() {
            MouseGridPos = null;
        }

        private void SetActiveBackground(Vector2Int? gridPos, bool enter) {
            if (gridPos == null) {
                return;
            }

            backgroundLayer.SetGridBackgroundItemColor(gridPos, enter);
        }

        [Serializable]
        public struct MovementConfiguration {
            [Header("Base movement speed multiplier")]
            public float speed;

            [Header("Zoom to movement speed remapping multiplier")]
            public ExaEase zoomCurve;
        }
    }
}