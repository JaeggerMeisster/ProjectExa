﻿using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using Exa.Validation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.ShipEditor
{
    public class BlueprintGridValidator : IValidator<BlueprintGridValidationArgs>
    {
        public ValidationResult Validate(BlueprintGridValidationArgs validationArgs)
        {
            var result = new ValidationResult(this);
            var blocks = validationArgs.blueprintBlocks;

            var controllers = blocks
                .Where(block => block.BlueprintBlock.Template.category.Is(BlockCategory.Controller));

            var controllerCount = controllers.Count();

            if (controllerCount > 1)
            {
                result.Throw<ControllerError>("Cannot have multiple controllers");
            }
            else if (controllerCount == 0)
            {
                result.Throw<ControllerError>("Must have atleast one controller");
            }

            if (blocks.Any(block => blocks.GetNeighbourCount(block) == 0))
            {
                result.Throw<DisconnectedBlocksError>("Blueprint has disconnected blocks");
            }

            return result;
        }

        public bool BlueprintBlocksAreConnected(Vector2Int startingPoint, BlueprintBlocks blocks)
        {
            var visited = new HashSet<Vector2Int>();

            void FloodFill(Vector2Int gridPos)
            {
                if (blocks.ContainsMember(gridPos) && !visited.Contains(gridPos))
                {
                    visited.Add(gridPos);
                    FloodFill(new Vector2Int(gridPos.x - 1, gridPos.y));
                    FloodFill(new Vector2Int(gridPos.x + 1, gridPos.y));
                    FloodFill(new Vector2Int(gridPos.x, gridPos.y - 1));
                    FloodFill(new Vector2Int(gridPos.x, gridPos.y + 1));
                }
            }

            FloodFill(startingPoint);

            return blocks.GetOccupiedTileCount() == visited.Count;
        }
    }
}