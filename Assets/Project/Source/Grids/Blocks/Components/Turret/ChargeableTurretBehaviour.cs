using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public abstract class ChargeableTurretBehaviour<T> : TurretBehaviour<T>, IChargeableTurretBehaviour
        where T : struct, IChargeableTurretValues {
        [Header("Settings")]
        [SerializeField] protected float chargeDecaySpeed = 4f;
        protected float chargeTime;
        protected bool charging;

        public abstract bool CanResumeCharge { get; }

        public virtual void StartCharge() {
            charging = true;

            if (!CanResumeCharge) {
                chargeTime = 0f;
            }
        }

        public virtual void EndCharge() {
            charging = false;
        }

        protected override void BlockUpdate() {
            base.BlockUpdate();

            if (charging) {
                chargeTime += Time.deltaTime;

                if (chargeTime > Data.ChargeTime) {
                    charging = false;
                    Fire();
                    chargeTime = 0f;
                }
            } else {
                chargeTime -= Time.deltaTime * chargeDecaySpeed;
            }

            if (chargeTime < 0f) {
                chargeTime = 0f;
            }
        }

        protected Scalar GetNormalizedChargeProgress() {
            // Get the charge 
            return chargeTime == 0f ? 0f : chargeTime / Data.ChargeTime;
        }
    }
}