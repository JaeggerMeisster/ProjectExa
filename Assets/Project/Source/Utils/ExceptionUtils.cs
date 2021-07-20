using System;
using UnityEngine;

namespace Exa.Utils {
    public static class ExceptionUtils {
        public static void LogToConsole(this Exception exception) {
            try {
                var output = S.UI.Console.output;
                output.BeginPrint("An uncaught exception was thrown");
                output.DumpExceptionLogRecursively(exception);
                output.EndPrint();
            } catch (Exception consoleException) {
                var message = "An exception has occurred while logging an exception to the console (seriously?)";
                Debug.LogError(new Exception(message, consoleException));
                S.UI.Logger.LogException(message);
            }
        }
    }
}