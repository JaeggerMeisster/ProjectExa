using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Autocannon")]
    public class AutocannonTemplate : BlockTemplate {
        [SerializeField] private TemplatePartial<AutocannonData> autocannonPartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials().Append(autocannonPartial);
        }
    }
}