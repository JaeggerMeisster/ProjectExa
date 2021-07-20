using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Exa.Utils {
    public static class ProcessExtensions {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr handle);

        public static void Focus(this Process process) {
            SetForegroundWindow(process.MainWindowHandle);
        }

        public static int StartRedirected(this Process process, out List<string> stdOut, out List<string> errOut, int timeout = 5000) {
            stdOut = new List<string>();
            errOut = new List<string>();

            using var stdOutWaitHandle = new AutoResetEvent(false);
            using var errOutWaitHandle = new AutoResetEvent(false);

            DataReceivedEventHandler GetHandler(AutoResetEvent handle, List<string> streamOutput) {
                return (sender, e) => {
                    if (e.Data == null) {
                        handle.Set();
                    } else {
                        streamOutput.Add(e.Data);
                    }
                };
            }

            process.OutputDataReceived += GetHandler(stdOutWaitHandle, stdOut);
            process.ErrorDataReceived += GetHandler(errOutWaitHandle, errOut);

            if (!process.Start()) {
                throw new Exception("Could not start process");
            }
                
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (process.WaitForExit(timeout) &&
                stdOutWaitHandle.WaitOne(timeout) &&
                errOutWaitHandle.WaitOne(timeout)) {
                return process.ExitCode;
            }

            throw new Exception("Timed out");
        }
    }
}