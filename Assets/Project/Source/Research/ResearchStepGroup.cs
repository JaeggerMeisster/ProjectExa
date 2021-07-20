using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using Exa.Utils;

namespace Exa.Research {
    public class ResearchStepGroup {
        public StepCache stepCache = new StepCache();

        public void AddSteps(IBlockComponentModifier modifier, List<ResearchStep> cache) {
            stepCache.Add(modifier, cache);
        }

        public void RemoveSteps(IBlockComponentModifier modifier) {
            stepCache.Remove(modifier);
        }

        public T ApplyContext<T>(BlockTemplate template, T baseValues)
            where T : struct, IBlockComponentValues {
            var currentValues = baseValues;

            foreach (var (_, researchSteps) in GroupByStepAndFilter(template, currentValues)) {
                var initialValues = currentValues;

                T Reducer(T current, ResearchStep step) {
                    return (T) step.CalculateCurrentValues(initialValues, current);
                }

                currentValues = researchSteps.Aggregate(currentValues, Reducer);
            }

            return currentValues;
        }

        /// <summary>
        ///     Gets the research steps associated with a given block template and block component values, grouped and ordered by
        ///     the order the steps should be applied
        /// </summary>
        private IEnumerable<(ValueModificationOrder, IEnumerable<ResearchStep>)> GroupByStepAndFilter(BlockTemplate template, IBlockComponentValues values) {
            // Reduce a list of research steps for a given block template
            List<ResearchStep> Reducer(List<ResearchStep> current, (IBlockComponentModifier, List<ResearchStep>) modifierSteps) {
                var (modifier, steps) = modifierSteps;

                // Only add the steps associated with a modifier if the modifier should affect the values of the template
                if (modifier.AffectsTemplate(template)) {
                    current.AddRange(steps.Where(step => step.MatchesType(values)));
                }

                return current;
            }

            return stepCache.Unpack()
                .Aggregate(new List<ResearchStep>(), Reducer)
                .GroupBy(step => step.Order)
                .Select(grouping => (grouping.Key, (IEnumerable<ResearchStep>) grouping));
        }
    }

    public class StepCache : Dictionary<IBlockComponentModifier, List<ResearchStep>> { }
}