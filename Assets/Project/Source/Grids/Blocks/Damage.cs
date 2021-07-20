using Exa.Grids.Blocks.BlockTypes;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks {
    public interface IDamageable {
        TakenDamage TakeDamage(Damage damage);
        
        Block Block { get; }
        
        bool IsQueuedForDestruction { get; }
    }
    
    public struct Damage {
        public float value;
        public object source;
    }
   
    public struct TakenDamage {
        public float absorbedDamage;
        public float appliedDamage;
    }

    public static class IDamageableExtensions {
        /// <summary>
        /// Get whether or not this damageable should be targeted
        /// </summary>
        /// <param name="damageable"></param>
        /// <returns></returns>
        public static bool PassesDamageMask(this IDamageable damageable, BlockContext damageMask) {
            if (damageable.IsQueuedForDestruction) {
                return false;
            }

            var block = damageable.Block; 
            if (block.Parent == null) {
                Debug.LogError($"Block {block.GetInstanceID()} has no parent");

                return false;
            }

            return block.Parent.BlockContext.HasAnyValue(damageMask);
        }
    }
}