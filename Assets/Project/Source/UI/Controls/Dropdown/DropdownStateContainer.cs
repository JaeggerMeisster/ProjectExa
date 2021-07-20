using System.Collections;
using System.Collections.Generic;
using Exa.Types.Generics;

namespace Exa.UI.Controls {
    public class DropdownStateContainer<T> : IEnumerable<T> {
        private readonly Dictionary<T, ILabeledValue<T>> contextByValue = new Dictionary<T, ILabeledValue<T>>();
        private readonly Dictionary<T, DropdownTab> tabByValue = new Dictionary<T, DropdownTab>();
        private readonly HashSet<T> values = new HashSet<T>();

        public IEnumerator<T> GetEnumerator() {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return values.GetEnumerator();
        }

        public void Add(ILabeledValue<T> namedValue, DropdownTab tab) {
            contextByValue.Add(namedValue.Value, namedValue);
            tabByValue.Add(namedValue.Value, tab);
            values.Add(namedValue.Value);
        }

        public bool ContainsValue(T value) {
            return values.Contains(value);
        }

        public string GetName(T value) {
            try {
                return contextByValue[value].Label;
            } catch (KeyNotFoundException) {
                throw new KeyNotFoundException($"value {value} doesn't exist");
            }
        }

        public DropdownTab GetTab(T value) {
            return tabByValue[value];
        }
    }
}