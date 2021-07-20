using Exa.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Exa.UI.Gameplay {
    public class MissionState : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Text headerText;
        [SerializeField] private Text infoText;
        [SerializeField] private ElementTracker headerTracker;
        [SerializeField] private ElementTracker buttonTracker;
        [SerializeField] private ElementTracker infoTracker;

        [Header("Input")]
        [SerializeField] private InputAction editorAction;

        private bool buttonIsVisible;

        private bool ShouldEnableButton {
            get => gameObject.activeSelf && buttonIsVisible;
        }

        private void Awake() {
            editorAction.started += context => { GS.MissionManager.StartEditing(); };
        }

        private void OnEnable() {
            editorAction.SetEnabled(ShouldEnableButton);
        }

        private void OnDisable() {
            editorAction.SetEnabled(false);
        }

        public void SetText(string headerText, string infoText, bool animate = true) {
            this.headerText.text = headerText;
            this.infoText.text = infoText;

            this.DelayOneFrame(
                () => {
                    headerTracker.TrackOnce(animate);
                    infoTracker.TrackOnce(animate);
                }
            );
        }

        public void ShowEditorButton() {
            buttonIsVisible = true;
            editorAction.SetEnabled(ShouldEnableButton);
            this.DelayOneFrame(buttonTracker.TrackOnce);
        }

        public void HideEditorButton() {
            buttonIsVisible = false;
            editorAction.SetEnabled(false);
            buttonTracker.Hide();
        }
    }
}