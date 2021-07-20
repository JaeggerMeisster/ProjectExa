using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI {
    public class PixelSkewedImage : Image {
        public Vector2 skew;

        protected override void OnPopulateMesh(VertexHelper vh) {
            base.OnPopulateMesh(vh);
            var r = GetPixelAdjustedRect();
            var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
            vh.Clear();
            var skew = GetPixelSkew();
            vh.AddVert(new Vector3(v.x - skew.x, v.y - skew.y), color, Vector2.zero);
            vh.AddVert(new Vector3(v.x + skew.x, v.w - skew.y), color, Vector2.up);
            vh.AddVert(new Vector3(v.z + skew.x, v.w + skew.y), color, Vector2.one);
            vh.AddVert(new Vector3(v.z - skew.x, v.y + skew.y), color, Vector2.right);
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }

        protected virtual Vector2 GetPixelSkew() {
            return skew;
        }
    }
}