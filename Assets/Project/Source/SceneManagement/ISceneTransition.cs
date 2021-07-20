using UnityEngine.Events;

namespace Exa.SceneManagement {
    public interface ISceneTransition {
        UnityEvent onPrepared { get; }

        void MarkPrepared();
    }
}