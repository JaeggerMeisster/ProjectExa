using System;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    [Serializable]
    public class BlueprintContainerCollection : ObservableDictionary<string, BlueprintContainer> {
        public BlueprintContainerCollection()
            : base(StringComparer.OrdinalIgnoreCase) { }

        public override void Add(BlueprintContainer item) {
            if (Contains(item)) {
                Debug.LogWarning($"Blueprint: {item.Data.name} has already been added");

                return;
            }

            base.Add(item);
        }

        public override bool Remove(BlueprintContainer item) {
            var result = base.Remove(item);

            item.BlueprintFileHandle.Delete();
            item.ThumbnailFileHandle.Delete();

            return result;
        }
    }
}