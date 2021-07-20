using Exa.Data;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using UnityEngine;

namespace Exa.Ships.Navigation {
    public class AxisThrustVectors : MonoBehaviour, IThrustVectors {
        [SerializeField] private Vector2 currentDirection;
        [SerializeField] private Vector2 targetDirection;
        private GridInstance gridInstance;

        private ThrusterAxis xAxis;
        private ThrusterAxis yAxis;

        public void Update() {
            if (!gridInstance.Active) {
                return;
            }

            xAxis.SetGraphics(0f);
            yAxis.SetGraphics(0f);

            return;

            MathUtils.MoveTowards(ref currentDirection, targetDirection, Time.deltaTime * 2f);

            xAxis.SetGraphics(currentDirection.x);
            yAxis.SetGraphics(currentDirection.y);
        }

        public void Register(ThrusterBehaviour thruster) {
            SelectAxis(thruster, out var component).Register(thruster, component);
        }

        public void Unregister(ThrusterBehaviour thruster) {
            SelectAxis(thruster, out var component).Unregister(thruster, component);
        }

        public void SetGraphics(Vector2 directionScalar) {
            targetDirection = directionScalar;
            xAxis.SetGraphics(targetDirection.x);
            yAxis.SetGraphics(targetDirection.y);
        }

        public void Setup(GridInstance gridInstance, Scalar thrustModifier) {
            this.gridInstance = gridInstance;
            xAxis = new ThrusterAxis(thrustModifier);
            yAxis = new ThrusterAxis(thrustModifier);
        }

        public void Fire(Vector2 force) {
            xAxis.Fire(force.x);
            yAxis.Fire(force.y);
        }

        public Vector2 GetForce(Vector2 forceDirection, Scalar thrustModifier) {
            var force = GetUnClampedForce(forceDirection);

            return thrustModifier.GetValue(force);
        }

        public Vector2 GetClampedForce(Vector2 forceDirection, Scalar thrustModifier) {
            var force = GetUnClampedForce(forceDirection);
            var clampedForce = MathUtils.GrowDirectionToMax(forceDirection.normalized, force);

            return thrustModifier.GetValue(clampedForce);
        }

        private Vector2 GetUnClampedForce(Vector2 forceDirection) {
            return new Vector2 {
                x = xAxis.Clamp(forceDirection.x),
                y = yAxis.Clamp(forceDirection.y)
            };
        }

        private ThrusterAxis SelectAxis(BlockBehaviour thruster, out bool positiveAxisComponent) {
            var direction = thruster.block.BlueprintBlock.Direction;

            positiveAxisComponent = direction <= 1;

            return direction % 2 == 0 ? xAxis : yAxis;
        }
    }
}