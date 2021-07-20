using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Controls {
    public class KeybindingView : MonoBehaviour {
        [SerializeField] private Text text;

        public void SetKey(string key) {
            text.text = key;
        }
    }
}