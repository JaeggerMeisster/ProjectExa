using System;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips {
    [Serializable]
    public class TitleView : TooltipComponentView<TooltipTitle> {
        [SerializeField] private Text text;

        public void SetFont(Font font) {
            text.font = font;
        }

        public void AddAnimator() {
            var animator = text.gameObject.AddComponent<UIAnimateable>();
            animator.msLocalAnimationOffset = 200f;
        }

        protected override void Refresh(TooltipTitle data) {
            text.text = data.Text;
        }
    }
}