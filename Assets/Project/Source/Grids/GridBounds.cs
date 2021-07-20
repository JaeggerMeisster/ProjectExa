using System.Collections.Generic;
using System.Linq;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Grids {
    public struct GridBounds {
        public MinMax<Vector2Int> minMax;

        public GridBounds(MinMax<Vector2Int> minMax) {
            this.minMax = minMax;
        }

        public GridBounds(IEnumerable<Vector2Int> positions) {
            minMax = new MinMax<Vector2Int>();

            if (!positions.Any()) {
                return;
            }

            var first = positions.First();
            minMax.min = first;
            minMax.max = first;

            foreach (var position in positions) {
                if (position.x < minMax.min.x) {
                    minMax.min.x = position.x;
                }

                if (position.y < minMax.min.y) {
                    minMax.min.y = position.y;
                }

                if (position.x > minMax.max.x) {
                    minMax.max.x = position.x;
                }

                if (position.y > minMax.max.y) {
                    minMax.max.y = position.y;
                }
            }
        }

        public IEnumerable<Vector2Int> GetAdjacentPositions() {
            // Get bottom and top adjacent positions
            for (var i = minMax.min.x; i < minMax.max.x + 1; i++) {
                yield return new Vector2Int(i, minMax.min.y - 1);
                yield return new Vector2Int(i, minMax.max.y + 1);
            }

            // Get right and left adjacent position
            for (var i = minMax.min.y; i < minMax.max.y + 1; i++) {
                yield return new Vector2Int(minMax.min.x - 1, i);
                yield return new Vector2Int(minMax.max.x + 1, i);
            }
        }

        public Vector2Int GetDelta() {
            return new Vector2Int {
                x = minMax.max.x - minMax.min.x + 1,
                y = minMax.max.y - minMax.min.y + 1
            };
        }
    }
}