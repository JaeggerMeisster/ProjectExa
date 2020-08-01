﻿using Exa.SceneManagement;
using Exa.UI.Components;
using UnityEditor;
using UnityEngine;

namespace Exa.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Navigateable self;
        [SerializeField] private Navigateable shipEditorBlueprintSelector;
        [SerializeField] private Navigateable settings;
        [SerializeField] private BlueprintViewController blueprintViewController;

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void NavigateToMission()
        {
            var transition = MainManager.Instance.sceneManager.Transition("Mission", new TransitionArgs
            {
                SetActiveScene = true
            });

            transition.onPrepared.AddListener(() =>
            {
                self.NavigateTo(MissionManager.Instance.navigateable);
            });
        }

        public void NavigateToEditor()
        {
            self.NavigateTo(shipEditorBlueprintSelector);
            blueprintViewController.Source = MainManager.Instance.blueprintManager.observableUserBlueprints;
            blueprintViewController.shipEditor.blueprintCollection = MainManager.Instance.blueprintManager.observableUserBlueprints;
        }

        public void NavigateToSettings()
        {
            self.NavigateTo(settings);
        }
    }
}