using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI.Actions {
    public class AMoveToTarget : GridAiAction<EnemyGrid> {
        internal AMoveToTarget(EnemyGrid grid)
            : base(grid) { }

        public override ActionLane Lanes {
            get => ActionLane.Movement;
        }

        public ITarget Target { get; set; } = null;

        public override ActionLane Update(ActionLane blockedLanes) {
            if (Target == null) {
                return ActionLane.None;
            }

            grid.Navigation.MoveTo = Target;

            return ActionLane.Movement;
        }

        protected override float CalculatePriority() {
            return Target != null
                ? 10
                : 0;
        }
    }
}