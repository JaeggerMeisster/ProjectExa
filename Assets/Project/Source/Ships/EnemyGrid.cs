using Exa.Debugging;
using Exa.Gameplay;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Project.Source.UI.Gameplay;
using Exa.Ships.Navigation;
using UnityEngine;

namespace Exa.Ships {
    public class EnemyGrid : GridInstance, IDebugDragable, IEnemyWarningCircleSource {
        [SerializeField] private NavigationOptions navigationOptions;
        public INavigation Navigation { get; private set; }

        public Vector2 GetDebugDraggerPosition() {
            return transform.position;
        }

        public void SetDebugDraggerGlobals(Vector2 position, Vector2 velocity) {
            transform.position = position;
            Rigidbody2D.velocity = velocity;
        }

        public override void Import(Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration) {
            Navigation = navigationOptions.GetNavigation(this, blueprint);
            base.Import(blueprint, blockContext, configuration);
            Overlay = GS.ShipFactory.CreateOverlay(this);
        }

        protected override void ActiveFixedUpdate(float fixedDeltaTime) {
            Navigation?.Update(fixedDeltaTime);
            base.ActiveFixedUpdate(fixedDeltaTime);
        }

        public override ShipSelection GetAppropriateSelection(Formation formation) {
            return new EnemyShipSelection(formation);
        }

        public override bool MatchesSelection(ShipSelection selection) {
            return selection is EnemyShipSelection;
        }

        float IEnemyWarningCircleSource.GetSize() {
            return BlockGrid.MaxSize;
        }

        public override Vector2 GetPosition() {
            return Rigidbody2D.worldCenterOfMass;
        }

        public override void SetPosition(Vector2 position) {
            transform.position = position - Rigidbody2D.centerOfMass.Rotate(-Rigidbody2D.rotation);
        }
    }
}