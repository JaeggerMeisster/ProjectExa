using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Ships {
    public class ShipOverlayHullBar : MonoBehaviour {
        public Image actualFillImage;
        public Image rememberedFillImage;
        [SerializeField] private readonly float rememberedFillSpeed = 0.4f;

        [SerializeField] private readonly float timeToUpdateRememberedFill = 3f;
        private float actualFill = 1f;
        private float rememberedFill = 1f;
        private float timeSinceFillChange;

        private float ActualFill {
            get => actualFill;
            set {
                actualFill = value;
                actualFillImage.fillAmount = value;
            }
        }

        private float RememberedFill {
            get => rememberedFill;
            set {
                rememberedFill = value;
                rememberedFillImage.fillAmount = value;
            }
        }

        private void Update() {
            timeSinceFillChange += Time.deltaTime;

            if (timeSinceFillChange > timeToUpdateRememberedFill) {
                var speed = Time.deltaTime * rememberedFillSpeed;
                RememberedFill = MathUtils.Increment(RememberedFill, ActualFill, speed);
            }
        }

        public void SetFill(float value) {
            if (ActualFill == value) {
                return;
            }

            ActualFill = value;
            timeSinceFillChange = 0f;
        }
    }
}