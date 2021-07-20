using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Logging;
using Exa.Types;
using Exa.UI.Tooltips;
using Exa.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Exa.Ships {
    public class BlockGrid : Grid<Block> {
        private readonly DefaultDict<Type, List<BlockBehaviour>> blockBehaviours;
        private readonly GridTotals totals;

        public BlockGrid(IGridInstance parent) {
            totals = S.Blocks.Totals.StartWatching(this, parent.BlockContext);

            blockBehaviours = new DefaultDict<Type, List<BlockBehaviour>>(_ => new List<BlockBehaviour>());
            Parent = parent;
        }

        public IGridInstance Parent { get; }
        public bool Rebuilding { get; set; }
        public Block Controller { get; protected set; }

        public GridTotals GetTotals() {
            return totals;
        }

        public override void Add(Block block) {
            if (block.GetIsController()) {
                if (Controller != null) {
                    throw new DuplicateControllerException(block.GridAnchor);
                }

                Controller = block;
            }

            foreach (var behaviour in block.GetBehaviours()) {
                blockBehaviours[behaviour.GetType()].Add(behaviour);
            }

            base.Add(block);
        }

        public override void Remove(Block block) {
            base.Remove(block);

            if (block.GetIsController()) {
                Controller = null;
            }

            foreach (var behaviour in block.GetBehaviours()) {
                blockBehaviours[behaviour.GetType()].Remove(behaviour);
            }

            // Only rebuild if it isn't being rebuilt already
            if (!Rebuilding) {
                GS.BlockGridManager.ScheduleRebuild(Parent);
            }
        }

        public IEnumerable<T> QueryStrict<T>()
            where T : BlockBehaviour {
            return blockBehaviours[typeof(T)].Cast<T>();
        }

        public IEnumerable<T> QueryLike<T>() {
            return blockBehaviours
                .Where(kvp => typeof(T).IsAssignableFrom(kvp.Key))
                .SelectMany(x => x.Value)
                .Cast<T>();
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() {
            return new ITooltipComponent[] { };
        }

        public void DestroyIfEmpty() {
            Logs.Log(
                "Destroying grid",
                new {
                    Name = Parent.Transform.gameObject.name
                }
            );

            // No need to clean up now. Wait a frame for any potential remaining blocks in the grid to have been deleted or returned to the pool
            if (!GridMembers.Any()) {
                EnumeratorUtils.DelayOneFrame(
                        () => { Parent.Transform.gameObject.Destroy(); }
                    )
                    .Start();
            }
        }

        internal void Import(Blueprint blueprint) {
            foreach (var anchoredBlueprintBlock in blueprint.Grid) {
                Place(anchoredBlueprintBlock);
            }
        }

        internal void Place(ABpBlock aBpBlock) {
            var block = aBpBlock.CreateInactiveBlockInGrid(Parent);
            Parent.AddBlock(block, false);
            block.gameObject.SetActive(true);
        }

        internal void Destroy(Vector2Int gridAnchor) {
            GetMember(gridAnchor).DestroyBlock();
        }
    }
}