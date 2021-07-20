using System.Collections.Generic;
using System.Linq;

namespace Exa.Utils {
    public class PropertyAdapter : IEnumerable<string> {
        private object obj;

        public PropertyAdapter(object obj) {
            this.obj = obj;
        }

        public override string ToString() {
            return obj.ToString();
        }

        public object this[string name] {
            get => obj.GetType().GetProperty(name)?.GetValue(obj, null);
        }

        public IEnumerable<(string, object)> GetNamesAndValues() {
            return this.Select(name => (name, this[name]));
        }

        public IEnumerator<string> GetEnumerator() {
            return obj.GetType()
                .GetProperties()
                .Select(pi => pi.Name)
                .GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}