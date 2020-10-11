﻿using Exa.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.Grids.Blueprints
{
    public class BlueprintManager : MonoBehaviour
    {
        [HideInInspector] public BlueprintContainerCollection observableUserBlueprints = new BlueprintContainerCollection();
        [HideInInspector] public BlueprintContainerCollection observableDefaultBlueprints = new BlueprintContainerCollection();
        public BlueprintTypeBag blueprintTypes;

        [SerializeField] private DefaultBlueprintBag defaultBlueprintBag;

        public IEnumerator StartUp(IProgress<float> progress)
        {
            var userBlueprintPaths = CollectionUtils
                .GetJsonPathsFromDirectory(IOUtils.GetPath("blueprints"))
                .ToList();

            var defaultBlueprints = defaultBlueprintBag.ToList();
            var iterator = 0;
            var blueprintTotal = userBlueprintPaths.Count + defaultBlueprints.Count;

            // Load default blueprints
            foreach (var defaultBlueprint in defaultBlueprintBag)
            {
                AddDefaultBlueprint(defaultBlueprint);

                yield return null;
                iterator++;
                progress.Report((float)iterator / blueprintTotal);
            }

            // Load user defined blueprints
            foreach (var blueprintPath in userBlueprintPaths)
            {
                try
                {
                    AddUserBlueprint(blueprintPath);
                }
                catch (Exception e)
                {
                    Systems.UI.logger.Log($"Error loading blueprint: {e.Message}");
                }

                yield return null;
                iterator++;
                progress.Report((float)iterator / blueprintTotal);
            }
        }

        public BlueprintContainer GetBlueprint(string name)
        {
            if (observableDefaultBlueprints.ContainsKey(name))
                return observableDefaultBlueprints[name];
            
            if (observableUserBlueprints.ContainsKey(name))
                return observableUserBlueprints[name];

            throw new KeyNotFoundException();
        }

        public bool ContainsName(string name)
        {
            return observableDefaultBlueprints.ContainsKey(name)
                || observableUserBlueprints.ContainsKey(name);
        }

        private void AddDefaultBlueprint(DefaultBlueprint defaultBlueprint)
        {
            var blueprint = defaultBlueprint.ToContainer();

            if (ContainsName(blueprint.Data.name))
            {
                throw new ArgumentException("Blueprint named is duplicate");
            }

            observableDefaultBlueprints.Add(blueprint);
        }

        private void AddUserBlueprint(string path)
        {
            var blueprint = IOUtils.JsonDeserializeFromPath<Blueprint>(path);

            if (blueprint == null)
            {
                throw new ArgumentNullException("blueprint");
            }

            if (ContainsName(blueprint.name))
            {
                throw new ArgumentException($"Blueprint named \"{blueprint.name}\" is duplicate");
            }

            var args = new BlueprintContainerArgs(blueprint)
            {
                generateBlueprintFileHandle = true,
                generateBlueprintFileName = false
            };

            var observableBlueprint = new BlueprintContainer(args);
            observableBlueprint.BlueprintFileHandle.CurrentPath = path;
            observableBlueprint.LoadThumbnail();
            observableUserBlueprints.Add(observableBlueprint);
        }
    }
}