using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/ShipController")]
    public class ShipControllerTemplate : BlockTemplate {
        [SerializeField] private TemplatePartial<ShipControllerData> shipControllerPartial;

        public override IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            return base.GetTemplatePartials().Append(shipControllerPartial);
        }
    }
}