using UnityEngine;

namespace Exa.Audio {
    public interface ISound {
        public AudioType AudioType { get; }
        public string Id { get; }
        public AudioClip AudioClip { get; }
        public SoundConfig Config { get; }
    }
}