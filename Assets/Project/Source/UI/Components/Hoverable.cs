using Exa.Types.Generics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Exa.UI {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public UnityEvent onPointerEnter = new UnityEvent();
        public UnityEvent onPointerExit = new UnityEvent();
        public bool invokeStateChangeOnHover;
        public ValueOverride<CursorState> cursorOverride;
        public bool checkMouseInsideRectOnEnable = true;
        private CanvasGroup canvasGroup;

        private RectTransform rectTransform;

        private bool InvokeStateChange {
            get => invokeStateChangeOnHover && canvasGroup.interactable;
        }

        public bool MouseOverControl { get; private set; }

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable() {
            if (checkMouseInsideRectOnEnable) {
                CheckMouseInsideRect();
            }
        }

        private void OnDisable() {
            TryExit();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            TryEnter();
        }

        public void OnPointerExit(PointerEventData eventData) {
            TryExit();
        }

        public void ForceExit() {
            if (!MouseOverControl) {
                return;
            }

            MouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange) {
                OnExit();
            }
        }

        public void Refresh() {
            CheckMouseInsideRect(true);
        }

        private void TryEnter() {
            if (MouseOverControl) {
                return;
            }

            MouseOverControl = true;
            onPointerEnter?.Invoke();

            if (InvokeStateChange) {
                OnEnter();
            }
        }

        private void TryExit() {
            if (!MouseOverControl) {
                return;
            }

            MouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange) {
                OnExit();
            }
        }

        private void CheckMouseInsideRect(bool exit = false) {
            if (MouseOverControl && !exit) {
                return;
            }

            if (S.Input.GetMouseInsideRect(rectTransform)) {
                TryEnter();
            } else if (exit) {
                TryExit();
            }
        }

        private void OnEnter() {
            S.UI.MouseCursor.stateManager.Add(cursorOverride);
        }

        private void OnExit() {
            S.UI.MouseCursor.stateManager.Remove(cursorOverride);
        }
    }
}