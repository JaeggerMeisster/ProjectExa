using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

#pragma warning disable CS0649

namespace Exa.Weapons {
    public class AutocannonPart : MonoBehaviour {
        [Header("References")]
        [SerializeField] private FiringPoint firingPoint;
        [SerializeField] private Animation barrelAnimator;
        [SerializeField] private Light2D light2D;

        [Header("Settings")]
        [SerializeField] private float peakIntensity;
        [SerializeField] private SpriteRenderer drumRenderer;
        [SerializeField] private Sprite drumNormal;
        [SerializeField] private Sprite drumCycling;

        private float animTime;
        private IGridInstance parent;

        private void OnDisable() {
            StopAllCoroutines();
        }

        public void Setup(float animTime, IGridInstance parent, BlockContext damageMask) {
            this.animTime = animTime;
            this.parent = parent;
            barrelAnimator["BarrelAnimation"].speed = 1f / animTime;
            firingPoint.Setup(damageMask);
        }

        public void Fire(float damage, float range) {
            barrelAnimator.Stop();
            barrelAnimator.Play();

            firingPoint.Fire(
                new Damage {
                    value = damage,
                    source = parent
                },
                80f,
                range
            );

            light2D.intensity = peakIntensity;
            light2D.DOIntensity(0, 0.1f);

            this.DelayLocally(SetCyclingDrumSprite, animTime * 0.25f);
            this.DelayLocally(SetNormalDrumSprite, animTime * 0.5f);
        }

        private void SetCyclingDrumSprite() {
            drumRenderer.sprite = drumCycling;
        }

        private void SetNormalDrumSprite() {
            drumRenderer.sprite = drumNormal;
        }
    }
}