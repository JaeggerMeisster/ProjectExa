using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    [Serializable]
    public class DynamicStrengthWave : IWave {
        [SerializeField] private float baseStrength;
        [SerializeField] private List<StaticBlueprint> blueprints;
        private Func<float> getWaveStrength;

        public void Setup(Func<float> getWaveStrength) {
            this.getWaveStrength = getWaveStrength;
        }

        public IEnumerable<Blueprint> Blueprints {
            get => blueprints.Select(b => b.GetBlueprint());
        }

        // TODO: Implement some kind of mechanism that distributes blueprints based on the blueprint and wave strength
        public IEnumerable<Blueprint> GetSpawnAbleBlueprints() {
            var targetStrength = getWaveStrength() + baseStrength;
            
            var blueprintsByStrength = Blueprints.Select(
                    blueprint => (blueprint, blueprint.Grid.GetTotals(BlockContext.EnemyGroup).Metadata.strength)
                )
                .OrderBy(tuple => tuple.strength);
            
            var currentStrength = 0f;

            foreach (var (blueprint, strength) in blueprintsByStrength) {
                // Keep track of the amount of total strength used by this blueprint
                var totalStrengthUsedByBlueprint = 0;
                
                bool Ok() {
                    // Make sure the blueprint doesn't exceed the strength budget
                    if (currentStrength + strength > targetStrength) {
                        return false;
                    }

                    // Only OK if this blueprint hasn't used 50% of the 'strength budget'
                    return totalStrengthUsedByBlueprint < targetStrength / 2f;
                }
                
                while (Ok()) {
                    totalStrengthUsedByBlueprint += strength;
                    currentStrength += strength;

                    yield return blueprint;
                }
            }
        }

        public IEnumerable<EnemyGrid> Spawn(Spawner spawner) {
            return spawner.SpawnFormationAtRandomPosition(this, new VicFormation(), 100f);
        }
    }
}