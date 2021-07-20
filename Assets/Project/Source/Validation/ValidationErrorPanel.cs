using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.Validation {
    /// <summary>
    ///     Validation error view
    /// </summary>
    public class ValidationErrorPanel : MonoBehaviour {
        [SerializeField] private Text text;

        public string Text {
            set => text.text = value;
        }
    }
}