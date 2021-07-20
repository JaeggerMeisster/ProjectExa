using UnityEngine;

namespace Exa.Camera {
    /// <summary>
    ///     Represents a camera target the user may move
    /// </summary>
    public class UserTarget : CameraTarget {
        private readonly float movementSpeedMultiplier = 1.5f;
        public Vector2 movementDelta;
        public Vector2 worldPosition;

        public UserTarget(CameraTargetSettings settings)
            : base(settings) { }

        public void Tick() {
            worldPosition += movementDelta * GetMovementSpeed();
        }

        public override Vector2 GetWorldPosition() {
            var mouseOffset = S.Input.MouseOffsetFromCentre * GetCalculatedOrthoSize() * 0.1f;

            return worldPosition + mouseOffset;
        }

        public override float GetCalculatedOrthoSize() {
            return base.GetCalculatedOrthoSize() * 0.9f;
        }

        public void ImportValues(ICameraTarget otherTarget) {
            worldPosition = otherTarget.GetWorldPosition();
            ZoomScale = otherTarget.ZoomScale;
        }

        private float GetMovementSpeed() {
            return movementSpeedMultiplier * GetCalculatedOrthoSize() * Time.deltaTime;
        }
    }
}