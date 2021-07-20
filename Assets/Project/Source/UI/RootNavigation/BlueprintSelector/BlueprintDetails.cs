using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class BlueprintDetails : MonoBehaviour {
        [SerializeField] private GameObject container;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text nameText;
        [SerializeField] private PropertyView blockCountView;
        [SerializeField] private PropertyView sizeView;
        [SerializeField] private PropertyView massView;
        [SerializeField] private PropertyView energyView;

        private void OnEnable() {
            container.SetActive(false);
        }

        public void Reflect(Blueprint blueprint) {
            if (blueprint == null) {
                container.SetActive(false);

                return;
            }

            if (!container.activeSelf) {
                container.SetActive(true);
            }

            try {
                var thumbnailRect = new Rect(0, 0, 512, 512);
                var thumbnailPivot = new Vector2(0.5f, 0.5f);
                thumbnailImage.sprite = Sprite.Create(blueprint.Thumbnail, thumbnailRect, thumbnailPivot);
            } catch {
                Debug.LogWarning("Error setting blueprint thumbnail");
            }

            nameText.text = blueprint.name;
            Vector2Int size = blueprint.Grid.Size;

            var totals = blueprint.Grid.GetTotals(BlockContext.UserGroup);

            blockCountView.SetValue(blueprint.Grid.GetMemberCount());
            sizeView.SetValue($"{size.x}x{size.y}");
            massView.SetValue($"{totals.Mass:0} Tonne");
        }
    }
}