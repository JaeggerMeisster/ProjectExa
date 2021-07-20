using System;
using Exa.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI.Controls {
    [Serializable]
    public class RadioControl : InputControl<bool> {
        [SerializeField] private Image buttonImage;
        [SerializeField] private ActivePair<Color> colors;

        private RadioCheckEvent onValueChange = new RadioCheckEvent();
        private bool value;

        public override bool Value {
            get => value;
            protected set {
                this.value = value;
                buttonImage.color = colors.GetValue(value);
            }
        }

        public override UnityEvent<bool> OnValueChange {
            get => onValueChange;
        }

        public static RadioControl Create(Transform container, string label, Action<bool> setter) {
            return S.UI.Controls.CreateRadio(container, label, setter);
        }

        public void Toggle() {
            Value = !value;
        }

        [Serializable]
        public class RadioCheckEvent : UnityEvent<bool> { }
    }
}