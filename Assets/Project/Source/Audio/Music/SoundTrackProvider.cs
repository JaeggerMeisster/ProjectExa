using System.Collections.Generic;
using System.Linq;
using Exa.Types.Generics;
using UnityEngine;
using Tree = Exa.IO.Tree;

namespace Exa.Audio.Music {
    public class SoundTrackProvider : MonoBehaviour {
        [SerializeField] private DefaultSoundTrack defaultSoundTrack;
        public LazyCache<IEnumerable<ISoundTrackDescription>> descriptionsBackingField;

        public ISoundTrackDescription DefaultSoundTrack {
            get => defaultSoundTrack.Description;
        }

        public IEnumerable<ISoundTrackDescription> Descriptions {
            get => descriptionsBackingField.Value;
        }

        private void Awake() {
            descriptionsBackingField = new LazyCache<IEnumerable<ISoundTrackDescription>>(() => { return GetSoundTrackDescriptions().ToList(); });
        }

        private IEnumerable<ISoundTrackDescription> GetSoundTrackDescriptions() {
            yield return new DefaultSoundTrackDescription(defaultSoundTrack);

            foreach (var file in Tree.Root.CustomSoundTracks.GetFiles("*.zip")) {
                yield return new CustomSoundTrackDescription(file);
            }
        }

        public bool HasSoundTrack(string soundTrackName) {
            return Descriptions.Any(description => description.Name == soundTrackName);
        }

        public ISoundTrackDescription Find(string soundTrackName) {
            if (string.IsNullOrEmpty(soundTrackName)) {
                return null;
            }

            return Descriptions.FirstOrDefault(description => description.Name == soundTrackName);
        }
    }
}