using System.Linq;
using Exa.UI.Controls;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Settings {
    public class VideoSettingsPanel : SettingsTab<ExaVideoSettings, VideoSettingsValues> {
        public ResolutionDropdown resolutionDropdown;
        public DropdownControl refreshRatesDropdown;
        public RadioControl fullscreenRadio;

        public override VideoSettingsValues GetSettingsValues() {
            return new VideoSettingsValues {
                resolution = (Resolution) resolutionDropdown.Value,
                fullscreen = fullscreenRadio.Value
            };
        }

        public override void ReflectValues(VideoSettingsValues values) {
            // TODO: Notify user of invalid configuration
            if (!resolutionDropdown.ContainsItem(values.resolution)) {
                values.resolution = Container.DefaultValues.resolution;
            }

            resolutionDropdown.SetValue(values.resolution, false);
            refreshRatesDropdown.SetValue(values.resolution.refreshRate, false);
            fullscreenRadio.SetValue(values.fullscreen, false);
        }

        public override void Init() {
            Container.Resolutions = new Resolutions();

            refreshRatesDropdown.CreateTabs(
                Container.Resolutions
                    .GetRefreshRateLabels()
            );

            refreshRatesDropdown.OnValueChange.AddListener(
                obj => {
                    resolutionDropdown.FilterByRefreshRate((int) obj);
                    resolutionDropdown.SelectFirst();
                }
            );

            resolutionDropdown.CreateTabs(
                Container.Resolutions
                    .GetResolutionLabels()
                    .Reverse()
            );

            // Get first refresh rate
            var firstRefreshRate = (int) refreshRatesDropdown.Value;
            resolutionDropdown.FilterByRefreshRate(firstRefreshRate);

            base.Init();
        }

        public override void ApplyChanges() {
            const int length = 15;
            const string format = "Do you wish to keep these settings? (Reverting in {0} seconds)";

            var backupValues = Container.Clone();
            var setter = S.UI.Prompts.PromptTextSetter;
            var uiGroup = S.UI.Root.interactableAdapter;

            var prompt = null as Prompt;

            var coroutine = EnumeratorUtils.OnceEverySecond(length, second => { setter(format.Format(length - second)); })
                .Then(
                    () => {
                        Apply(backupValues);
                        ReflectValues(backupValues);

                        prompt.CleanUp();
                    }
                )
                .Start(this);

            prompt = S.UI.Prompts.PromptYesNo(
                format.Format(length),
                uiGroup,
                value => {
                    StopCoroutine(coroutine);

                    if (value) {
                        return;
                    }

                    Apply(backupValues);
                    ReflectValues(backupValues);
                }
            );

            base.ApplyChanges();
        }

        protected override ExaVideoSettings GetSettingsContainer() {
            return new ExaVideoSettings();
        }
    }
}