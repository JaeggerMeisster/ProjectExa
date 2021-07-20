using System;
using System.Collections.Generic;
using System.Linq;
using Exa.AI;
using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Types.Generics;
using Exa.UI;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.Serialization;

#pragma warning disable CS0649

namespace Exa.Ships {
    public abstract class GridInstance : MonoBehaviour, IRaycastTarget, ITooltipPresenter, IGridInstance {
        [Header("References")]
        [SerializeField] private GridAi gridAi;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CircleCollider2D mouseOverCollider;

        [Header("Settings")]
        [SerializeField] private ValueOverride<CursorState> cursorOverride;
        [FormerlySerializedAs("shipDebugFont")] public Font debugFont;

        private Tooltip debugTooltip;
        private Controller controller;

        public ActionScheduler ActionScheduler { get; private set; }
        public Blueprint Blueprint { get; private set; }

        public Controller Controller {
            get => controller;
            set {
                if (value != null && controller != null) {
                    throw new InvalidOperationException(
                        "Cannot overwrite the controller by another one before explicitly setting the value as null." +
                        "This exception is probably caused by multiple controllers being set on a grid"
                    );
                }

                controller = value;
            }
        }

        public BlockGridDiff Diff { get; private set; }
        public bool Active { get; private set; }
        public IGridOverlay Overlay { get; set; }
        public float HullIntegrity { get; set; }
        public BlockContext BlockContext { get; private set; }
        public GridInstanceConfiguration Configuration { get; private set; }
        public BlockGrid BlockGrid { get; private set; }

        public Transform Transform {
            get => transform;
        }

        public Rigidbody2D Rigidbody2D {
            get => rb;
        }

        public GridAi Ai {
            get => gridAi;
        }

        protected virtual void Awake() {
            debugTooltip = new Tooltip(GetDebugTooltipComponents, debugFont);
        }

        private void Update() {
            var currentHull = BlockGrid.GetTotals().Hull;
            var totalHull = Blueprint.Grid.GetTotals(BlockContext).Hull;
            HullIntegrity = currentHull / totalHull;

            if (Active) {
                Overlay.SetHullFill(currentHull, totalHull);
            }
        }

        private void FixedUpdate() {
            if (Active) {
                ActiveFixedUpdate(Time.fixedDeltaTime);
            }

            mouseOverCollider.offset = rb.centerOfMass;

            if (debugTooltip != null) {
                debugTooltip.ShouldRefresh = true;
            }
        }

        public virtual void OnRaycastEnter() {
            if (!Active) {
                return;
            }

            EnterRaycast();
        }

        public virtual void OnRaycastExit() {
            if (!Active) {
                return;
            }

            ExitRaycast();
        }

        public Tooltip GetTooltip() {
            return debugTooltip;
        }

        public event Action ControllerDestroyed;

        protected virtual void ActiveFixedUpdate(float fixedDeltaTime) {
            ActionScheduler.ExecuteActions(fixedDeltaTime);
        }

        // TODO: Make this look nicer by breaking up the ship and adding an explosion
        public virtual void OnControllerDestroyed() {
            foreach (var thruster in BlockGrid.QueryLike<ThrusterBehaviour>()) {
                thruster.PowerDown();
            }

            ControllerDestroyed?.Invoke();
            Active = false;

            ExitRaycast();
        }

        public GridTotals GetCurrentTotals() {
            return BlockGrid.GetTotals();
        }

        public GridTotals GetBaseTotals() {
            return Blueprint.Grid.GetTotals(BlockContext);
        }

        public virtual void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            BlockGrid = new BlockGrid(this);
            Configuration = configuration;
            ActionScheduler = new ActionScheduler(this);
            Active = true;
            BlockContext = blockContext;

            var radius = blueprint.Grid.MaxSize / 2f;
            mouseOverCollider.radius = radius;
            BlockGrid.Import(blueprint);
            Blueprint = blueprint;

            Diff = S.Blocks.Diffs.StartWatching(BlockGrid, Blueprint.Grid);

