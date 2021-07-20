using System.Linq;
using Exa.AI;
using Exa.AI.Actions;
using Exa.Math;
using Exa.Ships.Targeting;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay {
    public class FriendlyShipSelection : ShipSelection {
        public FriendlyShipSelection(Formation formation)
            : base(formation) {
            CanControl = true;
        }

        public void MoveLookAt(Vector2 point) {
            var formationPositions = formation.GetGlobalLayout(this, point);
            var formationShips = this.OrderByDescending(ship => ship.Blueprint.Grid.MaxSize);

            foreach (var (ship, formationPosition) in formationShips.AsTupleEnumerable(formationPositions)) {
                var currentPosition = ship.transform.position.ToVector2();

                ship.Ai.GetAction<AMoveToTarget>().Target = new StaticPositionTarget(formationPosition);
                ship.Ai.GetAction<ALookAtTarget>().Target = new StaticAngleTarget(currentPosition, formationPosition);
            }
        }

        public override ShipSelection Clone() {
            var selection = new FriendlyShipSelection(formation);

            foreach (var ship in this) {
                selection.Add(ship);
            }

            return selection;
        }
    }
}