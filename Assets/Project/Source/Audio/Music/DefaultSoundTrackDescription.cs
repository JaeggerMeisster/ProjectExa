using System.Collections;
using System.Linq;
using UnityEngine;

namespace Exa.Audio.Music {
    public class DefaultSoundTrackDescription : ISoundTrackDescription {
        private readonly ISoundTrack soundTrack;

        public DefaultSoundTrackDescription(ISoundTrack soundTrack) {
            this.soundTrack = soundTrack;
        }

        public string Name {
            get => "Default soundtrack";
        }

        public int SongCount {
            get => soundTrack.Count();
        }

        public void LoadSoundTrack(SoundTrackLoadHandler loadHandler) {
            loadHandler.LoadEnumerator = Load(loadHandler);
            loadHandler.Progress.Report(0f);
        }

        private IEnumerator Load(SoundTrackLoadHandler loadHandler) {
            yield return new WaitForSeconds(0.5f);

            loadHandler.Progress.Report(1f);
            loadHandler.OutputSoundtrack = soundTrack;
        }
    }
}