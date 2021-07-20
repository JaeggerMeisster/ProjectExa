using Exa.Grids.Blocks.Components;

namespace Exa.Research {
    public class ResearchStep<T> : ResearchStep
        where T : struct, IBlockComponentValues {
        public delegate void ApplyValues(T init, ref T curr);

        public delegate void ApplyValuesOmitInit(ref T curr);

        private readonly ApplyValues applyFunc;

        public ResearchStep(ApplyValues applyFunc, ValueModificationOrder order)
            : base(order) {
            this.applyFunc = applyFunc;
        }

        public override IBlockComponentValues CalculateCurrentValues(IBlockComponentValues init, IBlockComponentValues curr) {
            var convertedCopy = (T) curr;
            applyFunc((T) init, ref convertedCopy);

            return convertedCopy;
        }

        public override bool MatchesType(IBlockComponentValues values) {
            return values is T;
        }
    }

    public abstract class ResearchStep {
        protected ResearchStep(ValueModificationOrder order) {
            Order = order;
        }

        public ValueModificationOrder Order { get; }

        public abstract IBlockComponentValues CalculateCurrentValues(IBlockComponentValues init, IBlockComponentValues curr);

        public abstract bool MatchesType(IBlockComponentValues values);
    }
}