using System;
using Exa.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Audio.Music {
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Atmosphere {
        None = 1 << 0,
        MainMenu = 1 << 1,
        Combat = 1 << 2,
        All = ~0
    }

    public class MusicPlayerGroup : AudioPlayerGroup {
        [SerializeField] private SoundTrackProvider soundTrackProvider;

        private Atmosphere atmosphere = Atmosphere.None;
        private SoundHandle currentSoundHandle;
        private ISoundTrack currentSoundtrack;
        private bool isPlaying;

        public ISoundTrack CurrentSoundtrack {
            get => currentSoundtrack;
            set {
                if (value == currentSoundtrack) {
                    return;
                }

                IsPlaying = false;
                currentSoundtrack = value;

                Clear();
                value.ForEach(Register);
            }
        }

        public bool IsPlaying {
            get => isPlaying;
            set {
                if (!isPlaying && value) {
                    Play();
                }

                if (isPlaying && !value) {
                    Stop();
                }

                isPlaying = value;
            }
        }

        public SoundTrackProvider Provider {
            get => soundTrackProvider;
        }

        public AtmosphereOverrideList Atmospheres { get; private set; }

        private void Awake() {
            Atmospheres = new AtmosphereOverrideList(Atmosphere.None, SetAtmosphere);
        }

        private void SetAtmosphere(Atmosphere atmosphere) {
            this.atmosphere = atmosphere;

            if (currentSoundHandle?.sound is ISong song && song.AtmosphereFilter.HasValue(atmosphere) && isPlaying) {
                Stop();
                Play();
            }
        }

        private void Play() {
            if (currentSoundtrack.SelectSong(atmosphere).IsNotNull(out var song)) {
                currentSoundHandle = PlayGlobal(song);
                currentSoundHandle.onEnd.AddListener(Play);
                S.UI.Logger.NotifyNowPlaying(song.Id);
            } else {
                var message = $"Cannot select a song from the current soundtrack {currentSoundtrack.Description.Name}, " +
                    $"as it probably doesn't have a song for the atmosphere {atmosphere}";

                throw new InvalidOperationException(message);
            }
        }

        private void Stop() {
            currentSoundHandle?.Stop();
            currentSoundHandle = null;
        }
    }
}