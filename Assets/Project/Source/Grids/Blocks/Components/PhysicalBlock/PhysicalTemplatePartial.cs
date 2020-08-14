﻿using Exa.Generics;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    [Serializable]
    public class PhysicalTemplatePartial : TemplatePartial<PhysicalData>
    {
        [SerializeField] private float maxHull;
        [SerializeField] private float armor;
        [SerializeField] private short mass; // In kg

        public float MaxHull => maxHull;
        public float Armor => armor;
        public float Mass => mass;

        public override BlockBehaviourBase AddSelf(Block block)
        {
            return SetupBehaviour<PhysicalBehaviour>(block);
        }

        public override PhysicalData Convert() => new PhysicalData
        {
            armor = armor,
            hull = maxHull,
            mass = mass
        };

        public override IEnumerable<ITooltipComponent> GetTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipSpacer(),
            new NamedValue<string>("Hull", maxHull.ToString()),
            new NamedValue<string>("Armor", armor.ToString()),
            new NamedValue<string>("Mass", mass.ToString())
        };
    }
}