using Exa.UI;
using UnityEditor;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(InteractableAdapter))]
    internal class InteractableAdapterEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            ((InteractableAdapter) target).Interactable = ((InteractableAdapter) target).Interactable;
        }
    }
}