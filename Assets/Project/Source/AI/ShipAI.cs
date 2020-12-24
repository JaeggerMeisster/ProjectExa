﻿using Exa.AI.Actions;
using Exa.Ships;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.AI
{
    public class ShipAi : Agent
    {
        public AAimAtClosestTarget aimAtTarget;
        public ALookAtTarget lookAtTarget;
        public AMoveToTarget moveToTarget;
        public AAvoidCollision avoidCollision;

        [SerializeField] private GridInstance gridInstance;
        [SerializeField] private float activeValueThreshold;
        private ActionList actionList;

        public GridInstance GridInstance => gridInstance;

        public void Init() {
            actionList = BuildActionList();
        }

        public override void AIUpdate() {
            actionList.RunActions();
        }

        protected virtual ActionList BuildActionList() {
            return new ActionList(activeValueThreshold, new IAction[] {
                aimAtTarget = new AAimAtClosestTarget(GridInstance, 200f),
                lookAtTarget = new ALookAtTarget(GridInstance),
                moveToTarget = new AMoveToTarget(GridInstance),
                avoidCollision = new AAvoidCollision(GridInstance, new AAvoidCollisionSettings {
                    detectionRadius = GridInstance.BlockGrid.MaxSize,
                    priorityMultiplier = 1,
                    priorityBase = 10,
                    headingCorrectionMultiplier = 8
                })
            });
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[] {
            new TooltipText(actionList.ToString())
        };
    }
}