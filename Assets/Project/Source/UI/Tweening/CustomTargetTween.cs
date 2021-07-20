using DG.Tweening;
using DG.Tweening.Core;

namespace Exa.UI.Tweening {
    public abstract class CustomTargetTween<T> : TweenRef<T> {
        protected DOGetter<T> getter;
        protected DOSetter<T> setter;

        public CustomTargetTween<T> DOGetter(DOGetter<T> getter) {
            this.getter = getter;

            return this;
        }

        public CustomTargetTween<T> DOSetter(DOSetter<T> setter) {
            this.setter = setter;

            return this;
        }
    }

    public class FloatTween : CustomTargetTween<float> {
        protected override Tween CreateTween(float endValue, float duration) {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }

    public class IntTween : CustomTargetTween<int> {
        protected override Tween CreateTween(int endValue, float duration) {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}