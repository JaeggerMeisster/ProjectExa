using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/GaussCannon")]
    public class GaussCannonTemplate : StationControllerTemplate {
        [SerializeField] private TemplatePartial<GaussCannonData> gaussCannonPartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials().Append(gaussCannonPartial);
        }
    }
}