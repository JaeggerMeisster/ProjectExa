using System;

namespace NaughtyAttributes {
    public enum EButtonEnableMode {
	    /// <summary>
	    ///     Button should be active always
	    /// </summary>
	    Always,
	    /// <summary>
	    ///     Button should be active only in editor
	    /// </summary>
	    Editor,
	    /// <summary>
	    ///     Button should be active only in playmode
	    /// </summary>
	    Playmode
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : SpecialCaseDrawerAttribute {
        public ButtonAttribute(string text = null, EButtonEnableMode enabledMode = EButtonEnableMode.Always) {
            Text = text;
            SelectedEnableMode = enabledMode;
        }

        public string Text { get; }
        public EButtonEnableMode SelectedEnableMode { get; }
    }
}