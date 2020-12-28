﻿using UnityEngine;

namespace Exa.Utils
{
    public static class ObjectExtensions
    {
        public static GameObject Instantiate(GameObject prefab, Transform parent, string namePrefix = null) {
            var go = Object.Instantiate(prefab, parent);
            go.name = $"{namePrefix}: {go.GetInstanceID()}";
            return go;
        }

        public static T InstantiateAndGet<T>(this GameObject prefab, Transform parent, string namePrefix = null) {
            var go = Instantiate(prefab, parent, namePrefix ?? typeof(T).Name);
            return go.GetComponent<T>();
        }
    }
}