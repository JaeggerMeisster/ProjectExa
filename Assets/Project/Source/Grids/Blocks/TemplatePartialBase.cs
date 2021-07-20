using System;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Utils;

namespace Exa.Grids.Blocks {
    public abstract class TemplatePartialBase<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : struct, IBlockComponentValues {
        public abstract T ToBaseComponentValues();

        public T ToContextfulComponentValues(BlockContext blockContext) {
            return S.Research.ApplyModifiers(blockContext, Template, ToBaseComponentValues());
        }

        public override IBlockComponentValues GetContextlessValues() {
            return ToBaseComponentValues();
        }

        public override IBlockComponentValues GetContextfulValues(BlockContext blockContext) {
            return ToContextfulComponentValues(blockContext);
        }

        public override void SetValues(Block block, IBlockComponentValues data) {
            block.GetBehaviourOfData<T>().Data = (T) data;
        }

        public override void AddGridTotals(GridTotals totals) {
            S.Blocks.Values.GetValues<T>(totals.GetInjectedContext(), Template).AddGridTotals(totals);
        }

        public override void RemoveGridTotals(GridTotals totals) {
            S.Blocks.Values.GetValues<T>(totals.GetInjectedContext(), Template).RemoveGridTotals(totals);
        }

        public override Type GetDataType() {
            return typeof(T);
        }
    }

    public abstract class TemplatePartialBase : IGridTotalsModifier, IBlockComponentContainer {
        public BlockTemplate Template { get; internal set; }

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);

        public abstract IBlockComponentValues GetContextlessValues();

        public abstract IBlockComponentValues GetContextfulValues(BlockContext blockContext);

        public abstract void SetValues(Block block, IBlockComponentValues data);

        public abstract Type GetDataType();
    }
}