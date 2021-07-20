using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor {
    public abstract class PropertyDrawerBase : PropertyDrawer {
        public sealed override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
            // Check if visible
            var visible = PropertyUtility.IsVisible(property);

            if (!visible) {
                return;
            }

            // Validate
            var validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);

            foreach (var validatorAttribute in validatorAttributes) {
                validatorAttribute.GetValidator().ValidateProperty(property);
            }

            // Check if enabled and draw
            EditorGUI.BeginChangeCheck();
            var enabled = PropertyUtility.IsEnabled(property);

            using (new EditorGUI.DisabledScope(!enabled)) {
                OnGUI_Internal(rect, property, new GUIContent(PropertyUtility.GetLabel(property)));
            }

            // Call OnValueChanged callbacks
            if (EditorGUI.EndChangeCheck()) {
                PropertyUtility.CallOnValueChangedCallbacks(property);
            }
        }

        protected abstract void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label);

        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var visible = PropertyUtility.IsVisible(property);

            if (!visible) {
                return 0.0f;
            }

            return GetPropertyHeight_Internal(property, label);
        }

        protected virtual float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label) {
            return base.GetPropertyHeight(property, label);
        }

        protected float GetPropertyHeight(SerializedProperty property) {
            var specialCaseAttribute = PropertyUtility.GetAttribute<SpecialCaseDrawerAttribute>(property);

            if (specialCaseAttribute != null) {
                return specialCaseAttribute.GetDrawer().GetPropertyHeight(property);
            }

            return EditorGUI.GetPropertyHeight(property, true);
        }

        public virtual float GetHelpBoxHeight() {
            return EditorGUIUtility.singleLineHeight * 2.0f;
        }

        public void DrawDefaultPropertyAndHelpBox(Rect rect, SerializedProperty property, string message, MessageType messageType) {
            var indentLength = NaughtyEditorGUI.GetIndentLength(rect);

            var helpBoxRect = new Rect(
                rect.x + indentLength,
                rect.y,
                rect.width - indentLength,
                GetHelpBoxHeight()
            );

            NaughtyEditorGUI.HelpBox(helpBoxRect, message, MessageType.Warning, property.serializedObject.targetObject);

            var propertyRect = new Rect(
                rect.x,
                rect.y + GetHelpBoxHeight(),
                rect.width,
                GetPropertyHeight(property)
            );

            EditorGUI.PropertyField(propertyRect, property, true);
        }
    }
}