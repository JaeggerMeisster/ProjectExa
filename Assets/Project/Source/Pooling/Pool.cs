using System;
using System.Collections.Generic;
using Exa.Logging;
using Exa.Utils;
using UnityEngine;

namespace Exa.Pooling {
    public class Pool : Pool<PoolMember> { }

    [Serializable]
    public class Pool<T> : MonoBehaviour, IPool<T>
        where T : PoolMember {
        public int totalMembers;
        private Stack<T> poolMembers = new Stack<T>();

        private PoolSettings poolSettings;

        private void Update() {
            // Grow the queue
            if (poolSettings.growToPreferredSize && poolMembers.Count < poolSettings.preferredSize) {
                poolMembers.Push(InstantiatePrefab());
            }
        }

        public virtual void Configure(PoolSettings poolSettings) {
            this.poolSettings = poolSettings;

            foreach (var poolMember in poolMembers) {
                Destroy(poolMember.gameObject);
            }

            poolMembers = new Stack<T>(poolSettings.maxSize);

            for (var i = 0; i < poolSettings.preferredSize; i++) {
                poolMembers.Push(InstantiatePrefab());
            }
        }

        public virtual T Retrieve() {
            return TryPop();
        }

        public void OnDestroyMember() {
            totalMembers--;

            Debug.LogWarning("Pool member destroyed, this shouldn't happen");
        }

        public virtual bool Return(PoolMember poolMember) {
            if (!(poolMember is T member)) {
                throw new ArgumentException(
                    $"Pool member type ({poolMember.GetType()}) does not match type ({typeof(T)})"
                );
            }

            return TryPush(member);
        }

        protected virtual T TryPop() {
            return poolMembers.Count == 0 ? InstantiatePrefab() : poolMembers.Pop();
        }

        protected virtual bool TryPush(T poolMember) {
            if (poolMembers.Count > poolSettings.maxSize) {
                Logs.Log(
                    "Destroyed member",
                    new {
                        Name = poolMember.gameObject.name
                    }
                );

                Destroy(poolMember.gameObject);

                return false;
            }

            EnumeratorUtils.DelayOneFrame(() => poolMember.transform.SetParent(transform)).Start();
            poolMembers.Push(poolMember);

            return true;
        }

        protected virtual T InstantiatePrefab() {
            totalMembers++;
            var poolMemberGO = Instantiate(poolSettings.prefab, transform);
            poolMemberGO.name = $"{poolSettings.prefab.name} ({totalMembers})";

            var poolMember = poolMemberGO.AddComponent<T>();
            poolMember.pool = this;

            return poolMember;
        }
    }
}