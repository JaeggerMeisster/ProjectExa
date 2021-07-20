using UnityEngine;
using UnityEngine.Events;

namespace Exa.UI {
    [RequireComponent(typeof(Hoverable))]
    public class MenuTooltipTrigger : MonoBehaviour {
        public string message;
        public UnityEvent<string> onHover = new UnityEvent<string>();

        private Hoverable hoverable;

        private void Awake() {
            hoverable = GetComponent<Hoverable>();
            hoverable.onPointerEnter.AddListener(() => { onHover?.Invoke(message); });
        }
    }
}