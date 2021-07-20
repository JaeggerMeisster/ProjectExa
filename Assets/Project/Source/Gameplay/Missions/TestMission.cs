using System.Collections;
using Exa.Grids;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    [CreateAssetMenu(menuName = "Missions/Test")]
    public class TestMission : Mission {
        private Spawner spawner;

        public override void Init(MissionManager manager, MissionArgs args) {
            base.Init(manager, args);
            spawner = new Spawner();
            GS.MissionManager.Station = spawner.SpawnPlayerStation(configuration: GridInstanceConfiguration.InvulnerableConfig);
            Spawn().Start();
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(1f);

            spawner.SpawnEnemy("defaultScout", 10, 0);
        }
    }
}