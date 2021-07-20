using System;
using System.Collections.Generic;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips {
    [Serializable]
    public class GroupView : TooltipComponentView<TooltipGroup> {
        public Transform container;

        [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
        private List<TooltipComponentView> views;

        protected override void Refresh(TooltipGroup value) {
            verticalLayoutGroup.padding.left = value.Tabs * 8;
            verticalLayoutGroup.spacing = value.Spacing;

            if (views == null) {
                views = new List<TooltipComponentView>();

                foreach (var child in value.Children) {
                    views.Add(child.InstantiateComponentView(container));
                }
            } else {
                foreach (var tuple in value.Children.AsTupleEnumerable(views)) {
                    tuple.Item2.Refresh(tuple.Item1);
                }
            }
        }
    }
}