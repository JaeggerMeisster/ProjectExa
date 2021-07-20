using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Exa.IO;
using Ionic.Zip;
using UnityEngine;
using UnityEngine.Networking;
using CompressionLevel = Ionic.Zlib.CompressionLevel;

namespace Exa.Audio.Music {
    public class CustomSoundTrackDescription : ISoundTrackDescription {
        private static readonly IEnumerable<string> SupportedAudioTypes = new[] {
            ".wav"
        };
        private static readonly string ConfigName = "config.json";

        private readonly CustomSoundTrackMetadata metadata;
        private readonly string path;

        public CustomSoundTrackDescription(string path) {
            this.path = path;

            metadata = GetMetadata(path);
        }

        public string Name {
            get => metadata.Name;
        }

        public int SongCount {
            get => metadata.Songs.Count();
        }

        public void LoadSoundTrack(SoundTrackLoadHandler loadHandler) {
            var zip = ZipFile.Read(path);
            var songList = new List<ISong>();
            var soundTrack = new CustomSoundTrack(songList, this);
            loadHandler.LoadEnumerator = LoadFiles(metadata, zip, loadHandler, soundTrack);
        }

        public IEnumerator LoadFiles(CustomSoundTrackMetadata metadata, ZipFile zip, SoundTrackLoadHandler loadHandler, ISoundTrack target) {
            loadHandler.Progress.Report(0f);

            var zipEntries = zip.Entries.ToDictionary(entry => entry.FileName);
            var songCount = (float) metadata.Songs.Count();
            var count = 0f;

            yield return new WaitForEndOfFrame();

            foreach (var songMetadata in metadata.Songs) {
                using var stream = zipEntries[songMetadata.FileName].GetStream();
                var fileName = Path.GetTempFileName();
                File.WriteAllBytes(fileName, stream.ToArray());

                yield return new WaitForEndOfFrame();

                var www = UnityWebRequestMultimedia.GetAudioClip(fileName, UnityEngine.AudioType.WAV);
                var request = www.SendWebRequest();

                while (!request.isDone) {
                    loadHandler.Progress.Report(count / songCount + request.progress / songCount);

                    yield return new WaitForEndOfFrame();
                }

                var clip = DownloadHandlerAudioClip.GetContent(www);

                File.Delete(fileName);

                target.Songs.Add(new CustomSong(clip, songMetadata));
                loadHandler.Progress.Report(++count / songCount);
            }

            loadHandler.OutputSoundtrack = target;
        }

        private CustomSoundTrackMetadata GetMetadata(string path) {
            using var zip = ZipFile.Read(path);
            zip.CompressionLevel = CompressionLevel.BestCompression;
            var configEntry = zip.Entries.FirstOrDefault(entry => entry.FileName == ConfigName);

            return configEntry == null
                ? GenerateMetadata(zip)
                : configEntry.ReadJson<CustomSoundTrackMetadata>(SerializationMode.Readable);
        }

        private CustomSoundTrackMetadata GenerateMetadata(ZipFile zip) {
            var name = Path.GetFileNameWithoutExtension(zip.Name);

            var songs = zip.Entries.Filter(SupportedAudioTypes)
                .Select(
                    entry => new CustomSongMetadata {
                        Name = Path.GetFileNameWithoutExtension(entry.FileName),
                        FileName = entry.FileName,
                        Atmospheres = Atmosphere.All,
                        Volume = 1f
                    }
                );

            var metadata = new CustomSoundTrackMetadata {
                Name = name,
                Songs = songs.ToList()
            };

            AppendMetadata(zip, metadata);

            return metadata;
        }

        private void AppendMetadata(ZipFile file, CustomSoundTrackMetadata metadata) {
            var json = IOUtils.JsonSerializeWithSettings(metadata, SerializationMode.Readable);
            file.AddEntry(ConfigName, Encoding.UTF8.GetBytes(json));
            file.Save();
        }
    }
}