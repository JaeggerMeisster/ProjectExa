using System;
using UnityEngine;

namespace NaughtyAttributes.Test {
    public class InfoBoxTest : MonoBehaviour {
        [InfoBox("Normal")]
        public int normal;

        public InfoBoxNest1 nest1;
    }

    [Serializable]
    public class InfoBoxNest1 {
        [InfoBox("Warning", EInfoBoxType.Warning)]
        public int warning;

        public InfoBoxNest2 nest2;
    }

    [Serializable]
    public class InfoBoxNest2 {
        [InfoBox("Error", EInfoBoxType.Error)]
        public int error;
    }
}