using System.Collections.Generic;
using System.Linq;
using Exa.Drawing;
using Exa.Grids;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.UI.Tweening;
using Exa.Utils;
using NaughtyAttributes;
using UnityEngine;
using Texture2DExtensions = Exa.Drawing.Texture2DExtensions;

namespace Exa.ShipEditor {
    public enum TurretOverlayMode {
        Stationary,
        Ghost
    }

    public class TurretOverlay : MonoBehaviour {
        [SerializeField] private BlockPresenter presenter;
        [SerializeField] private PolygonCollider2D polygonCollider;
        [SerializeField] private ExaEase ease;

        public BlockPresenter Presenter {
            get => presenter;
        }

        public ABpBlock Block { get; private set; }

        public void SetVisibility(bool value) {
            Presenter.Renderer.gameObject.SetActive(value);
        }

        public void SetColor(Color color) {
            presenter.Renderer.color = color;
        }

        public void Generate(ITurretValues values) {
            presenter.Renderer.sprite = GenerateTexture(values).CreateSprite();
            polygonCollider.points = GeneratePoints(values);
            SetVisibility(false);
        }

        public void Import(ABpBlock block) {
            Block = block;
        }

        public IEnumerable<Vector2Int> GetTurretClaims() {
            // Sync transforms, as the collider may be out of sync with the parent transform
            Physics2D.SyncTransforms();

            return polygonCollider.StationaryCast(LayerMask.GetMask("editorGridItems"))
                .Select(hit => hit.transform.GetComponent<EditorGridBackgroundItem>().GridPosition)
                .Except(Block.GetTileClaims());
        }

        [Button("Print turret claims")]
        public void PrintTurretClaims() {
            Debug.Log(GetTurretClaims().Join(", "));
        }

        private Texture2D GenerateTexture(ITurretValues values) {
            var pixelRadius = Mathf.RoundToInt(values.TurretRadius * 32);
            var size = pixelRadius * 2;

            var baseConfig = new ConeArgs {
                color = Color.white,
                centre = (pixelRadius - 0.5f).ToVector2(),
                radius = pixelRadius,
                arc = values.TurretArc
            };

            var fadedConfig = baseConfig;
            fadedConfig.color = Color.white.SetAlpha(0.5f);
            fadedConfig.easingFunc = ease.Evaluate;

            var tex = new Texture2D(size, size).SetDefaults();

            return tex
                .DrawSuperSampledCone(fadedConfig)
                .DrawSuperSampledCone(
                    baseConfig,
                    new SuperSamplingArgs<float> {
                        applier = (pixel, value) => {
                            if (value > tex.GetPixel(pixel).a) {
                                tex.SetPixel(pixel, baseConfig.color.SetAlpha(value));
                            }
                        },
                        sampler = (point, localPoint) => {
                            var between = localPoint.magnitude.Between(pixelRadius - 2f, pixelRadius);

                            return between ? Texture2DExtensions.ConeSampler(localPoint, baseConfig, false) : 0f;
                        }
                    }
                );
        }

        private Vector2[] GeneratePoints(ITurretValues values, int subdivisions = 20) {
            var points = new Vector2[subdivisions + 2];
            points[0] = Vector2.zero;

            var startAngle = values.GetTurretArcMinMax().min;
            var subdivisionAngleSize = values.TurretArc / subdivisions;

            for (var i = 0; i < subdivisions + 1; i++) {
                var angle = startAngle + subdivisionAngleSize * i;
                points[i + 1] = MathUtils.FromAngledMagnitude(values.TurretRadius, angle);
            }

            return points;
        }
    }
}