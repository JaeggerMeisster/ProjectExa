using Exa.Grids.Blueprints;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI {
    public class BlueprintTypeTabContent : AnimatedTabContent {
        public Transform container;
        [SerializeField] private Text emptyContentNotice;
        private int childCount;

        public int ChildCount {
            get => childCount;
            set {
                var targetState = value == 0;

                if (emptyContentNotice.gameObject.activeSelf != targetState) {
                    emptyContentNotice.gameObject.SetActive(targetState);
                }

                childCount = value;
            }
        }

        public void SetType(BlueprintType blueprintType) {
            emptyContentNotice.text = $"No blueprints of type \"{blueprintType.displayName}\" found";
        }
    }
}