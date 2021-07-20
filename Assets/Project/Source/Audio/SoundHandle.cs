using UnityEngine;
using UnityEngine.Events;

namespace Exa.Audio {
    /// <summary>
    ///     Defines a handle for a single sound
    /// </summary>
    public class SoundHandle {
        public AudioSource audioSource;

        private ITrackContext context;

        /// <summary>
        ///     Is invoked when the sound ends or is stopped
        /// </summary>
        public UnityEvent onEnd = new UnityEvent();

        /// <summary>
        ///     Is invoked when the sound is manually stopped
        /// </summary>
        public UnityEvent onStop = new UnityEvent();
        public ISound sound;

        public void Play(ITrackContext playableContext) {
            context = playableContext;

            if (!sound.Config.allowMultipleOfType) {
                context.StopAllSounds();
            }

            if (sound.Config.allowMultipleOfType) {
                audioSource.PlayOneShot(sound.AudioClip, sound.Config.volume);
            } else {
                audioSource.clip = sound.AudioClip;
                audioSource.volume = sound.Config.volume;
                audioSource.Play();
            }

            audioSource.pitch = sound.Config.pitch;

            context.RegisterHandle(this);
        }

        public void Stop() {
            onStop?.Invoke();
            audioSource.Stop();
        }
    }
}