using System.Collections.Generic;
using Exa.Generics;
using Exa.UI.Tooltips;
using Newtonsoft.Json;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    public class Blueprint : ICloneable<Blueprint> {
        public static readonly string DEFAULT_BLUEPRINT_NAME = "New blueprint";

        public string name;
        public BlueprintTypeGuid shipClass;

        public Blueprint(BlueprintOptions options) {
            name = options.name;
            shipClass = options.shipClass;
            Grid = new BlueprintGrid();
        }

        [JsonConstructor]
        public Blueprint(string name, BlueprintTypeGuid shipClass, BlueprintGrid grid) {
            this.name = name;
            this.shipClass = shipClass;
            Grid = grid;
        }

        public ABpBlock this[Vector2Int key] {
            get => Grid.GetMember(key);
        }

        [JsonProperty("blocks")] public BlueprintGrid Grid { get; private set; }
        [JsonIgnore] public Texture2D Thumbnail { get; set; }

        [JsonIgnore] public BlueprintType BlueprintType {
            get => S.Blueprints.blueprintTypes.typesById[shipClass];
        }

        public Blueprint Clone() {
            return new Blueprint(name, shipClass, Grid.Clone());
        }

        public void Add(ABpBlock aBpBlock) {
            Grid.Add(aBpBlock);
        }

        public void Remove(Vector2Int gridPos) {
            Grid.Remove(gridPos);
        }

        public void ClearBlocks() {
            Grid = new BlueprintGrid();
            Thumbnail = null;
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() {
            return new ITooltipComponent[] {
                new TooltipText($"Name: {name}"),
                new TooltipText($"Class: {shipClass}"),
                new TooltipText($"Size: {(Vector2Int) Grid.Size}"),
                new TooltipText($"Blocks (Count: {Grid.GetMemberCount()}):"),
                new TooltipGroup(Grid.GetTotals().GetDebugTooltipComponents(), 1)
            };
        }
    }
}