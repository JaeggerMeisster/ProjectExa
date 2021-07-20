using Exa.Math;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Project.Source.UI.Gameplay {
    public class EnemyWarningCircle : MonoBehaviour {
        [SerializeField] private Image circleImage;
        [SerializeField] private float arcLengthMultiplier;
        [SerializeField] private float arcLengthBase;
        private IEnemyWarningCircleSource source;
        private float arcLengthDegrees;

        public void Setup(IEnemyWarningCircleSource source) {
            this.source = source;
            UpdateLocals();
        }

        private void SetArcLength(float lengthDegrees) {
            circleImage.fillAmount = lengthDegrees / 360f;
            arcLengthDegrees = lengthDegrees;
        }

        public void UpdateLocals() {
            var position = source.GetPosition();

            // Only show warning for offscreen enemies
            var viewportPoint = S.CameraController.Camera.WorldToViewportPoint(position);
            
            // 0.0/1.0 range to -1.0/1.0 range
            var centreOffset = MathUtils.ViewportPointToCentreOffset(viewportPoint);
            var centreOffsetMagnitude = centreOffset.magnitude;
            var active = centreOffsetMagnitude > 1.1f;
            
            gameObject.SetActive(active);

            if (!active) {
                return;
            }
            
            // Fade as it gets closer
            var alpha = centreOffsetMagnitude.Remap(1.1f, 1.5f, 0f, 0.8f);
            circleImage.color = circleImage.color.SetAlpha(alpha);
            
            // Use the distance magnitude and size along a base value to calculate the degrees this
            var length = arcLengthMultiplier / centreOffset.magnitude * source.GetSize() + arcLengthBase;
            SetArcLength(length);

            var distance = position - S.CameraController.CurrentPosition;
            var rot = distance.GetAngle() - arcLengthDegrees / 2f;
            transform.localRotation = Quaternion.Euler(0, 0, rot);
        }
    }
}