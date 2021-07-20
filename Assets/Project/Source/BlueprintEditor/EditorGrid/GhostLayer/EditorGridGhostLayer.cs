using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor {
    public class EditorGridGhostLayer : MonoBehaviour {
        public GameObject ghostPrefab;
        public EditorGridBackgroundLayer backgroundLayer;
        public EditorGridBlueprintLayer blueprintLayer;
        public EditorGridTurretLayer turretLayer;

        private IEnumerable<GhostController> controllers;

        public bool PlacementIsAllowed { get; private set; }
        public BlockTemplate ImportedTemplate { get; private set; }

        private void Awake() {
            controllers = new List<GhostController> {
                InitController(BlockFlip.None),
                InitController(BlockFlip.FlipX),
                InitController(BlockFlip.FlipY),
                InitController(BlockFlip.Both)
            };
        }

        public void SetFlip(BlockFlip mask) {
            controllers.ForEach(controller => controller.SetActive(mask));
            UpdateGhostsOverlap();
        }

        public void RotateGhosts(int value) {
            if (!ImportedTemplate) {
                return;
            }

            controllers.ForEach(controller => controller.Rotate(value));
            UpdateGhosts();
        }

        public void MoveGhost(Vector2Int gridSize, Vector2Int? gridPos) {
            SetVisibility(gridPos != null);

            if (!ImportedTemplate) {
                return;
            }

            controllers.ForEach(controller => controller.MoveGhost(gridSize, gridPos));

            UpdateGhostsOverlap();
            UpdateGhosts();
        }

        public void TryPlace() {
            if (!PlacementIsAllowed) {
                return;
            }

            QueryActiveControllers()
                .ForEach(
                    controller => {
                        var block = controller.BlueprintBlock.Clone();
                        blueprintLayer.AddBlock(block);
                    }
                );

            UpdateGhosts();
        }

        public void TryDelete() {
            var blocks = blueprintLayer.ActiveBlueprint.Grid;

            QueryActiveControllers()
                .ForEach(
                    controller => {
                        var pos = controller.BlueprintBlock.gridAnchor;

                        if (blocks.TryGetMember(pos, out var member)) {
                            blueprintLayer.RemoveBlock(member);
                        }
                    }
                );

            UpdateGhosts();
        }

        public void ImportTemplate(BlockTemplate template) {
            ImportedTemplate = template;

            if (template == null) {
                controllers.ForEach(controller => controller.Clear());

                return;
            }

            var block = new BlueprintBlock {
                id = template.id,
                Rotation = 0
            };

            controllers.ForEach(
                controller => {
                    controller.ImportBlock(block);

                    var overlay = template.GetAnyPartialDataIsOf<ITurretValues>()
                        ? turretLayer.CreateGhostOverlay(controller.Ghost.Block)
                        : null;

                    controller.SetOverlay(overlay);
                }
            );
        }

        public void SetVisibility(bool value) {
            controllers.ForEach(controller => { controller.State = controller.State.SetFlag(GhostControllerState.Visible, value); });
        }

        private void UpdateGhostsOverlap() {
            void SetFlags(IEnumerable<GhostController> controllers, bool value) {
                controllers.ForEach(controller => { controller.State = controller.State.SetFlag(GhostControllerState.Overlapped, value); });
            }

            var activeControllers = QueryActiveControllers();
            SetFlags(controllers.Except(activeControllers), true);
            SetFlags(activeControllers, false);
        }

        private void UpdateGhosts() {
            // Block placement validation may rely on collider casts,
            // which by default only sync with the transform in fixedUpdate
            Physics2D.SyncTransforms();
            var ghostPlacementIsValid = GetBlockPlacementIsValid();
            PlacementIsAllowed = ghostPlacementIsValid;
            SetFilter(ghostPlacementIsValid);
        }

        private bool GetBlockPlacementIsValid() {
            var activeControllers = QueryActiveControllers();

            if (!activeControllers.Any()) {
                return false;
            }

            var occupiedBlocks = activeControllers
                .SelectMany(controller => controller.BlueprintBlock.GetTileClaims());

            var positionValid = occupiedBlocks.Distinct().Count() == occupiedBlocks.Count() && occupiedBlocks.All(pos => backgroundLayer.PosIsInGrid(pos)) && !blueprintLayer.ActiveBlueprint.Grid.HasOverlap(occupiedBlocks);

            if (!positionValid) {
                return false;
            }

            if (ImportedTemplate.GetAnyPartialDataIsOf<ITurretValues>()) {
                var ghostTurretClaims = activeControllers.SelectMany(controller => controller.Overlay.GetTurretClaims());
                var currentBlockClaims = turretLayer.TurretBlocks.SelectMany(block => block.GetTileClaims());

                return !ghostTurretClaims.Intersect(occupiedBlocks).Any() && !ghostTurretClaims.Intersect(currentBlockClaims).Any() && !turretLayer.TurretBlocks.GetTurretClaims().Intersect(occupiedBlocks).Any();
            }

            return true;
        }

        private GhostController InitController(BlockFlip flip) {
            var ghost = Instantiate(ghostPrefab, transform).GetComponent<BlockGhost>();
            ghost.gameObject.name = $"GhostController (Flip: {flip})";
            ghost.gameObject.SetActive(false);

            return new GhostController(ghost, flip);
        }

        private IEnumerable<GhostController> QueryActiveControllers() {
            return controllers.Where(controller => controller.State.HasValue(GhostControllerState.Active))
                .Where(controller => controller.BlueprintBlock != null)
                .GroupBy(controller => controller.BlueprintBlock.gridAnchor)
                .Select(group => group.First());
        }

        private void SetFilter(bool active) {
            controllers.ForEach(controller => controller.SetFilterColor(active));
        }
    }
}