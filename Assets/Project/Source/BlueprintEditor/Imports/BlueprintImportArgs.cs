using System;
using Exa.Grids.Blueprints;

namespace Exa.ShipEditor {
    public class BlueprintImportArgs : GridEditorImportArgs {
        private readonly Action<Blueprint> save;
        private Blueprint blueprint;

        public BlueprintImportArgs(Blueprint blueprint, Action<Blueprint> save) {
            this.blueprint = blueprint;
            this.save = save;
        }

        public override void Save(Blueprint blueprint) {
            this.blueprint = blueprint;
            save(blueprint);
        }

        public override Blueprint GetBlueprint() {
            return blueprint;
        }
    }
}