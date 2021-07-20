using UnityEngine;
using UnityEngine.UI;

namespace Exa.Utils {
    public static class LayoutElementUtils {
        public static void SetPreferredSize(this LayoutElement layoutElement, Vector2 size) {
            layoutElement.preferredWidth = size.x;
            layoutElement.preferredHeight = size.y;
        }
    }
}