using DG.Tweening;
using Exa.Data;
using Exa.Grids.Blueprints;
using Exa.Types.Binding;
using Exa.UI.Components;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class FleetBuilderBlueprintView : MonoBehaviour, IObserver<Blueprint> {
        [Header("References")]
        public Button button;
        public Hoverable hoverable;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private RectTransform titleRectTransform;
        [SerializeField] private Text titleText;
        [SerializeField] private CanvasGroup titleCanvasGroup;
        [SerializeField] private Transform border;
        [SerializeField] private Transform overlay;
        [SerializeField] private Border borderOutline;
        [SerializeField] private Text overlayText;

        [Header("Settings")]
        [SerializeField] private ActivePair<Color> selectedColor;

        private bool selected;

        public BlueprintTypeTabContent ParentTab { get; set; }

        public bool Selected {
            get => selected;
            set {
                var color = selectedColor.GetValue(value);
                borderOutline.Color = color;
                overlayText.color = color;
                overlayText.text = value ? "Remove" : "Select";
                hoverable.cursorOverride.Value = value ? CursorState.remove : CursorState.active;
                selected = value;
            }
        }

        public void OnUpdate(Blueprint data) {
            titleText.text = data.name;

            try {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                thumbnailImage.sprite = Sprite.Create(data.Thumbnail, thumbnailRect, thumbnailPivot);
            } catch {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }
        }

        public void OnPointerEnter() {
            titleRectTransform.DOAnchorPos(new Vector2(0f, -10f), 0.2f);
            titleCanvasGroup.DOFade(1, 0.2f);
        }

        public void OnPointerExit() {
            titleRectTransform.DOAnchorPos(new Vector2(0f, 40f), 0.2f);
            titleCanvasGroup.DOFade(0f, 0.2f);
        }
    }
}