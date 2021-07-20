using System;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls {
    public class InputFieldControl : InputControl<string> {
        public Text placeholderText;
        public ExtendedInputField inputField;

        [SerializeField] private readonly InputFieldEvent onValueChange = new InputFieldEvent();

        public override string Value {
            get => inputField.text;
            protected set => inputField.SetTextWithoutNotify(value);
        }

        public override UnityEvent<string> OnValueChange {
            get => onValueChange;
        }

        private void Awake() {
            inputField.onEndEdit.AddListener(onValueChange.Invoke);
        }

        public static InputFieldControl Create(Transform container, string label, Action<string> setter) {
            return S.UI.Controls.CreateInputField(container, label, setter);
        }

        public void SetValueWithoutNotify(string value) {
            inputField.SetTextWithoutNotify(value);
        }

        public void Setup(string valuePlaceholder, string value = "") {
            placeholderText.text = valuePlaceholder;
            inputField.text = value;
        }

        [Serializable]
        public class InputFieldEvent : UnityEvent<string> { }
    }
}