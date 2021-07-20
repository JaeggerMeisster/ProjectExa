using Exa.Types.Generics;

namespace Exa.Audio.Music {
    /// <summary>
    ///     Sets the current atmosphere when enabled
    /// </summary>
    public class AtmosphereTrigger : MonoOverride<Atmosphere> {
        protected override OverrideList<Atmosphere> GetPath() {
            return S.Audio.Music.Atmospheres;
        }
    }
}