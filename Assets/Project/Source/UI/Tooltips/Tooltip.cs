using System;
using UnityEngine;

namespace Exa.UI.Tooltips {
    // TODO: Support value refreshing, without destroying and instantiating ui objects
    public class Tooltip {
        private readonly Func<TooltipGroup> factory;
        private TooltipGroup group;

        public Tooltip(Func<TooltipGroup> factory, Font font = null) {
            this.factory = factory;
            ShouldRefresh = true;
            Font = font;
        }

        public bool ShouldRefresh { get; set; }
        public Font Font { get; }

        public TooltipGroup GetRootData() {
            if (ShouldRefresh) {
                group = factory();
            }

            return group;
        }
    }
}