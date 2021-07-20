using UnityEngine;
using UnityEngine.Events;

namespace Exa.SceneManagement {
    public class SceneTransition : ISceneTransition {
        public SceneTransition(AsyncOperation loadOperation) {
            onPrepared = new UnityEvent();
            loadOperation.completed += op => MarkPrepared();
        }

        public UnityEvent onPrepared { get; }

        public void MarkPrepared() {
            onPrepared?.Invoke();
        }
    }
}