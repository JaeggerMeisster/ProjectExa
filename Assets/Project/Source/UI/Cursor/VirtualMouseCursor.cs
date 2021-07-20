using System;
using DG.Tweening;
using Exa.Data;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class VirtualMouseCursor : MonoBehaviour, ICursor {
        [Header("References")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private CanvasGroup backgroundGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform normalCursor;
        [SerializeField] private RectTransform inputCursor;

        [Header("Color settings")]
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color removeColor;
        [SerializeField] private Color infoColor;
        [SerializeField] private float cursorColorAnimTime = 0.25f;

        [Header("Size anim settings")]
        [SerializeField] private float cursorScaleAnimTime = 0.25f;
        [SerializeField] private ActivePair<float> hoverableCursorSize;
        [SerializeField] private ActivePair<CursorSizeAnimSettings> sizeAnimSettings;

        [Header("Drag anim settings")]
        [SerializeField] private float cursorDragDistanceTolerance = 30f;
        [SerializeField] private ActivePair<CursorDragAnimSettings> dragAnimSettings;

        [Header("Input")]
        [SerializeField] private InputAction inputAction;
        private Tween cursorClickAlphaTween;
        private Tween cursorColorTween;
        private Tween cursorDragRotateTween;

        private CursorDragState cursorDragState = CursorDragState.NotDragging;
        private FloatTweenBlender cursorScaleBlender;
        private CursorState cursorState = CursorState.idle;

        public Vector2? DragPivot { get; set; }

        private void Update() {
            var viewportPoint = S.Input.MouseScaledViewportPoint;
            rectTransform.anchoredPosition = viewportPoint;
            cursorScaleBlender.Update();
            UpdateCursorScaleAnim(viewportPoint);
        }

        private void OnEnable() {
            inputAction.Enable();
        }

        private void OnDisable() {
            inputAction.Disable();
        }

        public void SetActive(bool active) {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState state) {
            SwitchActive(cursorState, false);
            SwitchActive(state, true);
            cursorState = state;

            backgroundImage.DOColor(GetCursorColor(state), cursorColorAnimTime)
                .Replace(ref cursorColorTween);
        }

        public void OnEnterViewport() {
            gameObject.SetActive(true);
            Cursor.visible = false;
        }

        public void OnExitViewport() {
            gameObject.SetActive(false);
            Cursor.visible = true;
        }

        public void Init(CursorStateOverrideList overrides) {
            overrides.ContainsItemChange.AddListener(active => { cursorScaleBlender.To(0, hoverableCursorSize.GetValue(active), cursorScaleAnimTime); });

            inputAction.started += OnLeftMouseStarted;
            inputAction.canceled += OnLeftMouseCanceled;

            cursorScaleBlender = new FloatTweenBlender(
                1f,
                value => rectTransform.localScale = new Vector3(value, value, 1),
                (current, blender) => current * blender
            );
        }

        private void UpdateCursorScaleAnim(Vector2 viewportPoint) {
            if (!DragPivot.HasValue || cursorDragState == CursorDragState.NotDragging) {
                return;
            }

            var difference = DragPivot.Value - viewportPoint;

            if (cursorDragState == CursorDragState.BeginDragging && difference.magnitude > cursorDragDistanceTolerance) {
                cursorDragState = CursorDragState.Dragging;
            }

            if (cursorDragState == CursorDragState.Dragging) {
                var angle = (difference.GetAngle() + 270f - 22.5f) % 360;
                AnimCursorDirection(angle, dragAnimSettings.active);
            }
        }

        private void OnLeftMouseStarted(InputAction.CallbackContext context) {
            AnimCursorSize(sizeAnimSettings.active);
            cursorDragState = CursorDragState.BeginDragging;
            DragPivot = S.Input.MouseScaledViewportPoint;
        }

        private void OnLeftMouseCanceled(InputAction.CallbackContext context) {
            AnimCursorSize(sizeAnimSettings.inactive);
            cursorDragState = CursorDragState.NotDragging;
            DragPivot = null;
            AnimCursorDirection(0f, dragAnimSettings.inactive);
        }

        private void AnimCursorSize(CursorSizeAnimSettings args) {
            cursorScaleBlender.To(1, args.sizeTarget, args.animTime)
                .SetEase(args.ease);

            backgroundGroup.DOFade(args.alphaTarget, args.animTime)
                .SetEase(args.ease)
                .Replace(ref cursorClickAlphaTween);
        }

        private void AnimCursorDirection(float angle, CursorDragAnimSettings args) {
            var targetVector = new Vector3(0, 0, angle);

            normalCursor.DORotate(targetVector, args.animTime)
                .SetEase(args.ease)
                .Replace(ref cursorDragRotateTween);
        }

        private void SwitchActive(CursorState state, bool active) {
            switch (state) {
                case CursorState.idle:
                case CursorState.active:
                case CursorState.remove:
                case CursorState.info:
                    normalCursor.gameObject.SetActive(active);

                    break;

                case CursorState.input:
                    inputCursor.gameObject.SetActive(active);

                    break;
            }
        }

        private Color GetCursorColor(CursorState state) {
            switch (state) {
                case CursorState.idle:
                    return idleColor;
                case CursorState.active:
                    return activeColor;
                case CursorState.remove:
                    return removeColor;
                case CursorState.info:
                    return infoColor;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Serializable]
        private struct CursorSizeAnimSettings {
            public float sizeTarget;
            public float alphaTarget;
            public float animTime;
            public ExaEase ease;
        }

        [Serializable]
        private struct CursorDragAnimSettings {
            public float animTime;
            public ExaEase ease;
        }

        private enum CursorDragState {
            NotDragging,
            BeginDragging,
            Dragging
        }
    }
}