namespace Exa.Grids {
    public struct GridInstanceConfiguration {
        public bool Invulnerable { get; set; }

        public static GridInstanceConfiguration InvulnerableConfig {
            get => new GridInstanceConfiguration {
                Invulnerable = true
            };
        }
    }
}