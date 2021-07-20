using Exa.Generics;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay {
    public class SelectionBuilder : IBuilder<ShipSelection> {
        private readonly Vector2 startSelectionPos;
        private ShipSelection selection;

        public SelectionBuilder(Vector2 startSelectionPos) {
            this.startSelectionPos = startSelectionPos;
        }

        public ShipSelection Build() {
            return selection?.Count > 0 ? selection : null;
        }

        public void UpdateSelection(Vector2 endSelectionPos) {
            var layerMask = LayerMask.GetMask("unit");
            var colliders = Physics2D.OverlapAreaAll(startSelectionPos, endSelectionPos, layerMask);

            foreach (var collider in colliders) {
                var ship = collider.gameObject.GetComponent<GridInstance>();

                if (ship != null && ship.Active) {
                    selection ??= ship.GetAppropriateSelection(new VicFormation());

                    if (ship.MatchesSelection(selection) && !selection.Contains(ship)) {
                        selection.Add(ship);
                    }
                }
            }
        }
    }
}