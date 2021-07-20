using System;
using DG.Tweening;

namespace Exa.UI.Tweening {
    public abstract class TweenRef<T> {
        private float? defaultDuration;

        public Tween Tween { get; private set; }

        public Tween To(T endValue) {
            return To(
                endValue,
                defaultDuration ?? throw new InvalidOperationException("A default duration must be set")
            );
        }

        public Tween To(T endValue, float time) {
            Tween?.Kill();
            Tween = CreateTween(endValue, time);

            return Tween;
        }

        public TweenRef<T> SetDuration(float duration) {
            defaultDuration = duration;

            return this;
        }

        protected abstract Tween CreateTween(T endValue, float duration);
    }
}