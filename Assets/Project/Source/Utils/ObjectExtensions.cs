using UnityEngine;

namespace Exa.Utils {
    public static class ObjectExtensions {
        public static GameObject Instantiate(GameObject prefab, Transform parent, string namePrefix = null) {
            var go = Object.Instantiate(prefab, parent);
            go.name = $"{namePrefix}: {go.GetInstanceID()}";

            return go;
        }

        public static T Create<T>(this GameObject prefab, Transform parent, string name = null) {
            return Instantiate(prefab, parent, name ?? typeof(T).Name).GetComponent<T>();
        }

        public static void Destroy(this Object @object) {
            Object.Destroy(@object);
        }
    }
}