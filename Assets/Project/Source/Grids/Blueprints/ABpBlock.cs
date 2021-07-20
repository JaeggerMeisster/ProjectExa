using System;
using Exa.Generics;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blueprints {
    [Serializable]
    public class ABpBlock : ICloneable<ABpBlock>, IGridMember {
        public Vector2Int gridAnchor;
        public BlueprintBlock blueprintBlock;

        public ABpBlock(Vector2Int gridAnchor, BlueprintBlock blueprintBlock) {
            this.gridAnchor = gridAnchor;
            this.blueprintBlock = blueprintBlock;
        }

        public BlockTemplate Template {
            get => BlueprintBlock.Template;
        }

        public ABpBlock Clone() {
            return new ABpBlock(gridAnchor, blueprintBlock);
        }

        public Vector2Int GridAnchor {
            get => gridAnchor;
            set => gridAnchor = value;
        }

        public BlueprintBlock BlueprintBlock {
            get => blueprintBlock;
            set => blueprintBlock = value;
        }

        public void AddGridTotals(GridTotals totals) {
            blueprintBlock.Template.AddGridTotals(totals);
        }

        public void RemoveGridTotals(GridTotals totals) {
            blueprintBlock.Template.RemoveGridTotals(totals);
        }

        public bool Equals(IGridMember other) {
            return IGridMemberComparer.Default.Equals(this, other);
        }

        public GameObject CreateInactiveInertBlockInGrid(Transform parent) {
            var blockGO = S.Blocks.GetInactiveInertBlock(blueprintBlock.id, parent);
            this.SetupGameObject(blockGO);

            return blockGO;
        }

        public Block CreateInactiveBlockInGrid(IGridInstance parent) {
            var block = S.Blocks.GetInactiveBlock(blueprintBlock.id, parent);
            var blockGO = block.gameObject;
            block.aBpBlock = this;
            this.SetupGameObject(blockGO);

            return block;
        }

        public override string ToString() {
            return $"{gridAnchor}:{blueprintBlock}";
        }
    }
}