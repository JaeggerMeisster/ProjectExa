using System.Collections.Generic;
using System.Linq;

namespace Exa.Audio {
    /// <summary>
    ///     Supports a collection of sound handle context objects
    /// </summary>
    public class SoundHandleGroup : List<SoundHandle> {
        /// <summary>
        ///     Stop every sound
        /// </summary>
        public void Stop() {
            foreach (var handle in this.ToList()) {
                handle.Stop();
            }
        }
    }
}