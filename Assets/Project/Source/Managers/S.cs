using System;
using System.Collections;
using Exa.Audio;
using Exa.Audio.Music;
using Exa.Camera;
using Exa.Data;
using Exa.Debugging;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.Research;
using Exa.SceneManagement;
using Exa.ShipEditor;
using Exa.UI;
using Exa.Utils;
using UnityEditor;
using UnityEngine;

#pragma warning disable 649

namespace Exa {
    public delegate void DebugChangeDelegate(DebugMode mode);

    public class S : MonoSingleton<S> {
        [Header("References")]
        [SerializeField] private BlockFactory blockFactory;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private ColorManager colorManager;
        [SerializeField] private ResearchStore researchStore;
        [SerializeField] private BlueprintManager blueprintManager;
        [SerializeField] private GridEditor gridEditor;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private ThumbnailGenerator thumbnailGenerator;
        [SerializeField] private DebugManager debugManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private SettingsManager settingsManager;
        [SerializeField] private ExaSceneManager sceneManager;
        [SerializeField] private LoggerInterceptor logger;
        [SerializeField] private MainUI mainUI;
        [SerializeField] private AtmosphereTrigger atmosphereTrigger;

        [Header("Settings")]
        [SerializeField] private bool godModeIsEnabled;
        [SerializeField] private bool loadSafe;

        public static BlockFactory Blocks {
            get => Instance.blockFactory;
        }

        public static CameraController CameraController {
            get => Instance.cameraController;
        }

        public static ColorManager Colors {
            get => Instance.colorManager;
        }

        public static ResearchStore Research {
            get => Instance.researchStore;
        }

        public static BlueprintManager Blueprints {
            get => Instance.blueprintManager;
        }

        public static GridEditor Editor {
            get => Instance.gridEditor;
        }

        public static AudioManager Audio {
            get => Instance.audioManager;
        }

        public static ThumbnailGenerator Thumbnails {
            get => Instance.thumbnailGenerator;
        }

        public static DebugManager Debug {
            get => Instance.debugManager;
        }

        public static InputManager Input {
            get => Instance.inputManager;
        }

        public static SettingsManager Settings {
            get => Instance.settingsManager;
        }

        public static ExaSceneManager Scenes {
            get => Instance.sceneManager;
        }

        public static LoggerInterceptor Logger {
            get => Instance.logger;
        }

        public static MainUI UI {
            get => Instance.mainUI;
        }

        public static bool GodModeIsEnabled {
            get => Instance.godModeIsEnabled;
            set => Instance.godModeIsEnabled = value;
        }

        public static bool IsQuitting { get; set; }

        private void Start() {
            var enumerator = loadSafe
                ? EnumeratorUtils.EnumerateSafe(Load(), OnLoadException)
                : Load();

            StartCoroutine(enumerator);
        }

        public static void Quit() {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart() {
            Application.quitting += () => { IsQuitting = true; };
        }

        private IEnumerator Load() {
            // Allow the screen to be shown
            UI.WipScreen?.Init();
            UI.LoadingScreen.Init();
            UI.LoadingScreen.ShowScreen(LoadingScreenDuration.Long);
            UI.Root.gameObject.SetActive(false);

            yield return new WorkUnit();

            Settings.AudioSettings.LoadHandler = new SoundTrackLoadHandler {
                Progress = UI.LoadingScreen.GetLoadReporter("soundtrack")
            };

            Settings.Load();

            yield return Settings.AudioSettings.LoadHandler.LoadEnumerator.ScheduleWithTargetFramerate();

            Settings.AudioSettings.LoadHandler = new SoundTrackLoadHandler {
                Progress = UI.Prompts.PromptProgress("Loading soundtrack", UI.Root.interactableAdapter)
            };

            // Play music only after settings have been loaded
            atmosphereTrigger.gameObject.SetActive(true);

            // Initialize research systems
            researchStore.Init();

            yield return blockFactory.Init(UI.LoadingScreen.GetLoadReporter("blocks"))
                .ScheduleWithTargetFramerate();

            // Enable research items after the block factory is initialized, as research items call the block factory when enabled
            researchStore.AutoEnableItems();

            yield return blueprintManager.Init(UI.LoadingScreen.GetLoadReporter("blueprints"))
                .ScheduleWithTargetFramerate();

            yield return new WorkUnit();

            UI.Root.blueprintSelector.Source = Blueprints.userBlueprints;
            UI.Root.gameObject.SetActive(true);
            UI.LoadingScreen.HideScreen();

            Audio.Music.IsPlaying = true;
        }

        private void OnLoadException(Exception exception) {
            UI.LoadingScreen.HideScreen("Error");
            UnityEngine.Debug.LogWarning(exception);
            UI.Logger.LogException($"An error has occurred while loading.\n {exception.Message}");
        }
    }
}