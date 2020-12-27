﻿using System;
using System.Diagnostics;

namespace Exa.Grids.Blocks
{
    [Flags]
    public enum BlockCategory
    {
        Armor = 1 << 0,
        ShipController = 1 << 1,
        Thruster = 1 << 2,
        Gyroscope = 1 << 3,
        Power = 1 << 4,
        Worker = 1 << 5,
        Weapon = 1 << 6,
        StationController = 1 << 7,
        AnyController = ShipController & StationController
    }

    public static class BlockCategoryExtensions
    {
        public static string ToFriendlyString(this BlockCategory category) {
            return category switch {
                BlockCategory.Armor => "Armor",
                BlockCategory.ShipController => "Controller",
                BlockCategory.Thruster => "Thruster",
                BlockCategory.Gyroscope => "Gyroscope",
                BlockCategory.Power => "Power",
                BlockCategory.Worker => "Worker",
                BlockCategory.Weapon => "Weapon",
                BlockCategory.StationController => "Controller",
                _ => "Not supported"
            };
        }
    }
}