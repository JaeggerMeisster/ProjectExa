using UnityEngine;

namespace Exa.Audio {
    /// <summary>
    ///     Supports an audioclip with additional properties that gives audio tracks more context
    /// </summary>
    [CreateAssetMenu(menuName = "Audio/Sound")]
    public class Sound : ScriptableObject, ISound {
        [SerializeField] private string id;
        [SerializeField] private AudioType audioType;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private SoundConfig soundConfig;

        public string Id {
            get => id;
        }

        public AudioType AudioType {
            get => audioType;
        }

        public AudioClip AudioClip {
            get => audioClip;
        }

        public SoundConfig Config {
            get => soundConfig;
        }
    }
}