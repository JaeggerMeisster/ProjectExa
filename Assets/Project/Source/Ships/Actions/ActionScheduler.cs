using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Ships {
    public class ActionScheduler {
        private readonly float generation;
        private readonly GridInstance gridInstance;
        private readonly List<ShipAction> shipActions = new List<ShipAction>();
        private readonly float storage;
        private float stored;

        public ActionScheduler(GridInstance gridInstance) {
            this.gridInstance = gridInstance;
            generation = 50f;
            stored = 500f;
            storage = 500f;
        }

        public void Add(ShipAction shipAction) {
            shipActions.Add(shipAction);
        }

        public void Remove(ShipAction shipAction) {
            shipActions.Remove(shipAction);
        }

        public void ExecuteActions(float deltaTime) {
            stored += generation * deltaTime;

            foreach (var actionCache in SortActions(deltaTime)) {
                // Calculate the energy coefficient the action can use
                var energyTaken = TryTake(actionCache.consumption);
                var energyCoefficient = Mathf.Clamp01(energyTaken / actionCache.consumption);

                // This is a result of an action that uses no power and has no storage
                if (float.IsNaN(energyCoefficient)) {
                    energyCoefficient = 0f;
                }

                actionCache.action.Update(energyCoefficient, deltaTime);
                stored -= energyTaken;
            }

            if (stored < 0) {
                Debug.Log("Current energy level below 0. This should never happen");
            }

            if (stored > storage) {
                stored = storage;
            }

            gridInstance.Overlay.SetEnergyFill(stored, storage);
        }

        /// <summary>
        ///     Tries to take a given amount of energy
        /// </summary>
        /// <param name="energy"></param>
        /// <returns>The amount of energy taken</returns>
        private float TryTake(float energy) {
            return stored - Mathf.Clamp(stored - energy, 0, float.MaxValue);
        }

        private IEnumerable<ActionCache> SortActions(float deltaTime) {
            return shipActions
                .Select(
                    action => new ActionCache {
                        consumption = action.CalculateConsumption(deltaTime),
                        action = action
                    }
                )
                .OrderBy(actionCache => actionCache.consumption);
        }

        private struct ActionCache {
            public float consumption;
            public ShipAction action;
        }
    }
}