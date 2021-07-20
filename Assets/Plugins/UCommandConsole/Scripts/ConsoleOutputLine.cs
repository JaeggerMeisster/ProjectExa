#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

namespace UCommandConsole {
    public class ConsoleOutputLine : MonoBehaviour {
        public Text text;

        [SerializeField] private LayoutElement indentationLayoutElement;

        public int Indentation {
            set {
                var width = value * 16;
                indentationLayoutElement.minWidth = width;
                indentationLayoutElement.preferredWidth = width;
            }
        }
    }
}