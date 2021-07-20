using System;
using Exa.Audio;
using Exa.Data;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Settings {
    public class ExaAudioSettings : SaveableSettings<AudioSettingsValues> {
        public SoundTrackLoadHandler LoadHandler { get; set; }

        public override AudioSettingsValues DefaultValues {
            get => new AudioSettingsValues {
                masterVolume = 0.75f,
                musicVolume = 0.5f,
                effectsVolume = 0.5f,
                soundTrackName = GetDefaultSoundTrackName()
            };
        }

        protected override string Key {
            get => "audioSettings";
        }

        public override void Apply() {
            AudioListener.volume = Values.masterVolume;
            S.Audio.Music.Volume = Values.musicVolume;
            S.Audio.Effects.Volume = Values.effectsVolume;
            LoadSoundTrack();
        }

        private void LoadSoundTrack() {
            if (LoadHandler == null) {
                throw new InvalidOperationException("Load handler not set when loading soundtrack");
            }

            var music = S.Audio.Music;

            if (music.CurrentSoundtrack != null &&
                music.CurrentSoundtrack.Description.Name == Values.soundTrackName) {
                return;
            }

            var provider = music.Provider;

            // Make sure we can atleast load the default soundtrack
            var description = provider.Find(Values.soundTrackName);

            if (description == null) {
                S.UI.Logger.LogException($"Could not find soundtrack by name \"{Values.soundTrackName}\"");
                provider.DefaultSoundTrack.LoadSoundTrack(LoadHandler);
            } else {
                description.LoadSoundTrack(LoadHandler);
            }

            LoadHandler.LoadEnumerator = LoadHandler.LoadEnumerator.Then(
                () => {
                    S.Audio.Music.CurrentSoundtrack = LoadHandler.OutputSoundtrack;
                    LoadHandler.LoadEnumerator = null;
                }
            );
        }

        private string GetDefaultSoundTrackName() {
            return S.Audio.Music.Provider.DefaultSoundTrack.Name;
        }

        public override AudioSettingsValues Clone() {
            return new AudioSettingsValues {
                masterVolume = Values.masterVolume,
                effectsVolume = Values.effectsVolume,
                musicVolume = Values.musicVolume,
                soundTrackName = Values.soundTrackName
            };
        }

        protected override AudioSettingsValues DeserializeValues(string path) {
            var values = base.DeserializeValues(path);

            if (S.Audio.Music.Provider.Find(values.soundTrackName) == null) {
                values.soundTrackName = GetDefaultSoundTrackName();
            }

            return values;
        }
    }
}