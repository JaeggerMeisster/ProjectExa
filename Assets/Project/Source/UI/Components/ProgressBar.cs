using System;
using UnityEngine;

namespace Exa.UI.Components {
    public class ProgressBar : MonoBehaviour, IProgress<float> {
        [SerializeField] private RectTransform barTransform;

        public void Report(float value) {
            barTransform.localScale = new Vector2(value, 1);
        }
    }
}