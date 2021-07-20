using Exa.Math;
using UnityEngine;

namespace Exa.Grids {
    public static class GridMemberUtils {
        public static void SetupGameObject(this IGridMember gridMember, GameObject blockGO) {
            var blueprintBlock = gridMember.BlueprintBlock;
            var gridAnchor = gridMember.GridAnchor;

            blockGO.name = $"{blueprintBlock.Template.displayId} {gridAnchor}";
            blockGO.GetComponent<BlockPresenter>().Present(gridMember);
        }

        public static void UpdateLocals(this IGridMember gridMember, GameObject blockGO) {
            var blueprintBlock = gridMember.BlueprintBlock;

            blockGO.transform.localRotation = blueprintBlock.GetDirection();
            blockGO.transform.localPosition = gridMember.GetLocalPosition();
        }

        public static Vector2 GetLocalPosition(this IGridMember gridMember) {
            var size = gridMember.BlueprintBlock.Template.size - Vector2Int.one;

            var offset = new Vector2 {
                x = size.x / 2f,
                y = size.y / 2f
            }.Rotate(gridMember.BlueprintBlock.Rotation);

            offset *= gridMember.BlueprintBlock.FlipVector;

            return offset + gridMember.GridAnchor;
        }
    }
}