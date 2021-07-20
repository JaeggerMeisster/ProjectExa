using System;
using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public struct GaussCannonData : IChargeableTurretValues {
        public float turningRate;
        public float turretArc;
        public float turretRadius;
        public float damage;
        public float chargeTime;
        public float range;

        public float Range {
            get => range;
        }

        public float TurningRate {
            get => turningRate;
        }

        public float FiringRate {
            get => 0f;
        }

        public float TurretArc {
            get => turretArc;
        }

        public float TurretRadius {
            get => turretRadius;
        }

        public float ChargeTime {
            get => chargeTime;
        }

        public void AddGridTotals(GridTotals totals) { }

        public void RemoveGridTotals(GridTotals totals) { }

        public IEnumerable<ITooltipComponent> GetTooltipComponents() {
            // TO BE LOCALIZED
            yield return new LabeledValue<object>("Turning rate", $"{turningRate}°/s");
            yield return new LabeledValue<object>("Charge time", $"{chargeTime}s");
            yield return new LabeledValue<object>("Firing arc", $"{TurretArc}°");
            yield return new LabeledValue<object>("Damage", $"{damage}");
        }
    }
}