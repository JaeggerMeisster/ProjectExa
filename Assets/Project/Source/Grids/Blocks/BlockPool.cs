using Exa.Grids.Blocks.BlockTypes;
using Exa.Pooling;

namespace Exa.Grids.Blocks {
    public class BlockPool : Pool<BlockPoolMember> {
        protected override BlockPoolMember InstantiatePrefab() {
            var member = base.InstantiatePrefab();
            var block = member.gameObject.GetComponent<Block>();
            block.blockPoolMember = member;
            member.block = block;

            return member;
        }
    }
}