using UnityEngine;

namespace Exa.UI.Components {
    public class NavigationArgs {
        public Navigateable current;
        public bool isReturning;
    }

    // TODO: Add a manager that implements a command pattern to store user Navigation actions
    public class Navigateable : MonoBehaviour, IUIGroup {
        public bool Interactable { get; set; } = true;

        public virtual void HandleExit(Navigateable target) {
            gameObject.SetActive(false);
        }

        public virtual void HandleEnter(NavigationArgs args) {
            gameObject.SetActive(true);
        }

        public virtual void NavigateTo(Navigateable target, NavigationArgs args = null) {
            HandleExit(target);

            target.HandleEnter(
                args ??
                new NavigationArgs {
                    current = this
                }
            );
        }
    }
}