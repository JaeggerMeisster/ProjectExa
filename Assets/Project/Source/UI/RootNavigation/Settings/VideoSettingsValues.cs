using System;
using UnityEngine;

namespace Exa.UI.Settings {
    public class VideoSettingsValues : IEquatable<VideoSettingsValues> {
        public bool fullscreen;
        public Resolution resolution;

        public bool Equals(VideoSettingsValues other) {
            return resolution.Equals(other?.resolution) && fullscreen.Equals(other?.fullscreen);
        }
    }
}