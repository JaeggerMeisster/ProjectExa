﻿using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    public enum CycleMode {
        Cycling,
        Volley
    }

    [Serializable]
    public struct AutocannonData : ITurretValues {
        public float turningRate;
        public float firingRate;
        public float turretArc;
        public float turretRadius;
        public float damage;
        public CycleMode cycleMode;

        public float TurningRate {
            get => turningRate;
        }

        public float FiringRate {
            get => firingRate;
        }

        public float TurretArc {
            get => turretArc;
        }

        public float TurretRadius {
            get => turretRadius;
        }

        public float Damage {
            get => damage;
        }

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            return new ITooltipComponent[] {
                new LabeledValue<object>("Turning rate", $"{turningRate}°/s"),
                new LabeledValue<object>("Firing rate", $"{60 / firingRate} RPM"),
                new LabeledValue<object>("Firing arc", $"{TurretArc}°"),
                new LabeledValue<object>("Damage", $"{damage}"),
                new LabeledValue<object>("Cycle mode", $"{cycleMode}")
            };
        }
    }
}