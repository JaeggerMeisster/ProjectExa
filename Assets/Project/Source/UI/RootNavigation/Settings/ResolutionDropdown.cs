using System.Linq;
using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI.Settings {
    public class ResolutionDropdown : DropdownControl {
        public void FilterByRefreshRate(int refreshRate) {
            foreach (var value in stateContainer) {
                var resolution = (Resolution) value;
                var tabRefreshRate = resolution.refreshRate;
                var matches = tabRefreshRate == refreshRate;
                stateContainer.GetTab(value).gameObject.SetActive(matches);
            }
        }

        public override void SelectFirst() {
            // Get the first value that has a corresponding active dropdown tab
            Value = stateContainer
                .FirstOrDefault(value => stateContainer.GetTab(value).gameObject.activeSelf);
        }
    }
}