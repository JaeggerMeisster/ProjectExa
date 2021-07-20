using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using UnityEngine;

namespace Exa.Utils {
    public static class PhysicsExtensions {
        public static IEnumerable<RaycastBlockHit> RaycastAll(Vector2 start, Vector2 direction, float distance, ContextMask contextMask) {
            return from hit in Physics2D.RaycastAll(start, direction, distance, contextMask.LayerMask)
                let damageable = hit.collider.transform.GetComponent<IDamageable>()
                where damageable.PassesDamageMask(contextMask.BlockContextMask)
                select new RaycastBlockHit {
                    hit = hit,
                    damageable = damageable
                };
        }
    }

    public struct RaycastBlockHit {
        public RaycastHit2D hit;
        public IDamageable damageable;
    }
}