﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Exa.Audio
{
    public class AudioTrack : MonoBehaviour, ITrackContext
    {
        [SerializeField] private string volumeKey;
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        // Stores a handle group for the currently playing sounds of the given id
        protected SoundHandleGroupDictionary handleGroups = new SoundHandleGroupDictionary();

        // Stores an audio source for sound on the track
        protected Dictionary<string, AudioSource> players = new Dictionary<string, AudioSource>();

        public float Volume
        {
            set
            {
                var actualVolume = value > 0.001f ? Mathf.Log(value) * 20 : -80;
                audioMixerGroup.audioMixer.SetFloat(volumeKey, actualVolume);
            }
        }

        /// <summary>
        /// Plays an audio object on this track
        /// </summary>
        /// <param name="sound">Audio object to be played</param>
        public SoundHandle PlayGlobal(Sound sound)
        {
            // Get a handle for the sound, play it, and add it to the collection
            var handle = GetGlobalHandle(sound);

            // Return the handle to the caller
            handle.Play(this);

            return handle;
        }

        /// <summary>
        /// Registers a sound on this track
        /// </summary>
        /// <param name="sound"></param>
        public void Register(Sound sound)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = audioMixerGroup;
            players[sound.id] = source;

            handleGroups.RegisterGroup(sound.id);
        }

        public void RegisterHandle(SoundHandle handle)
        {
            // Remove the handle for the sound after finishing playing
            var endRoutine = StartCoroutine(WaitForSoundEnd(handle));

            handle.onStop.AddListener(() =>
            {
                handle.onEnd.Invoke();
                handleGroups.Remove(handle);
                StopCoroutine(endRoutine);
            });

            handleGroups.Add(handle);
        }

        public void StopAllSounds()
        {
            foreach (var group in handleGroups.Handles)
            {
                group.Stop();
            }
        }

        /// <summary>
        /// Generates the handle for the sound that needs to be played
        /// </summary>
        /// <param name="audioObject"></param>
        /// <returns></returns>
        private SoundHandle GetGlobalHandle(Sound audioObject)
        {
            var source = players[audioObject.id];

            var handle = new SoundHandle
            {
                audioSource = source,
                sound = audioObject
            };

            return handle;
        }

        /// <summary>
        /// Waits for a sound to end,
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IEnumerator WaitForSoundEnd(SoundHandle handle)
        {
            var sound = handle.sound;

            // Wait for the sound to play
            yield return new WaitForSeconds(sound.audioClip.length);

            // Remove context from currently playing sounds
            handleGroups.Remove(handle);

            // Invoke the on end callback on the sound handle
            handle.onEnd?.Invoke();
        }
    }
}