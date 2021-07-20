using Exa.UI.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Settings {
    public class SettingsReturnNavigateable : ReturnNavigateable {
        [SerializeField] private SettingsTabManager settingsTabManager;

        public override void Return(bool force = false) {
            if (settingsTabManager.activeSettingsTab.IsDirty) {
                settingsTabManager.QueryUserConfirmation(
                    yes => {
                        if (yes) {
                            settingsTabManager.activeSettingsTab.ApplyChanges();
                        } else {
                            base.Return(true);
                        }
                    }
                );
            } else {
                base.Return(force);
            }
        }
    }
}