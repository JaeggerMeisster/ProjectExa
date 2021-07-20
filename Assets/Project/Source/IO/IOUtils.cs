using System;
using System.IO;
using System.Linq;
using Exa.Utils;
using UnityEditor;
using UnityEngine;

namespace Exa.IO {
    public static partial class IOUtils {
        public static string CombinePathWithDataPath(params string[] paths) {
            paths = paths.Prepend(Application.persistentDataPath)
                .ToArray();

            return CombinePath(paths);
        }

        public static string CombinePath(params string[] paths) {
            return NormalizeSystem(Path.Combine(paths));
        }

        public static string NormalizeSystem(string path) {
            return path.Replace('/', '\\');
        }

        public static string CombineAssetsPath(params string[] paths) {
            try {
                return NormalizeAssets(Path.Combine(paths));
            } catch (Exception e) {
                throw new Exception($"Exception throw with paths {string.Join(", ", paths)}", e);
            }
        }

        public static string NormalizeAssets(string path) {
            return path.Replace('\\', '/');
        }

        public static IDisposable TempFile(string extension, out string file) {
            var fileName = CombinePath(Path.GetTempPath(), $"{Guid.NewGuid()}.{extension}");
            file = fileName;

            File.Create(fileName).Dispose();

            return new DisposableAction(() => { File.Delete(fileName); });
        }

        public static void SaveTexture2D(Texture2D tex, string filePath) {
            var bytes = tex.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
        }

        public static void EnsureAssetPathCreated(string assetPath) {
            assetPath = NormalizeAssets(assetPath);

            var parentPath = Path.GetDirectoryName(assetPath);
            var childFolder = Path.GetFileName(assetPath);

            if (AssetDatabase.IsValidFolder($"{parentPath}/{childFolder}")) {
                return;
            }

            AssetDatabase.CreateFolder(parentPath, childFolder);
        }

        public static Sprite SaveSpriteToEditorPath(Sprite sprite, string path, Action<TextureImporter> modifyImportSettings = null) {
            // Delete if exists
            if (File.Exists(path)) {
                File.Delete(path);
            }

            // Write the texture and save the asset
            File.WriteAllBytes(path, sprite.texture.EncodeToPNG());
            AssetDatabase.Refresh();
            AssetDatabase.AddObjectToAsset(sprite, path);
            AssetDatabase.SaveAssets();

            var assetImporter = AssetImporter.GetAtPath(path);
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter == null) {
                throw new Exception($"Asset importer {assetImporter} at path {path} is not an instance of TextureImporter");
            }

            textureImporter.spritePixelsPerUnit = sprite.pixelsPerUnit;
            textureImporter.mipmapEnabled = false;

            modifyImportSettings?.Invoke(textureImporter);

            EditorUtility.SetDirty(textureImporter);
            textureImporter.SaveAndReimport();

            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        public static string SystemPathToAssetPath(string path) {
            path = NormalizeAssets(path);

            if (path.StartsWith(Application.dataPath)) {
                return "Assets" + path.Substring(Application.dataPath.Length);
            }

            throw new ArgumentException(nameof(path), $"Path: {path} is not a valid asset path");
        }

        public static Texture2D LoadTexture2D(string filePath, int width, int height) {
            var bytes = File.ReadAllBytes(filePath);
            var tex = new Texture2D(width, height);
            tex.LoadImage(bytes);

            return tex;
        }

        public static void EnsureCreated(string directoryString) {
            if (!Directory.Exists(directoryString)) {
                Directory.CreateDirectory(directoryString);
            }
        }
    }
}