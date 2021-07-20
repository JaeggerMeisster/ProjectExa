using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Types.Binding;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks {
    public class ObservableBlockTemplateCollection : ObservableCollection<BlockTemplateContainer> {
        private float? averageStrengthPerCredit = null;

        public float GetAverageStrengthPerCredit() {
            return averageStrengthPerCredit ??= this.Select(
                    template => {
                        var metadata = template.Data.metadata;

                        return (float) metadata.strength / metadata.blockCosts.creditCost;
                    }
                )
                .Average();
        }
    }

    /// <summary>
    ///     Registers block types and creates block pools
    /// </summary>
    public class BlockFactory : MonoBehaviour {
        [SerializeField] private TotalsManager totalsManager;
        [SerializeField] private BlockTemplateBag blockTemplateBag;
        [SerializeField] private InertBlockPoolGroup inertPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup defaultPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup userPrefabGroup;
        [SerializeField] private AliveBlockPoolGroup enemyPrefabGroup;
        public ObservableBlockTemplateCollection blockTemplates = new ObservableBlockTemplateCollection();
        public Dictionary<string, BlockTemplate> blockTemplatesDict = new Dictionary<string, BlockTemplate>();

        public BlockValuesStore Values { get; private set; }
        public BlockGridDiffManager Diffs { get; private set; }

        public TotalsManager Totals {
            get => totalsManager;
        }

        public T FindTemplate<T>()
            where T : class {
            return blockTemplateBag.FindFirst<T>();
        }

        public IEnumerator Init(IProgress<float> progress) {
            Values = new BlockValuesStore();
            Diffs = new BlockGridDiffManager();
            var enumerator = EnumeratorUtils.ReportForeachOperation(blockTemplateBag, RegisterBlockTemplate, progress);

            while (enumerator.MoveNext()) {
                yield return enumerator.Current;
            }
        }

        public GameObject GetInactiveInertBlock(string id, Transform transform) {
            return inertPrefabGroup.GetInactiveBlock(id, transform).gameObject;
        }

        /// <summary>
        ///     Get the block prefab with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Block GetInactiveBlock(string id, IGridInstance instance) {
            return GetGroup(instance.BlockContext)
                .GetInactiveBlock(id, instance.Transform)
                .block;
        }

        /// <summary>
        ///     Register a template, and set the values on the block prefab
        /// </summary>
        /// <param name="blockTemplate"></param>
        private IEnumerator RegisterBlockTemplate(BlockTemplate blockTemplate) {
            if (blockTemplatesDict.ContainsKey(blockTemplate.id)) {
                throw new Exception("Duplicate block id found");
            }

            blockTemplates.Add(new BlockTemplateContainer(blockTemplate));
            blockTemplatesDict[blockTemplate.id] = blockTemplate;
            inertPrefabGroup.CreateInertPrefab(blockTemplate);

            yield return new WorkUnit();

            foreach (var context in BlockContextExtensions.GetContexts()) {
                Values.Register(context, blockTemplate);

                try {
                    GetGroup(context).CreateAlivePrefabGroup(blockTemplate);
                } catch (Exception e) {
                    throw new Exception($"Exception while registering block template: {blockTemplate} for {context}", e);
                }

                yield return new WorkUnit();
            }
        }

        private AliveBlockPoolGroup GetGroup(BlockContext blockContext) {
            return blockContext switch {
                BlockContext.DefaultGroup => defaultPrefabGroup,
                BlockContext.UserGroup => userPrefabGroup,
                BlockContext.EnemyGroup => enemyPrefabGroup,
                _ => null
            };
        }
    }
}