using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    // TODO: Hide tooltip when not hovering over a tab button
    public class NavigateableTabManager : MonoBehaviour {
        [SerializeField] private Navigateable defaultTab;

        public Navigateable ActiveTab { get; private set; }

        private void Start() {
            if (defaultTab != null) {
                SetDefaultActive(defaultTab);
            }
        }

        public void SetDefaultActive(Navigateable tab) {
            ActiveTab = tab;
            ActiveTab.HandleEnter(null);
        }

        public void SwitchTo(Navigateable newTab) {
            if (newTab == ActiveTab) {
                return;
            }

            ActiveTab.NavigateTo(
                newTab,
                new NavigationArgs {
                    current = ActiveTab
                }
            );

            ActiveTab = newTab;
        }
    }
}