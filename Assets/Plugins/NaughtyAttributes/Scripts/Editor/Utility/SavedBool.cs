using UnityEditor;

namespace NaughtyAttributes.Editor {
    internal class SavedBool {
        private readonly string _name;
        private bool _value;

        public SavedBool(string name, bool value) {
            _name = name;
            _value = EditorPrefs.GetBool(name, value);
        }

        public bool Value {
            get => _value;
            set {
                if (_value == value) {
                    return;
                }

                _value = value;
                EditorPrefs.SetBool(_name, value);
            }
        }
    }
}