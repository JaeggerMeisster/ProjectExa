using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Exa.Grids.Blocks {
    [Serializable]
    public struct HealthPool {
        public float value;

        public bool TakeDamage(Damage damage, float armor, out TakenDamage takenDamage) {
            if (value <= 0) {
                throw new InvalidOperationException("Cannot take damage when health is empty");
            }
            
            takenDamage = new TakenDamage {
                absorbedDamage = Mathf.Min(value, damage.value),
                appliedDamage = Mathf.Min(value, ComputeDamage(damage.value, armor))
            };

            value -= takenDamage.appliedDamage;

            return value > 0;
        }

        private float ComputeDamage(float damage, float armor) {
            return Mathf.Max(damage - armor, 0f);
        }
    }
}