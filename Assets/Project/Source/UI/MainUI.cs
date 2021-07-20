using Exa.ShipEditor;
using Exa.UI.Controls;
using Exa.UI.Diagnostics;
using Exa.UI.Tooltips;
using UCommandConsole;
using UnityEngine;

namespace Exa.UI {
    public class MainUI : MonoBehaviour {
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private RectTransform rootTransform;
        [SerializeField] private LoadingScreen loadingScreen;
        [SerializeField] private ShipEditorOverlay editorOverlay;
        [SerializeField] private RootNavigation root;
        [SerializeField] private VariableTooltipManager tooltips;
        [SerializeField] private Console console;
        [SerializeField] private NotificationLogger logger;
        [SerializeField] private MouseCursorController mouseCursor;
        [SerializeField] private PromptController promptController;
        [SerializeField] private DiagnosticsPanel diagnostics;
        [SerializeField] private ControlFactory controlFactory;
        [SerializeField] private WipScreen wipScreen;

        public Canvas RootCanvas {
            get => rootCanvas;
        }

        public RectTransform RootTransform {
            get => rootTransform;
        }

        public LoadingScreen LoadingScreen {
            get => loadingScreen;
        }

        public ShipEditorOverlay EditorOverlay {
            get => editorOverlay;
        }

        public RootNavigation Root {
            get => root;
        }

        public VariableTooltipManager Tooltips {
            get => tooltips;
        }

        public Console Console {
            get => console;
        }

        public NotificationLogger Logger {
            get => logger;
        }

        public MouseCursorController MouseCursor {
            get => mouseCursor;
        }

        public PromptController Prompts {
            get => promptController;
        }

        public DiagnosticsPanel Diagnostics {
            get => diagnostics;
        }

        public ControlFactory Controls {
            get => controlFactory;
        }

        public WipScreen WipScreen {
            get => wipScreen;
        }

        private void Awake() {
            console.Initialize();
        }
    }
}