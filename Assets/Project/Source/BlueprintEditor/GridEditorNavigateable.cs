using Exa.UI.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    [RequireComponent(typeof(GridEditor))]
    public class GridEditorNavigateable : ReturnNavigateable {
        [SerializeField] private GridEditor gridEditor;

        public override void Return(bool force = false) {
            if (!Interactable) {
                return;
            }

            var confirmationString = "Are you sure you want to exit without saving?";

            if (!gridEditor.IsSaved) {
                S.UI.Prompts.PromptYesNo(confirmationString, gridEditor, OnClosePrompt);
            } else {
                DoReturn(force);
            }
        }

        private void OnClosePrompt(bool confirm) {
            if (confirm) {
                DoReturn(true);
            }
        }

        private void DoReturn(bool force) {
            gridEditor.ImportArgs.OnExit?.Invoke();
            base.Return(force);
        }
    }
}