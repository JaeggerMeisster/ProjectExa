using System.Collections.Generic;
using System.Linq;
using Exa.Utils;

namespace Exa.CustomEditors {
    public class AseExportConfiguration {
        public Dictionary<string, LayerConfigurationEntry> layers;

        public AseExportConfiguration() {
            layers = new Dictionary<string, LayerConfigurationEntry>();
        }

        public void NormalizeWithAseLayers(IEnumerable<string> currentLayers) {
            foreach (var (layer, layerConfig) in layers.Unpack().ToList()) {
                if (!currentLayers.Contains(layer)) {
                    layers.Remove(layer);
                }

                if (string.IsNullOrEmpty(layerConfig.name)) {
                    layerConfig.name = layer;
                }
            }

            foreach (var currentLayer in currentLayers) {
                if (!layers.ContainsKey(currentLayer)) {
                    layers.Add(
                        currentLayer,
                        new LayerConfigurationEntry {
                            mergeUp = false,
                            exportOnlyFirstFrame = false,
                            name = currentLayer
                        }
                    );
                }
            }
        }

        /// <summary>
        /// Gets a list of layer groups, by grouping ase layers that are configured to merge up.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GroupedLayerOutput> GetLayerGroups() {
            GroupedLayerOutput output = null;

            foreach (var (layer, layerConfig) in layers.Unpack()) {
                if (!layerConfig.mergeUp && output != null) {
                    yield return output;

                    output = null;
                }

                output ??= new GroupedLayerOutput {
                    aseLayers = new List<string>(),
                    configuration = layerConfig
                };

                output.aseLayers.Add(layer);
            }

            if (output != null) {
                yield return output;
            }
        }
    }

    public class GroupedLayerOutput {
        public List<string> aseLayers;
        public LayerConfigurationEntry configuration;
    }

    public class LayerConfigurationEntry {
        // Whether or not to merge the aseprite layer with the layer above
        public bool mergeUp;
        public bool exportOnlyFirstFrame;
        public string name;
    }
}