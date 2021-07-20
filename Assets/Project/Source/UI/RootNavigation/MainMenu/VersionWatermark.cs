using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class VersionWatermark : MonoBehaviour {
        [SerializeField] private Text versionText;

        private void Awake() {
            versionText.text = $"Build: {Application.version}";
        }
    }
}