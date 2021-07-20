using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks {
    public abstract class BlockTemplate : ScriptableObject, IGridTotalsModifier {
        [Header("Settings")]
        public string id;
        public string displayId;
        public BlockCategory category;
        public Sprite thumbnail;
        public Vector2Int size;
        public BlockMetadata metadata;
        public GameObject inertPrefab;
        public GameObject alivePrefab;
        
        [Header("Template partials")]
        [SerializeField] protected TemplatePartial<PhysicalData> physicalPartial;

        private void OnEnable() {
            foreach (var partial in GetTemplatePartials()) {
                partial.Template = this;
            }

            if (!inertPrefab) {
                Debug.LogWarning("inertPrefab must have a prefab reference");
            }
        }

        public void AddGridTotals(GridTotals totals) {
            totals.Metadata += metadata;

            foreach (var partial in GetTemplatePartials()) {
                partial.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.Metadata -= metadata;

            foreach (var partial in GetTemplatePartials()) {
                partial.RemoveGridTotals(totals);
            }
        }

        public T GetValues<T>(BlockContext blockContext)
            where T : IBlockComponentValues {
            return S.Blocks.Values.GetValues<T>(blockContext, this);
        }

        public bool GetAnyPartialDataIsOf<T>()
            where T : IBlockComponentValues {
            return GetTemplatePartials().Any(partial => partial.GetDataTypeIsOf<T>());
        }

        public bool GetAnyPartialDataIsOf(Type type) {
            return GetTemplatePartials().Any(partial => partial.GetDataTypeIsOf(type));
        }

        public virtual IEnumerable<TemplatePartialBase> GetTemplatePartials() {
            yield return physicalPartial;
        }
        
        public void SetContextlessValues(Block block) {
            if (block.BlueprintBlock.Template != this) {
                throw new InvalidOperationException("Cannot set contextless values of block whose template is not the current template");
            }
            
            foreach (var partial in GetTemplatePartials()) {
                partial.SetValues(block, partial.GetContextlessValues());
            }
        }

        public override string ToString() {
            return $"Block template id: {id}";
        }
    }
}