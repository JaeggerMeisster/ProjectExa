using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Weapons {
    /// <summary>
    /// Basic penetrative projectile
    /// Projectile is destroyed once the projectile goes beyond its range, or once it has dealt its full potential damage
    /// </summary>
    public class Projectile : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        private Damage damage;
        private BlockContext damageMask;
        private float lifeTime;
        private float timeAlive;

        public void Update() {
            // Instead of tracking the distance from the spawn position for range logic,
            // count the time the projectile is alive and compare it to its projected lifetime
            var deltaTime = Time.deltaTime;
            timeAlive += deltaTime;

            if (timeAlive > lifeTime) {
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            var damageable = collider.GetComponent<IDamageable>();

            if (!damageable.PassesDamageMask(damageMask)) {
                return;
            }

            // Get the absorbed points of damage of the damageable, subtract from the pooled amount of damage
            damage.value -= damageable.TakeDamage(damage).absorbedDamage;

            // Once the pooled damage is below 0, destroy the projectile
            if (damage.value <= 0f) {
                Destroy(gameObject);
            }
        }

        // TODO: Set rotation of the projectile
        /// <summary>
        /// Set initial properties of the projectile
        /// </summary>
        /// <param name="spawnPoint">Transform of the firing point for the projectile.
        /// Position and heading are inherited from the spawn point</param>
        /// <param name="speed">Units per second the projectile should be traveling at</param>
        /// <param name="range">Amount of units the projectile before it is destroyed</param>
        /// <param name="damage">Damage to apply when hitting damageable that pass the damage mask</param>
        /// <param name="damageMask">BlockContext filter to apply when hitting damageables</param>
        public void Setup(Transform spawnPoint, float speed, float range, Damage damage, BlockContext damageMask) {
            transform.position = spawnPoint.position;
            lifeTime = range / speed;
            this.damage = damage;
            this.damageMask = damageMask;

            rb.velocity = spawnPoint.right * speed;
        }
    }
}