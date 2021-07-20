using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Exa.Utils;

namespace Exa.UI.Tweening {
    public class TweenBlender<T, TTarget>
        where T : struct
        where TTarget : CustomTargetTween<T>, new() {
        private readonly Dictionary<int, Blender> blenders = new Dictionary<int, Blender>();
        private readonly T defaultValue;
        private readonly Action<T> setter;
        protected Func<T, T, T> aggregator;

        public TweenBlender(T defaultValue, Action<T> setter, Func<T, T, T> aggregator) {
            this.defaultValue = defaultValue;
            this.setter = setter;
            this.aggregator = aggregator;
        }

        public Tween To(int id, T endValue, float time) {
            return To(id, SelectValue(id), endValue, time);
        }

        public Tween To(int id, T startValue, T endValue, float duration) {
            blenders.EnsureCreated(
                id,
                () => new Blender {
                    tween = new TTarget()
                        .DOGetter(() => blenders[id].value)
                        .DOSetter(x => blenders[id].value = x)
                }
            );

            blenders[id].value = startValue;

            return blenders[id]
                .tween
                .To(endValue, duration);
        }

        public void Update() {
            var blenders = this.blenders.Values;
            var blendedValue = BlendValues(defaultValue, blenders.Select(b => b.value));
            setter(blendedValue);
        }

        public void Kill() {
            foreach (var tween in blenders.Values.Select(b => b.tween.Tween)) {
                tween.Kill();
            }
        }

        protected virtual T BlendValues(T value, IEnumerable<T> blenders) {
            if (aggregator == null) {
                throw new InvalidOperationException("Cannot blend values without a given aggregator function");
            }

            return blenders.Aggregate(value, aggregator);
        }

        private T SelectValue(int id) {
            return blenders.ContainsKey(id)
                ? blenders[id].value
                : defaultValue;
        }

        private class Blender {
            public TweenRef<T> tween;
            public T value;
        }
    }

    public class FloatTweenBlender : TweenBlender<float, FloatTween> {
        public FloatTweenBlender(float defaultValue, Action<float> setter, Func<float, float, float> aggregator)
            : base(defaultValue, setter, aggregator) { }
    }
}