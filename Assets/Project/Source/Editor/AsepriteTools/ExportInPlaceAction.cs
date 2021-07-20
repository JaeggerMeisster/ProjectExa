using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Exa.IO;
using Exa.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Exa.CustomEditors {
    [Serializable]
    public class ExportInPlaceAction : EditorAction {
        private MessageType messageType = MessageType.Info;
        private string output;

        public ExportInPlaceAction(AsepriteTools context) : base(context) { }

        public override void Render() {
            EditorGUILayout.HelpBox($"Execution output: {output}", messageType);
        }

        public override void Execute() {
            GetProcess($"-b -list-layers --all-layers \"{context.FilePath}\"").StartRedirected(out var layers, out _);
            var progress = 0f;

            // Output folder
            var parentSystemFolder = Path.GetDirectoryName(context.FilePath);
            var outputSystemPath = IOUtils.CombinePath(parentSystemFolder, "Output");
            var outputAssetPath = IOUtils.SystemPathToAssetPath(outputSystemPath);
            IOUtils.EnsureAssetPathCreated(outputAssetPath);

            if (context.EnableLogging) {
                Debug.Log($"File path {context.FilePath}");
                Debug.Log($"Parent system folder {parentSystemFolder}");
                Debug.Log($"Output system path {outputSystemPath}");
                Debug.Log($"Output asset path {outputAssetPath}");
            }

            var fileName = Path.GetFileNameWithoutExtension(context.FilePath);

            try {
                foreach (var layer in layers) {
                    // Keep track of export progress
                    progress += 1f / layers.Count;
                    EditorUtility.DisplayProgressBar("Exporting layers ...", $"exporting {layer}", progress);

                    // Save layer output
                    var bytes = GetLayerOutput(layer, out var output);
                    var width = (int) output.meta.size.w;
                    var height = (int) output.meta.size.h;

                    try {
                        SaveLayer(
                            outputAssetPath,
                            fileName,
                            layer.Replace(" ", "_"),
                            bytes,
                            width,
                            height
                        );
                    } catch (Exception e) {
                        throw new Exception("Failed to save layer", e);
                    }
                }

                messageType = MessageType.Info;
            } catch (Exception e) {
                output = $"Failed to export layers: {e}";
                messageType = MessageType.Error;
                Debug.LogException(e, context);
            } finally {
                EditorUtility.ClearProgressBar();
            }
        }

        private byte[] GetLayerOutput(string layer, out dynamic output) {
            using var fileDisposer = IOUtils.TempFile("png", out var file);
            using var process = GetProcess($"-b --layer \"{layer}\" --sheet \"{file}\" \"{context.FilePath}\"");

            if (process.StartRedirected(out var stdOut, out var errOut) != 0) {
                throw new Exception($"Non 0 exit code when exporting layer {layer}");
            }

            if (context.EnableLogging) {
                stdOut.Log("std out: ");
                errOut.Log("err out: ");
            }

            output = JObject.Parse(stdOut.Join());

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
                            spriteMetaData.Add(new SpriteMetaData {
                                name = $"{texture.name}_{frameNumber}",
                                rect = new Rect(i, j - sizeY, sizeX, sizeY),
                                alignment = 0,
                                pivot = new Vector2(0f, 0f)
                            });
                            
                            frameNumber++;
                        }
                    }

                    importer.spritesheet = spriteMetaData.ToArray();
                }
            );
        }
    }
}