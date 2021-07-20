using System.Linq;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components {
    /// <summary>
    ///     Sets the ignore layout property to true on the given layout element when there are no children on the transform
    /// </summary>
    public class LayoutGroupContext : MonoBehaviour {
        public LayoutElement layoutElement;

        public void UpdateActiveSelf() {
            layoutElement.ignoreLayout = !transform
                .GetChildren()
                .Any(transform => transform.gameObject.activeSelf);
        }
    }
}