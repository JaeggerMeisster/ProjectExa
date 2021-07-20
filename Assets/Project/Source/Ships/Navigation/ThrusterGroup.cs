using System.Collections.Generic;
using Exa.Data;
using Exa.Grids.Blocks.Components;

namespace Exa.Ships.Navigation {
    public class ThrusterGroup : List<ThrusterBehaviour> {
        private float thrust;
        private Scalar thrustModifier;

        public ThrusterGroup(Scalar thrustModifier) {
            this.thrustModifier = thrustModifier;
        }

        public float Thrust {
            get => thrustModifier.GetValue(thrust);
        }

        public new void Add(ThrusterBehaviour thruster) {
            base.Add(thruster);
            thrust += thruster.Data.thrust;
        }

        public new bool Remove(ThrusterBehaviour thruster) {
            var result = base.Remove(thruster);

            if (result) {
                thrust -= thruster.Data.thrust;
            }

            return result;
        }

        public void Fire(float force) {
            SetGraphics(force / Thrust);
        }

        public void SetGraphics(float strength) {
            foreach (var item in this) {
                item.Fire(strength);
            }
        }
    }
}