using System;
using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Grids.Blocks {
    [Serializable]
    public struct BlockMetadata {
        public int strength;
        public BlockCosts blockCosts;

        public static BlockMetadata operator +(BlockMetadata a, BlockMetadata b) {
            return new BlockMetadata {
                strength = a.strength + b.strength,
                blockCosts = a.blockCosts + b.blockCosts
            };
        }

        public static BlockMetadata operator -(BlockMetadata a, BlockMetadata b) {
            return new BlockMetadata {
                strength = a.strength - b.strength,
                blockCosts = a.blockCosts - b.blockCosts
            };
        }
    }

    [Serializable]
    public struct BlockCosts : ITooltipComponent {
        public int creditCost;
        public int metalsCost;

        public float GaugePotentialStrength() {
            return S.Blocks.blockTemplates.GetAverageStrengthPerCredit() * creditCost;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return S.UI.Tooltips.tooltipGenerator.CreateBlockCostsView(parent, this);
        }

        public static BlockCosts operator +(BlockCosts a, BlockCosts b) {
            return new BlockCosts {
                creditCost = a.creditCost + b.creditCost,
                metalsCost = a.metalsCost + b.metalsCost
            };
        }

        public static BlockCosts operator -(BlockCosts a, BlockCosts b) {
            return new BlockCosts {
                creditCost = a.creditCost - b.creditCost,
                metalsCost = a.metalsCost - b.metalsCost
            };
        }

        public static BlockCosts operator *(BlockCosts a, float b) {
            return new BlockCosts {
                creditCost = Mathf.RoundToInt(a.creditCost * b),
                metalsCost = Mathf.RoundToInt(a.metalsCost * b)
            };
        }
    }
}