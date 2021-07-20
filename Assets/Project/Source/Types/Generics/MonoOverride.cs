using UnityEngine;

namespace Exa.Types.Generics {
    public abstract class MonoOverride<T> : MonoBehaviour, IValueOverride<T> {
        [SerializeField] protected T value;

        protected virtual void OnEnable() {
            GetPath().Add(this);
        }

        protected virtual void OnDisable() {
            GetPath().Remove(this);
        }

        public T Value {
            get => value;
        }

        protected abstract OverrideList<T> GetPath();
    }
}