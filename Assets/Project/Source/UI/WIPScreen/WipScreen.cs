using System;
using DG.Tweening;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI {
    public class WipScreen : MonoBehaviour {
        [Header("References")]
        [SerializeField] private CanvasGroup rootCanvasGroup;
        [SerializeField] private CanvasGroup textCanvasGroup;
        [SerializeField] private GameObject wordPrefab;
        [SerializeField] private RectTransform textContainer;
        [SerializeField] private RectTransform wordContainer;
        [SerializeField] private HorizontalLayoutGroup wordLayoutGroup;

        [Header("Settings")]
        [SerializeField] private bool forceShowScreen;
        [SerializeField] private AnimSettings animSettings;
        private Tween textContainerScaleTween;

        private Tween titleSpacingTween;

        public void Init() {
            if (GetShouldShowScreen()) {
                ShowScreen();
            }
        }

        private bool GetShouldShowScreen() {
        #if !UNITY_EDITOR
            return true;
        #else
            return forceShowScreen;
        #endif
        }

        private void ShowScreen() {
            gameObject.SetActive(true);
            rootCanvasGroup.alpha = 1f;
            rootCanvasGroup.blocksRaycasts = true;
            textCanvasGroup.alpha = 0f;

            FadeInText(animSettings.textFadeEnter);
            GrowText(animSettings.textGrowthEnter);

            rootCanvasGroup.DOFade(0f, animSettings.fadeOutDuration)
                .SetDelay(animSettings.screenDuration);

            this.Delay(() => GrowText(animSettings.textGrowthExit), animSettings.textGrowthEnter.duration);

            GenerateTitle(animSettings.scrollTitle);
        }

        private void GenerateTitle(bool scrollAnimation) {
            var charCount = 0;

            foreach (var word in animSettings.message.Split(' ')) {
                var go = Instantiate(wordPrefab, wordContainer);
                var text = go.GetComponent<Text>();

                if (scrollAnimation) {
                    text.text = "";
                    var animator = go.AddComponent<TextAnimator>();
                    animator.CharTime = animSettings.charTime;

                    this.Delay(
                        () => animator.AnimateTo(word),
                        charCount * animSettings.charTime + animSettings.textFadeEnter.delay
                    );
                } else {
                    text.text = word;
                }

                charCount += word.Length;
            }
        }

        private void FadeInText(AnimSettings.TextFadeEnterArgs args) {
            textCanvasGroup.DOFade(1f, args.duration)
                .SetDelay(args.delay)
                .SetEase(args.ease);
        }

        private void GrowText(AnimSettings.TextGrowthArgs args) {
            //titleSpacingTween.To(args.spacing, args.duration)
            //    .SetEase(args.ease);

            var scale = new Vector3(args.scale, args.scale, 1f);

            textContainer.DOScale(scale, args.duration)
                .SetEase(args.ease)
                .Replace(ref textContainerScaleTween);
        }

        [Serializable]
        public struct AnimSettings {
            public TextFadeEnterArgs textFadeEnter;
            public TextGrowthArgs textGrowthEnter;
            public TextGrowthArgs textGrowthExit;
            public float screenDuration;
            public float fadeOutDuration;
            public bool scrollTitle;
            public float charTime;
            public string message;

            [Serializable]
            public struct TextFadeEnterArgs {
                public float delay;
                public float duration;
                public ExaEase ease;
            }

            [Serializable]
            public struct TextGrowthArgs {
                public float duration;
                public float scale;
                public float spacing;
                public ExaEase ease;
            }
        }
    }
}