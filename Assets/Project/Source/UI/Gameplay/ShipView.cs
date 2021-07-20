using System.Collections.Generic;
using System.Linq;
using Exa.Ships;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Gameplay {
    public class ShipView : MonoBehaviour {
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private Text countText;
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderImage;
        [SerializeField] private Image thumbnailImage;
        private readonly List<GridInstance> ships = new List<GridInstance>();
        private int count;

        public int Count {
            get => count;
            private set {
                count = value;
                countText.text = count.ToString();
            }
        }

        private void Update() {
            var total = ships.Average(ship => ship.HullIntegrity);
            SetHull(total);
        }

        public void SetThumbnail(Texture2D thumbnail) {
            var thumbnailRect = new Rect(0, 0, 512, 512);
            var thumbnailPivot = new Vector2(0.5f, 0.5f);
            thumbnailImage.sprite = Sprite.Create(thumbnail, thumbnailRect, thumbnailPivot);
        }

        public void Add(GridInstance gridInstance) {
            ships.Add(gridInstance);
            Count++;
        }

        public void Remove(GridInstance gridInstance) {
            ships.Remove(gridInstance);
            Count--;
        }

        private void SetHull(float value) {
            slider.value = value;
            sliderImage.color = colorGradient.Evaluate(value);
        }
    }
}