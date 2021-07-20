using Exa.UI.Components;
using UnityEditor;

namespace Exa.CustomEditors {
    [CustomEditor(typeof(UIAnimateable))]
    [CanEditMultipleObjects]
    public class UIAnimateableEditor : Editor {
        private SerializedProperty alphaSpeed;
        private SerializedProperty animateAlpha;
        private SerializedProperty movementDirection;
        private SerializedProperty movementMagnitude;
        private SerializedProperty movementSmoothDamp;
        private SerializedProperty msLocalAnimationOffset;

        private void OnEnable() {
            msLocalAnimationOffset = serializedObject.FindProperty("msLocalAnimationOffset");
            movementDirection = serializedObject.FindProperty("movementDirection");
            movementSmoothDamp = serializedObject.FindProperty("movementSmoothDamp");
            movementMagnitude = serializedObject.FindProperty("movementMagnitude");
            animateAlpha = serializedObject.FindProperty("animateAlpha");
            alphaSpeed = serializedObject.FindProperty("alphaSpeed");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(msLocalAnimationOffset);
            EditorGUILayout.PropertyField(movementDirection);

            if ((AnimationDirection) movementDirection.enumValueIndex != AnimationDirection.none) {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.Slider(movementSmoothDamp, 0f, 1f);
                EditorGUILayout.PropertyField(movementMagnitude);
                EditorGUI.indentLevel = 0;
            }

            EditorGUILayout.PropertyField(animateAlpha);

            if (animateAlpha.boolValue) {
                EditorGUI.indentLevel = 1;
                EditorGUILayout.Slider(alphaSpeed, 0f, 10f);
                EditorGUI.indentLevel = 0;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}