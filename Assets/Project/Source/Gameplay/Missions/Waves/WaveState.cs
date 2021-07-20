using System;
using Exa.Ships;

namespace Exa.Gameplay.Missions {
    public class WaveState {
        private bool finishedSpawning;

        private int totalCount;
        private int totalDestroyed;

        public event Action OnWaveEnded;

        public void OnEnemySpawned(EnemyGrid grid) {
            totalCount++;

            grid.ControllerDestroyed += OnEnemyDestroyed;
        }

        public void OnFinishSpawning() {
            finishedSpawning = true;
        }

        private void OnEnemyDestroyed() {
            totalDestroyed++;

            if (totalDestroyed == totalCount && finishedSpawning) {
                OnWaveEnded?.Invoke();
            }
        }
    }
}