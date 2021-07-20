using System;
using Exa.Types.Generics;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips {
    [Serializable]
    public class PropertyView : TooltipComponentView<LabeledValue<object>> {
        [Header("References")]
        [SerializeField] private Text keyText;
        [SerializeField] private Text valueText;

        public void SetFont(Font font) {
            keyText.font = font;
            valueText.font = font;
        }

        public void SetValue(object value) {
            valueText.text = value.ToString();
        }

        public void SetLabel(string label) {
            keyText.text = label;
        }

        protected override void Refresh(LabeledValue<object> labeledValue) {
            keyText.text = labeledValue.Label;
            valueText.text = labeledValue.Value.ToString();
        }
    }
}