using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Utils {
    public static class Collider2DExtensions {
        public static void SetMass(this BoxCollider2D collider, float mass) {
            var size = collider.size;
            var area = size.x * size.y;
            collider.density = mass / area;
        }

        public static IEnumerable<RaycastHit2D> StationaryCast(this Collider2D collider, LayerMask layerMask, int maxHits = 100) {
            if (!collider.enabled || !collider.gameObject.activeSelf) {
                throw new InvalidOperationException("Cannot perform a cast on a disabled collider");
            }

            var hits = new RaycastHit2D[maxHits];

            var filter = new ContactFilter2D {
                useLayerMask = true,
                layerMask = layerMask
            };

            var hitCount = collider.Cast(Vector2.zero, filter, hits);

            return hits.Take(hitCount);
        }
    }
}