using System;
using System.Collections;
using UnityEngine;

namespace Exa.Utils {
    public static class CoroutineUtils {
        public static Coroutine Delay(this MonoBehaviour monoBehaviour, Action action, float seconds) {
            return EnumeratorUtils.Delay(action, seconds).Start(monoBehaviour);
        }

        public static Coroutine Start(this IEnumerator enumerator) {
            return S.Instance.StartCoroutine(enumerator);
        }

        public static Coroutine Start(this IEnumerator enumerator, MonoBehaviour target) {
            return target.StartCoroutine(enumerator);
        }

        public static Coroutine DelayLocally(this MonoBehaviour monoBehaviour, Action action, float seconds) {
            return EnumeratorUtils.Delay(action, seconds).Start(monoBehaviour);
        }
    }
}