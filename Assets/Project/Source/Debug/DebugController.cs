﻿using Exa.Input;
using UCommandConsole;
using UnityEngine;
using UnityEngine.InputSystem;
using static Exa.Input.GameControls;

namespace Exa.Debugging
{
    public class DebugController : MonoBehaviour, IDebugActions
    {
        [SerializeField] private Console console;

        private GameControls gameControls;

        public void Awake()
        {
            gameControls = new GameControls();
            gameControls.Debug.SetCallbacks(this);
        }

        public void OnEnable()
        {
            gameControls.Enable();
        }

        public void OnDisable()
        {
            gameControls.Disable();
        }

        public void OnToggleConsole(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            var consoleGO = console.gameObject;
            consoleGO.SetActive(!consoleGO.activeSelf);
        }
    }
}