using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(DefaultAsset))]
    public class FileTypeEditorSwitch : Editor {
        private readonly FileTypeEditor[] editors = {
            new AseEditor()
        };

        private FileTypeEditor selectedEditor;

        public AssetImporter Importer { get; private set; }

        public string AssetPath { get; private set; }

        public bool EnableLogging { get; private set; }

        private void OnEnable() {
            var path = AssetDatabase.GetAssetPath(target);

            EnableLogging = EditorPrefs.GetBool("AsepriteTools_EnableLogging", false);

            foreach (var editor in editors) {
                editor.Context = this;

                if (editor.GetAcceptedFileTypes().Any(fileType => path.EndsWith(fileType))) {
                    Importer = AssetImporter.GetAtPath(path);
                    AssetPath = path;
                    editor.OnEnable();
                    selectedEditor = editor;

                    return;
                }
            }
        }

        private void OnDisable() {
            selectedEditor?.OnDisable();
            Importer = null;
            selectedEditor = null;

            EditorPrefs.SetBool("AsepriteTools_EnableLogging", EnableLogging);
        }

        public override void OnInspectorGUI() {
            if (selectedEditor != null) {
                GUI.enabled = true;

                EnableLogging = EditorGUILayout.Toggle("Enabled logging", EnableLogging);

                selectedEditor.OnInspectorGUI();
            } else {
                base.OnInspectorGUI();
            }
        }
    }
}