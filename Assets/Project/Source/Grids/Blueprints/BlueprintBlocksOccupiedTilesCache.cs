using System.Collections.Generic;
using Exa.Generics;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    public class BlueprintBlocksOccupiedTilesCache : Dictionary<Vector2Int, ABpBlock>,
        ICloneable<BlueprintBlocksOccupiedTilesCache> {
        public BlueprintBlocksOccupiedTilesCache Clone() {
            var dict = new BlueprintBlocksOccupiedTilesCache();

            foreach (var item in this) {
                dict.Add(item.Key, item.Value.Clone());
            }

            return dict;
        }
    }
}