using UnityEngine;

namespace Exa.Debugging {
    public abstract class DebugBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            DebugManager.DebugChange += OnDebugStateChange;
        }

        public abstract void OnDebugStateChange(DebugMode mode);
    }
}