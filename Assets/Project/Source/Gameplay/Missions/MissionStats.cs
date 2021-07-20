using Exa.Grids.Blocks;

namespace Exa.Gameplay.Missions {
    public class MissionStats {
        public BlockCosts CollectedResources { get; set; }
        public int DestroyedShips { get; set; }

        public int CollectedResourcesScore {
            get => CollectedResources.creditCost;
        }

        public int DestroyedShipsScore {
            get => DestroyedShips * 100;
        }

        public int TotalScore {
            get => CollectedResourcesScore + DestroyedShipsScore;
        }
    }
}