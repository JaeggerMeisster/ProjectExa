using System;
using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Exa.Grids.Blocks.Components {
    public class ShieldGeneratorBehaviour : BlockBehaviour<ShieldGeneratorData> {
        [Header("References")]
        [SerializeField] private SpriteRenderer lights;
        [SerializeField] private ShieldBubble shieldBubble;

        [Header("Settings")]
        [SerializeField] private float lightsAnimationDuration;

        [Header("State")]
        [SerializeField] private HealthPool healthPool;
        [SerializeField] private bool shieldIsRaised = true;
        private Tween lightsTween;
        private float normalizedLightsAlpha = 1f;

        protected override void OnAdd() {
            shieldBubble.Block = block;
            healthPool.value = Data.health;
        }

        protected override void OnBlockDataReceived(ShieldGeneratorData oldValues, ShieldGeneratorData newValues) {
            shieldBubble.SetRadius(newValues.shieldRadius);
        }

        public TakenDamage TakeDamage(Damage damage) {
            try {
                if (!shieldIsRaised) {
                    Debug.LogError("Received shield damage when shield is not raised");

                    return new TakenDamage();
                }

                if (!healthPool.TakeDamage(damage, 0, out var takenDamage)) {
                    LowerShields();
                }

                return takenDamage;
            } catch (Exception e) {
                Debug.LogException(e);
            }

            return new TakenDamage {
                absorbedDamage = damage.value,
                appliedDamage = 0f
            };
        }

        private void LowerShields() {
            shieldIsRaised = false;
            shieldBubble.Lower();
            AnimateLights(0f);
            
            this.Delay(RaiseShields, Data.recoverTime);
        }

        private void RaiseShields() {
            shieldIsRaised = true;
            shieldBubble.Raise();
            healthPool.value = Data.health;
            AnimateLights(1f);
        }

        private float GetNormalizedLightsAlpha() {
            return normalizedLightsAlpha;
        }

        private void SetNormalizedLightsAlpha(float value) {
            normalizedLightsAlpha = value;

            var randomizedAlpha = value + (Random.value - 0.5f) * 0.1f;
            lights.color = lights.color.SetAlpha(randomizedAlpha);
        }

        private void AnimateLights(float alpha) {
            DOTween.To(GetNormalizedLightsAlpha, SetNormalizedLightsAlpha, alpha, lightsAnimationDuration)
                .Replace(ref lightsTween);
        }
    }
}