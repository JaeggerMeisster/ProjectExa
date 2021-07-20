using Exa.Types;
using UnityEditor;
using UnityEngine;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(ScriptableObjectBagBase), true)]
    public class ScriptableObjectBagEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (GUILayout.Button("Find objects")) {
                var bag = target as ScriptableObjectBagBase;
                bag.FindObjects();
                EditorUtility.SetDirty(bag);
            }
        }
    }
}