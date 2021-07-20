using System;
using System.Diagnostics;
using Exa.Misc;
using Exa.UI;
using Exa.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Exa.Debugging {
    public class LoggerInterceptor : MonoBehaviour, ILogHandler {
        private ILogHandler defaultLogHandler;
        private NotificationLogger notificationLogger;

        private void Awake() {
            notificationLogger = S.UI.Logger;
            defaultLogHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }

        public void LogException(Exception exception, Object context) {
            // Missing reference exceptions when the application is quitting should be ignored
            if (exception is MissingReferenceException && S.IsQuitting) {
                return;
            }

            if (DebugMode.ConsoleDump.IsEnabled()) {
                exception.LogToConsole();
            }

            defaultLogHandler.LogException(exception, context);

            if (exception is UserException userException) {
                LogUserException(userException);
            }
        }

        [DebuggerHidden]
        public void LogFormat(LogType logType, Object context, string format, params object[] args) {
            defaultLogHandler.LogFormat(logType, context, format, args);
        }

        [DebuggerHidden]
        public void LogUserException(UserException exception) {
            notificationLogger.LogException(exception);
        }
    }
}