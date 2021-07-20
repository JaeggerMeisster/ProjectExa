using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Controls {
    public abstract class InputControl<T> : InputControl {
        /// <summary>
        ///     Gets the current value
        /// </summary>
        public abstract T Value { get; protected set; }

        public abstract UnityEvent<T> OnValueChange { get; }

        public virtual void SetValue(T value, bool notify = true) {
            Value = value;

            if (notify) {
                OnValueChange?.Invoke(value);
            }
        }
    }

    public abstract class InputControl : MonoBehaviour {
        [SerializeField] private Text labelText;

        public void SetLabelText(string label) {
            labelText.text = label;
        }
    }
}