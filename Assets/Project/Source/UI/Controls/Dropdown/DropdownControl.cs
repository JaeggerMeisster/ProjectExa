using System;
using System.Collections.Generic;
using System.Linq;
using Coffee.UIEffects;
using Exa.Audio;
using Exa.Types.Generics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Exa.UI.Controls {
    [Serializable]
    public class DropdownControl : InputControl<object> {
        [SerializeField] private Text selectedText;
        [SerializeField] private GlobalAudioPlayerProxy playerProxy;
        [SerializeField] private UIFlip tabArrow;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform selectedTab;
        [SerializeField] private RectTransform tabContainer;
        [SerializeField] private GameObject tabPrefab;
        [SerializeField] private InputAction clickAction;

        [SerializeField] private DropdownTabSelected onValueChange = new DropdownTabSelected();

        private bool isFoldedOpen;

        protected DropdownStateContainer<object> stateContainer = new DropdownStateContainer<object>();
        private object value;

        public override object Value {
            get => value;
            protected set {
                if (!stateContainer.ContainsValue(value)) {
                    throw new ArgumentException($"Value {value} not found", nameof(value));
                }

                if (stateContainer.ContainsValue(this.value)) {
                    stateContainer.GetTab(this.value).Selected = false;
                }

                this.value = value;
                var name = stateContainer.GetName(value);
                stateContainer.GetTab(value).Selected = true;
                selectedText.text = name;
            }
        }

        private bool IsFoldedOpen {
            get => isFoldedOpen;
            set {
                isFoldedOpen = value;

                tabContainer.gameObject.SetActive(value);
                tabArrow.vertical = value;
                playerProxy.Play(value ? "UI_SFX_ButtonSelectPositive" : "UI_SFX_ButtonSelectNegative");
            }
        }

        public override UnityEvent<object> OnValueChange {
            get => onValueChange;
        }

        protected virtual void Awake() {
            button.onClick.AddListener(ToggleContainer);

            clickAction.started += context => {
                if (IsFoldedOpen && GetMouseOutsideControl()) {
                    IsFoldedOpen = false;
                }
            };
        }

        protected void OnEnable() {
            clickAction.Enable();
        }

        private void OnDisable() {
            clickAction.Disable();
        }

        public static DropdownControl Create<T>(
            Transform container,
            string label,
            IEnumerable<ILabeledValue<T>> possibleValues,
            Action<T> setter,
            Action<T, DropdownTab> onTabCreated = null
        ) {
            return S.UI.Controls.CreateDropdown(
                container,
                label,
                possibleValues,
                setter,
                onTabCreated
            );
        }

        public virtual void CreateTabs<T>(
            IEnumerable<ILabeledValue<T>> options,
            Action<T, DropdownTab> onTabCreated = null
        ) {
            foreach (var option in options) {
                var tab = Instantiate(tabPrefab, tabContainer).GetComponent<DropdownTab>();
                tab.Text = option.Label;

                tab.button.onClick.AddListener(
                    () => {
                        SetValue(option.Value);
                        ToggleContainer();
                    }
                );

                onTabCreated?.Invoke(option.Value, tab);

                stateContainer.Add(option as ILabeledValue<object>, tab);
            }

            SelectFirst();
        }

        public bool ContainsItem(object item) {
            return stateContainer.ContainsValue(item);
        }

        public virtual void SelectFirst() {
            SetValue(stateContainer.First());
        }

        private void ToggleContainer() {
            IsFoldedOpen = !IsFoldedOpen;
        }

        private bool GetMouseOutsideControl() {
            return !S.Input.GetMouseInsideRect(tabContainer, selectedTab);
        }

        [Serializable]
        public class DropdownTabSelected : UnityEvent<object> { }
    }
}