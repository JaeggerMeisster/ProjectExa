using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Exa.Ships {
    public class CentreOfMassCache : Dictionary<Vector2, float> {
        private float totalMass;
        private Vector2 vectoredTotal;

        public new void Add(Vector2 position, float mass) {
            base.Add(position, mass);

            totalMass += mass;
            vectoredTotal += position * mass;
        }

        public new void Remove(Vector2 position) {
            var mass = base[position];
            base.Remove(position);

            totalMass -= mass;
            vectoredTotal -= position * mass;
        }

        public Vector2 GetCentreOfMass() {
            return vectoredTotal / totalMass;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append($"Centre of mass: {GetCentreOfMass()}");

            return sb.ToString();
        }
    }
}