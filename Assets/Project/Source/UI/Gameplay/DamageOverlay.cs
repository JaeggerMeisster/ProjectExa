using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Gameplay {
    public class DamageOverlay : MonoBehaviour {
        [SerializeField] private Image image;
        [SerializeField] private float alpha;
        [SerializeField] private float duration;

        private Tween alphaTween;

        public void NotifyDamage() {
            image.color = image.color.SetAlpha(alpha);

            image.DOFade(0, duration)
                .Replace(ref alphaTween);
        }
    }
}