using System;
using Exa.Generics;
using UnityEngine;

namespace Exa.Pooling {
    [Serializable]
    public class PoolSettings : ICloneable<PoolSettings> {
        public int maxSize;
        public int preferredSize;
        public bool growToPreferredSize;
        public GameObject prefab;

        public PoolSettings Clone() {
            return new PoolSettings {
                maxSize = maxSize,
                preferredSize = preferredSize,
                growToPreferredSize = growToPreferredSize,
                prefab = prefab
            };
        }
    }
}