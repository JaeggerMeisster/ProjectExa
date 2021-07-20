using UnityEngine;

namespace Exa.Pooling {
    public class PoolMember : MonoBehaviour {
        [HideInInspector] public IPool<PoolMember> pool;

        protected virtual void OnDisable() {
            if (IgnoreClause()) {
                return;
            }

            pool.Return(this);
        }

        protected virtual void OnDestroy() {
            if (IgnoreClause()) {
                return;
            }

            pool.OnDestroyMember();
        }

        protected virtual bool IgnoreClause() {
            return S.IsQuitting;
        }
    }
}