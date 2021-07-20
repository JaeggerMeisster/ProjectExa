using System;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Utils {
    public static class DictionaryExtensions {
        public static IEnumerable<(TKey key, TValue value)> Unpack<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) {
            return dictionary.Select(kvp => (kvp.Key, kvp.Value));
        }

        public static TValue Get<Tkey, TValue>(this IDictionary<Tkey, TValue> dictionary, Tkey key)
            where TValue : class {
            return dictionary.ContainsKey(key) ? dictionary[key] : null;
        }

        public static void EnsureCreated<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> factory
        ) {
            if (!dictionary.ContainsKey(key)) {
                dictionary.Add(key, factory());
            }
        }

        public static bool NestedContainsKey<TKey1, TKey2, TValue1>(
            this IDictionary<TKey1, IDictionary<TKey2, TValue1>> dictionary,
            TKey1 key1,
            TKey2 key2
        ) {
            return
                dictionary.ContainsKey(key1) &&
                dictionary[key1].ContainsKey(key2);
        }

        public static IEnumerable<TValue> WhereKeys<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Func<TKey, bool> predicate
        ) {
            return dictionary.Where(kvp => predicate(kvp.Key))
                .Select(kvp => kvp.Value);
        }
    }
}