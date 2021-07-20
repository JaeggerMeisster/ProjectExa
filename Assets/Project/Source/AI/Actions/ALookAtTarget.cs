using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI {
    public class ALookAtTarget : GridAiAction<EnemyGrid> {
        internal ALookAtTarget(EnemyGrid grid)
            : base(grid) { }

        public override ActionLane Lanes {
            get => ActionLane.Rotation;
        }

        public ITarget Target { get; set; } = null;

        public override ActionLane Update(ActionLane blockedLanes) {
            if (Target == null) {
                return ActionLane.None;
            }

            grid.Navigation.LookAt = Target;

            return ActionLane.Rotation;
        }

        protected override float CalculatePriority() {
            return Target != null
                ? 2
                : 0;
        }
    }
}