using System.Collections.Generic;
using Exa.Grids.Blocks.BlockTypes;

namespace Exa.Gameplay {
    /// <summary>
    /// A cluster represents a temporary collection of blocks, currently part of a grid.
    /// Clusters that doesn't contain a controller will be split from the parent grid into a debris grid
    /// </summary>
    public class Cluster : List<Block> {
        public bool containsController = false;
    }
}