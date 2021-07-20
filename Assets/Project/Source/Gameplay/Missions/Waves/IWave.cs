using System.Collections.Generic;
using Exa.Grids.Blueprints;

namespace Exa.Gameplay.Missions {
    public interface IWave {
        IEnumerable<Blueprint> GetSpawnAbleBlueprints();
    }
}