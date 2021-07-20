using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay {
    public class BlockGridManager : MonoBehaviour {
        [SerializeField] private GameObject debrisGridPrefab;
        private Queue<IGridInstance> gridInstances = new Queue<IGridInstance>();

        /// <summary>
        /// Schedules a grid to be rebuilt the next frame. This batches multiple rebuild requests
        /// </summary>
        /// <param name="gridInstance"></param>
        public void ScheduleRebuild(IGridInstance gridInstance) {
            if (!gridInstances.Contains(gridInstance)) {
                gridInstances.Enqueue(gridInstance);
            }
        }

        private void Update() {
            // Drain queue
            while (gridInstances.Count > 0) {
                AttemptRebuild(gridInstances.Dequeue());
            }
        }

        private void AttemptRebuild(IGridInstance gridInstance) {
            var blockGrid = gridInstance.BlockGrid;

            if (!blockGrid.Any()) {
                Debug.LogWarning("Grid found with no block ?");

                return;
            }

            // Get the clusters associated 
            var clusters = GetClusters(blockGrid).ToArray();

            // Rebuild the grid, if multiple clusters were found;
            if (clusters.Length > 1) {
                Rebuild(gridInstance, clusters);
            }
        }

        /// <summary>
        /// Split a grid instance into multiple grid instances
        /// </summary>
        /// <param name="gridInstance">Initial grid instance</param>
        /// <param name="clusters">Clusters of the grid instance that need to be separated from the grid instance</param>
        private void Rebuild(IGridInstance gridInstance, IEnumerable<Cluster> clusters) {
            var blockGrid = gridInstance.BlockGrid;

            // Set a flag that lets the block grid know it's being rebuilt
            // This prevents the block grid from scheduling a rebuild when blocks are removed 
            blockGrid.Rebuilding = true;
            var clustersScheduledForRebuild = clusters.Where(cluster => !cluster.containsController);

            foreach (var cluster in clustersScheduledForRebuild) {
                var debris = debrisGridPrefab.Create<Debris>(transform);
                debris.FromCluster(cluster);
                debris.Rigidbody2D.velocity = gridInstance.Rigidbody2D.velocity;
            }

            blockGrid.Rebuilding = false;
            blockGrid.DestroyIfEmpty();
        }

        /// <summary>
        /// Generates the clusters of a grid
        /// </summary>
        /// <param name="blockGrid">Block grid</param>
        /// <returns></returns>
        private IEnumerable<Cluster> GetClusters(BlockGrid blockGrid) {
            var vacantBlocks = new List<Block>(blockGrid);

            while (vacantBlocks.Count > 0) {
                var firstBlock = vacantBlocks.First();

                yield return GetCluster(firstBlock.GridAnchor, vacantBlocks, blockGrid);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Floodfills a grid to get a single cluster of blocks
        /// </summary>
        /// <param name="startPoint">Point in grid to start floodfilling</param>
        /// <param name="vacantBlocks">Collection of blocks that are yet to be added to any cluster</param>
        /// <param name="blockGrid">Block grid</param>
        /// <returns></returns>
        private Cluster GetCluster(Vector2Int startPoint, ICollection<Block> vacantBlocks, BlockGrid blockGrid) {
            var cluster = new Cluster();
            var visitedGridPos = new HashSet<Vector2Int>();

            // Flood fill using loop instead of tail recursion
            void FloodFill(int x, int y) {
                while (true) {
                    var gridPos = new Vector2Int(x, y);

                    if (!visitedGridPos.Add(gridPos)) {
                        return;
                    }

                    if (!blockGrid.TryGetMember(gridPos, out var member)) {
                        return;
                    }

                    if (vacantBlocks.Contains(member)) {
                        vacantBlocks.Remove(member);
                        cluster.Add(member);

                        if (member.GetIsController()) {
                            if (cluster.containsController) {
                                Debug.LogWarning($"Cluster {cluster} contains multiple controllers, this shouldn't happen");
                            }

                            cluster.containsController = true;
                        }
                    }

                    FloodFill(x - 1, y);
                    FloodFill(x + 1, y);
                    FloodFill(x, y - 1);
                    y++;
                }
            }

            FloodFill(startPoint.x, startPoint.y);

            return cluster;
        }
    }
}