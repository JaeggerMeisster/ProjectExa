using System;
using Exa.Grids.Blueprints;

namespace Exa.ShipEditor {
    public class ContainerImportArgs : GridEditorImportArgs {
        private readonly Action<BlueprintContainer> save;

        public ContainerImportArgs(BlueprintContainer container, Action<BlueprintContainer> save) {
            Container = container;
            ValidateName = true;
            this.save = save;
        }

        public BlueprintContainer Container { get; }

        public override void Save(Blueprint blueprint) {
            // Set the value of the observable
            Container.SetData(blueprint);
            // Save the blueprint
            save(Container);
        }

        public override Blueprint GetBlueprint() {
            return Container.Data;
        }
    }
}