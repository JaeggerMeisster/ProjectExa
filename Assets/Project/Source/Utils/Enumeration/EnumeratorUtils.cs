using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Utils {
    public static class EnumeratorUtils {
        public static Coroutine DelayOneFrame(this MonoBehaviour monoBehaviour, Action callback) {
            return DelayOneFrame(callback).Start(monoBehaviour);
        }

        public static bool MoveNext<T>(this IEnumerator<T> enumerator, out T current) {
            var next = enumerator.MoveNext();
            current = enumerator.Current;

            return next;
        }

        public static bool MoveNext(this IEnumerator enumerator, out object current) {
            var next = enumerator.MoveNext();
            current = enumerator.Current;

            return next;
        }

        public static IEnumerator EnumerateSafe(IEnumerator enumerator, Action<Exception> catcher) {
            while (true) {
                object current;

                try {
                    if (!enumerator.MoveNext()) {
                        break;
                    }

                    current = enumerator.Current;
                } catch (Exception e) {
                    catcher(e);

                    yield break;
                }

                yield return current;
            }
        }

        public static IEnumerator Then(this IEnumerator enumerator, Action callback) {
            while (enumerator.MoveNext()) {
                yield return enumerator.Current;
            }

            callback();
        }

        public static IEnumerable Enumerate(IEnumerator enumerator) {
            while (enumerator.MoveNext()) {
                yield return enumerator.Current;
            }
        }

        public static IEnumerator ScheduleWithTargetFramerate(this IEnumerator enumerator) {
            // Select a target frame rate
            var targetFrameRate = S.Settings.VideoSettings.Values.resolution.refreshRate;

            return ScheduleWithFramerate(enumerator, targetFrameRate);
        }

        private static IEnumerator ScheduleWithFramerate(IEnumerator enumerator, float framerate) {
            var maxTimeDelta = 1.0f / framerate;
            var timeStamp = Time.realtimeSinceStartup;

            while (enumerator.MoveNext(out var current)) {
                if (current is WorkUnit) {
                    if (Time.realtimeSinceStartup - timeStamp > maxTimeDelta) {
                        yield return null;

                        timeStamp = Time.realtimeSinceStartup;
                    }
                } else {
                    yield return current;
                }
            }
        }

        public static IEnumerator ReportForeachOperation<T>(
            IEnumerable<T> enumerable,
            Func<T, IEnumerator> func,
            IProgress<float> progress
        ) {
            var index = 0;
            var list = enumerable.ToList();
            progress.Report(0);

            foreach (var item in list) {
                var enumerator = func(item);

                while (enumerator.MoveNext(out var current)) {
                    yield return current;
                }

                index++;
                progress.Report((float) index / list.Count);
            }
        }

        public static IEnumerator ReportForeachOperation<T>(
            IEnumerable<T> enumerable,
            Action<T> action,
            IProgress<float> progress
        ) {
            var index = 0;
            var list = enumerable.ToList();
            progress.Report(0);

            foreach (var item in list) {
                action(item);

                yield return null;

                index++;
                progress.Report((float) index / list.Count);
            }
        }

        public static IEnumerator DelayOneFrame(Action callback) {
            yield return 0;

            callback();
        }

        public static IEnumerator Delay(Action callback, float seconds) {
            if (seconds > 0f) {
                yield return new WaitForSeconds(seconds);
            }

            callback();
        }

        public static IEnumerator OnceEverySecond(int seconds, Action<int> secondCallback, Func<bool> timePasses = null) {
            var time = 0f;
            var currentSecond = -1;

            while (currentSecond < seconds) {
                if (timePasses == null || timePasses()) {
                    if (time > currentSecond) {
                        currentSecond = Mathf.FloorToInt(time);
                        secondCallback(currentSecond);
                    }

                    time += Time.deltaTime;
                }

                yield return null;
            }
        }
    }
}