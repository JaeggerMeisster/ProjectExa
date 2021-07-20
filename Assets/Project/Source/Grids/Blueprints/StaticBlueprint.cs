using Exa.IO;
using UnityEngine;
using SerializationMode = Exa.IO.SerializationMode;
#if UNITY_EDITOR
using Exa.Utils;
using UnityEditor;

#endif

namespace Exa.Grids.Blueprints {
    [CreateAssetMenu(menuName = "Grids/Blueprints/StaticBlueprint")]
    public class StaticBlueprint : ScriptableObject {
        [TextArea(3, 3000)] public string blueprintJson;

        private BlueprintContainer container;

    #if UNITY_EDITOR
        public void Save(Blueprint blueprint) {
            blueprint.name = StringExtensions.GetUniqueName(blueprint.name, S.Blueprints.GetBlueprintNames());
            blueprintJson = IOUtils.JsonSerializeWithSettings(blueprint, SerializationMode.Readable);
            var path = $"Assets/Project/GameData/Grids/Blueprints/StaticBlueprints/{blueprint.name}.asset";
            AssetDatabase.CreateAsset(this, path);
        }
    #endif

        public BlueprintContainer GetContainer() {
            return container ??= ToContainer();
        }

        public Blueprint GetBlueprint() {
            return GetContainer().Data;
        }

        private BlueprintContainer ToContainer() {
            var blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(blueprintJson, SerializationMode.Readable);

            var args = new BlueprintContainerArgs(blueprint) {
                generateBlueprintFileHandle = false,
                useDefaultThumbnailFolder = true
            };

            var container = new BlueprintContainer(args);
            container.LoadThumbnail();

            return container;
        }
    }
}