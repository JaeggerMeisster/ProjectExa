namespace Exa.Data {
    public interface ISettings {
        void Load();

        void Save();

        void Apply();
    }
}