using System;
using DG.Tweening;
using Exa.Data;
using Exa.Math;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    public class NavigateableTabButton : Navigateable {
        public int order;

        [Header("References")]
        [SerializeField] private RectTransform self;
        [SerializeField] private Text text;
        [SerializeField] private Image image;
        [SerializeField] private AnimatedTabContent content;

        [Header("Settings")]
        [SerializeField] private float duration;
        [SerializeField] private ActivePair<AnimationArgs> animArgs;
        private Tween fontTween;
        private Tween imageTween;

        private Tween rectTween;

        public override void HandleExit(Navigateable target) {
            content.HandleExit(
                target is NavigateableTabButton button
                    ? Vector2.right * (button.order > order).To1()
                    : Vector2.zero
            );

            Animate(animArgs.inactive);
        }

        public override void HandleEnter(NavigationArgs args) {
            content.HandleEnter(
                args?.current is NavigateableTabButton button
                    ? Vector2.left * (button.order > order).To1()
                    : Vector2.zero
            );

            Animate(animArgs.active);
        }

        private void Animate(AnimationArgs args) {
            self.DOSizeDelta(self.sizeDelta.SetY(args.height), duration)
                .Replace(ref rectTween);

            image.DOColor(args.color, duration)
                .Replace(ref imageTween);

            text.DOFontSize(args.fontSize, duration)
                .Replace(ref fontTween);
        }

        [Serializable]
        private struct AnimationArgs {
            public Color color;
            public float height;
            public int fontSize;
        }
    }
}