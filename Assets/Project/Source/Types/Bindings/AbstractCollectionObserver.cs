using UnityEngine;

namespace Exa.Types.Binding {
    public abstract class AbstractCollectionObserver<T> : MonoBehaviour, ICollectionObserver<T> {
        private IObservableEnumerable<T> source;

        public virtual IObservableEnumerable<T> Source {
            get => source;
            set {
                // If the new source is the same, do nothing
                if (source == value) {
                    return;
                }

                // If there is already a source, unregister
                if (source != null) {
                    // Unregistered if we are registered
                    if (source.Observers.Contains(this)) {
                        source.Observers.Remove(this);
                    }

                    source = null;
                }

                // if the new value is supposed to be null, clear views and return
                if (value == null) {
                    OnClear();

                    return;
                }

                // Set source and register
                source = value;
                source.Observers.Add(this);

                // Clear views
                OnClear();

                foreach (var item in source) {
                    OnAdd(item);
                }
            }
        }

        public abstract void OnAdd(T value);

        public abstract void OnRemove(T value);

        public abstract void OnClear();
    }
}