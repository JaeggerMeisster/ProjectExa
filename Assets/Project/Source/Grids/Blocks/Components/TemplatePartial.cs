using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    [Serializable]
    public class TemplatePartial<T> : TemplatePartialBase<T>
        where T : struct, IBlockComponentValues {
        [SerializeField] private T data;
        
        public override T ToBaseComponentValues() {
            return data;
        }
    }
}