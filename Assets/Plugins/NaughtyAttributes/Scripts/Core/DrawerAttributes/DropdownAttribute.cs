using System;
using System.Collections;
using System.Collections.Generic;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownAttribute : DrawerAttribute {
        public DropdownAttribute(string valuesName) {
            ValuesName = valuesName;
        }

        public string ValuesName { get; }
    }

    public interface IDropdownList : IEnumerable<KeyValuePair<string, object>> { }

    public class DropdownList<T> : IDropdownList {
        private readonly List<KeyValuePair<string, object>> _values;

        public DropdownList() {
            _values = new List<KeyValuePair<string, object>>();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(string displayName, T value) {
            _values.Add(new KeyValuePair<string, object>(displayName, value));
        }

        public static explicit operator DropdownList<object>(DropdownList<T> target) {
            var result = new DropdownList<object>();

            foreach (var kvp in target) {
                result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }
    }
}