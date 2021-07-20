using DG.Tweening;
using Exa.Math;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Tooltips {
    /// <summary>
    ///     A tooltip that follows cursor movement and stays inside screen bounds
    ///     <para>Supports padding and offset</para>
    /// </summary>
    public class FloatingTooltip : MonoBehaviour {
        [Header("References")]
        [SerializeField] protected RectTransform tooltipRoot;
        [SerializeField] protected RectTransform itemsContainer;

        [Header("Settings")]
        [SerializeField] private Vector2 offset = new Vector2(4f, 4f);
        [SerializeField] private float padding = 4f;
        [SerializeField] private float animTime = 0.1f;

        private Tween posTween;
        private Rect? rootRect;

        private Rect RootRect {
            get {
                rootRect = rootRect ?? S.UI.RootTransform.rect;

                return rootRect.Value;
            }
        }

        protected virtual void Update() {
            UpdatePosition();
        }

        /// <summary>
        ///     Update the position of the tooltip
        /// </summary>
        /// <param name="immediate"></param>
        public void UpdatePosition(bool immediate = false) {
            // Get the mouse position
            var mousePos = S.Input.MouseScaledViewportPoint;
            var clampedCorner = ClampPos(mousePos + offset);
            SetAnchoredPos(clampedCorner, immediate);
        }

        /// <summary>
        ///     Clamp the mouse position
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual Vector2 ClampPos(Vector2 input) {
            var size = GetTooltipSize();
            var pivot = tooltipRoot.pivot;
            var min = GetMinBounds(size, pivot);
            var max = GetMaxBounds(size, pivot, GetSize(RootRect));

            return input.Clamp(min, max);
        }

        protected virtual Vector2 GetTooltipSize() {
            return GetSize(tooltipRoot.rect);
        }

        protected Vector2 GetSize(Rect rect) {
            return new Vector2(rect.width, rect.height);
        }

        private Vector2 GetMinBounds(Vector2 size, Vector2 pivot) {
            var result = size * pivot;

            return AddFloat(result, padding);
        }

        private Vector2 GetMaxBounds(Vector2 size, Vector2 pivot, Vector2 boundSize) {
            var result = boundSize - size * (Vector2.one - pivot);

            return AddFloat(result, -padding);
        }

        private Vector2 AddFloat(Vector2 vector, float value) {
            return new Vector2(vector.x + value, vector.y + value);
        }

        /// <summary>
        ///     Sets the position of the tooltip
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="immediate">
        ///     Whether the update needs to happen now. This is usually used to notify any animation systems
        ///     that they shouldn't animate, but instead set the position directly
        /// </param>
        protected virtual void SetAnchoredPos(Vector2 pos, bool immediate) {
            if (immediate) {
                tooltipRoot.anchoredPosition = pos;

                return;
            }

            posTween?.Kill();

            posTween = tooltipRoot.DOAnchorPos(pos, animTime)
                .SetEase(Ease.OutQuad);
        }
    }
}