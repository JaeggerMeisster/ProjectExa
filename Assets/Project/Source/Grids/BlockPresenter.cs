using UnityEngine;

namespace Exa.Grids {
    public class BlockPresenter : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public SpriteRenderer Renderer {
            get => spriteRenderer;
        }

        public virtual void Present(IGridMember gridMember) {
            gridMember.UpdateLocals(gameObject);
            // As all current block sprites are horizontally symmetric, sprite renderers don't need to flip the sprite
            // gridMember.BlueprintBlock.SetSpriteRendererFlips(spriteRenderer);
        }
    }
}