using System.Collections.Generic;
using DG.Tweening;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI;
using Exa.UI.Tweening;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    /// <summary>
    ///     Grid layer for the Ship layer
    /// </summary>
    public partial class EditorGrid : MonoBehaviour, IUIGroup {
        [SerializeReference] private List<CustomEditorGridLayer> customLayers;
        public EditorGridBackgroundLayer backgroundLayer;
        public EditorGridBlueprintLayer blueprintLayer;
        public EditorGridGhostLayer ghostLayer;
        public EditorGridTurretLayer turretLayer;
        private Vector2 centerPos;
        private bool interactable = true;
        private Vector2 playerPos = Vector2.zero;
        private TweenWrapper<Vector3> positionTween;
        private Vector2Int size;

        public Vector2 MovementVector { private get; set; }

        public IEnumerable<ICustomEditorGridLayer> CustomLayers {
            get => customLayers;
        }

        /// <summary>
        ///     Whether or not the mouse is over UI
        /// </summary>
        public bool MouseOverUI { get; set; }

        private void Awake() {
            backgroundLayer.EnterGrid += OnEnterGrid;
            backgroundLayer.ExitGrid += OnExitGrid;
            positionTween = new TweenWrapper<Vector3>((e, t) => transform.DOLocalMove(e, t));
        }

        public void Update() {
            if (!Interactable) {
                return;
            }

            UpdatePosition();

            // Check for mouse input
            backgroundLayer.UpdateCurrActiveGridItem(transform.localPosition.ToVector2());
        }

        public void OnDisable() {
            // Reset values
            playerPos = Vector2.zero;
            mouseGridPos = null;
            transform.localPosition = Vector3.zero;
        }

        /// <summary>
        ///     Whether or not the grid can be interacted with
        /// </summary>
        public bool Interactable {
            get => interactable;
            set {
                interactable = value;

                if (!value) {
                    ghostLayer.SetVisibility(false);
                }
            }
        }

        /// <summary>
        ///     Creates a grid with the given size
        /// </summary>
        /// <param name="size"></param>
        public void GenerateGrid(Vector2Int size) {
            // Set the active size and set target player position the center of the grid
            this.size = size;

            // Set the movement pivot
            centerPos = GetGridOffset();
            transform.localPosition = centerPos.ToVector3();

            // Generate the grid
            backgroundLayer.GenerateGrid(size);
        }

        public void Import(Blueprint blueprint) {
            // Get size of blueprint class and resize the grid accordingly
            var editorSize = blueprint.BlueprintType.maxSize;
            GenerateGrid(editorSize);
            blueprintLayer.Import(blueprint);
        }

        public void ClearBlueprint() {
            blueprintLayer.ClearBlueprint();
        }

        private Vector2 GetGridOffset() {
            var halfSize = size.ToVector2() / 2f;

            return new Vector2 {
                x = -halfSize.x + 0.5f,
                y = -halfSize.y + 0.5f
            };
        }
    }
}