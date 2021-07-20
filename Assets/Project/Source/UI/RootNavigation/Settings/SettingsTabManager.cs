using System;
using Exa.UI.Settings;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    public class SettingsTabManager : MonoBehaviour {
        [HideInInspector] public SettingsTabBase activeSettingsTab;

        [SerializeField] private InteractableAdapter applyButton;
        [SerializeField] private InteractableAdapter setDefaultButton;
        [SerializeField] private Text activeTabText;
        [SerializeField] private SettingsTabBase defaultSettingsTab;
        [SerializeField] private InteractableAdapter interactableAdapter;

        public void Update() {
            applyButton.Interactable = activeSettingsTab.IsDirty;
            setDefaultButton.Interactable = !activeSettingsTab.IsDefault;
        }

        private void OnEnable() {
            ProcessTab(defaultSettingsTab);
        }

        /// <summary>
        ///     Switches to the given if the values are not dirty
        /// </summary>
        /// <param name="settingsTab"></param>
        public void SetActive(SettingsTabBase settingsTab) {
            if (activeSettingsTab == settingsTab) {
                return;
            }

            if (activeSettingsTab != null && !activeSettingsTab.IsDirty) {
                ProcessTab(settingsTab);
            } else {
                QueryUserConfirmation(
                    yes => {
                        if (yes) {
                            activeSettingsTab.ApplyChanges();
                        }

                        ProcessTab(settingsTab);
                    }
                );
            }
        }

        public void QueryUserConfirmation(Action<bool> onClosePrompt) {
            S.UI.Prompts.PromptYesNo(
                "Changes were not saved, do you wish to apply the changes?",
                interactableAdapter,
                onClosePrompt
            );
        }

        /// <summary>
        ///     Switches tab
        /// </summary>
        /// <param name="settingsTab"></param>
        public void ProcessTab(SettingsTabBase settingsTab) {
            activeSettingsTab?.gameObject.SetActive(false);
            activeSettingsTab = settingsTab;
            activeTabText.text = settingsTab.tabName;
            settingsTab.gameObject.SetActive(true);
        }

        public void OnSetDefault() {
            activeSettingsTab.SetDefaultValues();
        }

        public void OnApply() {
            activeSettingsTab.ApplyChanges();
        }
    }
}