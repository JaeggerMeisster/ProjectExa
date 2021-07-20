using Exa.SceneManagement;
using UnityEngine;

namespace Exa.AI {
    public abstract class Agent : MonoBehaviour, IAgent {
        protected virtual void OnEnable() {
            GS.AI.Register(this);
        }

        protected virtual void OnDisable() {
            if (GS.Instance != null && GS.AI != null && !GS.AI.GetParentSceneIsUnloading()) {
                GS.AI.Unregister(this);
            }
        }

        public abstract void AIUpdate();
    }
}