namespace Exa.Grids.Blueprints {
    public class BlueprintOptions {
        public string name;
        public BlueprintTypeGuid shipClass;

        public BlueprintOptions(string name, BlueprintTypeGuid shipClass) {
            this.name = name;
            this.shipClass = shipClass;
        }
    }
}