using UnityEngine;

namespace Exa.Audio.Music {
    [CreateAssetMenu(menuName = "Audio/Song")]
    public class Song : Sound, ISong {
        [SerializeField] private Atmosphere atmosphereFilter;

        public Atmosphere AtmosphereFilter {
            get => atmosphereFilter;
        }
    }
}