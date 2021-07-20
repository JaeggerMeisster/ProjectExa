using Exa.Grids.Blocks;
using Exa.UI.Tooltips;
using TMPro;
using UnityEngine.UI;

public class BlockCostsView : TooltipComponentView<BlockCosts> {
    public TextMeshProUGUI credits;
    public TextMeshProUGUI metals;

    public Image creditsBackground;
    public Image metalsBackground;

    protected override void Refresh(BlockCosts value) {
        credits.text = value.creditCost.ToString();
        metals.text = value.metalsCost.ToString();
    }
}