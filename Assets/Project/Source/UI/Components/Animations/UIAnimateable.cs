using System.Collections;
using DG.Tweening;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Components {
    public enum AnimationDirection {
        none = 0,
        top = 1,
        left = 2,
        bottom = 3,
        right = 4
    }

    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class UIAnimateable : MonoBehaviour {
        public float msLocalAnimationOffset;

        // movement animation
        public AnimationDirection movementDirection = AnimationDirection.none;

        public float movementSmoothDamp = 0.1f;
        public float movementMagnitude = 20f;

        // Alpha animation
        public bool animateAlpha;

        public float alphaSpeed = 8f;

        private Tween alphaTween;

        private CanvasGroup canvasGroup;

        private Vector2 elementVelocity = Vector2.zero;
        private float originalAlpha;
        private Vector2 originalPos;
        private RectTransform rect;

        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            rect = GetComponent<RectTransform>();
        }

        private void OnEnable() {
            originalAlpha = canvasGroup.alpha;

            if (animateAlpha) {
                canvasGroup.alpha = 0f;

                canvasGroup.DOFade(1, 1 / alphaSpeed)
                    .SetDelay(msLocalAnimationOffset / 1000)
                    .Replace(ref alphaTween);
            }

            if (movementDirection == AnimationDirection.none) {
                return;
            }

            originalPos = rect.anchoredPosition;

            // Create a vector that points upwards and rotate it by the animation direction
            // This is used as an offset from the original rect position
            var offset = new Vector2(0, movementMagnitude).Rotate(movementDirection.GetRotation());

            rect.anchoredPosition = originalPos + offset;

            StartDelayedCoroutine(msLocalAnimationOffset / 1000f, SlideIn(originalPos));
        }

        private void OnDisable() {
            SkipAnimations();
        }

        private void SkipAnimations() {
            StopAllCoroutines();
            alphaTween?.Kill();

            canvasGroup.alpha = originalAlpha;

            if (movementDirection == AnimationDirection.none) {
                return;
            }

            rect.anchoredPosition = originalPos;
        }

        private IEnumerator SlideIn(Vector2 towards) {
            while (rect.anchoredPosition != towards) {
                rect.anchoredPosition = Vector2.SmoothDamp(
                    rect.anchoredPosition,
                    towards,
                    ref elementVelocity,
                    movementSmoothDamp
                );

                yield return null;
            }
        }

        private void StartDelayedCoroutine(float seconds, IEnumerator routine) {
            StartCoroutine(DelayCoroutine(seconds, routine));
        }

        private IEnumerator DelayCoroutine(float seconds, IEnumerator routine) {
            if (seconds != 0f) {
                yield return new WaitForSeconds(seconds);
            }

            StartCoroutine(routine);
        }
    }
}