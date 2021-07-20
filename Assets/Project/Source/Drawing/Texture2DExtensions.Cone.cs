using System;
using System.Linq;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.Drawing {
    public static partial class Texture2DExtensions {
        public static Texture2D DrawCone(this Texture2D tex, Color color, Vector2 centre, float radius, float arc) {
            var halfArc = arc / 2f;

            return tex.CallbackCircularDraw(
                centre,
                radius,
                (localPoint, point) => {
                    if (localPoint.GetAngle().Between(180 - halfArc, 180 + halfArc)) {
                        tex.SetPixel(point, color);
                    }
                }
            );
        }

        public static Texture2D DrawFadingCone(
            this Texture2D tex,
            Color color,
            Vector2 centre,
            float radius,
            float arc,
            Func<float, float> easingFunc
        ) {
            var halfArc = arc / 2f;

            return tex.CallbackCircularDraw(
                centre,
                radius,
                (localPoint, point) => {
                    if (localPoint.GetAngle().Between(180 - halfArc, 180 + halfArc)) {
                        tex.SetPixel(point, color.SetAlpha(color.a * easingFunc(localPoint.magnitude / radius)));
                    }
                }
            );
        }

        public static Texture2D CallbackCircularDraw(this Texture2D tex, Vector2 centre, float radius, Action<Vector2, Vector2Int> pixelCallback) {
            foreach (var point in tex.EnumeratePixels()) {
                var localPoint = centre - point;

                if (localPoint.magnitude < radius) {
                    pixelCallback(localPoint, point);
                }
            }

            tex.Apply();

            return tex;
        }

        public static Texture2D DrawSuperSampledCone(this Texture2D tex, ConeArgs coneArgs, SuperSamplingArgs<float> @override = null) {
            var useEasing = coneArgs.easingFunc != null;

            var args = new SuperSamplingArgs<float> {
                sampleSize = 4,
                applier = (pixel, value) => { tex.SetPixel(pixel, coneArgs.color.SetAlpha(value)); },
                samplesAverageFactory = values => values.Average(),
                sampler = (point, localPoint) => ConeSampler(localPoint, coneArgs, useEasing)
            };

            return tex.CallbackCircularSuperSampledDraw(coneArgs.centre, @override?.Override(args) ?? args);
        }

        public static float ConeSampler(Vector2 localPoint, ConeArgs args, bool ease) {
            var magnitude = localPoint.magnitude;
            var halfArc = args.arc / 2f;

            return magnitude < args.radius
                ? localPoint.GetAngle().Between(180 - halfArc, 180 + halfArc)
                    ? ease
                        ? args.easingFunc(magnitude / args.radius)
                        : 1f
                    : 0f
                : 0f;
        }
    }

    public struct ConeArgs {
        public Color color;
        public Vector2 centre;
        public float radius;
        public float arc;
        public Func<float, float> easingFunc;
    }
}