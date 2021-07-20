using System.Collections.Generic;
using Exa.Grids.Blueprints;
using Exa.Math;
using UnityEngine;

namespace Exa.Gameplay {
    public class VicFormation : Formation {
        private readonly float echelonAngle;

        public VicFormation(float echelonAngle = -130f) {
            this.echelonAngle = echelonAngle;
        }

        protected override IEnumerable<Vector2> GetLocalLayout(IEnumerable<Blueprint> blueprints) {
            using var enumerator = blueprints.GetEnumerator();

            // Skip first element, as it always has a Vector2.zero position value
            enumerator.MoveNext();
            var firstShip = enumerator.Current;

            yield return Vector2.zero;

            var echelonSpread = CalculateEchelonSpread(firstShip);
            var rightEchelonSize = echelonSpread;
            var leftEchelonSize = echelonSpread;

            // Handle echelon layout
            var rightEchelonPivot = Vector2.zero;
            var leftEchelonPivot = Vector2.zero;

            while (enumerator.MoveNext()) {
                // Get right echelon position
                yield return GetLocalPosition(
                    enumerator.Current,
                    ref rightEchelonSize,
                    ref rightEchelonPivot,
                    echelonAngle
                );

                if (!enumerator.MoveNext()) {
                    break;
                }

                // Get left echelon positon
                yield return GetLocalPosition(
                    enumerator.Current,
                    ref leftEchelonSize,
                    ref leftEchelonPivot,
                    -echelonAngle
                );
            }
        }

        private Vector2 GetLocalPosition(
            Blueprint blueprint,
            ref float echelonMagnitude,
            ref Vector2 positionPivot,
            float angle
        ) {
            var positionOffset = MathUtils.FromAngledMagnitude(echelonMagnitude, angle);
            positionPivot += positionOffset;
            echelonMagnitude = CalculateEchelonSpread(blueprint);

            return positionPivot;
        }

        private float CalculateEchelonSpread(Blueprint blueprint) {
            return blueprint.Grid.MaxSize * 2f;
        }
    }
}