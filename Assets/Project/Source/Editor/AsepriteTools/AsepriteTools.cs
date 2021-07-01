using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Exa.IO;
using UnityEditor;
using UnityEngine;

namespace Exa.CustomEditors {
    interface IEditorAction {
        void Render();

        void Execute(string asepritePath, string filePath);
    }

    [Serializable]
    public class AsepriteTools : EditorWindow {
        [MenuItem("Window/AsepriteTools")]
        public static void ShowWindow() {
            GetWindow<AsepriteTools>();
        }

        private void OnEnable() {
            actions = new IEditorAction[] {
                new ExportInPlaceAction()
            };

            displayActions = actions.Select(x => x.GetType().Name).ToArray();

            asepritePath = PlayerPrefs.GetString("AsepriteTools_AsepritePath", "");

            GetPaths();
        }

        [SerializeField] private string asepritePath;
        private IEditorAction[] actions;
        private string[] displayActions;

        [SerializeField] private string renderError;
        [SerializeField] private string executionError;

        [SerializeField] private int selectedPath;
        [SerializeField] private int selectedAction;

        private string[] paths;
        private string[] displayPaths;

        private void GetPaths() {
            var path = IOUtils.CombinePath(Application.dataPath, "Project", "Graphics");

            paths = Directory.EnumerateFiles(path, "*.ase", SearchOption.AllDirectories).ToArray();

            displayPaths = paths.Select(
                    x => x
                        .Replace(path + "\\", String.Empty)
                        .Replace(".aseprite", String.Empty)
                        .Replace(".ase", String.Empty)
                )
                .ToArray();
        }

        void OnGUI() {
            GUILayout.BeginHorizontal();

            {
                if (GUILayout.Button("Locate aseprite executeable")) {
                    LocateAseprite();
                }

                if (GUILayout.Button("Start aseprite")) {
                    Process.Start(asepritePath);
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(16);

            selectedPath = EditorGUILayout.Popup("Select file ...", selectedPath, displayPaths);
            selectedAction = EditorGUILayout.Popup("Select action ...", selectedAction, displayActions);

            GUILayout.Space(8);
            RenderAction();
            GUILayout.Space(8);

            if (!string.IsNullOrEmpty(asepritePath)) {
                if (GUILayout.Button("Execute action")) {
                    try {
                        actions[selectedAction].Execute(asepritePath, paths[selectedPath]);
                        executionError = null;
                    } catch (Exception e) {
                        executionError = e.Message;
                    }
                }
            } else {
                EditorGUILayout.HelpBox("Aseprite path not set", MessageType.Warning);
            }

            if (executionError != null) {
                GUILayout.Space(8);
                EditorGUILayout.HelpBox("Execution error: " + executionError, MessageType.Error);
            }
        }

        private void LocateAseprite() {
            asepritePath = EditorUtility.OpenFilePanelWithFilters(
                "Locate aseprite",
                "",
                new[] {
                    "Executeable",
                    "exe"
                }
            );

            EditorPrefs.SetString("AsepriteTools_AsepritePath", asepritePath);
        }

        private void RenderAction() {
            try {
                actions[selectedAction].Render();
                renderError = null;
            } catch (Exception e) {
                renderError = e.Message;
            }

            if (renderError != null) {
                EditorGUILayout.HelpBox("Render error: " + renderError, MessageType.Error);
            }
        }
    }
}