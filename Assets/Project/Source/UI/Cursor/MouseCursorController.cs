using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI {
    public enum CursorType {
        HardwareCursor,
        VirtualCursor
    }

    public class MouseCursorController : MonoBehaviour {
        [SerializeField] private CursorType cursorType = CursorType.HardwareCursor;
        [SerializeField] private VirtualMouseCursor virtualMouseCursor;
        [SerializeField] private HardwareMouseCursor hardwareMouseCursor;
        [SerializeField] private CursorState cursorState;
        public CursorStateOverrideList stateManager;

        public bool MouseInViewport { get; private set; }
        public ICursor CurrentCursor { get; private set; }

        private void Start() {
            // Activates the default cursor type
            SetCursor(cursorType);

            stateManager = new CursorStateOverrideList(cursorState, CurrentCursor.SetState);
            virtualMouseCursor.Init(stateManager);
            CurrentCursor.SetState(cursorState);
        }

        public void SetCursor(CursorType cursorType) {
            CurrentCursor?.SetActive(false);
            CurrentCursor = GetCursor(cursorType);
            CurrentCursor.SetActive(true);
        }

        public void UpdateMouseInViewport(bool value) {
            if (value != MouseInViewport) {
                MouseInViewport = value;
                SetMouseInViewport(value);
            }
        }

        public void SetMouseInViewport(bool value) {
            if (value) {
                CurrentCursor.OnEnterViewport();
            } else {
                CurrentCursor.OnExitViewport();
            }
        }

        private ICursor GetCursor(CursorType cursorType) {
            switch (cursorType) {
                case CursorType.HardwareCursor:
                    return hardwareMouseCursor;

                case CursorType.VirtualCursor:
                    return virtualMouseCursor;

                default:
                    return null;
            }
        }
    }
}