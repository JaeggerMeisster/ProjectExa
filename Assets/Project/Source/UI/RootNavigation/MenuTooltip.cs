using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class MenuTooltip : MonoBehaviour {
        [SerializeField] private Text text;

        public void DisplayTooltip(string message) {
            if (message == "") {
                gameObject.SetActive(false);

                return;
            }

            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }

            text.text = message;
        }
    }
}