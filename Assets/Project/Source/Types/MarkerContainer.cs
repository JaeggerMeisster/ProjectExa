using System;

namespace Exa.Types {
    public class MarkerContainer {
        private readonly Action<bool> onChangeContainsItem;
        private int count;

        public MarkerContainer(Action<bool> onChangeContainsItem) {
            this.onChangeContainsItem = onChangeContainsItem;
        }

        public void AddMarker() {
            count++;

            if (count == 1) {
                onChangeContainsItem(true);
            }
        }

        public void RemoveMarker() {
            count--;

            if (count == 0) {
                onChangeContainsItem(false);
            }

            if (count < 0) {
                throw new Exception("Marker count cannot be below 0");
            }
        }
    }
}