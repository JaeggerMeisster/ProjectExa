using System.Collections.Generic;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Audio.Music {
    [CreateAssetMenu(menuName = "Audio/TrackList")]
    public class DefaultSoundTrack : ScriptableObjectBag<Song>, ISoundTrack {
        private void OnEnable() {
            Songs = new List<ISong>(objects);
            Description = new DefaultSoundTrackDescription(this);
        }

        public IList<ISong> Songs { get; private set; }
        public ISoundTrackDescription Description { get; private set; }

        IEnumerator<ISong> IEnumerable<ISong>.GetEnumerator() {
            return GetEnumerator();
        }
    }
}