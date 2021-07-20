using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips {
    [Serializable]
    public class TextView : TooltipComponentView<TooltipText> {
        [SerializeField] private Text text;

        public void SetFont(Font font) {
            text.font = font;
        }

        protected override void Refresh(TooltipText value) {
            text.text = value.Text;
        }
    }
}