using System;
using DG.Tweening;
using Exa.Types.Generics;
using Exa.UI.Tweening;

namespace Exa.Camera {
    [Serializable]
    public class CameraTargetSettings {
        public ExaEase ZoomEase = new ExaEase(Ease.InOutSine);
        public MinMax<float> zoomMinMax = new MinMax<float>(0.1f, 2.5f);
        public float zoomSpeed = 1f;
        public float orthoMultiplier = 30f;
    }
}