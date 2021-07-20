using System;
using Exa.UI.Components;

namespace Exa.UI.Gameplay {
    public class PauseMenu : ReturnNavigateable {
        public bool Paused { get; private set; }

        public override void HandleEnter(NavigationArgs args) {
            GS.Raycaster.IsRaycasting = false;
            Paused = true;
            base.HandleEnter(args);
        }

        public override void HandleExit(Navigateable target) {
            GS.Raycaster.IsRaycasting = true;
            Paused = false;
            base.HandleExit(target);
        }

        public void Save() {
            throw new NotImplementedException();
        }

        public void Load() {
            throw new NotImplementedException();
        }

        public void QuitToMenu() {
            GS.Navigateable.NavigateTo(S.UI.Root.navigateable);
            S.Scenes.UnloadAsync("Game");
            GS.MissionManager.UnloadMission();
        }

        public void QuitToDesktop() {
            S.Quit();
        }
    }
}