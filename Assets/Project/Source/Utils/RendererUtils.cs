using Exa.ShipEditor;
using UnityEngine;

namespace Exa.Utils {
    public static class RendererUtils {
        public static Material CopyMaterial(this Renderer renderer) {
            var materialCopy = new Material(renderer.material);
            renderer.material = materialCopy;

            return materialCopy;
        }

        public static void SetFlip(this SpriteRenderer renderer, BlockFlip flip) {
            renderer.flipX = flip.HasValue(BlockFlip.FlipX);
            renderer.flipY = flip.HasFlag(BlockFlip.FlipY);
        }
    }
}