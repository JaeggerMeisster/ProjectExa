using System.Collections;
using System.Collections.Generic;
using Exa.UI;
using Exa.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exa.SceneManagement {
    public enum LoadScreenMode {
        CloseOnPrepared,
        ManuallyClose,
        None
    }

    public class TransitionArgs {
        public LoadSceneMode loadSceneMode;
        public LoadScreenMode loadScreenMode;
        public bool reportProgress = false;
        public bool setActiveScene = false;
    }

    public class ExaSceneManager : MonoBehaviour {
        private LoadingScreen loadingScreen;
        private Dictionary<string, SceneStatus> sceneStatuses;

        private void Awake() {
            loadingScreen = S.UI.LoadingScreen;
            sceneStatuses = new Dictionary<string, SceneStatus>();
        }

        public SceneTransition Transition(string name, TransitionArgs transitionArgs) {
            var operation = SceneManager.LoadSceneAsync(name, transitionArgs.loadSceneMode);
            var transition = new SceneTransition(operation);

            sceneStatuses.EnsureCreated(name, () => new SceneStatus());
            sceneStatuses[name].loading = true;
            transition.onPrepared.AddListener(() => { sceneStatuses[name].loading = false; });

            if (transitionArgs.loadScreenMode != LoadScreenMode.None) {
                loadingScreen.ShowScreen(LoadingScreenDuration.Short);

                if (transitionArgs.loadScreenMode == LoadScreenMode.CloseOnPrepared) {
                    transition.onPrepared.AddListener(loadingScreen.HideScreen);
                }

                StartCoroutine(ReportOperation(operation));
            }

            if (transitionArgs.setActiveScene) {
                transition.onPrepared.AddListener(
                    () => {
                        var scene = SceneManager.GetSceneByName(name);
                        SceneManager.SetActiveScene(scene);
                    }
                );
            }

            return transition;
        }

        public bool GetSceneIsLoading(string name) {
            sceneStatuses.EnsureCreated(name, () => new SceneStatus());

            return sceneStatuses[name].loading;
        }

        public bool GetSceneIsUnloading(string name) {
            sceneStatuses.EnsureCreated(name, () => new SceneStatus());

            return sceneStatuses[name].unloading;
        }

        public AsyncOperation UnloadAsync(string name) {
            sceneStatuses.EnsureCreated(name, () => new SceneStatus());
            sceneStatuses[name].unloading = true;
            var asyncOperation = SceneManager.UnloadSceneAsync(name);
            asyncOperation.completed += op => { sceneStatuses[name].unloading = false; };

            return asyncOperation;
        }

        private IEnumerator ReportOperation(AsyncOperation operation) {
            while (true) {
                loadingScreen.UpdateMessage(
                    $"Loading scene ({Mathf.RoundToInt(operation.progress * 100)}% complete) ..."
                );

                if (operation.isDone) {
                    break;
                }

                yield return operation;
            }
        }

        private class SceneStatus {
            public bool loading;
            public bool unloading;
        }
    }
}