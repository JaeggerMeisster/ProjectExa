using System;
using Exa.Camera;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.ShipEditor;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    public struct EditResult {
        public Blueprint blueprint;
        // NOTE: This should be recalculated if the research context changes
        public BlockCosts editCost;
    }

    public class MissionManager : MonoBehaviour {
        private EditResult? editResult;
        public bool IsEditing { get; private set; }
        public Mission Mission { get; private set; }
        public BlockCosts CurrentResources { get; internal set; }
        public PlayerStation Station { get; internal set; }
        public MissionStats Stats { get; internal set; }

        public void Update() {
            if (Mission != null) {
                Mission.Update();
            }

            GS.UI.gameplayLayer.currentResources.Refresh(CurrentResources);
        }

        public void LoadMission(Mission mission, MissionArgs args) {
            if (Mission != null) {
                throw new InvalidOperationException("Cannot load a mission without unloading previous one");
            }

            Mission = mission;
            Stats = new MissionStats();
            mission.Init(this, args);
        }

        public void AddResources(BlockCosts resources) {
            CurrentResources += resources;
        }

        public void UnloadMission() {
            Mission.Unload();
            Mission = null;
        }

        public void StartEditing() {
            IsEditing = true;
            editResult = null;

            var currentTarget = S.CameraController.CurrentTarget;
            GS.SpawnLayer.SetLayerActive(false);
            GS.UI.gameplayLayer.NavigateTo(S.Editor.navigateable);

            var settings = new BlueprintImportArgs(
                Station.Blueprint,
                blueprint => {
                    var newCosts = blueprint.Grid.GetTotals(BlockContext.UserGroup).Metadata.blockCosts;
                    var oldCosts = Station.GetBaseTotals().Metadata.blockCosts;

                    editResult = new EditResult {
                        blueprint = blueprint,
                        editCost = newCosts - oldCosts
                    };
                }
            ) {
                OnExit = () => StopEditing(currentTarget)
            };

            settings.AddValidator(
                new BlueprintCostValidator(
                    CurrentResources,
                    Station.GetBaseTotals().Metadata.blockCosts
                )
            );

            S.Editor.Import(settings);
        }

        private void StopEditing(ICameraTarget cameraTarget) {
            IsEditing = false;

            S.CameraController.SetTarget(cameraTarget);
            GS.SpawnLayer.SetLayerActive(true);

            // TODO: use edit result
            if (editResult != null) {
                Station.SetBlueprint(editResult.Value.blueprint);
                CurrentResources -= editResult.Value.editCost;
            }
        }
    }
}