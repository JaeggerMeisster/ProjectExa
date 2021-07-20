using UnityEngine;

namespace Exa.Gameplay {
    public class SpawnLayer : MonoBehaviour {
        public Transform overlay;
        public Transform projectiles;
        public Transform ships;

        public void SetLayerActive(bool active) {
            overlay.gameObject.SetActive(active);
            projectiles.gameObject.SetActive(active);
            ships.gameObject.SetActive(active);
        }
    }
}