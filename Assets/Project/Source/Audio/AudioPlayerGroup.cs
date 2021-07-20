using System.Collections;
using System.Collections.Generic;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Audio;

#pragma warning disable CS0649

namespace Exa.Audio {
    public class AudioPlayerGroup : MonoBehaviour, ITrackContext {
        [SerializeField] private string volumeKey;
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        // Stores a handle group for the currently playing sounds of the given id
        protected SoundHandleGroupDictionary handleGroups = new SoundHandleGroupDictionary();

        // Stores an audio source for sound on the track
        protected Dictionary<string, AudioSource> players = new Dictionary<string, AudioSource>();

        // Set the volume of an audio track using a 0-1 based float range
        public float Volume {
            set {
                // Convert linear to logarithmic
                var actualVolume = value > 0.001f
                    ? Mathf.Log(value) * 20
                    : -80;

                audioMixerGroup.audioMixer.SetFloat(volumeKey, actualVolume);
            }
        }

        public void RegisterHandle(SoundHandle handle) {
            // Remove the handle for the sound after finishing playing
            var endRoutine = WaitForSoundEnd(handle).Start();

            handle.onStop.AddListener(
                () => {
                    handleGroups.Remove(handle);
                    handle.onEnd.Invoke();
                    StopCoroutine(endRoutine);
                }
            );

            handleGroups.Add(handle);
        }

        public void StopAllSounds() {
            foreach (var group in handleGroups.Handles) {
                group.Stop();
            }
        }

        /// <summary>
        ///     Plays an audio object on this track
        /// </summary>
        /// <param name="sound">Audio object to be played</param>
        public SoundHandle PlayGlobal(ISound sound) {
            // Get a handle for the sound, play it, and add it to the collection
            var handle = GetGlobalHandle(sound);

            // Return the handle to the caller
            handle.Play(this);

            return handle;
        }

        /// <summary>
        ///     Registers a sound on this track
        /// </summary>
        /// <param name="sound"></param>
        public void Register(ISound sound) {
            var go = new GameObject($"Player: {sound.Id}");
            go.transform.SetParent(transform);
            var source = go.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = audioMixerGroup;
            players[sound.Id] = source;
            handleGroups.RegisterGroup(sound.Id);
        }

        public void Clear() {
            players.Values.ForEach(player => player.gameObject.Destroy());
            players.Clear();
            handleGroups.Clear();
        }

        /// <summary>
        ///     Generates the handle for the sound that needs to be played
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        private SoundHandle GetGlobalHandle(ISound sound) {
            var source = players[sound.Id];

            var handle = new SoundHandle {
                audioSource = source,
                sound = sound
            };

            return handle;
        }

        /// <summary>
        ///     Waits for a sound to end,
        ///     assumes a sound
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        private IEnumerator WaitForSoundEnd(SoundHandle handle) {
            // Wait for the sound to play
            yield return new WaitForSeconds(handle.sound.AudioClip.length - handle.audioSource.time);

            // Remove context from currently playing sounds
            handleGroups.Remove(handle);

            // Invoke the on end callback on the sound handle
            handle.onEnd?.Invoke();
        }
    }
}