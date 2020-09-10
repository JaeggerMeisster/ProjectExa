﻿using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.Math.ControlSystems;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    // TODO: Clamp the requested thrust vector to what the Ship can output
    public class ThrustVectors : IThrustVectors
    {
        private readonly Dictionary<int, ThrusterGroup> thrusterDict;

        public ThrustVectors(float directionalThrust)
        {
            thrusterDict = new Dictionary<int, ThrusterGroup>
            {
                { 0, new ThrusterGroup(directionalThrust) },
                { 1, new ThrusterGroup(directionalThrust) },
                { 2, new ThrusterGroup(directionalThrust) },
                { 3, new ThrusterGroup(directionalThrust) }
            };
        }

        public void Register(IThruster thruster)
        {
            SelectGroup(thruster)?.Add(thruster);
        }

        public void Unregister(IThruster thruster)
        {
            SelectGroup(thruster)?.Remove(thruster);
        }

        /// <summary>
        /// Sets the graphics using a local space scalar vector
        /// </summary>
        /// <param name="directionScalar"></param>
        public void SetGraphics(Vector2 directionScalar)
        {

            SelectHorizontalGroup(directionScalar.x, false).SetFireStrength(Mathf.Abs(directionScalar.x));
            SelectHorizontalGroup(directionScalar.x, true).SetFireStrength(0);
            SelectVerticalGroup(directionScalar.y, false).SetFireStrength(Mathf.Abs(directionScalar.y));
            SelectVerticalGroup(directionScalar.y, true).SetFireStrength(0);
        }

        private ThrusterGroup SelectGroup(IThruster thruster)
        {
            var rotation = GetDirection(thruster);
            try
            {
                return thrusterDict[rotation];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning($"thruster {thruster} with rotation {rotation} cannot find a corresponding thruster group");
                return null;
            }
        }

        private ThrusterGroup SelectHorizontalGroup(float x, bool revert) =>
            x > 0 ^ revert
                ? thrusterDict[0]
                : thrusterDict[2];

        private ThrusterGroup SelectVerticalGroup(float y, bool revert) =>
            y > 0 ^ revert
                ? thrusterDict[1]
                : thrusterDict[3];

        private int GetDirection(IThruster thruster)
        {
            return thruster.Component.block.anchoredBlueprintBlock.blueprintBlock.Direction;
        }
    }
}