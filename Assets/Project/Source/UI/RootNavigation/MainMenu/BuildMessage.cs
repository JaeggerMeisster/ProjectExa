using UnityEngine;

namespace Exa.UI {
    [CreateAssetMenu(fileName = "buildMessage", menuName = "UI/MainMenu/BuildMessage")]
    public class BuildMessage : ScriptableObject {
        [TextArea(3, 100)] public string buildMessage;
    }
}