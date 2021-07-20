using System;
using DG.Tweening;
using Exa.Gameplay;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Camera {
    public class CameraController : MonoBehaviour {
        [SerializeField] private CameraTargetSettings defaultSettings;
        [SerializeField] private UnityEngine.Camera targetCamera;

        private Tween cameraMoveTween;
        private Tween cameraZoomTween;

        public ICameraTarget CurrentTarget { get; private set; }
        public UserTarget UserTarget { get; private set; }

        public CameraTargetSettings DefaultSettings {
            get => defaultSettings;
        }

        public UnityEngine.Camera Camera {
            get => targetCamera;
        }

        public Vector2 CurrentPosition {
            get => targetCamera.transform.position;
        }

        private void Awake() {
            UserTarget = new UserTarget(defaultSettings);
        }

        private void Update() {
            if (CurrentTarget != null && !CurrentTarget.TargetValid) {
                EscapeTarget();
            }

            if (CurrentTarget != null) {
                UserTarget.ImportValues(CurrentTarget);
            }

            UserTarget.Tick();

            var target = GetTarget();
            var cameraOrthoSize = target.GetCalculatedOrthoSize();
            var cameraPosition = target.GetWorldPosition().ToVector3(-10);

            targetCamera.DOOrthoSize(cameraOrthoSize, 0.2f)
                .Replace(ref cameraZoomTween);

            targetCamera.transform.DOMove(cameraPosition, 0.2f)
                .Replace(ref cameraMoveTween);
        }

        public void SetSelectionTarget(ShipSelection selection, bool teleport = false) {
            if (selection.Count <= 0) {
                throw new ArgumentException("Cannot set an empty selection as target", nameof(selection));
            }

            var target = new SelectionTarget(selection, defaultSettings);
            SetTarget(target, teleport);
        }

        public void SetTarget(ICameraTarget newTarget, bool teleport = false) {
            if (teleport) {
                targetCamera.transform.position = newTarget.GetWorldPosition().ToVector3(-10);
                targetCamera.orthographicSize = newTarget.GetCalculatedOrthoSize();
            }

            CurrentTarget = newTarget;
        }

        public void EscapeTarget() {
            CurrentTarget = null;
        }

        public ICameraTarget GetTarget() {
            return CurrentTarget ?? UserTarget;
        }
    }
}