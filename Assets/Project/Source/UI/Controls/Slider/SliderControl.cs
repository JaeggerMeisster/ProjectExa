using System;
using Exa.Types.Generics;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Slider;

#pragma warning disable CS0649

namespace Exa.UI.Controls {
    public class SliderControl : InputControl<float> {
        [SerializeField] private Slider slider;
        [SerializeField] private ExtendedInputField inputField;
        [SerializeField] private ValueOverride<CursorState> cursorState;

        [SerializeField] private readonly SliderEvent onValueChanged = new SliderEvent();

        public override float Value {
            get => slider.value;
            protected set {
                slider.value = value;
                inputField.text = FormatFloat(value);
            }
        }

        public override UnityEvent<float> OnValueChange {
            get => onValueChanged;
        }

        private void Awake() {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            slider.onValueChanged.AddListener(onValueChanged.Invoke);
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
            inputField.text = FormatFloat(slider.value);
        }

        public static SliderControl Create(
            Transform container,
            string label,
            Action<float> setter,
            MinMax<float>? minMax = null
        ) {
            return S.UI.Controls.CreateSlider(container, label, setter, minMax);
        }

        public void SetMinMax(MinMax<float> minMax) {
            slider.minValue = minMax.min;
            slider.maxValue = minMax.max;
        }

        public void SetMax() {
            slider.value = slider.maxValue;
        }

        public void SetMin() {
            slider.value = slider.minValue;
        }

        public void OnBeginDrag() {
            S.UI.MouseCursor.stateManager.Add(cursorState);
        }

        public void OnEndDrag() {
            S.UI.MouseCursor.stateManager.Remove(cursorState);
        }

        private void OnSliderValueChanged(float value) {
            inputField.text = FormatFloat(value);
        }

        private void OnInputFieldValueChanged(string value) {
            if (string.IsNullOrEmpty(value)) {
                return;
            }

            var valid = int.TryParse(value, out var intValue);

            if (valid) {
                inputField.SetTextWithoutNotify(value);
            } else {
                inputField.SetTextWithoutNotify(FormatFloat(slider.value));

                return;
            }

            if (intValue < slider.minValue) {
                inputField.text = FormatFloat(slider.minValue);
            }

            if (intValue > slider.maxValue) {
                inputField.text = FormatFloat(slider.maxValue);
            }
        }

        private void OnInputFieldEndEdit(string value) {
            var valid = float.TryParse(value, out var floatValue);

            if (valid) {
                slider.value = (float) System.Math.Round(floatValue, 2);
            } else {
                inputField.text = FormatFloat(slider.value);
            }
        }

        private string FormatFloat(float value) {
            return System.Math.Round(value, 2).ToString();
        }
    }
}