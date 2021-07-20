namespace Exa.Audio.Music {
    public class CustomSongMetadata {
        public string Name { get; set; }
        public string FileName { get; set; }
        public Atmosphere Atmospheres { get; set; } = Atmosphere.All;
        public float Volume { get; set; } = 1f;
    }
}