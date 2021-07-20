using System.Collections.Generic;
using Exa.Types.Generics;
using Exa.UI.Tooltips;

namespace Exa.Grids.Blocks.Components {
    public interface ITurretValues : IBlockComponentValues {
        float TurningRate { get; } // in degrees rotation per second
        float FiringRate { get; } // As seconds elapsed between each shot
        float TurretArc { get; }
        float TurretRadius { get; }
        float Range { get; }
    }

    public static class ITurretValuesExtensions {
        public static MinMax<float> GetTurretArcMinMax(this ITurretValues values) {
            return new MinMax<float>(-(values.TurretArc / 2f), values.TurretArc / 2f);
        }
    }
}