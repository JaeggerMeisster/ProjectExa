using System;
using UnityEngine;

namespace Exa.Types.Generics {
    [Serializable]
    public class ValueOverride<T> : IValueOverride<T> {
        [SerializeField] private T value;

        public ValueOverride(T value) {
            this.value = value;
        }

        public T Value {
            get => value;
            set => this.value = value;
        }
    }
}