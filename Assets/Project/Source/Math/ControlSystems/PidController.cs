using System;
using UnityEngine;

namespace Exa.Math.ControlSystems {
    public class PidController {
        private const float MaxOutput = 1000.0f;
        private float calculatedIntegral;

        private float calculatedIntegralMax;
        private float derivitive;
        private float integral;

        private float proportional;

        public PidController(float proportional, float integral, float derivitive) {
            Proportional = proportional;
            Integral = integral;
            Derivitive = derivitive;

            calculatedIntegralMax = MaxOutput / Integral;
        }

        public float Proportional {
            get => proportional;
            set {
                EnsureNonNegative(proportional, "proportional");
                proportional = value;
            }
        }

        public float Integral {
            get => integral;
            set {
                EnsureNonNegative(integral, "integral");

                integral = value;

                calculatedIntegralMax = MaxOutput / Integral;

                calculatedIntegral = Mathf.Clamp(
                    calculatedIntegral,
                    -calculatedIntegralMax,
                    calculatedIntegralMax
                );
            }
        }

        /// <summary>
        ///     Gets or sets the derivative gain.
        /// </summary>
        /// <value>
        ///     The derivative gain.
        /// </value>
        public float Derivitive {
            get => derivitive;
            set {
                EnsureNonNegative(value, "Derivative");
                derivitive = value;
            }
        }

        public float ComputeOutput(float error, float delta, float deltaTime) {
            calculatedIntegral += error * deltaTime;

            calculatedIntegral = Mathf.Clamp(
                calculatedIntegral,
                -calculatedIntegralMax,
                calculatedIntegralMax
            );

            var derivative = delta / deltaTime;
            var output = Proportional * error + Integral * calculatedIntegral + Derivitive * derivative;

            output = Mathf.Clamp(output, -MaxOutput, MaxOutput);

            return output;
        }

        private void EnsureNonNegative(float value, string paramName) {
            if (value < 0f) {
                throw new ArgumentOutOfRangeException(paramName, "param must be a non-negative number");
            }
        }
    }
}