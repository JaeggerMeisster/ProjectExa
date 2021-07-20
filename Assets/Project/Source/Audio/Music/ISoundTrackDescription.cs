using Exa.Types.Generics;

namespace Exa.Audio.Music {
    public interface ISoundTrackDescription {
        public string Name { get; }
        public int SongCount { get; }

        public void LoadSoundTrack(SoundTrackLoadHandler loadHandler);
    }

    public static class ISoundTrackDescriptionExtensions {
        public static ILabeledValue<string> GetLabeledValue(this ISoundTrackDescription description) {
            return new LabeledValue<string> {
                Label = description.Name,
                Value = description.Name
            };
        }
    }
}