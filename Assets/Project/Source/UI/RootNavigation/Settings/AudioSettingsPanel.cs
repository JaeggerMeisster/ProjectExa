using System.Diagnostics;
using System.Linq;
using Exa.Audio.Music;
using Exa.IO;
using Exa.UI.Controls;
using Exa.UI.Tooltips;
using Exa.Utils;
using UnityEngine.UI;

namespace Exa.UI.Settings {
    public class AudioSettingsPanel : SettingsTab<ExaAudioSettings, AudioSettingsValues> {
        public SliderControl masterVolumeSlider;
        public SliderControl musicVolumeSlider;
        public SliderControl effectsVolumeSlider;
        public DropdownControl soundTrackNameDropdown;
        public Button soundTrackNameFolderButton;

        public override void Init() {
            CreateSoundTrackDropdownTabs();
            soundTrackNameFolderButton.onClick.AddListener(OpenFolder);
            base.Init();
        }

        protected override ExaAudioSettings GetSettingsContainer() {
            return new ExaAudioSettings();
        }

        public override AudioSettingsValues GetSettingsValues() {
            return new AudioSettingsValues {
                masterVolume = masterVolumeSlider.Value / 100f,
                musicVolume = musicVolumeSlider.Value / 100f,
                effectsVolume = effectsVolumeSlider.Value / 100f,
                soundTrackName = soundTrackNameDropdown.Value as string
            };
        }

        public override void ReflectValues(AudioSettingsValues values) {
            masterVolumeSlider.SetValue(values.masterVolume * 100f, false);
            musicVolumeSlider.SetValue(values.musicVolume * 100f, false);
            effectsVolumeSlider.SetValue(values.effectsVolume * 100f, false);
            soundTrackNameDropdown.SetValue(values.soundTrackName, false);
        }

        public override void ApplyChanges() {
            base.ApplyChanges();

            if (Container.LoadHandler.LoadEnumerator.IsNotNull(out var enumerator)) {
                StartCoroutine(
                    enumerator.ScheduleWithTargetFramerate()
                        .Then(() => S.Audio.Music.IsPlaying = true)
                );
            }
        }

        private void CreateSoundTrackDropdownTabs() {
            var dict = S.Audio.Music.Provider.Descriptions
                .ToDictionary(description => description.Name);

            var options = dict.Values.Select(description => description.GetLabeledValue());

            soundTrackNameDropdown.CreateTabs(
                options,
                (key, tab) => {
                    tab.gameObject
                        .AddComponent<TextTooltipTrigger>()
                        .SetText($"Song count: {dict[key].SongCount}");
                }
            );
        }

        private void OpenFolder() {
            var path = Tree.Root.CustomSoundTracks.GetPath();

            var startInfo = new ProcessStartInfo {
                Arguments = path,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo).Focus();
        }
    }
}