using Exa.Gameplay;
using Exa.Project.Source.UI.Gameplay;
using Exa.UI.Components;

namespace Exa.UI.Gameplay {
    public class GameplayLayer : Navigateable {
        public SelectionOverlay selectionOverlay;
        public SelectionHotbar selectionHotbar;
        public SelectionArea selectionArea;
        public CoreHealthBar coreHealthBar;
        public BlockCostsView currentResources;
        public MissionState missionState;
        public DamageOverlay damageOverlay;
        public EnemyWarningCircleManager warningCircleOverlay;
    }
}