            gridAi.Init();
        }

        public virtual void SetBlueprint(Blueprint blueprint) {
            Blueprint = blueprint;
        }

        public string GetInstanceString() {
            return $"({GetType().Name}) {Blueprint.name} : {gameObject.GetInstanceID()}";
        }

        private void EnterRaycast() {
            (Overlay as GridOverlay)?.SetHovered(true);
            S.UI.MouseCursor.stateManager.Add(cursorOverride);

            if (DebugMode.Ships.IsEnabled()) {
                S.UI.Tooltips.shipAIDebugTooltip.Show(this);
            }
        }

        private void ExitRaycast() {
            (Overlay as GridOverlay)?.SetHovered(false);
            S.UI.MouseCursor.stateManager.Remove(cursorOverride);

            if (DebugMode.Ships.IsEnabled()) {
                S.UI.Tooltips.shipAIDebugTooltip.Hide();
            }
        }

        // TODO: Somehow cache this, or let the results come from a central manager
        public IEnumerable<GridInstance> QueryNeighbours(float radius, ContextMask contextMask, bool mustBeActive = false) {
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius, contextMask.LayerMask);

            foreach (var collider in colliders) {
                var neighbour = collider.gameObject.GetComponent<GridInstance>();

                if (neighbour == null) {
                    continue;
                }

                var passesContextMask = contextMask.HasValue(neighbour.BlockContext);

                if (!ReferenceEquals(neighbour, this) && passesContextMask) {
                    if (mustBeActive && !neighbour.Active) {
                        continue;
                    }

                    yield return neighbour;
                }
            }
        }

        public abstract ShipSelection GetAppropriateSelection(Formation formation);

        public abstract bool MatchesSelection(ShipSelection selection);

        public abstract Vector2 GetPosition();

        public abstract void SetPosition(Vector2 position);

        protected virtual TooltipGroup GetDebugTooltipComponents() {
            return new TooltipGroup(
                1,
                new TooltipTitle(GetInstanceString(), false),
                new TooltipSpacer(),
                new TooltipText("Blueprint:"),
                new TooltipGroup(Blueprint.GetDebugTooltipComponents(), 1),
                new TooltipSpacer(),
                new TooltipText("BlockGrid:"),
                new TooltipGroup(BlockGrid.GetDebugTooltipComponents(), 1),
                new TooltipSpacer(),
                new TooltipText("State:"),
                new TooltipGroup(
                    1,
                    new TooltipText($"HullIntegrity: {HullIntegrity}")
                ),
                new TooltipSpacer(),
                new TooltipText("State:"),
                new TooltipGroup(
                    1,
                    new TooltipText($"Rotation: {rb.rotation}"),
                    new TooltipText($"Clamped Rotation: {MathUtils.NormalizeAngle360(rb.rotation)}")
                ),
                new TooltipSpacer(),
                new TooltipText("AI:"),
                new TooltipGroup(gridAi.GetDebugTooltipComponents(), 1)
            );
        }

        public void SetLookAt(Vector2 globalLookAt) {
            Rigidbody2D.rotation = (globalLookAt - GetPosition()).GetAngle();
        }

        public void SetRotation(float angle) {
            Rigidbody2D.rotation = angle;
        }

        public void Rotate(float degrees) {
            Rigidbody2D.rotation += degrees;
        }

        public void Repair() {
            foreach (var physicalBehaviour in BlockGrid.QueryLike<PhysicalBehaviour>()) {
                physicalBehaviour.Repair();
            }
        }

        public void ReconcileWithDiff() {
            var pendingAdd = Diff.PendingAdd.ToList();
            var pendingRemove = Diff.PendingRemove.ToList();

            foreach (var block in pendingRemove) {
                BlockGrid.Destroy(block.GridAnchor);
            }

            foreach (var block in pendingAdd) {
                BlockGrid.Place(Blueprint[block.GridAnchor]);
            }
        }
    }
}