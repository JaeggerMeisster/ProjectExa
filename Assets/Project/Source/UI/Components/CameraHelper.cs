using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Components {
    public class CameraHelper : MonoBehaviour {
        [SerializeField] private Canvas canvas;
        [SerializeField] private float planeDistance;

        private void Awake() {
            canvas.worldCamera = S.CameraController.Camera;
            canvas.planeDistance = planeDistance;
        }
    }
}