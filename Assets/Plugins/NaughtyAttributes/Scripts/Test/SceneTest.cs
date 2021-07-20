using System;
using UnityEngine;

namespace NaughtyAttributes.Test {
    public class SceneTest : MonoBehaviour {
        [Scene]
        public string scene0;

        public SceneNest1 nest1;
    }

    [Serializable]
    public class SceneNest1 {
        [Scene]
        public string scene1;

        public SceneNest2 nest2;
    }

    [Serializable]
    public struct SceneNest2 {
        [Scene]
        public int scene2;
    }
}