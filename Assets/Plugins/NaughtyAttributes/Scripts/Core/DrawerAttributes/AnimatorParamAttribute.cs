using System;
using UnityEngine;

namespace NaughtyAttributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class AnimatorParamAttribute : DrawerAttribute {
        public AnimatorParamAttribute(string animatorName) {
            AnimatorName = animatorName;
            AnimatorParamType = null;
        }

        public AnimatorParamAttribute(string animatorName, AnimatorControllerParameterType animatorParamType) {
            AnimatorName = animatorName;
            AnimatorParamType = animatorParamType;
        }

        public string AnimatorName { get; }
        public AnimatorControllerParameterType? AnimatorParamType { get; }
    }
}