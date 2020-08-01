﻿using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Generics
{
    public struct TooltipTitle : ITooltipComponent
    {
        public string Text { get; private set; }

        public TooltipTitle(string text)
        {
            Text = text;
        }

        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return Systems.MainUI.variableTooltipManager.tooltipGenerator.GenerateTooltipTitle(this, parent);
        }
    }
}