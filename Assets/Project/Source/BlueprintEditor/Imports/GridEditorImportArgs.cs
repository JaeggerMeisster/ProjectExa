using System;
using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Validation;

namespace Exa.ShipEditor {
    public abstract class GridEditorImportArgs {
        private readonly List<IPlugableValidator> validators;

        public GridEditorImportArgs() {
            validators = new List<IPlugableValidator>();
        }

        public BlockContext BlockContext { get; } = BlockContext.UserGroup;
        public Action OnExit { get; set; }

        public IEnumerable<IPlugableValidator> Validators {
            get => validators;
        }

        public bool ValidateName { get; protected set; }

        public abstract void Save(Blueprint blueprint);

        public abstract Blueprint GetBlueprint();

        public void AddValidator(IPlugableValidator validator) {
            validators.Add(validator);
        }
    }
}