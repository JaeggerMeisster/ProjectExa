using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI.Diagnostics {
    public class FpsCounter : MonoBehaviour {
        [SerializeField] private Text fpsText;

        public void Update() {
            fpsText.text = $"{Mathf.RoundToInt(1f / Time.smoothDeltaTime)} FPS";
        }
    }
}