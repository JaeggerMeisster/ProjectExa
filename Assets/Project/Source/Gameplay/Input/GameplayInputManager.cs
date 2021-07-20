using Exa.Input;
using UnityEngine;
using static Exa.Input.GameControls;

#pragma warning disable 649

namespace Exa.Gameplay {
    public partial class GameplayInputManager : MonoBehaviour, IGameplayActions {
        private GameControls gameControls;

        public void Awake() {
            gameControls = new GameControls();
            gameControls.Gameplay.SetCallbacks(this);
        }

        public void Update() {
            if (IsSelectingArea) {
                OnUpdateSelectionArea();
            }
        }

        public void OnEnable() {
            gameControls.Enable();
        }

        public void OnDisable() {
            gameControls.Disable();
        }
    }
}