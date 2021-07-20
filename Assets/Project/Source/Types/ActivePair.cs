using System;

namespace Exa.Data {
    [Serializable]
    public class ActivePair<T> {
        public T active;
        public T inactive;

        public ActivePair(T active, T inactive) {
            this.active = active;
            this.inactive = inactive;
        }

        public T GetValue(bool active) {
            return active ? this.active : inactive;
        }
    }
}