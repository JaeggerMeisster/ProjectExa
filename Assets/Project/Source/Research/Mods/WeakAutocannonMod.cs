using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Research {
    [CreateAssetMenu(menuName = "Research/WeakAutocannonMod")]
    public class WeakAutocannonMod : BlockComponentModifier<AutocannonData> {
        protected override void MultiplicativeStep(AutocannonData initialData, ref AutocannonData currentData) {
            currentData.damage *= 0.01f;
        }
    }
}