using UnityEngine.InputSystem;

namespace Exa.Utils {
    public static class InputActionExtensions {
        public static void SetEnabled(this InputAction inputAction, bool value) {
            switch (inputAction.enabled) {
                case false when value:
                    inputAction.Enable();

                    break;
                case true when !value:
                    inputAction.Disable();

                    break;
            }
        }
    }
}