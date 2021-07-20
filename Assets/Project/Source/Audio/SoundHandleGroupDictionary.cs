using System.Collections.Generic;

namespace Exa.Audio {
    public class SoundHandleGroupDictionary {
        private readonly Dictionary<string, SoundHandleGroup> dict = new Dictionary<string, SoundHandleGroup>();

        public IEnumerable<SoundHandleGroup> Handles {
            get => dict.Values;
        }

        public void Add(SoundHandle handle) {
            dict[handle.sound.Id].Add(handle);
        }

        public void Remove(SoundHandle handle) {
            dict[handle.sound.Id].Remove(handle);
        }

        public void RegisterGroup(string id) {
            dict[id] = new SoundHandleGroup();
        }

        public void Clear() {
            dict.Clear();
        }
    }
}