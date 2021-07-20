using UnityEngine;

namespace Exa.Ships.Targeting {
    public readonly struct ShipTarget : IWeaponTarget {
        private readonly GridInstance gridInstance;

        public ShipTarget(GridInstance gridInstance) {
            this.gridInstance = gridInstance;
        }

        public Vector2 GetPosition(Vector2 current) {
            return gridInstance.Controller.transform.position;
        }

        public bool GetTargetValid() {
            return gridInstance != null && gridInstance.Active;
        }
    }
}