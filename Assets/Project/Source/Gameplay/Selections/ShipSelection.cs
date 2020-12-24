﻿using Exa.Bindings;
using Exa.Generics;
using Exa.Math;
using Exa.Ships;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Gameplay
{
    public abstract class ShipSelection : ObservableCollection<GridInstance>, ICloneable<ShipSelection>
    {
        protected Formation formation;

        protected ShipSelection(Formation formation) {
            this.formation = formation;
        }

        public bool CanControl { get; protected set; }

        public Vector2 AveragePosition {
            get {
                var positions = this.Select(ship => ship.gameObject.transform.position.ToVector2());

                return MathUtils.Average(positions);
            }
        }

        private readonly Dictionary<GridInstance, UnityAction> callbackDict = new Dictionary<GridInstance, UnityAction>();

        public override void Add(GridInstance gridInstance) {
            base.Add(gridInstance);

            gridInstance.Overlay.overlayCircle.IsSelected = true;

            // Set a callback that removes the Ship from the collection when destroyed
            void Callback() => Remove(gridInstance);
            callbackDict.Add(gridInstance, Callback);
            gridInstance.ControllerDestroyedEvent.AddListener(Callback);
        }

        public override bool Remove(GridInstance gridInstance) {
            OnRemove(gridInstance);
            return base.Remove(gridInstance);
        }

        public override void Clear() {
            foreach (var ship in this) {
                OnRemove(ship);
            }

            base.Clear();
        }

        private void OnRemove(GridInstance gridInstance) {
            gridInstance.Overlay.overlayCircle.IsSelected = false;

            // Get the callback and remove it
            var callback = callbackDict[gridInstance];
            callbackDict.Remove(gridInstance);
            gridInstance.ControllerDestroyedEvent.RemoveListener(callback);
        }

        public abstract ShipSelection Clone();
    }
}