using Exa.Math;
using Exa.Ships;
using Exa.Ships.Targeting;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public abstract class TurretBehaviour<T> : BlockBehaviour<T>, ITurretBehaviour
        where T : struct, ITurretValues {
        [Header("References")]
        [SerializeField] protected Transform turret;
        protected RotationResult rotationResult;

        public bool AutoFireEnabled { get; set; }

        public IWeaponTarget Target { get; set; }

        public abstract void Fire();

        protected override void BlockUpdate() {
            rotationResult = TryRotateTowardsTarget();
        }

        protected RotationResult TryRotateTowardsTarget() {
            // Dereference target if it isn't valid anymore
            if (!Target?.GetTargetValid() ?? false) {
                Target = null;
            }

            var result = RotateTowards(SelectTargetAngle());
            SetCurrentAngle(result.endRotation);

            return result;
        }

        protected virtual float SelectTargetAngle() {
            if (Target == null) {
                return GetDefaultAngle();
            }

            var currentPosition = transform.position.ToVector2();
            var difference = Target.GetPosition(currentPosition) - currentPosition;

            return difference.Rotate(-transform.rotation.eulerAngles.z).GetAngle();
        }

        protected float GetDefaultAngle() {
            return 0f;
        }

        // TODO: Clamp to local space
        protected virtual RotationResult RotateTowards(float targetAngle) {
            var currentAngle = GetCurrentAngle();
            var deltaAngle = GetDeltaAngleTowards(currentAngle, targetAngle);

            if (deltaAngle == 0f) {
                return new RotationResult(0f, currentAngle);
            }

            // Clamp the delta to whatever we are allowed to turn this frame
            var maxDelta = Data.TurningRate * Time.deltaTime;
            currentAngle += Mathf.Clamp(deltaAngle, -maxDelta, maxDelta);

            return new RotationResult {
                deltaToTarget = Mathf.DeltaAngle(currentAngle, targetAngle),
                endRotation = currentAngle
            };
        }

        protected virtual float GetCurrentAngle() {
            return turret.localRotation.eulerAngles.z;
        }

        protected virtual void SetCurrentAngle(float angle) {
            turret.localRotation = Quaternion.Euler(0, 0, angle);
        }

        protected virtual float GetDeltaAngleTowards(float currentAngle, float targetAngle) {
            // If the arc is 360f, simply allow wrapping
            if (Data.TurretArc == 360f) {
                return Mathf.DeltaAngle(currentAngle, targetAngle);
            }

            MathUtils.WrapAngle(ref currentAngle);
            MathUtils.WrapAngle(ref targetAngle);

            return Data.GetTurretArcMinMax().Clamp(targetAngle) - currentAngle;
        }

        protected Vector3 HitScanFire(float damage, float maxDistance, Transform firingPoint) {
            var enemyMask = (~Parent.BlockContext).GetBlockMask();
            var hits = PhysicsExtensions.RaycastAll(firingPoint.position, firingPoint.right, maxDistance, enemyMask);

            using var hitsEnumerator = hits.GetEnumerator();

            var lastHitPosition = new Vector2();

            while (damage > 0f && hitsEnumerator.MoveNext(out var hit)) {
                lastHitPosition = hit.hit.point;

                var damageInstance = hit.damageable.TakeDamage(
                    new Damage {
                        source = Parent,
                        value = damage
                    }
                );

                damage -= damageInstance.absorbedDamage;
            }

            return damage > 0f
                ? firingPoint.right * maxDistance
                : lastHitPosition.ToVector3();
        }
    }

    // TODO: Fix delta to target
    public struct RotationResult {
        public float deltaToTarget;
        public float endRotation;

        public RotationResult(float deltaToTarget, float endRotation) {
            this.deltaToTarget = deltaToTarget;
            this.endRotation = endRotation;
        }
    }
}