using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.UI.Tooltips {
    public struct TooltipGroup : ITooltipComponent {
        public IEnumerable<ITooltipComponent> Children { get; private set; }
        public int Tabs { get; }
        public int Spacing { get; }

        public TooltipGroup(IEnumerable<ITooltipComponent> children, int tabs = 0, int spacing = 10) {
            Children = children ??
                throw new ArgumentNullException(
                    nameof(children),
                    "A collection of children must not be null"
                );

            Tabs = tabs;
            Spacing = spacing;
        }

        public TooltipGroup(int tabs, params ITooltipComponent[] children) {
            Tabs = tabs;
            Spacing = 10;

            Children = children ??
                throw new ArgumentNullException(
                    nameof(children),
                    "A collection of children must not be null"
                );
        }

        public TooltipGroup AppendRange(params ITooltipComponent[] children) {
            Children = Children.Concat(children);

            return this;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return S.UI.Tooltips.tooltipGenerator.GenerateTooltipGroup(parent, this);
        }
    }
}