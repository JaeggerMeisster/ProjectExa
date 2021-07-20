using Exa.Data;
using UnityEngine;

#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace Exa.UI.Settings {
    public class ExaVideoSettings : SaveableSettings<VideoSettingsValues> {
        public override VideoSettingsValues DefaultValues {
            get => new VideoSettingsValues {
                resolution = Resolutions.GetHighestSupportedResolution(),
                fullscreen = true
            };
        }

        public Resolutions Resolutions { get; set; }

        protected override string Key {
            get => "videoSettings";
        }

        public override void Apply() {
        #if !UNITY_EDITOR
            Screen.SetResolution(
                width:                  Values.resolution.width,
                height:                 Values.resolution.height,
                fullscreen:             Values.fullscreen,
                preferredRefreshRate:   Values.resolution.refreshRate);
        #endif
        }

        public override VideoSettingsValues Clone() {
            return new VideoSettingsValues {
                resolution = Values.resolution,
                fullscreen = Values.fullscreen
            };
        }

        protected override VideoSettingsValues DeserializeValues(string path) {
            var values = base.DeserializeValues(path);

            if (values.resolution.Equals(default(Resolution))) {
                values.resolution = Resolutions.GetHighestSupportedResolution();
            }

            return values;
        }
    }
}