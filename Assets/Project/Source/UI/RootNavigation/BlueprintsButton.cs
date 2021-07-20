using UnityEngine;

namespace Exa.UI {
    public class BlueprintsButton : MonoBehaviour {
        private void Start() {
            if (!Application.isEditor) {
                gameObject.SetActive(false);
            }
        }
    }
}