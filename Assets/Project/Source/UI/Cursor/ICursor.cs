namespace Exa.UI {
    public enum CursorState {
        idle,
        active,
        remove,
        info,
        input
    }

    public interface ICursor {
        void SetActive(bool active);

        void SetState(CursorState cursorState);

        void OnEnterViewport();

        void OnExitViewport();
    }
}