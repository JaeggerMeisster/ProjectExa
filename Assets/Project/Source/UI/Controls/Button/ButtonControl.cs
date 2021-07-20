using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls {
    public class ButtonControl : MonoBehaviour {
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private LayoutElement layoutElement;

        public Button Button {
            get => button;
        }

        public LayoutElement LayoutElement {
            get => layoutElement;
        }

        public UnityEvent OnClick {
            get => button.onClick;
        }

        public static ButtonControl Create(Transform container, string label) {
            return S.UI.Controls.CreateButton(container, label);
        }

        public void Setup(string label) {
            text.text = label;
        }
    }
}