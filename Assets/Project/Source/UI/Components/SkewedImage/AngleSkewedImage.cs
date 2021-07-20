using Exa.Math;
using UnityEngine;

namespace Exa.UI {
    public class AngleSkewedImage : PixelSkewedImage {
        private Vector2? pixelSkew;

        private void Update() {
            if (transform.hasChanged) {
                transform.hasChanged = false;

                InvalidatePixels();
            }
        }

        public void InvalidatePixels() {
            pixelSkew = null;
        }

        protected override Vector2 GetPixelSkew() {
            return pixelSkew ??= RecalculatePixels();
        }

        private Vector2 RecalculatePixels() {
            var r = GetPixelAdjustedRect();

            return new Vector2 {
                x = new Vector2(0, r.height / 2).Rotate(-skew.x).x,
                y = new Vector2(r.width / 2, 0).Rotate(-skew.y).y
            };
        }
    }
}