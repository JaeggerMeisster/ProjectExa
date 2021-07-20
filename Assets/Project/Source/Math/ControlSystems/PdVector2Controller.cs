using UnityEngine;

namespace Exa.Math.ControlSystems {
    public class PdVector2Controller {
        public PdVector2Controller(float proportional, float derivitive, float maxVel) {
            Proportional = proportional;
            Derivitive = derivitive;
            MaxVel = maxVel;
        }

        public float Proportional { get; set; }

        public float Derivitive { get; set; }

        public float MaxVel { get; set; }

        public void SetSettings(PdSettings pdSettings) {
            Proportional = pdSettings.proportional;
            Derivitive = pdSettings.derivitive;
        }

        public Vector2 CalculateRequiredVelocity(Vector2 currentPos, Vector2 targetPos, Vector2 currentVelocity) {
            var dist = targetPos - currentPos;
            var targetVel = Vector2.ClampMagnitude(Proportional * dist, MaxVel);
            var error = targetVel - currentVelocity;

            return Derivitive * error;
        }
    }
}