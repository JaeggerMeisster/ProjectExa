using UnityEngine;

namespace Exa.Debugging {
    public class DebugDragger : MonoBehaviour {
        private readonly float sampleTime = 0.2f;
        private Vector2 averagedVelocity;
        private IDebugDragable currentDragable;
        private Vector2 offset;

        public void Update() {
            if (currentDragable == null) {
                return;
            }

            // Set the velocity
            var mouseWorldPoint = S.Input.MouseWorldPoint;
            var velocity = currentDragable.GetDebugDraggerPosition() - offset - mouseWorldPoint;
            AverageVelocity(-velocity, sampleTime);

            var targetPoint = mouseWorldPoint + offset;
            currentDragable.SetDebugDraggerGlobals(targetPoint, Vector2.zero);
        }

        public void OnPress() {
            if (!TryGetDebugDragable(out var dragable)) {
                return;
            }

            currentDragable = dragable;
            averagedVelocity = Vector2.zero;

            var mouseWorldPoint = S.Input.MouseWorldPoint;
            var currentPoint = currentDragable.GetDebugDraggerPosition();

            offset = currentPoint - mouseWorldPoint;
            currentDragable.SetDebugDraggerGlobals(currentPoint, Vector2.zero);
        }

        public void OnRelease() {
            if (currentDragable == null) {
                return;
            }

            var mouseWorldPoint = S.Input.MouseWorldPoint;
            var targetPoint = mouseWorldPoint + offset;
            currentDragable.SetDebugDraggerGlobals(targetPoint, averagedVelocity);
            currentDragable = null;
        }

        public void OnRotate(float degrees) {
            currentDragable?.Rotate(degrees);
        }

        private void AverageVelocity(Vector2 newVelocity, float sampleTime) {
            var deltaTime = Time.deltaTime;
            var total = averagedVelocity * deltaTime * (sampleTime - deltaTime) + newVelocity;
            averagedVelocity = total * sampleTime / deltaTime;
        }

        private static bool TryGetDebugDragable(out IDebugDragable dragable) {
            try {
                return GS.Raycaster.TryGetTarget(out dragable);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch {
                dragable = null;

                return false;
            }
        }
    }
}