using Exa.Grids.Blueprints;
using Exa.Types.Binding;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class BlueprintView : MonoBehaviour, IObserver<Blueprint> {
        [Header("References")]
        public Button deleteButton;
        public Button button;
        public Hoverable hoverable;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Text classText;

        public void OnUpdate(Blueprint data) {
            nameText.text = data.name;
            classText.text = data.BlueprintType.displayName;

            try {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                thumbnailImage.sprite = Sprite.Create(data.Thumbnail, thumbnailRect, thumbnailPivot);
            } catch {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }
        }
    }
}