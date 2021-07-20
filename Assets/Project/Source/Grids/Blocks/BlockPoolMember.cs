using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks {
    public class BlockPoolMember : PoolMember {
        public Block block;

        protected override void OnDisable() { }

        public void ReturnBlock() {
        #if ENABLE_BLOCK_LOGS
            block.Logs.Add("Function: ReturnBlock");
        #endif    
            
            base.OnDisable();
        }

        protected override bool IgnoreClause() {
            return GS.IsQuitting;
        }
    }
}