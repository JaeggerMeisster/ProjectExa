﻿using System.Collections;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/Endless")]
    public class EndlessMission : Mission
    {
        public override void Init(MissionArgs args) {
            SpawnMothership(args.fleet.mothership.Data);
            StartCoroutine(Spawn());
            
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(0.5f);
            SpawnFriendly("defaultScout", 20, 20);

            yield return new WaitForSeconds(0.5f);
            SpawnEnemy("defaultScout", 30, 20);
        }
    }
}