using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Research {
    public abstract class ResearchItem : ScriptableObject {
        [SerializeField] private BlockContext autoEnableOn;

        public string Id {
            get => name;
        }

        public void AutoEnable() {
            if (autoEnableOn != BlockContext.None) {
                EnableOn(autoEnableOn);
            }
        }

        public abstract void EnableOn(BlockContext filter);

        public abstract void DisableOn(BlockContext filter);
    }
}