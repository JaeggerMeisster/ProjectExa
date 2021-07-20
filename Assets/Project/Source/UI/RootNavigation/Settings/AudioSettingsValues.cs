using System;

namespace Exa.UI.Settings {
    public class AudioSettingsValues : IEquatable<AudioSettingsValues> {
        public float effectsVolume;
        public float masterVolume;
        public float musicVolume;
        public string soundTrackName;

        public bool Equals(AudioSettingsValues other) {
            return masterVolume.Equals(other?.masterVolume) &&
                musicVolume.Equals(other?.musicVolume) &&
                effectsVolume.Equals(other?.effectsVolume) &&
                soundTrackName.Equals(other?.soundTrackName);
        }
    }
}