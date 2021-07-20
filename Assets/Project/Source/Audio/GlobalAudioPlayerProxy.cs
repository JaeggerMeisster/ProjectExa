using UnityEngine;

namespace Exa.Audio {
    public class GlobalAudioPlayerProxy : MonoBehaviour {
        public void Play(string soundId) {
            S.Audio.PlayGlobal(soundId);
        }

        public void Play(Sound sound) {
            S.Audio.PlayGlobal(sound);
        }
    }
}