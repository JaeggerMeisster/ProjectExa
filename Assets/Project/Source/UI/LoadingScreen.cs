using System;
using System.Collections;
using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public enum LoadingScreenDuration {
        Short,
        Long
    }

    public class LoadingScreen : MonoBehaviour {
        [Header("References")]
        [SerializeField] private GameObject background;
        [SerializeField] private RectTransform foreground;
        [SerializeField] private CanvasGroup foregroundGroup;
        [SerializeField] private RectTransform loadingCircle;
        [SerializeField] private RectTransform wipNotice;
        [SerializeField] private CanvasGroup wipNoticeGroup;
        [SerializeField] private Text loadingMessage;

        [Header("Options")]
        [SerializeField] private bool forceDisplay;

        private Tween foregroundAlphaTween;
        private Tween foregroundAnchoredPosTween;

        private bool loaded;
        private float minimumTimeActive;
        private bool shouldDisplay;
        private float timeActive;
        private Tween wipNoticeAnchoredPosTween;

        public void Init() {
            shouldDisplay = GetShouldDisplay();
        }

        public void ShowScreen(LoadingScreenDuration duration) {
            if (!shouldDisplay) {
                return;
            }

            minimumTimeActive = GetDuration(duration);
            timeActive = 0f;
            loaded = false;

            gameObject.SetActive(true);
            background.SetActive(true);
            loadingCircle.gameObject.SetActive(true);

            foregroundAnchoredPosTween = SlowSlide(foreground);

            wipNoticeGroup.alpha = 0f;

            this.Delay(
                () => {
                    wipNoticeGroup.DOFade(1f, 1f);

                    SlideIn(wipNotice)
                        .OnComplete(() => wipNoticeAnchoredPosTween = SlowSlide(wipNotice));
                },
                0.5f
            );

            foregroundGroup.alpha = 0;
            foregroundAlphaTween = foregroundGroup.DOFade(1f, 1f);

            StartCoroutine(WaitForDeactivation());
        }

        public void UpdateMessage(string thing, float progress) {
            UpdateMessage($"Loading {thing} ({Mathf.RoundToInt(progress * 100)}% complete)");
        }

        // Creates a new progress object that updates the loading screen message when reporting
        public IProgress<float> GetLoadReporter(string label) {
            return new Progress<float>(value => { UpdateMessage(label, value); });
        }

        public void UpdateMessage(string message) {
            if (!shouldDisplay) {
                return;
            }

            loadingMessage.gameObject.SetActive(message != "");
            loadingMessage.text = message;
        }

        public void HideScreen() {
            HideScreen("Done");
        }

        public void HideScreen(string message) {
            if (!shouldDisplay) {
                return;
            }

            loaded = true;
            loadingMessage.text = message;
            loadingCircle.gameObject.SetActive(false);
        }

        private IEnumerator WaitForDeactivation() {
            while (!loaded || timeActive < minimumTimeActive) {
                var euler = new Vector3(0, 0, timeActive * 360f % 360f);
                loadingCircle.rotation = Quaternion.Euler(euler);
                timeActive += Time.deltaTime;

                yield return null;
            }

            wipNoticeGroup.DOFade(0f, 1f);
            wipNoticeAnchoredPosTween?.Kill();
            SlideOut(wipNotice);

            background.SetActive(false);

            foregroundAnchoredPosTween = SlideOut(foreground);
            foregroundAlphaTween = foregroundGroup.DOFade(0f, 1f);

            this.Delay(
                () => {
                    foregroundAnchoredPosTween?.Kill();
                    foregroundAlphaTween?.Kill();
                    gameObject.SetActive(false);
                },
                1f
            );
        }

        private Tween SlideIn(RectTransform target) {
            gameObject.SetActive(true);
            target.anchoredPosition = new Vector2(50, 0);

            return target.DOAnchorPos(new Vector2(0, 0), 1f)
                .SetEase(Ease.OutSine);
        }

        private Tween SlideOut(RectTransform target) {
            var targetPos = target.anchoredPosition - new Vector2(50, 0);

            return target.DOAnchorPos(targetPos, 1f)
                .SetEase(Ease.InSine);
        }

        private Tween SlowSlide(RectTransform target) {
            return target.DOAnchorPos(new Vector2(-80, 0), 10f)
                .SetEase(Ease.Linear);
        }

        private bool GetShouldDisplay() {
        #if !UNITY_EDITOR
            return true;
        #else
            return forceDisplay;
        #endif
        }

        private float GetDuration(LoadingScreenDuration duration) {
            return duration switch {
                LoadingScreenDuration.Long => 5f,
                LoadingScreenDuration.Short => 2f,
                _ => throw new ArgumentOutOfRangeException(nameof(duration))
            };
        }
    }
}