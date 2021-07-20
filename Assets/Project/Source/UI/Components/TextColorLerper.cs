using DG.Tweening;
using Exa.Data;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    public class TextColorLerper : MonoBehaviour {
        [SerializeField] private Text text;
        [SerializeField] private ActivePair<Color> color;

        private Tween colorTween;

        public void SetColor(bool active) {
            text.DOColor(color.GetValue(active), 0.1f)
                .Replace(ref colorTween);
        }
    }
}