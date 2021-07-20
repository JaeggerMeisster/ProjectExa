using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public abstract class AutoFireTurret<T> : TurretBehaviour<T>
        where T : struct, ITurretValues {

        private float timeBetweenFire;
        private float timeSinceFire;
        
        private void Awake() {
            AutoFireEnabled = true;
        }

        protected override void OnBlockDataReceived(T oldValues, T newValues) {
            base.OnBlockDataReceived(oldValues, newValues);
            timeBetweenFire = 60 / newValues.FiringRate;
        }

        protected override void BlockUpdate() {
            base.BlockUpdate();
            
            // Only add to the time since fire if the current time since fire is below the firing rate of the turret
            // This implementation allows for the time since fire to be slightly bigger than than the firing rate
            // automatic fire is accurate
            if (timeSinceFire < timeBetweenFire) {
                timeSinceFire += Time.deltaTime;
            }
            
            AttemptFire();
        }

        protected virtual void AttemptFire() {
            if (DebugFocused) { }

            if (Target == null || !AutoFireEnabled) {
                return;
            }

            // Long debug line cause debugging in unity sucks
            // Debug.Log($"({block.GetInstanceString()}) Current angle: {GetCurrentAngle()}, Target angle: {SelectTargetAngle()}, Diff {Mathf.DeltaAngle(GetCurrentAngle(), SelectTargetAngle())} ({rotationResult.deltaToTarget})");

            
            // Only fire if the turret is facing the target properly, and it is ready to fire
            if (Mathf.Abs(rotationResult.deltaToTarget) > 0.5f || timeSinceFire < timeBetweenFire) {
                return;
            }

            timeSinceFire -= timeBetweenFire;
            Fire();
        }
    }
}