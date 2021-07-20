#pragma warning disable CS0649

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UCommandConsole {
    public enum OutputColor {
        normal,
        error,
        warning,
        accent
    }

    public class ConsoleOutput : MonoBehaviour {
        [Header("References")]
        [SerializeField] private Transform outputContent;
        [SerializeField] private GameObject commandOutputContainerPrefab;
        [SerializeField] private GameObject linePrefab;

        [Header("Output colors")]
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color darkDefaultColor;
        [SerializeField] private Color errorColor;
        [SerializeField] private Color darkErrorColor;
        [SerializeField] private Color warningColor;
        [SerializeField] private Color darkWarningColor;
        [SerializeField] private Color accentColor;
        [SerializeField] private Color darkAccentColor;

        [Header("Settings")]
        [SerializeField] private int OutputSize = 100;
        [SerializeField] private bool dumpExceptionMessage;
        [SerializeField] private bool dumpExceptionStackTrace;
        [SerializeField] private bool dumpInnerException;
        private readonly Queue<CommandOutputContainer> outputContainersQueue = new Queue<CommandOutputContainer>();
        private CommandOutputContainer currentContainer;

        private bool encapsulatingPrintCalls;

        public void Clear() {
            currentContainer = null;

            while (outputContainersQueue.Count != 0) {
                var container = outputContainersQueue.Dequeue();
                Destroy(container.gameObject);
            }
        }

        public void DumpExceptionLogRecursively(Exception e, int indentationLevel = 1) {
            if (dumpExceptionMessage) {
                var message = $"Exception Message: {e.Message}";
                Print(message, indentationLevel, OutputColor.error, true);
            }

            if (dumpExceptionStackTrace) {
                var message = $"Exception StackTrace: {e.StackTrace}".Replace("  ", "");
                Print(message, indentationLevel, OutputColor.error, true);
            }

            if (dumpInnerException && e.InnerException != null) {
                indentationLevel++;
                var message = "Inner exception:";
                Print(message, indentationLevel, OutputColor.error, true);
                DumpExceptionLogRecursively(e.InnerException, indentationLevel);
            }
        }

        public void BeginPrint(string name) {
            encapsulatingPrintCalls = true;

            var containerGO = Instantiate(commandOutputContainerPrefab, outputContent);
            containerGO.name = name;

            var container = containerGO.GetComponent<CommandOutputContainer>();
            container.timestamp.text = DateTime.Now.ToString("HH:mm:ss");

            currentContainer = container;
            outputContainersQueue.Enqueue(container);

            if (outputContainersQueue.Count > OutputSize) {
                var oldContainer = outputContainersQueue.Dequeue();
                Destroy(oldContainer.gameObject);
            }
        }

        public void EndPrint() {
            encapsulatingPrintCalls = false;
            currentContainer = null;
        }

        public void Print(string line, int indentationLevel, Color color) {
            if (!encapsulatingPrintCalls) {
                throw new Exception("Begin print must be called before any print calls");
            }

            var lineGO = Instantiate(linePrefab, currentContainer.lineContainer);
            var outputLine = lineGO.GetComponent<ConsoleOutputLine>();
            outputLine.Indentation = indentationLevel;
            outputLine.text.text = line;
            outputLine.text.color = color;
        }

        public void Print(string line, int indentationLevel, OutputColor color = OutputColor.normal, bool dark = false) {
            Print(line, indentationLevel, GetColor(color, dark));
        }

        public void Print(string line, OutputColor color = OutputColor.normal, bool dark = false) {
            Print(line, 0, GetColor(color, dark));
        }

        private Color GetColor(OutputColor outputColor, bool dark) {
            switch (outputColor) {
                case OutputColor.normal:
                    return dark ? darkDefaultColor : defaultColor;

                case OutputColor.error:
                    return dark ? darkErrorColor : errorColor;

                case OutputColor.warning:
                    return dark ? darkWarningColor : warningColor;

                case OutputColor.accent:
                    return dark ? darkAccentColor : accentColor;

                default:
                    return default;
            }
        }
    }
}