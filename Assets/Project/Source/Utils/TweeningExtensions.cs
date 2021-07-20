using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

namespace Exa.Utils {
    public static class TweeningExtensions {
        public static Tween Replace(this Tween tween, ref Tween oldTween) {
            oldTween?.Kill();

            return oldTween = tween;
        }

    #region Shortcuts

    #region Transform

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DORotate(
            this Transform target,
            Vector3 endValue,
            float duration
        ) {
            return ShortcutExtensions.DORotate(target, endValue, duration);
        }

    #endregion

    #region RectTransform

        public static TweenerCore<Vector2, Vector2, VectorOptions> DOSizeDelta(
            this RectTransform target,
            Vector2 endValue,
            float duration
        ) {
            return DOTweenModuleUI.DOSizeDelta(target, endValue, duration);
        }

        public static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPos(
            this RectTransform target,
            Vector2 endValue,
            float duration
        ) {
            return DOTweenModuleUI.DOAnchorPos(target, endValue, duration);
        }

    #endregion

    #region Text

        public static Tween DOFontSize(
            this Text target,
            int endValue,
            float duration
        ) {
            return DOTween
                .To(() => target.fontSize, x => target.fontSize = x, endValue, duration)
                .SetTarget(target);
        }

    #endregion

    #region LayoutElement

        public static TweenerCore<float, float, FloatOptions> DOPreferredHeight(
            this LayoutElement target,
            float endValue,
            float duration
        ) {
            return DOTween
                .To(() => target.preferredHeight, x => target.preferredHeight = x, endValue, duration)
                .SetTarget(target);
        }

        public static TweenerCore<float, float, FloatOptions> DOPreferredWidth(
            this LayoutElement target,
            float endValue,
            float duration
        ) {
            return DOTween
                .To(() => target.preferredWidth, x => target.preferredWidth = x, endValue, duration)
                .SetTarget(target);
        }

    #endregion

    #region HorizontalLayoutGroup

        public static TweenerCore<float, float, FloatOptions> DOSpacing(
            this HorizontalLayoutGroup target,
            float endValue,
            float duration
        ) {
            return DOTween
                .To(() => target.spacing, x => target.spacing = x, endValue, duration)
                .SetTarget(target);
        }

    #endregion

    #region Lights

        public static TweenerCore<float, float, FloatOptions> DOIntensity(
            this Light2D target,
            float value,
            float time
        ) {
            return DOTween
                .To(() => target.intensity, x => target.intensity = x, value, time)
                .SetTarget(target);
        }

    #endregion

    #region LineRenderer

        public static TweenerCore<float, float, FloatOptions> DOWidthMultiplier(
            this LineRenderer target,
            float value,
            float time
        ) {
            return DOTween
                .To(() => target.widthMultiplier, x => target.widthMultiplier = x, value, time)
                .SetTarget(target);
        }

    #endregion

    #endregion
    }
}