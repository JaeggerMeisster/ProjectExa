using Exa.Camera;
using UnityEngine;

namespace Exa.ShipEditor {
    public class EditorCameraTarget : CameraTarget {
        public EditorCameraTarget(CameraTargetSettings settings)
            : base(settings) { }

        public override Vector2 GetWorldPosition() {
            return Vector2.zero;
        }
    }
}