using System.Linq;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Grids.Blocks {
    [CreateAssetMenu(menuName = "Grids/Blocks/BlockTemplateBag")]
    public class BlockTemplateBag : ScriptableObjectBag<BlockTemplate> { }
}