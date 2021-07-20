using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Exa.IO;
using Exa.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Exa.CustomEditors {
    public class AseEditor : FileTypeEditor {
        private string asepritePath;
        private AseExportConfiguration configuration;

        public override void OnEnable() {
            base.OnEnable();
            asepritePath = EditorPrefs.GetString("AsepriteTools_AsepritePath", "");
            configuration = GetConfiguration();
        }

        public override void OnDisable() {
            base.OnDisable();
        }

        public override IEnumerable<string> GetAcceptedFileTypes() {
            yield return ".ase";
            yield return ".aseprite";
        }

        public override void OnInspectorGUI() {
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Locate aseprite executeable")) {
                LocateAseprite();
            }

            if (GUILayout.Button("Start aseprite")) {
                Process.Start(asepritePath);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(8);
            EditorGUI.indentLevel++;

            var isFirstLayer = true;

            foreach (var (layer, layerConfig) in configuration.layers.Unpack()) {
                EditorGUILayout.LabelField($"Ase Layer: {layer}");
                EditorGUI.indentLevel++;

                if (isFirstLayer) {
                    layerConfig.mergeUp = false;
                    GUI.enabled = false;
                }

                layerConfig.mergeUp = EditorGUILayout.Toggle("Merge up", layerConfig.mergeUp);

                if (isFirstLayer) {
                    isFirstLayer = false;
                    GUI.enabled = true;
                }

                if (!layerConfig.mergeUp) {
                    layerConfig.name = EditorGUILayout.TextField("Export name", layerConfig.name);
                    layerConfig.exportOnlyFirstFrame = EditorGUILayout.Toggle("Export only first frame", layerConfig.exportOnlyFirstFrame);
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
            GUILayout.Space(8);

            if (GUILayout.Button("Save configuration")) {
                SaveConfiguration(configuration);
            }

            if (GUILayout.Button("Export in place")) {
                ExportInPlace();
            }
        }

        private void SaveConfiguration(AseExportConfiguration config) {
            config.NormalizeWithAseLayers(GetLayers());
            Context.Importer.userData = JsonConvert.SerializeObject(config);
        }

        private AseExportConfiguration GetConfiguration() {
            var output = JsonConvert.DeserializeObject<AseExportConfiguration>(Context.Importer.userData) ?? new AseExportConfiguration();

            SaveConfiguration(output);

            return output;
        }

        private void LocateAseprite() {
            asepritePath = EditorUtility.OpenFilePanelWithFilters(
                "Locate aseprite",
                "",
                new[] {
                    "Executable",
                    "exe"
                }
            );

            EditorPrefs.SetString("AsepriteTools_AsepritePath", asepritePath);
        }

        private void ExportInPlace() {
            var progress = 0f;

            // Output folder
            var fileName = Path.GetFileNameWithoutExtension(Context.AssetPath);
            var outputAssetPath = Path.Combine(Path.GetDirectoryName(Context.AssetPath), $"{fileName}-output");
            IOUtils.EnsureAssetPathCreated(outputAssetPath);

            try {
                var layerGroups = configuration.GetLayerGroups().ToList();

                foreach (var layerGroup in layerGroups) {
                    if (string.IsNullOrEmpty(layerGroup.configuration.name)) {
                        throw new InvalidOperationException("Layer configuration must not have an empty name");
                    }

                    // Keep track of export progress
                    progress += 1f / layerGroups.Count;
                    EditorUtility.DisplayProgressBar("Exporting layers ...", $"exporting {layerGroup.aseLayers.Join(", ")}", progress);

                    // Save layer output
                    var bytes = GetLayerOutput(layerGroup, out var output);
                    var width = (int) output.meta.size.w;
                    var height = (int) output.meta.size.h;

                    try {
                        SaveLayer(
                            outputAssetPath,
                            fileName,
                            layerGroup.configuration.name.Replace(" ", "_"),
                            bytes,
                            width,
                            height
                        );
                    } catch (Exception e) {
                        throw new Exception("Failed to save layer", e);
                    }
                }
            } catch (Exception e) {
                Debug.LogException(e, Context);
            } finally {
                EditorUtility.ClearProgressBar();
            }
        }

        private byte[] GetLayerOutput(GroupedLayerOutput layerGroup, out dynamic output) {
            using var fileDisposer = IOUtils.TempFile("png", out var file);

            string GetArgumentString() {
                var builder = new StringBuilder();
                builder.Append(" -b");

                foreach (var layer in layerGroup.aseLayers) {
                    builder.Append($" --layer \"{layer}\"");
                }

                builder.Append($" --sheet \"{file}\"");

                if (layerGroup.configuration.exportOnlyFirstFrame) {
                    builder.Append(" --frame-range 0,0");
                }

                builder.Append($" \"{Context.AssetPath}\"");

                return builder.ToString();
            }

            var argumentString = GetArgumentString();

            if (Context.EnableLogging) {
                Debug.Log($"exporting with argument string ${argumentString}");
            }

            using var process = GetProcess(argumentString);

            if (process.StartRedirected(out var stdOut, out var errOut) != 0) {
                throw new Exception($"Non 0 exit code when exporting layer {layerGroup.configuration.name}");
            }

            if (Context.EnableLogging) {
                stdOut.Log("std out: ");
                errOut.Log("err out: ");
            }

            try {
                output = JObject.Parse(stdOut.Join());
            } catch (JsonReaderException e) {
                throw new Exception($"Failed to parse JSON output, probably due to export failure, output: {stdOut.Join("\n")}", e);
            } catch (Exception e) {
                throw e;
            }

            return File.ReadAllBytes(file);
        }

        private void SaveLayer(string assetFolder, string fileName, string layer, byte[] sheet, int width, int height) {
            var texture = new Texture2D(width, height) {
                name = $"{fileName}-{layer}"
            };

            texture.LoadImage(sheet);
            var sprite = texture.CreateSprite();

            IOUtils.SaveSpriteToEditorPath(
                sprite,
                IOUtils.CombineAssetsPath(assetFolder, $"{texture.name}.png"),
                importer => {
                    importer.ApplyDefaultBlockImportSettings();

                    var sizeY = height;
                    var sizeX = height;

                    importer.wrapMode = TextureWrapMode.Clamp;
                    importer.maxTextureSize = 2048;
                    importer.crunchedCompression = false;
                    importer.compressionQuality = 100;
                    importer.isReadable = true;
                    importer.textureShape = TextureImporterShape.Texture2D;
                    importer.npotScale = TextureImporterNPOTScale.None;

                    var spriteMetaData = new List<SpriteMetaData>();
                    var frameNumber = 0;

                    for (var j = height; j > 0; j -= sizeY) {
                        for (var i = 0; i < width; i += sizeX) {
                            spriteMetaData.Add(
                                new SpriteMetaData {
                                    name = $"{texture.name}_{frameNumber}",
                                    rect = new Rect(i, j - sizeY, sizeX, sizeY),
                                    alignment = 0,
                                    pivot = new Vector2(0f, 0f)
                                }
                            );

                            frameNumber++;
                        }
                    }

                    importer.spritesheet = spriteMetaData.ToArray();
                }
            );
        }

        private Process GetProcess(string parameters) {
            return new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = asepritePath,
                    Arguments = parameters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
        }

        private IEnumerable<string> GetLayers() {
            GetProcess($"-b -list-layers --all-layers \"{Context.AssetPath}\"").StartRedirected(out var layers, out _);

            return layers;
        }
    }
}