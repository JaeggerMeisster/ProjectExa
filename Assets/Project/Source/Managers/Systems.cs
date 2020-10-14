﻿using Exa.Audio;
using Exa.Debugging;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.SceneManagement;
using Exa.UI;
using Exa.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Must;

#pragma warning disable 649

namespace Exa
{
    public delegate void DebugChangeDelegate(DebugMode mode);

    public class Systems : MonoSingleton<Systems>
    {
        [Header("References")]
        [SerializeField] private BlockFactory blockFactory;
        [SerializeField] private BlueprintManager blueprintManager;
        [SerializeField] private ShipEditor.ShipEditor shipEditor;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private ThumbnailGenerator thumbnailGenerator;
        [SerializeField] private DebugManager debugManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private ExaSceneManager sceneManager;
        [SerializeField] private LoggerInterceptor logger;
        [SerializeField] private MainUI mainUI;

        [Header("Settings")]
        [SerializeField] private bool godModeIsEnabled = false;

        public static BlockFactory Blocks => Instance.blockFactory;
        public static BlueprintManager Blueprints => Instance.blueprintManager;
        public static ShipEditor.ShipEditor Editor => Instance.shipEditor;
        public static AudioManager Audio => Instance.audioManager;
        public static ThumbnailGenerator Thumbnails => Instance.thumbnailGenerator;
        public static DebugManager Debug => Instance.debugManager;
        public static InputManager Input => Instance.inputManager;
        public static ExaSceneManager Scenes => Instance.sceneManager;
        public static LoggerInterceptor Logger => Instance.logger;
        public static MainUI UI => Instance.mainUI;

        public static bool GodModeIsEnabled
        {
            get => Instance.godModeIsEnabled;
            set => Instance.godModeIsEnabled = value;
        }

        public static bool IsQuitting { get; set; } = false;

        private void Start()
        {
            StartCoroutine(EnumeratorUtils.EnumerateSafe(Load(), OnLoadException));
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart()
        {
            Application.quitting += () =>
            {
                IsQuitting = true;
            };
        }

        private IEnumerator Load()
        {
            // Allow the screen to be shown
            UI.wipScreen.Init();
            UI.loadingScreen.ShowScreen();

            yield return 0;

            UI.nav.settings.Load();

            var targetFrameRate = UI.nav.settings.videoSettings.current.Values.resolution.refreshRate;

            yield return EnumeratorUtils.ScheduleWithFramerate(blockFactory.StartUp(new Progress<float>(value =>
            {
                var message = $"Loading blocks ({Mathf.RoundToInt(value * 100)} % complete) ...";
                UI.loadingScreen.ShowMessage(message);
            })), targetFrameRate);

            yield return EnumeratorUtils.ScheduleWithFramerate(blueprintManager.StartUp(new Progress<float>(value =>
            {
                var message = $"Loading blueprints ({Mathf.RoundToInt(value * 100)} % complete) ...";
                UI.loadingScreen.ShowMessage(message);
            })), targetFrameRate);

            UI.nav.blueprintSelector.Source = Blueprints.observableUserBlueprints;
            shipEditor.blueprintCollection = Blueprints.observableUserBlueprints;

            UI.nav.fleetBuilder.Init();

            UI.loadingScreen.HideScreen();
        }

        private void OnLoadException(Exception exception)
        {
            UI.loadingScreen.HideScreen();
            UI.logger.Log($"An error has occurred while loading.\n {exception.Message}");
        }
    }
}