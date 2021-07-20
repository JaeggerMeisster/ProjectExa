using System;
using DG.Tweening;

namespace Exa.UI.Tweening {
    public class TweenWrapper<T> : TweenRef<T> {
        private readonly Func<T, float, Tween> tweenFactory;

        public TweenWrapper(Func<T, float, Tween> tweenFactory) {
            this.tweenFactory = tweenFactory;
        }

        protected override Tween CreateTween(T endValue, float duration) {
            return tweenFactory(endValue, duration);
        }
    }
}