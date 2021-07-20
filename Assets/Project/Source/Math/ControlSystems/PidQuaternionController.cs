using System;
using UnityEngine;

namespace Exa.Math.ControlSystems {
    public class PidQuaternionController {
        private readonly PidController[] internalControllers;

        public PidQuaternionController(float proportional, float integral, float derivitive) {
            internalControllers = new[] {
                new PidController(proportional, integral, derivitive),
                new PidController(proportional, integral, derivitive),
                new PidController(proportional, integral, derivitive),
                new PidController(proportional, integral, derivitive)
            };
        }

        public float Proportional {
            get => internalControllers[0].Proportional;
            set {
                EnsureNonNegative(value, "Proportional");

                internalControllers[0].Proportional = value;
                internalControllers[1].Proportional = value;
                internalControllers[2].Proportional = value;
                internalControllers[3].Proportional = value;
            }
        }

        public float Integral {
            get => internalControllers[0].Integral;
            set {
                EnsureNonNegative(value, "Integral");

                internalControllers[0].Integral = value;
                internalControllers[1].Integral = value;
                internalControllers[2].Integral = value;
                internalControllers[3].Integral = value;
            }
        }

        public float Derivative {
            get => internalControllers[0].Derivitive;
            set {
                EnsureNonNegative(value, "Derivative");

                internalControllers[0].Derivitive = value;
                internalControllers[1].Derivitive = value;
                internalControllers[2].Derivitive = value;
                internalControllers[3].Derivitive = value;
            }
        }

        public static Quaternion MultiplyAsVector(Matrix4x4 matrix, Quaternion quaternion) {
            var vector = new Vector4(quaternion.w, quaternion.x, quaternion.y, quaternion.z);

            var result = matrix * vector;

            return new Quaternion(result.y, result.z, result.w, result.x);
        }

        public static Quaternion ToEulerAngleQuaternion(Vector3 eulerAngles) {
            return new Quaternion(eulerAngles.x, eulerAngles.y, eulerAngles.z, 0);
        }

        public Vector3 ComputeRequiredAngularAcceleration(
            Quaternion currentOrientation,
            Quaternion desiredOrientation,
            Vector3 currentAngularVelocity,
            float deltaTime
        ) {
            var requiredRotation = QuaternionExtensions.RequiredRotation(currentOrientation, desiredOrientation);

            var error = Quaternion.identity.Subtract(requiredRotation);
            var angularVelocity = ToEulerAngleQuaternion(currentAngularVelocity);
            var delta = angularVelocity * requiredRotation;

            var orthogonal = OrthogonalizeMatrix(requiredRotation);

            var neededAngularVelocity = ComputeOutput(error, delta, deltaTime);

            neededAngularVelocity = MultiplyAsVector(orthogonal, neededAngularVelocity);

            var doubleNegative = neededAngularVelocity.Multiply(-2.0f);
            var result = doubleNegative * Quaternion.Inverse(requiredRotation);

            return new Vector3(result.x, result.y, result.z);
        }

        private Quaternion ComputeOutput(Quaternion error, Quaternion delta, float deltaTime) {
            var output = new Quaternion {
                x = internalControllers[0].ComputeOutput(error.x, delta.x, deltaTime),
                y = internalControllers[1].ComputeOutput(error.y, delta.y, deltaTime),
                z = internalControllers[2].ComputeOutput(error.z, delta.z, deltaTime),
                w = internalControllers[3].ComputeOutput(error.w, delta.w, deltaTime)
            };

            return output;
        }

        private void EnsureNonNegative(float value, string paramName) {
            if (value < 0f) {
                throw new ArgumentOutOfRangeException(paramName, "param must be a non-negative number");
            }
        }

        private Matrix4x4 OrthogonalizeMatrix(Quaternion requiredRotation) {
            return new Matrix4x4 {
                m00 =
                    -requiredRotation.x * -requiredRotation.x +
                    -requiredRotation.y * -requiredRotation.y +
                    -requiredRotation.z * -requiredRotation.z,
                m01 =
                    -requiredRotation.x * requiredRotation.w +
                    -requiredRotation.y * -requiredRotation.z +
                    -requiredRotation.z * requiredRotation.y,
                m02 =
                    -requiredRotation.x * requiredRotation.z +
                    -requiredRotation.y * requiredRotation.w +
                    -requiredRotation.z * -requiredRotation.x,
                m03 =
                    -requiredRotation.x * -requiredRotation.y +
                    -requiredRotation.y * requiredRotation.x +
                    -requiredRotation.z * requiredRotation.w,
                m10 =
                    requiredRotation.w * -requiredRotation.x +
                    -requiredRotation.z * -requiredRotation.y +
                    requiredRotation.y * -requiredRotation.z,
                m11 =
                    requiredRotation.w * requiredRotation.w +
                    -requiredRotation.z * -requiredRotation.z +
                    requiredRotation.y * requiredRotation.y,
                m12 =
                    requiredRotation.w * requiredRotation.z +
                    -requiredRotation.z * requiredRotation.w +
                    requiredRotation.y * -requiredRotation.x,
                m13 =
                    requiredRotation.w * -requiredRotation.y +
                    -requiredRotation.z * requiredRotation.x +
                    requiredRotation.y * requiredRotation.w,
                m20 =
                    requiredRotation.z * -requiredRotation.x +
                    requiredRotation.w * -requiredRotation.y +
                    -requiredRotation.x * -requiredRotation.z,
                m21 =
                    requiredRotation.z * requiredRotation.w +
                    requiredRotation.w * -requiredRotation.z +
                    -requiredRotation.x * requiredRotation.y,
                m22 =
                    requiredRotation.z * requiredRotation.z +
                    requiredRotation.w * requiredRotation.w +
                    -requiredRotation.x * -requiredRotation.x,
                m23 =
                    requiredRotation.z * -requiredRotation.y +
                    requiredRotation.w * requiredRotation.x +
                    -requiredRotation.x * requiredRotation.w,
                m30 =
                    -requiredRotation.y * -requiredRotation.x +
                    requiredRotation.x * -requiredRotation.y +
                    requiredRotation.w * -requiredRotation.z,
                m31 =
                    -requiredRotation.y * requiredRotation.w +
                    requiredRotation.x * -requiredRotation.z +
                    requiredRotation.w * requiredRotation.y,
                m32 =
                    -requiredRotation.y * requiredRotation.z +
                    requiredRotation.x * requiredRotation.w +
                    requiredRotation.w * -requiredRotation.x,
                m33 =
                    -requiredRotation.y * -requiredRotation.y +
                    requiredRotation.x * requiredRotation.x +
                    requiredRotation.w * requiredRotation.w
            };
        }
    }
}