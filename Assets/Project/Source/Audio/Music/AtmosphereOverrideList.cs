using System;
using System.Linq;
using Exa.Types.Generics;

namespace Exa.Audio.Music {
    public class AtmosphereOverrideList : OverrideList<Atmosphere> {
        public AtmosphereOverrideList(Atmosphere defaultValue, Action<Atmosphere> onValueChange)
            : base(defaultValue, onValueChange) { }

        protected override Atmosphere SelectValue() {
            return overrides.Aggregate(Atmosphere.None, (atmosphere, valueOverride) => atmosphere | valueOverride.Value);
        }
    }
}