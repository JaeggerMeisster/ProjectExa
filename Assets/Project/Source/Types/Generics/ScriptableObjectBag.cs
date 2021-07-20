using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Exa.Types.Generics {
    public class ScriptableObjectBag<T> : ScriptableObjectBagBase, IEnumerable<T>
        where T : ScriptableObject {
        [SerializeField] protected List<T> objects = new List<T>();

        public virtual IEnumerator<T> GetEnumerator() {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return objects.GetEnumerator();
        }

        public override void FindObjects() {
            objects = new List<T>(GetAllInstances());
        }

        protected virtual IEnumerable<T> GetAllInstances() {
        #if UNITY_EDITOR
            foreach (var guid in QueryGUIDs()) {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                yield return AssetDatabase.LoadAssetAtPath<T>(path);
            }
        #else
            throw new System.Exception("Cannot get instances in runtime");
        #endif
        }

        protected virtual string[] QueryGUIDs() {
        #if UNITY_EDITOR
            return AssetDatabase.FindAssets("t:" + typeof(T).Name);
        #else
            throw new System.Exception("Cannot QueryGUIDs in runtime");
        #endif
        }
    }
}