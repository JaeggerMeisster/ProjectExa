namespace Exa.Grids.Blueprints {
    public class BlueprintContainerArgs {
        public Blueprint blueprint;
        public bool generateBlueprintFileHandle = true;
        public bool generateBlueprintFileName = true;
        public bool useDefaultThumbnailFolder = false;

        public BlueprintContainerArgs(Blueprint blueprint) {
            this.blueprint = blueprint;
        }
    }
}