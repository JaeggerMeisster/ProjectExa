using DG.Tweening;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI.Components;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class BlueprintTypeButton : Navigateable {
        public int order;

        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private LayoutElement layoutElement;
        private BlueprintTypeTabContent content;

        public BlueprintTypeSelectEvent SelectType = new BlueprintTypeSelectEvent();
        private Tween sizeTween;

        public void Init(
            BlueprintType blueprintType,
            BlueprintTypeTabContent content,
            int order,
            NavigateableTabManager tabManager
        ) {
            this.content = content;
            this.order = order;
            text.text = blueprintType.displayName;

            button.onClick.AddListener(
                () => {
                    SelectType?.Invoke(blueprintType);
                    tabManager.SwitchTo(this);
                }
            );
        }

        public override void HandleExit(Navigateable target) {
            Animate(48f);

            content.HandleExit(
                target is BlueprintTypeButton button
                    ? Vector2.up * (button.order > order).To1()
                    : Vector2.zero
            );
        }

        public override void HandleEnter(NavigationArgs args) {
            Animate(72f);

            content.HandleEnter(
                args?.current is BlueprintTypeButton button
                    ? Vector2.down * (button.order > order).To1()
                    : Vector2.zero
            );
        }

        private void Animate(float target) {
            layoutElement.DOPreferredHeight(target, 0.2f)
                .Replace(ref sizeTween);
        }
    }
}