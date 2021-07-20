using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.Ships {
    public class ShipOverlayEnergyBar : MonoBehaviour {
        [SerializeField] private Image image;

        public void SetFill(float fill) {
            image.fillAmount = fill;
        }
    }
}