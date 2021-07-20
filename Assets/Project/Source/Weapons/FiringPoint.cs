using Exa.Grids.Blocks;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Weapons {
    public class FiringPoint : MonoBehaviour {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform spawnPoint;

        private BlockContext damageMask;

        public void Setup(BlockContext damageMask) {
            this.damageMask = damageMask;
        }

        public void Fire(Damage damage, float speed, float range) {
            // TODO: Pool projectiles
            projectilePrefab.Create<Projectile>(GS.SpawnLayer.projectiles)
                .Setup(
                    spawnPoint,
                    speed,
                    range,
                    damage,
                    damageMask
                );
        }
    }
}