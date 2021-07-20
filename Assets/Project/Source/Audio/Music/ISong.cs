namespace Exa.Audio.Music {
    public interface ISong : ISound {
        public Atmosphere AtmosphereFilter { get; }
    }
}