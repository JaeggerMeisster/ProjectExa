using System.Collections.Generic;
using System.Linq;
using Exa.AI.Actions;
using Exa.Ships;

namespace Exa.AI {
    public class EnemyAi : GridAi {
        protected override IEnumerable<IAction> BuildActionList() {
            return base.BuildActionList()
                .Append(new ALookAtTarget(GridInstance as EnemyGrid))
                .Append(new AMoveToTarget(GridInstance as EnemyGrid))
                .Append(
                    new AAvoidCollision(
                        GridInstance as EnemyGrid,
                        new AAvoidCollisionSettings {
                            detectionRadius = GridInstance.BlockGrid.MaxSize,
                            priorityMultiplier = 1,
                            priorityBase = 10,
                            headingCorrectionMultiplier = 8
                        }
                    )
                );
        }
    }
}