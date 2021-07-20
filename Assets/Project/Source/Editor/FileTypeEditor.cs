using System.Collections.Generic;
using UnityEngine;

namespace Exa.CustomEditors {
    public abstract class FileTypeEditor {
        internal FileTypeEditorSwitch Context { get; set; }

        public virtual void OnEnable() {
            GUI.enabled = true;
            Context.hideFlags = HideFlags.None;
        }

        public virtual void OnDisable() {
            GUI.enabled = false;
        }

        public abstract IEnumerable<string> GetAcceptedFileTypes();

        public abstract void OnInspectorGUI();
    }
}