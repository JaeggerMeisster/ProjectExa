using UnityEditor;
using UnityEngine;

namespace Exa.Utils {
    public static class AssetImporterExtensions {
        public static void ApplyDefaultBlockImportSettings(this TextureImporter importer) {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
        }
    }
}