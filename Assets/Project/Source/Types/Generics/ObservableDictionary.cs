using System.Collections.Generic;
using Exa.Types.Binding;

namespace Exa.Types.Generics {
    // NOTE: Poor lookup time, don't use for large datasets
    public class ObservableDictionary<TKey, TValue> : ObservableCollection<TValue>
        where TValue : IKeySelector<TKey> {
        private readonly IEqualityComparer<TKey> comparer;

        public ObservableDictionary(IEqualityComparer<TKey> comparer) {
            this.comparer = comparer;
        }

        public TValue this[TKey key] {
            get {
                foreach (var item in this) {
                    if (comparer.Equals(key, KeySelector(item))) {
                        return item;
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        public bool ContainsKey(TKey key) {
            foreach (var item in this) {
                if (comparer.Equals(key, KeySelector(item))) {
                    return true;
                }
            }

            return false;
        }

        public override bool Contains(TValue item) {
            return ContainsKey(KeySelector(item));
        }

        protected virtual TKey KeySelector(TValue value) {
            return value.Key;
        }
    }
}