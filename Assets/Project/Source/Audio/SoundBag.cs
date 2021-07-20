using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;

namespace Exa.Audio {
    [CreateAssetMenu(menuName = "Audio/SoundBag")]
    [Serializable]
    public class SoundBag : ScriptableObjectBag<Sound> {
        [SerializeField] private AudioType filter;

        protected override IEnumerable<Sound> GetAllInstances() {
            return base.GetAllInstances().Where(sound => filter.HasValue(sound.AudioType));
        }
    }
}