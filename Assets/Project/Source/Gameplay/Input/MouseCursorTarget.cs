using Exa.Ships.Targeting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Gameplay {
    public class MouseCursorTarget : IWeaponTarget {
        private readonly UnityEngine.Camera camera;
        private readonly bool useDefault;

        public MouseCursorTarget() {
            camera = null;
            useDefault = true;
        }

        public MouseCursorTarget(UnityEngine.Camera camera) {
            this.camera = camera;
            useDefault = false;
        }

        public Vector2 GetPosition(Vector2 current) {
            return useDefault
                ? S.Input.MouseWorldPoint
                : (Vector2) camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        public bool GetTargetValid() {
            return true;
        }
    }
}