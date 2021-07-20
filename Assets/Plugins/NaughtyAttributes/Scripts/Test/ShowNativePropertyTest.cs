using UnityEngine;

namespace NaughtyAttributes.Test {
    public class ShowNativePropertyTest : MonoBehaviour {
        [ShowNativeProperty]
        private Transform Transform {
            get => transform;
        }

        [ShowNativeProperty]
        private Transform ParentTransform {
            get => transform.parent;
        }
    }
}