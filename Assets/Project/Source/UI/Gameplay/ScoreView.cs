using Exa.Gameplay.Missions;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.UI.Gameplay {
    public class ScoreView : MonoBehaviour {
        [SerializeField] private PropertyView collectedResourcesView;
        [SerializeField] private PropertyView enemiesDestroyedView;
        [SerializeField] private PropertyView totalScoreView;

        public void PresentStats(MissionStats stats) {
            collectedResourcesView.SetLabel($"{stats.CollectedResources.creditCost}x credits collected");
            collectedResourcesView.SetValue(stats.CollectedResourcesScore);

            enemiesDestroyedView.SetLabel($"{stats.DestroyedShips}x enemies destroyed");
            enemiesDestroyedView.SetValue(stats.DestroyedShipsScore);

            totalScoreView.SetValue(stats.TotalScore);
        }
    }
}