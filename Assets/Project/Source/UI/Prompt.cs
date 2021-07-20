using System;
using System.Collections.Generic;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI {
    public class Prompt {
        private readonly Action cleanUp;
        private readonly PromptController controller;
        private readonly IUIGroup group;
        private readonly IEnumerable<GameObject> uiObjects;

        public Prompt(PromptController controller, string message, IUIGroup group, Action cleanUp, IEnumerable<GameObject> uiObjects) {
            this.controller = controller;
            this.group = group;
            this.cleanUp = cleanUp;
            this.uiObjects = uiObjects;

            uiObjects?.ForEach(uiObject => uiObject.SetActive(true));
            controller.ActivateMessage(message, group);
        }

        public void CleanUp() {
            cleanUp?.Invoke();

            controller.DeactivateMessage(group);
            uiObjects?.ForEach(uiObject => uiObject.SetActive(true));
        }
    }
}