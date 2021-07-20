using Exa.Types.Binding;

namespace Exa.Grids.Blocks {
    /// <summary>
    ///     Observable container for block templates
    ///     Used by the editor inventory to keep track of block template updates
    /// </summary>
    public class BlockTemplateContainer : Observable<BlockTemplate> {
        public BlockTemplateContainer(BlockTemplate data)
            : base(data) { }
    }
}