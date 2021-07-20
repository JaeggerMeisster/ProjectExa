using Exa.Math;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Gameplay {
    public class DamagePopup : MonoBehaviour {
        [Header("References")]
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private TextMeshPro tmp;

        [Header("Settings")]
        [SerializeField] private AnimationCurve scaleAnimationCurve;
        [SerializeField] private AnimationCurve positionAnimationCurve;
        [SerializeField] private float maxLifetime;
        [SerializeField] private float scaleMultiplier;
        [SerializeField] private float positionMultiplier;

        [Header("Events")]
        public UnityEvent DestroyEvent;

        private float currentLifetime;
        private float damage;
        private Vector2 worldPosition;

        private void Awake() {
            DestroyEvent = new UnityEvent();
        }

        private void Update() {
            currentLifetime += Time.deltaTime;

            if (currentLifetime > maxLifetime) {
                DestroyEvent.Invoke();
                Destroy(gameObject);

                return;
            }

            var time = currentLifetime / maxLifetime;
            SetPosition(time);
            SetScale(scaleAnimationCurve.Evaluate(time));
        }

        public void Setup(Vector2 worldPosition, float damage, int order) {
            SetScale(scaleAnimationCurve.Evaluate(0f));
            transform.position = worldPosition.WithZ(-5f);
            this.worldPosition = worldPosition;
            meshRenderer.sortingOrder = order;
            this.damage += damage;
            tmp.SetText(this.damage.Round().ToString());
            currentLifetime = 0f;
        }

        private void SetScale(float scale) {
            scale *= scaleMultiplier;
            transform.localScale = new Vector3(scale, scale);
        }

        private void SetPosition(float time) {
            var evaluatedPosition = positionAnimationCurve.Evaluate(time) * positionMultiplier;
            transform.localPosition = (worldPosition + new Vector2(0f, evaluatedPosition - 1f)).WithZ(-5);
        }
    }
}