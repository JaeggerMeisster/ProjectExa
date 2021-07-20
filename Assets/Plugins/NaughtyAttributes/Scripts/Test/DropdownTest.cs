using System;
using System.Collections.Generic;
using UnityEngine;

namespace NaughtyAttributes.Test {
    public class DropdownTest : MonoBehaviour {
        [Dropdown("intValues")]
        public int intValue;

        public DropdownNest1 nest1;

    #pragma warning disable 414
        private int[] intValues = {
            1,
            2,
            3
        };
    #pragma warning restore 414
    }

    [Serializable]
    public class DropdownNest1 {
        [Dropdown("StringValues")]
        public string stringValue;

        public DropdownNest2 nest2;

        private List<string> StringValues {
            get => new List<string> {
                "A",
                "B",
                "C"
            };
        }
    }

    [Serializable]
    public class DropdownNest2 {
        [Dropdown("GetVectorValues")]
        public Vector3 vectorValue;

        private DropdownList<Vector3> GetVectorValues() {
            return new DropdownList<Vector3> {
                {
                    "Right", Vector3.right
                }, {
                    "Up", Vector3.up
                }, {
                    "Forward", Vector3.forward
                }
            };
        }
    }
}