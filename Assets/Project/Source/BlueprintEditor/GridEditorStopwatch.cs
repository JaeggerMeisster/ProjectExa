using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0649

namespace Exa.ShipEditor {
    public class GridEditorStopwatch : MonoBehaviour {
        public UnityEvent onTime;

        [SerializeField] private float invokeOnTime;
        private bool invokedOnTime;
        private float timeElapsedFromLastEdit;

        public void Reset() {
            timeElapsedFromLastEdit = 0f;
            invokedOnTime = false;
        }

        public void Update() {
            if (timeElapsedFromLastEdit > invokeOnTime && !invokedOnTime) {
                invokedOnTime = true;
                onTime?.Invoke();
            }

            timeElapsedFromLastEdit += Time.deltaTime;
        }

        public void OnEnable() {
            timeElapsedFromLastEdit = 0f;
            invokedOnTime = false;
        }

        public void EmulateInvoke() {
            invokedOnTime = true;
            onTime?.Invoke();
        }
    }
}