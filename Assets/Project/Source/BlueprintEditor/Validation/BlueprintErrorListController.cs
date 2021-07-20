using System.Linq;
using Exa.UI.Controls;
using Exa.Validation;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    public class BlueprintErrorListController : ErrorListController {
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private CanvasGroup canvasGroup;

        public override ValidationResult Validate<T>(IValidator<T> validator, T args) {
            var result = base.Validate(validator, args);
            var visible = state.GetActiveErrors().Any();

            canvasGroup.interactable = visible;
            canvasGroup.alpha = visible ? 1 : 0;
            layoutElement.ignoreLayout = !visible;

            return result;
        }
    }
}