using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class NotificationView : MonoBehaviour {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text header;
        [SerializeField] private Text body;

        private void Awake() {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1, 0.2f);

            StartCoroutine(EnumeratorUtils.Delay(() => { canvasGroup.DOFade(0f, 0.2f); }, 3f));

            StartCoroutine(EnumeratorUtils.Delay(() => { Destroy(gameObject); }, 3.2f));
        }

        public void Setup(string header, string body) {
            this.header.text = header;
            this.body.text = body;
        }
    }
}