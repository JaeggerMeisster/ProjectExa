using System.Collections.Generic;
using UnityEngine;

namespace Exa.Utils {
    public static class TransformExtensions {
        /// <summary>
        ///     Provides type safe enumerator for a given transform
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static IEnumerable<Transform> GetChildren(this Transform transform) {
            foreach (Transform child in transform) {
                yield return child;
            }
        }

        public static void SetActiveChildren(this Transform transform, bool active) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(active);
            }
        }

        public static void DestroyChildren(this Transform transform) {
            foreach (Transform child in transform) {
                Object.Destroy(child.gameObject);
            }
        }

        public static RectTransform GetRectTransform(this Component behaviour) {
            return behaviour.transform as RectTransform;
        }
    }
}