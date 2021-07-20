using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace Exa.ShipEditor {
    public class EditorGridTurretLayer : CustomEditorGridLayer {
        [SerializeField] private GameObject basePrefab;
        [SerializeField] private Transform prefabContainer;
        [SerializeField] private Transform instanceContainer;
        private Dictionary<ABpBlock, TurretOverlay> overlayInstances;

        private Dictionary<BlockTemplate, GameObject> overlayPrefabs;
        private bool overlayVisibility;

        public bool OverlayVisibility {
            set {
                overlayVisibility = value;
                overlayInstances.Values.ForEach(overlay => overlay.SetVisibility(value));
            }
        }

        public TurretBlocks TurretBlocks { get; private set; }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Init() {
            prefabContainer.DestroyChildren();
            instanceContainer.DestroyChildren();
            TurretBlocks = new TurretBlocks(this);
            overlayPrefabs = new Dictionary<BlockTemplate, GameObject>();
            overlayInstances = new Dictionary<ABpBlock, TurretOverlay>();

            var templates = S.Blocks.blockTemplates
                .Select(elem => elem.Data)
                .Where(Predicate);

            if (!templates.Any()) {
                Debug.LogError("Template filtering found no turret templates, this shouldn't happen");
            }
            
            templates.ForEach(GenerateTurretOverlayPrefab);
        }

        [Button]
        public void PrintTurretClaims() {
            Debug.Log(TurretBlocks.GetTurretClaims().Join(", "));
        }

        public TurretOverlay CreateGhostOverlay(ABpBlock block) {
            return CreateOverlay(block);
        }

        public TurretOverlay CreateStationaryOverlay(ABpBlock block) {
            var overlay = CreateOverlay(block);
            overlay.SetColor(Color.white.SetAlpha(0.5f));
            overlay.Presenter.Present(block);
            overlay.SetVisibility(overlayVisibility);
            overlayInstances[block] = overlay;

            return overlay;
        }

        private TurretOverlay CreateOverlay(ABpBlock block) {
            var overlay = overlayPrefabs[block.Template].Create<TurretOverlay>(instanceContainer);
            overlay.Import(block);

            return overlay;
        }

        public void RemoveStationaryOverlay(ABpBlock block) {
            Destroy(overlayInstances[block].gameObject);
            overlayInstances.Remove(block);
        }

        // TODO: Don't hardcode the user group here
        private void GenerateTurretOverlayPrefab(BlockTemplate template) {
            var overlay = basePrefab.Create<TurretOverlay>(prefabContainer);
            overlay.Generate(template.GetValues<ITurretValues>(BlockContext.UserGroup));
            overlayPrefabs[template] = overlay.gameObject;
        }

        protected override bool Predicate(BlockTemplate template) {
            // this editor layer should only be triggered by templates that contain turret values
            return template.GetAnyPartialDataIsOf<ITurretValues>();
        }

        protected override void OnAdd(ABpBlock block) {
            TurretBlocks.AddTurret(block);
        }

        protected override void OnRemove(ABpBlock block) {
            TurretBlocks.Remove(block);
        }
    }
}