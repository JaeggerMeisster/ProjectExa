using Exa.Misc;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI {
    public class NotificationLogger : MonoBehaviour {
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private Transform container;

        public void NotifyNowPlaying(string name) {
            Notify("Now playing", name);
        }

        public void LogException(UserException exception) {
            LogException(exception.Message);
        }

        public void LogException(string message, bool unhandled = true) {
            var header = unhandled ? "Unhandled exception" : "Error";
            Notify(header, message);
        }

        public void Notify(string header, string message) {
            notificationPrefab.Create<NotificationView>(container).Setup(header, message);
        }
    }
}