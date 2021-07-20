using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Math;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    /// <summary>
    ///     Represents a single block that is not yet placed
    /// </summary>
    public class BlockGhost : MonoBehaviour {
        [SerializeField] private SpriteRenderer filter;
        [SerializeField] private Transform filterTransform;
        [SerializeField] private BlockPresenter presenter;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;

        public BlockPresenter Presenter {
            get => presenter;
        }

        public ABpBlock Block { get; private set; }

        /// <summary>
        ///     Update the block the ghost is representing
        /// </summary>
        /// <param name="block"></param>
        public void ImportBlock(BlueprintBlock block) {
            presenter.Renderer.sprite = block.Template.thumbnail;
            filterTransform.localScale = block.Template.size.ToVector3();
            Block = new ABpBlock(new Vector2Int(), block);
            presenter.Present(Block);
        }

        public void SetFilterColor(bool active) {
            filter.color = active ? activeColor : inactiveColor;
        }

        public void Clear() {
            presenter.Renderer.sprite = null;
            filterTransform.localScale = Vector3.one;
            Block = null;
        }
    }
}