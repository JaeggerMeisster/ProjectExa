using System.Collections.Generic;
using Exa.Grids.Blueprints;
using Exa.Math;
using UnityEngine;

namespace Exa.ShipEditor {
    public static class GridEditorUtils {
        public static IEnumerable<Vector2Int> GetOccupiedTilesByAnchor(ABpBlock aBpBlock) {
            var block = aBpBlock.BlueprintBlock;
            var gridAnchor = aBpBlock.GridAnchor;

            return GetOccupiedTilesByAnchor(block, gridAnchor);
        }

        public static IEnumerable<Vector2Int> GetOccupiedTilesByAnchor(BlueprintBlock block, Vector2Int gridAnchor) {
            var area = block.Template.size.Rotate(block.Rotation);

            if (block.flippedX) {
                area.x = -area.x;
            }

            if (block.flippedY) {
                area.y = -area.y;
            }

            return MathUtils.EnumerateVectors(area, gridAnchor);
        }

        public static Vector2Int GetMirroredGridPos(Vector2Int size, Vector2Int gridPos) {
            return new Vector2Int {
                x = gridPos.x,
                y = size.y - 1 - gridPos.y
            };
        }
    }
}