using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;

namespace Exa.Research {
    public class ResearchBuilder {
        private readonly List<Action> clearActions;
        private readonly ResearchStore store;
        private BlockContext context = BlockContext.DefaultGroup;
        private Type templateTypeFilter;

        public ResearchBuilder(ResearchStore store) {
            this.store = store;
            clearActions = new List<Action>();
        }

        public ResearchBuilder Context(BlockContext context) {
            this.context = context;

            return this;
        }

        public ResearchBuilder Add<T>(
            ResearchStep<T>.ApplyValues applyValues,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where T : struct, IBlockComponentValues {
            Func<BlockTemplate, bool> GetClosure() {
                if (templateTypeFilter == null) {
                    return template => template.GetAnyPartialDataIsOf<T>();
                }

                var currFilter = templateTypeFilter;

                return template => currFilter.IsInstanceOfType(template);
            }

            clearActions.Add(
                store.AddModifier(
                    context,
                    new DynamicBlockComponentModifier(
                        new ResearchStep<T>(applyValues, order),
                        GetClosure()
                    )
                )
            );

            return this;
        }

        public ResearchBuilder Add<T>(
            ResearchStep<T>.ApplyValuesOmitInit applyValuesOmitInit,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where T : struct, IBlockComponentValues {
            void ApplyValues(T init, ref T curr) {
                applyValuesOmitInit(ref curr);
            }

            Add<T>(ApplyValues, order);

            return this;
        }

        public ResearchBuilder ForTemplate<TTemplate>()
            where TTemplate : BlockTemplate {
            templateTypeFilter = typeof(TTemplate);

            return this;
        }

        public ResearchBuilder ClearTemplate() {
            templateTypeFilter = null;

            return this;
        }

        public ResearchBuilder AddFor<TTemplate, TBlockComponentValues>(
            ResearchStep<TBlockComponentValues>.ApplyValuesOmitInit applyValuesOmitInit,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where TTemplate : BlockTemplate
            where TBlockComponentValues : struct, IBlockComponentValues {
            void ApplyValues(TBlockComponentValues init, ref TBlockComponentValues curr) {
                applyValuesOmitInit(ref curr);
            }

            return AddFor<TTemplate, TBlockComponentValues>(ApplyValues, order);
        }

        public ResearchBuilder AddFor<TTemplate, TBlockComponentValues>(
            ResearchStep<TBlockComponentValues>.ApplyValues applyValues,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where TTemplate : BlockTemplate
            where TBlockComponentValues : struct, IBlockComponentValues {
            clearActions.Add(
                store.AddModifier(
                    context,
                    new DynamicBlockComponentModifier(
                        new ResearchStep<TBlockComponentValues>(applyValues, order),
                        template => template is TTemplate
                    )
                )
            );

            return this;
        }

        public void Clear() {
            foreach (var clearAction in clearActions) {
                clearAction();
            }
        }
    }
}