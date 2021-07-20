using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.IO;
using Exa.Types.Binding;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Tree = Exa.IO.Tree;

#pragma warning disable CS0649

namespace Exa.Grids.Blueprints {
    public class BlueprintManager : MonoBehaviour {
        [HideInInspector] public BlueprintContainerCollection userBlueprints = new BlueprintContainerCollection();
        [HideInInspector] public BlueprintContainerCollection defaultBlueprints = new BlueprintContainerCollection();
        public BlueprintTypeBag blueprintTypes;

        [FormerlySerializedAs("defaultBlueprintBag")] [SerializeField] private StaticBlueprintBag staticBlueprintBag;
        [HideInInspector] public CompositeObservableEnumerable<BlueprintContainer> useableBlueprints;

        public IEnumerator Init(IProgress<float> progress) {
            var userBlueprintPaths = CollectionUtils
                .GetJsonPathsFromDirectory(Tree.Root.Blueprints)
                .ToList();

            var defaultBlueprintsList = staticBlueprintBag.ToList();
            var iterator = 0;
            var blueprintTotal = userBlueprintPaths.Count + defaultBlueprintsList.Count;

            useableBlueprints =
                new CompositeObservableEnumerable<BlueprintContainer>(userBlueprints, defaultBlueprints);

            // Load user defined blueprints
            foreach (var blueprintPath in userBlueprintPaths) {
                try {
                    AddUserBlueprint(blueprintPath);
                } catch (Exception e) {
                    S.UI.Logger.LogException($"Error loading blueprint: {e.Message}", false);
                }

                yield return new WorkUnit();

                iterator++;
                progress.Report((float) iterator / blueprintTotal);
            }

            // Load default blueprints
            foreach (var defaultBlueprint in staticBlueprintBag) {
                AddDefaultBlueprint(defaultBlueprint);

                yield return new WorkUnit();

                iterator++;
                progress.Report((float) iterator / blueprintTotal);
            }

            yield return new WorkUnit();
        }

        public Blueprint GetBlueprint(string name) {
            if (defaultBlueprints.ContainsKey(name)) {
                return defaultBlueprints[name].Data;
            }

            if (userBlueprints.ContainsKey(name)) {
                return userBlueprints[name].Data;
            }

            throw new KeyNotFoundException($"Name: \"{name}\" not found");
        }

        public bool ContainsName(string name) {
            return GetBlueprintNames().Contains(name);
        }

        public IEnumerable<string> GetBlueprintNames() {
            return defaultBlueprints
                .Concat(userBlueprints)
                .Select(blueprint => blueprint.Data.name);
        }

        private void AddDefaultBlueprint(StaticBlueprint staticBlueprint) {
            var blueprint = staticBlueprint.GetContainer();

            if (ContainsName(blueprint.Data.name)) {
                throw new ArgumentException("Blueprint named is duplicate");
            }

            defaultBlueprints.Add(blueprint);
        }

        private void AddUserBlueprint(string path) {
            var blueprint = IOUtils.JsonDeserializeFromPath<Blueprint>(path);

            if (blueprint == null) {
                throw new ArgumentNullException("blueprint");
            }

            if (ContainsName(blueprint.name)) {
                throw new ArgumentException($"Blueprint named \"{blueprint.name}\" is duplicate");
            }

            var args = new BlueprintContainerArgs(blueprint) {
                generateBlueprintFileHandle = true,
                generateBlueprintFileName = false
            };

            var observableBlueprint = new BlueprintContainer(args) {
                BlueprintFileHandle = {
                    CurrentPath = path
                }
            };

            observableBlueprint.LoadThumbnail();
            userBlueprints.Add(observableBlueprint);
        }
    }
}