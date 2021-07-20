using System.Collections;
using System.Collections.Generic;

namespace Exa.Audio.Music {
    public class CustomSoundTrack : ISoundTrack {
        public CustomSoundTrack(IList<ISong> songs, ISoundTrackDescription description) {
            Songs = songs;
            Description = description;
        }

        public IList<ISong> Songs { get; }
        public ISoundTrackDescription Description { get; }

        public IEnumerator<ISong> GetEnumerator() {
            return Songs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}