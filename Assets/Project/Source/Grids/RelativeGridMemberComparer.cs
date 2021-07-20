using System.Collections.Generic;
using Exa.Ships;
using UnityEngine;

namespace Exa.Grids {
    public class RelativeGridMemberComparer : IComparer<IGridMember> {
        private readonly Vector2 pivot;

        public RelativeGridMemberComparer(BlockGrid blockGrid) {
            pivot = blockGrid.Controller.GetLocalPosition();
        }

        public int Compare(IGridMember x, IGridMember y) {
            var xMag = (pivot - x.GetLocalPosition()).magnitude;
            var yMag = (pivot - y.GetLocalPosition()).magnitude;

            return xMag == yMag
                ? 0
                : xMag > yMag
                    ? 1
                    : -1;
        }
    }
}