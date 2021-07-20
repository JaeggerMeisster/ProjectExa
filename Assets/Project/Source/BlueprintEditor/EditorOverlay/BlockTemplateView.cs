using Exa.Data;
using Exa.Grids.Blocks;
using Exa.Types.Binding;
using Exa.UI;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    public class BlockTemplateView : MonoBehaviour, IObserver<BlockTemplate> {
        [Header("References")]
        public Button button;
        [SerializeField] private Text blockSizeText;
        [SerializeField] private Image image;
        [SerializeField] private Border activeBorder;
        [SerializeField] private Hoverable hoverable;

        [Header("Settings")]
        [SerializeField] private ActivePair<Color> backgroundColors;

        private BlockTemplate data;

        public bool Selected {
            set => activeBorder.SetVisibility(value);
        }

        private void Awake() {
            hoverable.onPointerEnter.AddListener(() => { S.UI.Tooltips.blockTemplateTooltip.Show(BlockContext.DefaultGroup, data); });
            hoverable.onPointerExit.AddListener(() => { S.UI.Tooltips.blockTemplateTooltip.Hide(); });
        }

        public void OnUpdate(BlockTemplate data) {
            this.data = data;
            image.sprite = data.thumbnail;
            blockSizeText.text = $"{data.size.x}x{data.size.y}";
        }
    }
}