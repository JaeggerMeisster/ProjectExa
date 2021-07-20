using Exa.Ships.Targeting;

namespace Exa.Grids.Blocks.Components {
    public interface ITurretBehaviour {
        public bool AutoFireEnabled { get; }
        public IWeaponTarget Target { set; }

        public void Fire();
    }

    public interface IChargeableTurretBehaviour : ITurretBehaviour {
        void StartCharge();

        void EndCharge();
    }
}