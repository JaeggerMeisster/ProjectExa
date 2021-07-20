using System;
using System.Collections.Generic;
using Exa.AI.Actions;
using Exa.Ships;
using Exa.UI.Tooltips;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.AI {
    public class GridAi : Agent {
        [SerializeField] private GridInstance gridInstance;
        [SerializeField] private float activeValueThreshold;
        private ActionList actionList;

        public GridInstance GridInstance {
            get => gridInstance;
        }

        public void Init() {
            actionList = new ActionList(activeValueThreshold, BuildActionList());
        }

        public override void AIUpdate() {
            actionList.RunActions();
        }

        public T GetAction<T>()
            where T : class, IAction {
            foreach (var action in actionList) {
                if (action is T convertedAction) {
                    return convertedAction;
                }
            }

            throw new InvalidOperationException($"Cannot find action of type {typeof(T)}");
        }

        protected virtual IEnumerable<IAction> BuildActionList() {
            return new IAction[] {
                new AAimAtClosestTarget(GridInstance, 200f)
            };
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() {
            return new ITooltipComponent[] {
                new TooltipText(actionList.ToString())
            };
        }
    }
}