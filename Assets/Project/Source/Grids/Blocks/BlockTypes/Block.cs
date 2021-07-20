using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes {
    /// <summary>
    ///     Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IGridMember {
        [HideInInspector] public BlockPoolMember blockPoolMember;

        [SerializeField] private PhysicalBehaviour physicalBehaviour;
        [SerializeField] [HideInInspector] private new BoxCollider2D collider;
        [NonSerialized] public ABpBlock aBpBlock;
        private IGridInstance parent;

    #if UNITY_EDITOR
        public bool DebugFocused { get; set; }
    #endif

    #if ENABLE_BLOCK_LOGS
        public List<string> Logs { get; } = new List<string>();
    #endif

        private List<BlockBehaviour> behaviours;

        public PhysicalBehaviour PhysicalBehaviour {
            get => physicalBehaviour;
        }

        public BoxCollider2D Collider {
            get => collider;
            set => collider = value;
        }

        public IGridInstance Parent {
            get => parent;
        }

        public void SetParentWithoutNotify(IGridInstance value) {
            parent = value;
        }

        /// <summary>
        /// Notifies the block as being added.
        /// </summary>
        /// <param name="mockSetValues">If true, it will call the OnBlockDataReceived handler on every behaviour</param>
        public void NotifyAdded(bool mockSetValues) {
            OnAdd();

            foreach (var behaviour in GetBehaviours()) {
                behaviour.NotifyAdded();

                if (mockSetValues) {
                    behaviour.MockSetValues();
                }
            }
        }

        public void NotifyRemoved() {
            OnRemove();

            foreach (var behaviour in GetBehaviours()) {
                behaviour.NotifyRemoved();
            }
        }

        public GridInstance GridInstance {
            get => Parent as GridInstance;
        }

        public Vector2Int GridAnchor {
            get => aBpBlock.gridAnchor;
        }

        public BlueprintBlock BlueprintBlock {
            get => aBpBlock.blueprintBlock;
        }

        public void AddGridTotals(GridTotals totals) {
            totals.Metadata += BlueprintBlock.Template.metadata;

            foreach (var behaviour in GetBehaviours()) {
                behaviour.BlockComponentData.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.Metadata += BlueprintBlock.Template.metadata;

            foreach (var behaviour in GetBehaviours()) {
                behaviour.BlockComponentData.RemoveGridTotals(totals);
            }
        }

        public bool Equals(IGridMember other) {
            return IGridMemberComparer.Default.Equals(this, other);
        }

        /// <summary>
        ///     Returns the block to the pool
        /// </summary>
        public void DestroyBlock() {
        #if ENABLE_BLOCK_LOGS
            Logs.Add("Function: DestroyBlock");
        #endif

            if (GS.IsQuitting) {
                parent = null;

            #if ENABLE_BLOCK_LOGS
                Logs.Add("Function: DestroyBlock: Return");

                return;
            #endif
            }

            Parent.RemoveBlock(this);
            gameObject.SetActive(false);
            blockPoolMember.ReturnBlock();
        }

        public T GetBehaviour<T>() {
            return GetBehaviours().FindFirst<T>();
        }

        public BlockBehaviour<T> GetBehaviourOfData<T>()
            where T : struct, IBlockComponentValues {
            return GetBehaviours()
                .Where(behaviour => behaviour.GetDataTypeIsOf<T>())
                .Cast<BlockBehaviour<T>>()
                .FirstOrDefault();
        }

        public bool TryGetBehaviour<T>(out T value) {
            value = GetBehaviour<T>();

            return value.Equals(default);
        }

        public IEnumerable<BlockBehaviour> GetBehaviours() {
            if (behaviours == null) {
                gameObject.GetComponents(behaviours = new List<BlockBehaviour>());
            }

            return behaviours;
        }

        protected virtual void OnAdd() {
        #if ENABLE_BLOCK_LOGS
            Logs.Add("Function: OnAdd");
        #endif
        }

        protected virtual void OnRemove() {
        #if ENABLE_BLOCK_LOGS
            Logs.Add("Function: OnRemove");
        #endif
        }

        public override string ToString() {
            return $"Block: {BlueprintBlock.Template.id}";
        }
    }
}