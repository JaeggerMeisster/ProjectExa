using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.ShipEditor {
    public abstract class CustomEditorGridLayer : EditorGridLayer {
        public override void TryAddToGrid(ABpBlock block) {
            if (Predicate(block.Template)) {
                OnAdd(block);
            }
        }

        public override void TryRemoveFromGrid(ABpBlock block) {
            if (Predicate(block.Template)) {
                OnRemove(block);
            }
        }

        protected abstract bool Predicate(BlockTemplate template);

        protected abstract void OnAdd(ABpBlock block);

        protected abstract void OnRemove(ABpBlock block);
    }

    public abstract class EditorGridLayer : MonoBehaviour, ICustomEditorGridLayer {
        public abstract void TryAddToGrid(ABpBlock block);

        public abstract void TryRemoveFromGrid(ABpBlock block);
    }
}