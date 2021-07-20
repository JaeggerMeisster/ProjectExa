namespace Exa.UI.Components {
    public static class UIAnimateableHelper {
        public static int GetRotation(this AnimationDirection animationDirection) {
            return (int) animationDirection - 1;
        }
    }
}