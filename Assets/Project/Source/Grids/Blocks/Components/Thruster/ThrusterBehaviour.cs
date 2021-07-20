using DG.Tweening;
using Exa.Ships;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.Components {
    public class ThrusterBehaviour : BlockBehaviour<ThrusterData> {
        [Header("References")]
        [SerializeField] private Transform thrusterFlameContainer;
        [SerializeField] private SpriteRenderer thrusterFlame;
        [SerializeField] private Light2D light2D;

        [Header("Settings")]
        [SerializeField] private MinMax<float> xScale;
        [SerializeField] private MinMax<float> yScale;
        [SerializeField] private MinMax<float> lightIntensityScale;

        private float strength;

        public void Fire(float strength) {
            this.strength = strength;

            thrusterFlame.transform.localScale = new Vector2 {
                x = xScale.Evaluate(strength),
                y = yScale.Evaluate(strength)
            };

            light2D.intensity = lightIntensityScale.Evaluate(strength);
        }

        public void PowerDown() {
            thrusterFlame.transform.DOScale(0f, 0.5f);
            light2D.DOIntensity(0f, 0.5f);
        }

        protected override void OnAdd() {
            (GridInstance as EnemyGrid)?.Navigation.ThrustVectors.Register(this);
        }

        protected override void OnRemove() {
            (GridInstance as EnemyGrid)?.Navigation.ThrustVectors.Unregister(this);
        }
    }
}