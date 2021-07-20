using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Research;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission {
        [SerializeField] private BlockCosts initialResources;
        [SerializeField] private float resourceMultiplier;
        [SerializeField] private List<DynamicStrengthWave> waves;

        public override void Init(MissionManager manager, MissionArgs args) {
            base.Init(manager, args);

            var spawner = new Spawner();

            manager.CurrentResources = initialResources;
            manager.Station = spawner.SpawnPlayerStation();

            manager.Station.ControllerDestroyed += () => {
                GS.UI.gameOverMenu.scoreView.PresentStats(manager.Stats);
                GS.UI.gameplayLayer.NavigateTo(GS.UI.gameOverMenu);
            };

            manager.Station.Controller.PhysicalBehaviour.OnDamage += damage => { GS.UI.gameplayLayer.damageOverlay.NotifyDamage(); };

            foreach (var wave in waves) {
                wave.Setup(GaugeCurrentStrength);
            }
            
            var waveManager = GS.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);

            waveManager.WaveStarted += () => {
                GS.MissionManager.Station.Repair();
                GS.MissionManager.Station.ReconcileWithDiff();
            };

            waveManager.EnemySpawned += grid => { GS.UI.gameplayLayer.warningCircleOverlay.Add(grid); };

            waveManager.EnemyDestroyed += grid => {
                var costs = grid.GetBaseTotals().Metadata.blockCosts * resourceMultiplier;
                GS.UI.gameplayLayer.warningCircleOverlay.Remove(grid);
                GS.MissionManager.AddResources(costs);
                GS.MissionManager.Stats.CollectedResources += costs;
                GS.MissionManager.Stats.DestroyedShips += 1;
            };
        }

        protected override void AddResearchModifiers(ResearchBuilder builder) {
            builder
                .Context(BlockContext.UserGroup)
                .Add((ref AutocannonData curr) => curr.damage *= 0.5f)
                .ForTemplate<GaussCannonTemplate>()
                .Add((ref PhysicalData curr) => curr.hull *= 100f);
        }

        private float GaugeCurrentStrength() {
            // Calculate station strength
            var stationStrength = GS.MissionManager.Station.GetBaseTotals().Metadata.strength;
            
            // Somewhat punish hoarding resources by taking it into account
            var resourcesStrength = GS.MissionManager.CurrentResources.GaugePotentialStrength() * 0.5f;

            return stationStrength + resourcesStrength;
        }
    }
}