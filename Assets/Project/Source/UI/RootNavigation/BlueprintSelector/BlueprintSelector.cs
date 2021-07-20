#pragma warning disable CS0649

using System;
using System.Collections.Generic;
using Exa.Grids.Blueprints;
using Exa.IO;
using Exa.ShipEditor;
using Exa.Types.Binding;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI {

    public class BlueprintSelector : ViewBinder<BlueprintView, BlueprintContainer, Blueprint>, IUIGroup {

        [SerializeField] private Navigateable blueprintSelectorNavigateable;
        [SerializeField] private Navigateable shipEditorNavigateable;
        [SerializeField] private BlueprintDetails blueprintDetails;

        private bool interactable = true;

        public bool Interactable {
            get => interactable;
            set {
                interactable = value;

                blueprintSelectorNavigateable.Interactable = value;
            }
        }

        public void OnAddNewBlueprint() {
            S.UI.Prompts.PromptForm(
                "Add blueprint",
                this,
                new BlueprintOptionsDescriptor(),
                ImportBlueprintWithOptions
            );
        }

        public void TrySave(BlueprintContainer container) {
            if (Source is ICollection<BlueprintContainer> collection) {
                if (!collection.Contains(container)) {
                    collection.Add(container);
                }
            } else {
                throw new InvalidOperationException("Source must be an observable collection");
            }
        }

        protected override BlueprintView CreateView(BlueprintContainer value, Transform container) {
            var view = base.CreateView(value, container);

            view.button.onClick.AddListener(
                () => {
                    if (!Interactable) {
                        return;
                    }

                    blueprintSelectorNavigateable.NavigateTo(
                        shipEditorNavigateable,
                        new NavigationArgs {
                            current = blueprintSelectorNavigateable
                        }
                    );

                    S.Editor.Import(new ContainerImportArgs(value, TrySave));
                }
            );

            view.deleteButton.onClick.AddListener(
                () => {
                    if (!Interactable) {
                        return;
                    }

                    S.UI.Prompts.PromptYesNo(
                        "Are you sure you want to delete this blueprint?",
                        this,
                        yes => {
                            if (!yes) {
                                return;
                            }

                            if (Source is ICollection<BlueprintContainer> collection) {
                                collection.Remove(value);
                            } else {
                                throw new InvalidOperationException("Source must be an observable collection");
                            }
                        }
                    );
                }
            );

            view.hoverable.onPointerEnter.AddListener(() => { blueprintDetails.Reflect(value.Data); });
            view.hoverable.onPointerExit.AddListener(() => { blueprintDetails.Reflect(null); });

            return view;
        }

        private void ImportBlueprintWithOptions(BlueprintOptions options) {
            var blueprint = new Blueprint(options);

            if (S.Blueprints.ContainsName(blueprint.name)) {
                S.UI.Logger.LogException($"Blueprint name \"{blueprint.name}\" is already used");

                return;
            }

            var args = new BlueprintContainerArgs(blueprint);
            var container = new BlueprintContainer(args);

            blueprintSelectorNavigateable.NavigateTo(shipEditorNavigateable);
            S.Editor.Import(new ContainerImportArgs(container, TrySave));
        }

    #region Clipboard import

        public void OnClipboardImport() {
            var clipboardText = GUIUtility.systemCopyBuffer;

            if (string.IsNullOrEmpty(clipboardText)) {
                S.UI.Logger.LogException("Clipboard is empty");

                return;
            }

            if (!OnClipboardImportDeserialize(clipboardText, out var blueprint)) {
                return;
            }

            var args = new BlueprintContainerArgs(blueprint);
            var container = new BlueprintContainer(args);

            if (S.Blueprints.ContainsName(blueprint.name)) {
                S.UI.Logger.LogException("Blueprint with given name already added");

                return;
            }

            // Save blueprint and generate thumbnail
            TrySave(container);
        }

        private bool OnClipboardImportDeserialize(string json, out Blueprint blueprint) {
            try {
                blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(json);

                return true;
            } catch {
                S.UI.Logger.LogException("Clipboard data is formatted incorrectly", false);
                blueprint = null;

                return false;
            }
        }

    #endregion

    }

}