#pragma warning disable CS0649

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace UCommandConsole {
    [Serializable]
    public class OutputEvent : UnityEvent<string> { }

    public class ConsoleInput : MonoBehaviour {
        [Space]
        [SerializeField] private InputField inputField;

        [Space]
        [SerializeField] private InputAction submitAction;

        [Space]
        public OutputEvent submit = new OutputEvent();

        private void Awake() {
            submitAction.performed += OnSubmit;
        }

        private void OnEnable() {
            submitAction.Enable();
            Focus();
        }

        private void OnDisable() {
            submitAction.Disable();
        }

        public void Focus() {
            inputField.ActivateInputField();
        }

        private void OnSubmit(CallbackContext context) {
            if (!inputField.isFocused) {
                return;
            }

            if (inputField.text == "") {
                inputField.ActivateInputField();

                return;
            }

            submit.Invoke(inputField.text);
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }
}