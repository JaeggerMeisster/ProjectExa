using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/ShieldGenerator")]
    public class ShieldGeneratorTemplate : BlockTemplate {
        [SerializeField] protected TemplatePartial<ShieldGeneratorData> shieldGeneratorPartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials().Append(shieldGeneratorPartial);
        }
    }
}