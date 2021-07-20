using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.ShipEditor {
    public class TurretBlocks : Grid<ABpBlock> {
        private readonly Dictionary<ABpBlock, TurretCache> cache;
        private readonly EditorGridTurretLayer turretLayer;

        public TurretBlocks(EditorGridTurretLayer turretLayer) {
            this.turretLayer = turretLayer;
            cache = new Dictionary<ABpBlock, TurretCache>();
        }

        public void AddTurret(ABpBlock block) {
            var overlay = turretLayer.CreateStationaryOverlay(block);
            var claims = overlay.GetTurretClaims().ToList();

            cache.Add(
                block,
                new TurretCache {
                    Template = block.Template,
                    Overlay = overlay,
                    TurretClaimedTiles = claims
                }
            );

            base.Add(block);
        }

        public IEnumerable<Vector2Int> GetTurretClaims() {
            return cache.Values.SelectMany(cache => cache.TurretClaimedTiles).Distinct();
        }

        public override void Remove(ABpBlock block) {
            cache.Remove(block);
            turretLayer.RemoveStationaryOverlay(block);

            base.Remove(block);
        }

        private struct TurretCache {
            public BlockTemplate Template { get; set; }
            public IEnumerable<Vector2Int> TurretClaimedTiles { get; set; }
            public TurretOverlay Overlay { get; set; }
        }
    }
}