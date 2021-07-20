using UnityEngine;

namespace Exa.Utils {
    public class MonoSingleton<T> : MonoSingleton
        where T : MonoSingleton<T> {
        protected static T instance;

        /// <summary>
        ///     Static reference that can only be set once internally
        /// </summary>
        public static T Instance {
            private set {
                if (instance != null) {
                    Debug.LogWarning($"Instance value has already been set on type {typeof(T)}");
                } else {
                    instance = value;
                }
            }
            get => instance;
        }

        /// <summary>
        ///     Get the component
        /// </summary>
        protected virtual void Awake() {
            if (instance == null) {
                Instance = GetComponent<T>();
            }
        }
    }

    public abstract class MonoSingleton : MonoBehaviour { }
}