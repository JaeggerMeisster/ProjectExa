using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class RootNavigationContent : MonoBehaviour {
        [SerializeField] private LayoutElement tooltipContainer;
        [SerializeField] private TextAnimator textAnimator;

        private Tween heightTween;

        private void Awake() {
            RemoveLock();
        }

        public void RemoveLock() {
            Animate(false);
        }

        public void SetTooltip(string value) {
            textAnimator.AnimateTo(value);
            Animate(true);
        }

        private void Animate(bool active) {
            tooltipContainer.DOPreferredHeight(active ? 60f : 0f, 0.15f)
                .Replace(ref heightTween);
        }
    }
}