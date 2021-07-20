using System;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public abstract class BlockBehaviour<T> : BlockBehaviour
        where T : struct, IBlockComponentValues {
        [NonSerialized] protected T data;

        public T Data {
            get => data;
            set {
                var copy = data;
                data = value;

            #if UNITY_EDITOR
                if (Application.isPlaying) {
                    OnBlockDataReceived(copy, data);
                }
            #else
                OnBlockDataReceived(copy, data);
            #endif
            }
        }

        public override IBlockComponentValues BlockComponentData {
            get => Data;
            set => Data = (T) value;
        }

        public T GetDefaultData() {
            var context = block.Parent.BlockContext;
            var template = block.BlueprintBlock.Template;
            var store = S.Blocks.Values;
            var success = store.TryGetValues<T>(context, template, out var result);

            return success ? result : throw new InvalidOperationException("Could not find default values");
        }

        public sealed override void NotifyRemoved() {
            OnRemove();
            OnBlockDataReceived(data, default);
        }

        public override void MockSetValues() {
            OnBlockDataReceived(default, data);
        }

        public override Type GetDataType() {
            return typeof(T);
        }

        protected virtual void OnBlockDataReceived(T oldValues, T newValues) { }
    }

    public abstract class BlockBehaviour : MonoBehaviour, IBlockComponentContainer {
        [HideInInspector] public Block block;
        private bool forceActive;

        public abstract IBlockComponentValues BlockComponentData { get; set; }

        public Block Block {
            get => block;
        }

    #if UNITY_EDITOR
        protected bool DebugFocused {
            get => block.DebugFocused;
        }
    #endif

        public IGridInstance Parent {
            get => block.Parent;
        }

        public GridInstance GridInstance {
            get => Parent as GridInstance;
        }

        public void NotifyAdded() {
            OnAdd();
        }

        public abstract void NotifyRemoved();

        private void Update() {
            if (forceActive || GridInstance && GridInstance.Active) {
                BlockUpdate();
            }
        }

        protected virtual void BlockUpdate() { }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }

        public void ForceActive() {
            forceActive = true;
        }

        public abstract void MockSetValues();

        public abstract Type GetDataType();
    }
}