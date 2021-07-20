using System.Linq;
using Exa.Math;
using Exa.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Input {
    public class InputManager : MonoBehaviour {
        [HideInInspector] public bool inputIsCaptured;

        private MouseCursorController mouseCursor;
        private Canvas root;

        public Vector2 MouseScaledViewportPoint { get; private set; }
        public Vector2 MouseScreenPoint { get; private set; }
        public Vector2 MouseViewportPoint { get; private set; }
        public Vector2 MouseWorldPoint { get; private set; }
        public Vector2 MouseOffsetFromCentre { get; private set; }

        private void Awake() {
            mouseCursor = S.UI.MouseCursor;
            root = S.UI.RootCanvas;
        }

        private void Update() {
            var mousePos = Mouse.current.position.ReadValue();
            MouseScaledViewportPoint = mousePos / root.scaleFactor;
            MouseScreenPoint = mousePos;
            MouseWorldPoint = S.CameraController.Camera.ScreenToWorldPoint(mousePos);
            MouseViewportPoint = S.CameraController.Camera.ScreenToViewportPoint(mousePos);
            MouseOffsetFromCentre = CalculateMouseOffsetFromCentre(MouseViewportPoint);

            mouseCursor.UpdateMouseInViewport(MathUtils.GetPointIsInViewport(MouseViewportPoint));
        }

        public bool GetMouseInsideRect(RectTransform rect) {
            return RectTransformUtility.RectangleContainsScreenPoint(rect, MouseScreenPoint, S.CameraController.Camera);
        }

        public bool GetMouseInsideRect(params RectTransform[] rects) {
            return rects.Any(GetMouseInsideRect);
        }

        private Vector2 CalculateMouseOffsetFromCentre(Vector2 mouseViewportPoint) {
            var min = new Vector2(-1, -1);
            var max = new Vector2(1, 1);
            var unclampedOffset = MathUtils.ViewportPointToCentreOffset(mouseViewportPoint);

            return unclampedOffset.Clamp(min, max);
        }
    }
}