using Exa.UI;
using UnityEditor;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(Hoverable))]
    [CanEditMultipleObjects]
    public class HoverableEditor : Editor {
        private SerializedProperty checkMouseInsideRectOnEnable;
        private SerializedProperty cursorState;
        private SerializedProperty invokeStateChangeOnHover;
        private SerializedProperty onPointerEnter;
        private SerializedProperty onPointerExit;

        protected void OnEnable() {
            onPointerEnter = serializedObject.FindProperty(nameof(Hoverable.onPointerEnter));
            onPointerExit = serializedObject.FindProperty(nameof(Hoverable.onPointerExit));
            invokeStateChangeOnHover = serializedObject.FindProperty(nameof(Hoverable.invokeStateChangeOnHover));
            cursorState = serializedObject.FindProperty(nameof(Hoverable.cursorOverride));
            checkMouseInsideRectOnEnable = serializedObject.FindProperty(nameof(Hoverable.checkMouseInsideRectOnEnable));
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(checkMouseInsideRectOnEnable);
            EditorGUILayout.PropertyField(invokeStateChangeOnHover);

            if (invokeStateChangeOnHover.boolValue) {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.PropertyField(cursorState);
                EditorGUI.indentLevel = 0;
            }

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(onPointerEnter);
            EditorGUILayout.PropertyField(onPointerExit);

            serializedObject.ApplyModifiedProperties();
        }
    }
}