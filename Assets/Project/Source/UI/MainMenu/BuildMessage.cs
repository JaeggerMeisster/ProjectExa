﻿using UnityEngine;

namespace Exa.UI
{
    [CreateAssetMenu(fileName = "buildMessage", menuName = "UI/MainMenu/BuildMessage")]
    public class BuildMessage : ScriptableObject
    {
        [TextArea]
        public string buildMessage;
    }
}