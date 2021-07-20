using Exa.Data;
using Exa.Debugging;
using Exa.Math;
using Exa.Ships.Targeting;
using UnityEngine;

namespace Exa.Ships.Navigation {
    public class DirectionalNavigation : INavigation {
        private static readonly Scalar DampeningThrustMultiplier = new Scalar(1.5f);
        private static readonly Scalar TargetThrustMultiplier = new Scalar(1f);

        private readonly GridInstance gridInstance;
        private readonly NavigationOptions options;
        private readonly AxisThrustVectors thrustVectors;

        public DirectionalNavigation(GridInstance gridInstance, NavigationOptions options, Scalar thrustModifier) {
            this.gridInstance = gridInstance;
            this.options = options;

            thrustVectors = gridInstance.gameObject.AddComponent<AxisThrustVectors>();
            thrustVectors.Setup(gridInstance, thrustModifier);
        }

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }

        public IThrustVectors ThrustVectors {
            get => thrustVectors;
        }

        public void Update(float deltaTime) {
            var velocityValues = GetLocalVelocity();

            // Calculate the force required to travel
            var frameTargetForce = Target(velocityValues, deltaTime);

            // Calculate the force required to dampen
            var frameDampenForce = Dampen(velocityValues, deltaTime);

            var resultFrameForce = MergeForces(frameTargetForce, frameDampenForce);

            if (DebugMode.Navigation.IsEnabled()) {
                void DrawRay(Vector2 localFrameForce, Color color) {
                    var force = localFrameForce.Rotate(GetCurrentRotation()) / deltaTime;
                    Debug.DrawRay(gridInstance.GetPosition(), force / gridInstance.Rigidbody2D.mass / 10, color);
                }

                DrawRay(frameTargetForce, Color.red);
                DrawRay(frameDampenForce, Color.blue);
                DrawRay(resultFrameForce, (Color.red + Color.blue) / 2);
            }

            Fire(resultFrameForce, deltaTime);
        }

        private Vector2 Target(VelocityValues velocityValues, float deltaTime) {
            if (MoveTo == null) {
                return Vector2.zero;
            }

            // Calculate the distance between the current and target position from the perspective of the ship
            var diff = GetLocalDifference(MoveTo);

            if (diff.magnitude < 1f) {
                return Vector2.zero;
            }

            return diff.magnitude > GetBrakeDistance(diff, velocityValues)
                ? thrustVectors.GetClampedForce(diff, TargetThrustMultiplier) * deltaTime
                : Vector2.zero;
        }

        private Vector2 GetLocalDifference(ITarget target) {
            var currentPos = gridInstance.GetPosition();
            var diff = target.GetPosition(currentPos) - currentPos;

            return diff.Rotate(-GetCurrentRotation());
        }

        private float GetDecelerationVelocity(Vector2 direction, Scalar thrustModifier) {
            var force = thrustVectors.GetClampedForce(direction, thrustModifier);

            return -(force / Mathf.Pow(gridInstance.Rigidbody2D.mass, 2)).magnitude;
        }

        // TODO: Fix this
        private float GetBrakeDistance(Vector2 diff, VelocityValues velocityValues) {
            var currentVelocity = velocityValues.localVelocity.magnitude;
            var deceleration = GetDecelerationVelocity(-diff, DampeningThrustMultiplier);

            return CalculateBrakeDistance(currentVelocity, 0, deceleration);
        }

        private float CalculateBrakeDistance(float currentVelocity, float targetVelocity, float deceleration) {
            var t = (targetVelocity - currentVelocity) / deceleration;

            return currentVelocity * t + deceleration * (t * t) / 2f;
        }

        private Vector2 Dampen(VelocityValues velocityValues, float deltaTime) {
            void ProcessAxis(ref float forceAxis, float velocityAxis) {
                if (Mathf.Abs(forceAxis) > Mathf.Abs(velocityAxis)) {
                    forceAxis = velocityAxis;
                }
            }

            // Get force for this frame
            var frameTargetForce =
                thrustVectors.GetForce(-velocityValues.localVelocityForce, DampeningThrustMultiplier) * deltaTime;

            var frameVelocityForce = -velocityValues.localVelocityForce / deltaTime;

            ProcessAxis(ref frameTargetForce.x, frameVelocityForce.x);
            ProcessAxis(ref frameTargetForce.y, frameVelocityForce.y);

            return frameTargetForce;
        }

        private Vector2 MergeForces(Vector2 frameTargetForce, Vector2 frameDampenForce) {
            // TODO: Make this branch-less
            float ProcessAxis(float targetComponent, float dampenComponent) {
                if (targetComponent == 0f) {
                    return dampenComponent;
                }

                // Check if the target and the dampen component have different signs
                return (targetComponent > 0f) ^ (dampenComponent > 0f)
                    // If so, return the target vector component
                    ? targetComponent
                    // Otherwise, return biggest of the two components
                    : Mathf.Abs(targetComponent) > Mathf.Abs(dampenComponent)
                        ? targetComponent
                        : dampenComponent;
            }

            return new Vector2 {
                x = ProcessAxis(frameTargetForce.x, frameDampenForce.x),
                y = ProcessAxis(frameTargetForce.y, frameDampenForce.y)
            };
        }

        private void Fire(Vector2 frameTargetForce, float deltaTime) {
            // Transform force for this frame to velocity
            thrustVectors.Fire(frameTargetForce / deltaTime);
            gridInstance.Rigidbody2D.AddForce(frameTargetForce, ForceMode2D.Force);
        }

        private VelocityValues GetLocalVelocity() {
            var localVelocity = gridInstance.Rigidbody2D.velocity.Rotate(-GetCurrentRotation());

            return new VelocityValues {
                localVelocity = localVelocity,
                localVelocityForce = localVelocity * gridInstance.Rigidbody2D.mass
            };
        }

        private float GetCurrentRotation() {
            return MathUtils.NormalizeAngle360(gridInstance.Rigidbody2D.rotation);
        }

        private struct VelocityValues {
            public Vector2 localVelocity;
            public Vector2 localVelocityForce;
        }
    }
}