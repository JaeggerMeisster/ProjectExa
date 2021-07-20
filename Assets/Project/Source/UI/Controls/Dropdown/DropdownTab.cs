using Exa.Data;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Controls {
    [RequireComponent(typeof(Button))]
    public class DropdownTab : MonoBehaviour {
        public Button button;

        [SerializeField] private Text text;
        [SerializeField] private ActivePair<Color> color;

        public string Text {
            set => text.text = value;
        }

        public bool Selected {
            set => text.color = color.GetValue(value);
        }

        private void Awake() {
            Selected = false;
        }
    }
}