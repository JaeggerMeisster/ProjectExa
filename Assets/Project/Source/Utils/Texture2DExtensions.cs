using System.Collections.Generic;
using Exa.Math;
using UnityEngine;

namespace Exa.Utils {
    public static class Texture2DExtensions {
        public static Sprite CreateSprite(this Texture2D tex, float pixelsPerUnit = 32) {
            var rect = new Rect(0, 0, tex.width, tex.height);

            return Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }

        public static Texture2D SetDefaults(this Texture2D tex) {
            tex.filterMode = FilterMode.Point;
            tex.EnumeratePixels().ForEach(pixel => tex.SetPixel(pixel, Color.clear));

            return tex;
        }

        public static IEnumerable<Vector2Int> EnumeratePixels(this Texture2D tex) {
            return MathUtils.EnumerateVectors(tex.width, tex.height);
        }

        public static Color GetPixel(this Texture2D tex, Vector2Int point) {
            return tex.GetPixel(point.x, point.y);
        }

        public static void SetPixel(this Texture2D tex, Vector2Int point, Color color) {
            tex.SetPixel(point.x, point.y, color);
        }
    }
}