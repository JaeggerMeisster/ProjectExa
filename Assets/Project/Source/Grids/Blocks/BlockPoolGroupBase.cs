using System.Collections.Generic;
using Exa.Pooling;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks {
    public abstract class BlockPoolGroupBase<TPoolMember> : MonoBehaviour
        where TPoolMember : PoolMember {
        [SerializeField] private PoolSettings defaultPoolSettings;
        protected Dictionary<string, IPool<TPoolMember>> poolById = new Dictionary<string, IPool<TPoolMember>>();

        public TPoolMember GetInactiveBlock(string id, Transform container) {
            var member = poolById[id].Retrieve();
            member.transform.SetParent(container);
            return member;
        }

        /// <summary>
        ///     Creates a block prefab for the given block template on this group
        /// </summary>
        /// <param name="blockTemplate"></param>
        /// <param name="inert"></param>
        /// <returns></returns>
        protected GameObject CreatePrefab(BlockTemplate blockTemplate, PrefabType prefabType) {
            var prefab = GetBasePrefab(blockTemplate, prefabType);
            var instance = Instantiate(prefab, transform);
            instance.name = $"{blockTemplate.displayId}";

            return instance;
        }

        protected TPool CreatePool<TPool>(GameObject prefab, string name, out PoolSettings settings)
            where TPool : Component, IPool<TPoolMember> {
            var poolGO = new GameObject(name);
            poolGO.transform.SetParent(transform);

            var pool = poolGO.AddComponent<TPool>();

            settings = defaultPoolSettings.Clone();
            settings.prefab = prefab;

            return pool;
        }

        private GameObject GetBasePrefab(BlockTemplate blockTemplate, PrefabType prefabType) {
            switch (prefabType) {
                case PrefabType.inert:
                    return blockTemplate.inertPrefab;

                case PrefabType.alive:
                    return blockTemplate.alivePrefab;

                default:
                    return null;
            }
        }
    }
}