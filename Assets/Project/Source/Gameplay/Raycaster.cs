using System.Collections.Generic;
using System.Linq;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay {
    public class Raycaster : MonoBehaviour {
        private bool isRaycasting = true;
        public IEnumerable<IRaycastTarget> CurrentTargets { get; private set; }

        public bool IsRaycasting {
            private get => isRaycasting;
            set {
                isRaycasting = value;

                if (!value) {
                    ClearTargets();
                }
            }
        }

        public void Update() {
            if (IsRaycasting) {
                UpdateRaycastTarget();
            }
        }

        public void OnDisable() {
            ClearTargets();
        }

        public bool TryGetTarget<T>(out T result)
            where T : class {
            if (CurrentTargets == null) {
                result = null;

                return false;
            }

            result = CurrentTargets.FindFirst<T>();

            return result != null;
        }

        private void UpdateRaycastTarget() {
            var worldPoint = S.Input.MouseWorldPoint;

            var hits = Physics2D.RaycastAll(worldPoint, Vector2.zero)
                .Select(hit => hit.transform.gameObject.GetComponent<IRaycastTarget>())
                .Where(hit => hit != null)
                .ToList();

            if (CurrentTargets != null) {
                var intersection = hits.Intersect(CurrentTargets)
                    .ToList();

                foreach (var newTarget in hits.Except(intersection)) {
                    newTarget.OnRaycastEnter();
                }

                foreach (var oldTarget in CurrentTargets.Except(intersection)) {
                    oldTarget.OnRaycastExit();
                }
            } else {
                foreach (var newTarget in hits) {
                    newTarget.OnRaycastEnter();
                }
            }

            CurrentTargets = hits;
        }

        private void ClearTargets() {
            if (CurrentTargets == null) {
                return;
            }

            CurrentTargets.ForEach(target => target.OnRaycastExit());
            CurrentTargets = null;
        }
    }
}