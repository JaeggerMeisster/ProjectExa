using System;
using DG.Tweening;
using Exa.Data;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    public class AnimatedTabContent : MonoBehaviour {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] private float duration = 0.25f;
        [SerializeField] private ActivePair<AnimationArgs> animArgs;

        private Tween alphaTween;
        private Tween positionTween;

        public void HandleEnter(Vector2 direction) {
            gameObject.SetActive(true);
            Animate(animArgs.active, direction * animArgs.active.animAmplitude, Vector2.zero);
            this.Delay(() => canvasGroup.interactable = true, duration);
        }

        public void HandleExit(Vector2 direction) {
            canvasGroup.interactable = false;
            Animate(animArgs.inactive, Vector2.zero, direction * animArgs.inactive.animAmplitude);
            this.Delay(() => gameObject.SetActive(false), duration);
        }

        protected virtual void Animate(AnimationArgs args, Vector2 initialPos, Vector2 targetPos) {
            canvasGroup.DOFade(args.targetAlpha, duration)
                .SetEase(args.ease)
                .Replace(ref alphaTween);

            rectTransform.anchoredPosition = initialPos;

            rectTransform.DOAnchorPos(targetPos, duration)
                .SetEase(args.ease)
                .Replace(ref positionTween);
        }

        [Serializable]
        public struct AnimationArgs {
            public float targetAlpha;
            public float animAmplitude;
            public Ease ease;
        }
    }
}