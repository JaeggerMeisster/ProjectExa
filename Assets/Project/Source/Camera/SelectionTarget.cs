using System.Linq;
using Exa.Gameplay;
using UnityEngine;

namespace Exa.Camera {
    public class SelectionTarget : CameraTarget {
        private readonly ShipSelection selection;

        public SelectionTarget(ShipSelection selection, CameraTargetSettings settings)
            : base(settings) {
            this.selection = selection;
            SetZoomFromOrtho(selection.Max(ship => ship.BlockGrid.MaxSize) * 0.75f);
        }

        public override bool TargetValid {
            get => selection.Any();
        }

        public override Vector2 GetWorldPosition() {
            return selection.AveragePosition;
        }
    }
}