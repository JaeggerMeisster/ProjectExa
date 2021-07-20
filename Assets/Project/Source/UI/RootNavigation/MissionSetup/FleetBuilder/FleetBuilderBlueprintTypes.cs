using System;
using System.Linq;
using Exa.Grids.Blueprints;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.UI {
    public class BlueprintTypeSelectEvent : UnityEvent<BlueprintType> { }

    public class FleetBuilderBlueprintTypes : NavigateableTabManager {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject buttonPrefab;
        public BlueprintTypeSelectEvent SelectType = new BlueprintTypeSelectEvent();

        public void BuildList(Func<BlueprintType, BlueprintTypeTabContent> tabFactory) {
            var index = 0;

            foreach (var blueprintType in S.Blueprints.blueprintTypes.Reverse()) {
                var blueprintTypeButton = Instantiate(buttonPrefab, container)
                    .GetComponent<BlueprintTypeButton>();

                var tab = tabFactory(blueprintType);
                blueprintTypeButton.Init(blueprintType, tab, index, this);
                blueprintTypeButton.SelectType.AddListener(SelectType.Invoke);

                if (index == 0) {
                    SetDefaultActive(blueprintTypeButton);
                }

                index++;
            }
        }
    }
}