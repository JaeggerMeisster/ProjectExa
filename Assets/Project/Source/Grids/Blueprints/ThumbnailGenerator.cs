using System;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blueprints {
    public class ThumbnailGenerator : MonoBehaviour {
        [SerializeField] private Color backgroundColor;
        [SerializeField] private float padding;

        private void Awake() {
            RuntimePreviewGenerator.Padding = padding;
            RuntimePreviewGenerator.OrthographicMode = true;
            RuntimePreviewGenerator.BackgroundColor = backgroundColor;
            RuntimePreviewGenerator.MarkTextureNonReadable = false;
        }

        public void GenerateThumbnail(Blueprint blueprint) {
            // Generate Ship
            foreach (var block in blueprint.Grid) {
                var blockGO = block.CreateInactiveInertBlockInGrid(transform);
                blockGO.SetActive(true);
            }

            // Thumbnails are not that important, so nulling them is fine
            try {
                var thisTransform = transform;
                RuntimePreviewGenerator.PreviewDirection = thisTransform.forward;
                var tex = RuntimePreviewGenerator.GenerateModelPreview(thisTransform, 512, 512);

                // Make sure the texture is set
                if (tex == null) {
                    throw new Exception("Generated thumbnail is null");
                }

                blueprint.Thumbnail = tex;
            } catch (Exception e) {
                blueprint.Thumbnail = null;
                Debug.LogWarning($"Cannot generate grid thumbnail, {e}");
            } finally {
                // Disable children, thus returning the blocks to the pool
                transform.SetActiveChildren(false);
            }
        }
    }
}