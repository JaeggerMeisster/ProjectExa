using System.Collections.Generic;

namespace Exa.Audio.Music {
    public class CustomSoundTrackMetadata {
        public string Name { get; set; }
        public IEnumerable<CustomSongMetadata> Songs { get; set; }
    }
}