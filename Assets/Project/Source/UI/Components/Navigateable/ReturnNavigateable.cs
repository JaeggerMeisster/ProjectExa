using Exa.Audio;
using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;
using static UnityEngine.InputSystem.InputAction;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    public class ReturnNavigateable : Navigateable, IReturnNavigateableActions {
        [SerializeField] private GlobalAudioPlayerProxy audioPlayer;
        private GameControls gameControls;
        private Navigateable returnTarget;

        protected virtual void Awake() {
            gameControls = new GameControls();
            gameControls.ReturnNavigateable.SetCallbacks(this);
        }

        protected virtual void OnEnable() {
            gameControls.Enable();
        }

        protected virtual void OnDisable() {
            gameControls.Disable();
        }

        public void OnReturn(CallbackContext context) {
            if (context.phase != InputActionPhase.Started) {
                return;
            }

            Return();
        }

        public override void HandleEnter(NavigationArgs args) {
            if (!args.isReturning) {
                returnTarget = args.current;
            }

            base.HandleEnter(args);
        }

        public virtual void Return(bool force = false) {
            if (!Interactable && !force) {
                return;
            }

            audioPlayer.Play("UI_SFX_MenuTransitionOut");

            NavigateTo(
                returnTarget,
                new NavigationArgs {
                    current = this,
                    isReturning = true
                }
            );
        }
    }
}