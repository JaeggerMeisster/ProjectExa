using Exa.Math;
using UnityEngine;

namespace Exa.Ships.Targeting {
    public readonly struct StaticAngleTarget : ITarget {
        private readonly float directionAngle;
        private readonly float magnitude;

        public StaticAngleTarget(float directionAngle, float magnitude = 10) {
            this.directionAngle = directionAngle;
            this.magnitude = magnitude;
        }

        public StaticAngleTarget(Vector2 from, Vector2 to, float magnitude = 10) {
            directionAngle = (to - from).GetAngle();
            this.magnitude = magnitude;
        }

        public Vector2 GetPosition(Vector2 current) {
            var offset = (Vector2.right * magnitude).Rotate(directionAngle);
            var directionVector = current + offset;

            return directionVector;
        }
    }
}