using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blueprints;
using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay {
    public abstract class Formation {
        protected abstract IEnumerable<Vector2> GetLocalLayout(IEnumerable<Blueprint> ships);

        public IEnumerable<Vector2> GetGlobalLayout(ShipSelection ships, Vector2 point) {
            if (!ships.Any()) {
                throw new ArgumentException("Cannot layout an empty collection", nameof(ships));
            }

            // Direction is right aligned, so we subtract 90 degrees to correct
            var direction = point - ships.AveragePosition;
            var angle = direction.GetAngle();

            return GetGlobalLayout(ships.Select(x => x.Blueprint), point, angle);
        }

        public IEnumerable<Vector2> GetGlobalLayout(IEnumerable<Blueprint> blueprints, Vector2 point, float angle) {
            return GetLocalLayout(blueprints).Select(localShipPos => localShipPos.Rotate(angle) + point);
        }
    }
}