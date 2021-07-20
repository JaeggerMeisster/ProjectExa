using Exa.Ships;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay {
    public class CoreHealthBar : MonoBehaviour, IGridOverlay {
        [SerializeField] private RectTransform bar;
        [SerializeField] private float maxHullSize;
        [SerializeField] private Text maxHullText;
        [SerializeField] private Text currentHullText;

        public void SetEnergyFill(float current, float max) { }

        public void SetHullFill(float current, float max) {
            bar.SetRight(4 + Mathf.Min((1 - current / max) * maxHullSize, maxHullSize));
            currentHullText.text = Mathf.RoundToInt(current).ToString();
            maxHullText.text = Mathf.RoundToInt(max).ToString();
        }
    }
}