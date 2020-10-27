﻿using System.Linq;
using UnityEngine;

namespace Exa.Debugging
{
    public class DebugDragger : MonoBehaviour
    {
        private IDebugDragable currentDragable;
        private Vector2 offset;
        private Vector2 averagedVelocity;
        private float sampleTime = 0.2f;

        public void Update() {
            if (currentDragable == null) return;

            // Set the velocity
            var mouseWorldPoint = Systems.Input.MouseWorldPoint;
            var velocity = currentDragable.GetPosition() - offset - mouseWorldPoint;
            AverageVelocity(-velocity, sampleTime);

            var targetPoint = mouseWorldPoint + offset;
            currentDragable.SetGlobals(targetPoint, Vector2.zero);
        }

        public void OnPress() {
            if (!TryGetDebugDragable(out var dragable)) return;

            currentDragable = dragable;
            averagedVelocity = Vector2.zero;

            var mouseWorldPoint = Systems.Input.MouseWorldPoint;
            var currentPoint = currentDragable.GetPosition();

            offset = currentPoint - mouseWorldPoint;
            currentDragable.SetGlobals(currentPoint, Vector2.zero);
        }

        public void OnRelease() {
            if (currentDragable == null) return;

            var mouseWorldPoint = Systems.Input.MouseWorldPoint;
            var targetPoint = mouseWorldPoint + offset;
            currentDragable.SetGlobals(targetPoint, averagedVelocity);
            currentDragable = null;
        }

        private void AverageVelocity(Vector2 newVelocity, float sampleTime) {
            var deltaTime = Time.deltaTime;
            var total = averagedVelocity * deltaTime * (sampleTime - deltaTime) + newVelocity;
            averagedVelocity = total * sampleTime / deltaTime;
        }

        private static bool TryGetDebugDragable(out IDebugDragable dragable) {
            try {
                return GameSystems.Raycaster.TryGetTarget(out dragable);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch {
                dragable = null;
                return false;
            }
        }
    }
}