using System.Collections.Generic;
using System.Linq;
using Exa.Utils;

namespace Exa.Audio.Music {
    public interface ISoundTrack : IEnumerable<ISong> {
        public IList<ISong> Songs { get; }
        public ISoundTrackDescription Description { get; }
    }

    public static class ISoundTrackExtensions {
        public static ISong SelectSong(this ISoundTrack soundTrack, Atmosphere currentAtmosphere) {
            return soundTrack
                .Where(sound => sound.AtmosphereFilter.HasAnyValue(currentAtmosphere))
                .GetRandomElement();
        }
    }
}