﻿using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class PowerGeneratorTemplatePartial : TemplatePartial<PowerGeneratorData> {
        [SerializeField] private float powerGeneration; // In MW

        public override PowerGeneratorData ToBaseComponentValues() {
            return new PowerGeneratorData {
                powerGeneration = powerGeneration
            };
        }
    }
}