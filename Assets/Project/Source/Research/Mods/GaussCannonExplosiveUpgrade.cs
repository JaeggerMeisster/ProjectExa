using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Research {
    [CreateAssetMenu(menuName = "Research/GaussCannonExplosiveUpgrade")]
    public class GaussCannonExplosiveUpgrade : BlockComponentModifier<GaussCannonData> {
        protected override void AdditiveStep(GaussCannonData initialData, ref GaussCannonData currentData) {
            currentData.damage += 10;
        }

        protected override void MultiplicativeStep(GaussCannonData initialData, ref GaussCannonData currentData) {
            currentData.damage += initialData.damage * 0.5f;
        }
    }
}