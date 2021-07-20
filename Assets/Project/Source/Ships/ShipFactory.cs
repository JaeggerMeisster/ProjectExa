using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Ships {
    public class ShipFactory : MonoBehaviour {
        [SerializeField] private GameObject enemyShipPrefab;
        [SerializeField] private GameObject friendlyStationPrefab;
        [SerializeField] private GameObject shipOverlayPrefab;

        public PlayerStation CreateStation(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration? configuration = null) {
            return Configure<PlayerStation>(
                friendlyStationPrefab,
                worldPos,
                blueprint,
                BlockContext.UserGroup,
                configuration
            );
        }

        public EnemyGrid CreateEnemy(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration? configuration = null) {
            return Configure<EnemyGrid>(
                enemyShipPrefab,
                worldPos,
                blueprint,
                BlockContext.EnemyGroup,
                configuration
            );
        }

        private T Configure<T>(GameObject prefab, Vector2 worldPos, Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration? configuration)
            where T : GridInstance {
            var grid = prefab.Create<T>(GS.SpawnLayer.ships);

            grid.Import(
                blueprint,
                blockContext,
                configuration ??
                new GridInstanceConfiguration {
                    Invulnerable = false
                }
            );

            grid.SetPosition(worldPos);
            grid.name = grid.GetInstanceString();

            Physics2D.SyncTransforms();

            return grid;
        }

        public GridOverlay CreateOverlay(GridInstance gridInstance) {
            var overlayGo = Instantiate(shipOverlayPrefab, GS.SpawnLayer.overlay);
            gridInstance.ControllerDestroyed += () => { Destroy(overlayGo); };

            var overlay = overlayGo.GetComponent<GridOverlay>();
            overlay.SetGrid(gridInstance);
            overlay.Update();
            overlay.gameObject.name = $"Overlay: {gridInstance.GetInstanceString()}";

            return overlay;
        }
    }
}