using Exa.UI.Components;

namespace Exa.UI {
    public class ExitNavigateableTabButton : Navigateable {
        public override void HandleEnter(NavigationArgs args) {
            Exit();
        }

        public void Exit() {
            S.Quit();
        }
    }
}