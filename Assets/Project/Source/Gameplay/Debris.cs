using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using Exa.Ships;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Gameplay {
    public class Debris : MonoBehaviour, IGridInstance {
        [SerializeField] private Rigidbody2D rb;

        public BlockGrid BlockGrid { get; set; }

        public Rigidbody2D Rigidbody2D {
            get => rb;
        }

        public Transform Transform {
            get => transform;
        }

        public BlockContext BlockContext {
            get => BlockContext.Debris;
        }

        public GridInstanceConfiguration Configuration { get; private set; }

        public void FromCluster(Cluster cluster) {
            BlockGrid = new BlockGrid(this);

            Configuration = new GridInstanceConfiguration {
                Invulnerable = false
            };

            foreach (var block in cluster) {
                block.Parent.RemoveBlock(block);
                this.AddBlock(block, true);
            }

            foreach (var thruster in BlockGrid.QueryLike<ThrusterBehaviour>()) {
                thruster.PowerDown();
            }
        }
    }
}