using System;
using System.Collections.Generic;
using Exa.Audio.Music;
using UnityEngine;

namespace Exa.Audio {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private SoundBag soundBag;
        [SerializeField] private MusicPlayerGroup ST_AudioTrack;
        [SerializeField] private AudioPlayerGroup UI_SFX_AudioTrack;

        private readonly Dictionary<string, Sound> soundById = new Dictionary<string, Sound>();

        public MusicPlayerGroup Music {
            get => ST_AudioTrack;
        }

        public AudioPlayerGroup Effects {
            get => UI_SFX_AudioTrack;
        }

        private void Awake() {
            foreach (var sound in soundBag) {
                Register(sound);
            }
        }

        /// <summary>
        ///     Play a sound with the given id
        /// </summary>
        /// <param name="soundId"></param>
        public void PlayGlobal(string soundId) {
            if (!soundById.ContainsKey(soundId)) {
                Debug.LogError($"{soundId} doesn't exist");

                return;
            }

            PlayGlobal(soundById[soundId]);
        }

        public void PlayGlobal(Sound sound) {
            GetTrack(sound.AudioType).PlayGlobal(sound);
        }

        public void Register(Sound sound) {
            soundById[sound.Id] = sound;
            GetTrack(sound.AudioType).Register(sound);
        }

        private AudioPlayerGroup GetTrack(AudioType audioType) {
            return audioType switch {
                AudioType.ST => ST_AudioTrack,
                AudioType.UI_SFX => UI_SFX_AudioTrack,
                _ => throw new ArgumentException("Invalid audioType given", nameof(audioType))
            };
        }
    }
}