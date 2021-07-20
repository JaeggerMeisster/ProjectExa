using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Gameplay {
    public class SelectionArea : MonoBehaviour {
        [SerializeField] private RectTransform rect;
        private Vector2 endWorldPoint;
        private Vector2 startWorldPoint;

        private void Awake() {
            startWorldPoint = Vector2.zero;
            endWorldPoint = Vector2.zero;
        }

        private void Update() {
            SetPosition();
        }

        public void Show(Vector2 startWorldPoint) {
            rect.gameObject.SetActive(true);
            this.startWorldPoint = startWorldPoint;
            SetEnd(startWorldPoint);
        }

        public void SetEnd(Vector2 endWorldPoint) {
            this.endWorldPoint = endWorldPoint;
            SetPosition();
        }

        public void Hide() {
            rect.gameObject.SetActive(false);
        }

        private void SetPosition() {
            var start = GetScreenSpace(startWorldPoint);
            var end = GetScreenSpace(endWorldPoint);

            var min = new Vector2 {
                x = Mathf.Min(start.x, end.x),
                y = Mathf.Min(start.y, end.y)
            };

            var max = new Vector2 {
                x = Mathf.Max(start.x, end.x),
                y = Mathf.Max(start.y, end.y)
            };

            rect.anchoredPosition = min;
            rect.sizeDelta = max - min;
        }

        private Vector2 GetScreenSpace(Vector2 worldPos) {
            return S.CameraController.Camera.WorldToScreenPoint(worldPos);
        }
    }
}