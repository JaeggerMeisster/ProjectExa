using System;
using System.Linq;
using DG.Tweening;
using Exa.Data;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Exa.UI {
    public class FleetView : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Transform mothershipContainer;
        [SerializeField] private Transform unitsContainer;
        [SerializeField] private Text unitCountText;

        [Header("Settings")]
        [SerializeField] private AnimationArgs animationArgs;

        [Header("Events")]
        public UnityEvent fleetChange = new UnityEvent();
        private Tween colorTween;

        private Tween fontSizeTween;
        private Func<BlueprintContainer, FleetBuilderBlueprintView> viewFactory;

        public Fleet Fleet { get; private set; }

        public void Create(int capacity, Func<BlueprintContainer, FleetBuilderBlueprintView> viewFactory) {
            Fleet = new Fleet(capacity);
            this.viewFactory = viewFactory;
        }

        public void Toggle(BlueprintContainer blueprint) {
            if (Fleet.Contains(blueprint)) {
                Remove(blueprint);
            } else {
                Insert(blueprint);
            }
        }

        public void Insert(BlueprintContainer blueprint) {
            if (blueprint.Data.BlueprintType.IsMothership) {
                AddMothership(blueprint);
            } else {
                AddUnit(blueprint);
            }
        }

        public void Remove(BlueprintContainer blueprint) {
            if (blueprint.Data.BlueprintType.IsMothership) {
                Fleet.mothership = null;
            } else {
                Fleet.units.Remove(blueprint);
                UpdateUnitCountText();
            }

            fleetChange.Invoke();
            SetSelected(blueprint, false);
        }

        public void Clear(int capacity) {
            var blueprints = Fleet.Where(blueprint => blueprint != null);

            foreach (var blueprint in blueprints) {
                SetSelected(blueprint, false);
            }

            Fleet = new Fleet(capacity);
            UpdateUnitCountText();
        }

        private void AddMothership(BlueprintContainer blueprint) {
            if (Fleet.mothership != null) {
                SetSelected(Fleet.mothership, false);
            }

            Fleet.mothership = blueprint;
            fleetChange.Invoke();

            SetSelected(blueprint, true, mothershipContainer);
        }

        private void AddUnit(BlueprintContainer blueprint) {
            if (Fleet.units.Count == Fleet.units.Capacity) {
                OnUnitsFull(animationArgs);

                return;
            }

            Fleet.units.Add(blueprint);
            UpdateUnitCountText();
            fleetChange.Invoke();

            SetSelected(blueprint, true, unitsContainer);
        }

        private void SetSelected(BlueprintContainer blueprint, bool active, Transform container = null) {
            var view = viewFactory(blueprint);
            view.Selected = active;

            if (container != null) {
                view.transform.SetParent(container);
                view.ParentTab.ChildCount--;
            } else {
                view.transform.SetParent(view.ParentTab.container);
                view.ParentTab.ChildCount++;
            }

            view.hoverable.ForceExit();
        }

        private void OnUnitsFull(AnimationArgs args) {
            unitCountText.fontSize = args.fontSize.active;

            unitCountText.DOFontSize(args.fontSize.inactive, args.animTime)
                .Replace(ref fontSizeTween);

            unitCountText.color = args.color.active;

            unitCountText.DOColor(args.color.inactive, args.animTime)
                .Replace(ref colorTween);
        }

        private void UpdateUnitCountText() {
            unitCountText.text = $"{Fleet.units.Count}/{Fleet.units.Capacity}";
        }

        [Serializable]
        public struct AnimationArgs {
            public float animTime;
            public ActivePair<int> fontSize;
            public ActivePair<Color> color;
        }
    }
}