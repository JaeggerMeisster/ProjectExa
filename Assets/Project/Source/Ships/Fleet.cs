using System.Collections;
using System.Collections.Generic;
using Exa.Grids.Blueprints;

#pragma warning disable CS0649

namespace Exa.Ships {
    public class Fleet : IEnumerable<BlueprintContainer> {
        public BlueprintContainer mothership;
        public List<BlueprintContainer> units;

        public Fleet(int unitCapacity) {
            mothership = null;
            units = new List<BlueprintContainer>(unitCapacity);
        }

        public IEnumerator<BlueprintContainer> GetEnumerator() {
            yield return mothership;

            foreach (var unit in units) {
                yield return unit;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}