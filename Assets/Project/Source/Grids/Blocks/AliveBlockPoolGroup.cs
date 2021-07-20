using System;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks {
    public class AliveBlockPoolGroup : BlockPoolGroupBase<BlockPoolMember> {
        /// <summary>
        ///     Creates an alive prefab on this group.
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <returns></returns>
        public void CreateAlivePrefabGroup(BlockTemplate blockTemplate) {
            var blockGO = CreatePrefab(blockTemplate, PrefabType.alive);
            var block = blockGO.GetComponent<Block>();
            block.Collider = blockGO.GetComponent<BoxCollider2D>();

            foreach (var component in block.GetBehaviours()) {
                if (component == null) {
                    throw new Exception($"Null component in block of template {blockTemplate.id} on block {blockGO.name}");
                }

                component.block = block;
            }

            var id = blockTemplate.id;
            var pool = CreatePool<BlockPool>(blockGO, $"Block pool: {id}", out var settings);
            poolById[id] = pool;
            pool.Configure(settings);
        }
    }
}