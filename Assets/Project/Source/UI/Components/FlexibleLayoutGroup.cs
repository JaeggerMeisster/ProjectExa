using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Components {
    public class FlexibleLayoutGroup : LayoutGroup {
        public enum FitType {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns
        }

        public FitType fitType;
        public int rows;
        public int columns;
        public Vector2 cellSize;
        public Vector2 spacing;

        public bool fitX;
        public bool fitY;

        public override void CalculateLayoutInputHorizontal() {
            base.CalculateLayoutInputHorizontal();

            if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform) {
                var sqrRt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }

            if (fitType == FitType.Width || fitType == FitType.FixedColumns) {
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
            }

            if (fitType == FitType.Height || fitType == FitType.FixedRows) {
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
            }

            var parentWidth = rectTransform.rect.width;
            var parentHeight = rectTransform.rect.height;

            var cellWidth =
                parentWidth / columns - spacing.x / columns * 2f - padding.left / (float) columns - padding.right / (float) columns;

            var cellHeight =
                parentHeight / rows - spacing.y / rows * 2f - padding.top / (float) rows - padding.bottom / (float) rows;

            cellSize.x = fitX ? cellWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;

            for (var i = 0; i < rectChildren.Count; i++) {
                var rowCount = i / columns;
                var columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = cellSize.x * columnCount + spacing.x * columnCount + padding.left;
                var yPos = cellSize.y * rowCount + spacing.y * rowCount + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical() { }

        public override void SetLayoutHorizontal() { }

        public override void SetLayoutVertical() { }
    }
}