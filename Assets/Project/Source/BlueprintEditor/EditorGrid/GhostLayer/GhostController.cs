using System;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor {
    [Flags]
    public enum GhostControllerState {
        Visible = 1 << 0,
        Active = 1 << 1,
        Overlapped = 1 << 2,
        Valid = Visible | Active
    }

    public class GhostController {
        private readonly BlockFlip flip;
        private GhostControllerState state;

        public GhostController(BlockGhost blockGhost, BlockFlip flip) {
            Ghost = blockGhost;
            this.flip = flip;
        }

        public BlockGhost Ghost { get; }
        public TurretOverlay Overlay { get; private set; }

        public ABpBlock BlueprintBlock {
            get => Ghost.Block;
        }

        public GhostControllerState State {
            get => state;
            set {
                state = value;
                Ghost.gameObject.SetActive(state == GhostControllerState.Valid);
                UpdateOverlay();
            }
        }

        public void SetActive(BlockFlip mask) {
            State = State.SetFlag(GhostControllerState.Active, mask.HasValue(flip));
        }

        public void SetFilterColor(bool active) {
            Ghost.SetFilterColor(active);

            if (Overlay != null) {
                Overlay.SetColor(Colors.GetActiveColor(active));
            }
        }

        public void ImportBlock(BlueprintBlock block) {
            Ghost.ImportBlock(
                new BlueprintBlock {
                    flippedX = flip.HasValue(BlockFlip.FlipX),
                    flippedY = flip.HasValue(BlockFlip.FlipY),
                    id = block.id,
                    Rotation = block.Rotation
                }
            );
        }

        public void Clear() {
            Ghost.Clear();
        }

        public void SetOverlay(TurretOverlay overlay) {
            if (Overlay != null) {
                Overlay.gameObject.Destroy();
            }

            Overlay = overlay;

            if (overlay != null) {
                overlay.SetVisibility(false);
            }

            UpdateOverlay();
        }

        public void Rotate(int value) {
            Ghost.Block.blueprintBlock.Rotation += value;
            Ghost.Presenter.Present(Ghost.Block);

            UpdateOverlay();
        }

        public void MoveGhost(Vector2Int gridSize, Vector2Int? originalPos) {
            if (originalPos != null) {
                var gridPos = GetMirroredGridPos(gridSize, originalPos.Value);
                Ghost.Block.GridAnchor = gridPos;
                Ghost.Presenter.Present(Ghost.Block);

                if (Overlay != null) {
                    Overlay.SetVisibility(true);
                }

                UpdateOverlay();
            } else {
                if (Overlay != null) {
                    Overlay.SetVisibility(false);
                }
            }
        }

        public Vector2Int GetMirroredGridPos(Vector2Int gridSize, Vector2Int originalPos) {
            return GridUtils.GetMirroredGridPos(gridSize, originalPos, flip);
        }

        private void UpdateOverlay() {
            if (Overlay != null && Ghost.Block != null) {
                Overlay.Presenter.Present(Ghost.Block);
                Overlay.SetVisibility(state.HasValue(GhostControllerState.Valid));
            }
        }
    }
}